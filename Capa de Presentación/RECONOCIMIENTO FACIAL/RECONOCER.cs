using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Capa_de_Presentación.RECONOCIMIENTO_FACIAL
{
    public partial class RECONOCER : Form
    {
        enum RecordingType
        {
            training = 0,
            recognition = 1
        }
        RecordingType recordingType;
        VideoCapture cam;
        Mat frame;
        CascadeClassifier faceDetector;
        EigenFaceRecognizer eigenFaceRecognizer;
        bool running = false;

        int modelWidth = 100;
        int modelHeight = 100;
        int threshold = 3000;
        private bool accesoConcedido = false;
        private bool mensajeMostrado = false;
        private bool accesoEnProceso = false;



        string pathTrainedFaceModel = $"{Application.StartupPath}\\Faces\\stateModel.yaml";
        string pathReconzierFacesModel = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";

        public RECONOCER()
        {
            InitializeComponent();
            frame = new Mat();

            // Cargar HaarCascade
            if (!File.Exists(pathReconzierFacesModel))
                MessageBox.Show("No se encontró haarcascade_frontalface_default.xml");
            else
                faceDetector = new CascadeClassifier(pathReconzierFacesModel);

            // Crear el recognizer
            eigenFaceRecognizer = EigenFaceRecognizer.Create(80, threshold);

            // Cargar modelo entrenado
            if (File.Exists(pathTrainedFaceModel))
                eigenFaceRecognizer.Read(pathTrainedFaceModel);
            TurnOffCamera(); ;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TurnOnCamera();
        }

        private async void TurnOnCamera()
        {
            try
            {
                cam = new VideoCapture(0);

                if (!cam.IsOpened())
                {
                    MessageBox.Show("No se pudo abrir la cámara.");
                    return;
                }

                running = true;

                //MOSTRAR CÁMARA POR 5 SEGUNDOS SIN RECONOCER
                int tempTime = Environment.TickCount;

                while (Environment.TickCount - tempTime < 5000) // 5 segundos
                {
                    Mat tmpFrame = new Mat();
                    cam.Read(tmpFrame);

                    if (!tmpFrame.Empty())
                    {
                        Invoke(new Action(() =>
                        {
                            pictureBox1.Image = BitmapConverter.ToBitmap(tmpFrame);
                        }));
                    }

                    await Task.Delay(50); // refresco suave
                }

                //LUEGO DE 5 SEGUNDOS EMPIEZA EL RECONOCIMIENTO
                Task.Run(() =>
                {
                    while (running)
                    {
                        RecognizeFace();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar cámara: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
        }

        private async void TurnOffCamera()
        {
            running = false;
            await Task.Delay(150);

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                if (cam != null)
                {
                    if (cam.IsOpened())
                        cam.Release();

                    cam.Dispose();
                    cam = null;
                }
            }
            catch { }
        }
        private void RecognizeFace()
        {
            if (cam == null || accesoConcedido) return;

            Mat imageFrame = new Mat();
            cam.Read(imageFrame);

            if (imageFrame.Empty())
                return;

            Mat grayFrame = new Mat();
            Cv2.CvtColor(imageFrame, grayFrame, ColorConversionCodes.BGR2GRAY);

            var faces = faceDetector.DetectMultiScale(
                grayFrame,
                1.4,
                4,
                HaarDetectionTypes.ScaleImage,
                new OpenCvSharp.Size(imageFrame.Width / 8, imageFrame.Height / 8)
            );

            string detectedUserName = accesoConcedido ? label1.Text : "Desconocido";

            foreach (var face in faces)
            {
                Cv2.Rectangle(imageFrame, face, Scalar.Blue, 2);

                Mat faceRegion = new Mat(grayFrame, face);
                Mat resizedFace = new Mat();
                Cv2.Resize(faceRegion, resizedFace,
                    new OpenCvSharp.Size(modelWidth, modelHeight));

                try
                {
                    eigenFaceRecognizer.Predict(resizedFace,
                        out int predictedId,
                        out double confidence);

                    if (predictedId > 0 && confidence < threshold)
                    {
                        // Obtener datos del usuario
                        ClsAccionesDB db = new ClsAccionesDB();
                        var datos = db.ObtenerUsuarioReconocimiento(predictedId);

                        string nombre = datos.nombre;
                        int rolId = datos.rolId;
                        int estadoCuenta = datos.estadoCuenta;
                        int parroquiaId = datos.parroquiaId;

                        // Almacenar datos en la clase estática UsuarioLogueado
                        UsuarioLogueado.UsuarioId = predictedId;
                        UsuarioLogueado.Nombre = nombre;
                        UsuarioLogueado.RolId = rolId;
                        UsuarioLogueado.ParroquiaId = parroquiaId;

                        if (!accesoConcedido)
                            detectedUserName = nombre;

                        // Validar estado de cuenta
                        if (estadoCuenta != 1)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show("La cuenta del usuario está inactiva o bloqueada.",
                                                "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                            continue;
                        }

                        // Evitar abrir más de una vez
                        accesoConcedido = true;

                        // Abrir formulario según el rol
                        Invoke(new Action(async () =>
                        {
                            Form f = null;

                            switch (rolId)
                            {
                                case 1:
                                    // Para el rol de administrador
                                    f = new Ventana_Principal_Administrador(predictedId, parroquiaId);
                                    break;

                                case 2:
                                case 3:
                                    // Para otros roles (empleado)
                                    f = new FRM_42(predictedId, parroquiaId); // Pasa el ID y parroquiaId aquí
                                    break;

                                default:
                                    MessageBox.Show("Rol no reconocido.", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    accesoConcedido = false;
                                    return;
                            }

                            // Mostrar mensaje de bienvenida
                            if (!mensajeMostrado)
                            {
                                mensajeMostrado = true;
                                MessageBox.Show("Bienvenido " + nombre, "Acceso concedido");
                            }

                            // Esperar 3 segundos antes de mostrar el formulario
                            await Task.Delay(3000);

                            // Apagar la cámara y ocultar el formulario de reconocimiento
                            TurnOffCamera();
                            this.Hide();

                            // Mostrar el formulario correspondiente
                            f.Show();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en el reconocimiento: " + ex.Message);
                }
            }

            // Mostrar imagen y nombre en pantalla
            Invoke(new Action(() =>
            {
                label1.Text = detectedUserName;
                pictureBox1.Image = BitmapConverter.ToBitmap(imageFrame);
            }));
        }




        private void RECONOCER_Load(object sender, EventArgs e)
        {

        }

        private void RECONOCER_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}