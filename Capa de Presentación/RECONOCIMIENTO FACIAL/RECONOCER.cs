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
<<<<<<< HEAD
        RecordingType recording_type;
        VideoCapture cam;
        Mat frame;
        CascadeClassifier face_detector;
        EigenFaceRecognizer eigen_face_recognizer;
        bool running = false;

        int model_width = 100;
        int model_height = 100;
        int threshold = 2500;
        private bool acceso_concedido = false;
        private bool mensaje_mostrado = false;
        private bool acceso_enproceso = false;



        string path_trained_faceModel = $"{Application.StartupPath}\\Faces\\stateModel.yaml";
        string path_reconzier_faces_model = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";
=======
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
>>>>>>> v

        public RECONOCER()
        {
            InitializeComponent();
            frame = new Mat();

            // Cargar HaarCascade
<<<<<<< HEAD
            if (!File.Exists(path_reconzier_faces_model))
                MessageBox.Show("No se encontró haarcascade_frontalface_default.xml");
            else
                face_detector = new CascadeClassifier(path_reconzier_faces_model);

            // Crear el recognizer
            eigen_face_recognizer = EigenFaceRecognizer.Create(80, threshold);

            // Cargar modelo entrenado
            if (File.Exists(path_trained_faceModel))
                eigen_face_recognizer.Read(path_trained_faceModel);
=======
            if (!File.Exists(pathReconzierFacesModel))
                MessageBox.Show("No se encontró haarcascade_frontalface_default.xml");
            else
                faceDetector = new CascadeClassifier(pathReconzierFacesModel);

            // Crear el recognizer
            eigenFaceRecognizer = EigenFaceRecognizer.Create(80, threshold);

            // Cargar modelo entrenado
            if (File.Exists(pathTrainedFaceModel))
                eigenFaceRecognizer.Read(pathTrainedFaceModel);
>>>>>>> v
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
<<<<<<< HEAD
                int temp_time = Environment.TickCount;

                while (Environment.TickCount - temp_time < 5000) // 5 segundos
                {
                    Mat tmp_frame = new Mat();
                    cam.Read(tmp_frame);

                    if (!tmp_frame.Empty())
                    {
                        Invoke(new Action(() =>
                        {
                            pictureBox1.Image = BitmapConverter.ToBitmap(tmp_frame);
=======
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
>>>>>>> v
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
<<<<<<< HEAD
            if (cam == null || acceso_concedido) return;

            Mat image_frame = new Mat();
            cam.Read(image_frame);

            if (image_frame.Empty())
                return;

            Mat gray_frame = new Mat();
            Cv2.CvtColor(image_frame, gray_frame, ColorConversionCodes.BGR2GRAY);

            var faces = face_detector.DetectMultiScale(
                gray_frame,
                1.4,
                4,
                HaarDetectionTypes.ScaleImage,
                new OpenCvSharp.Size(image_frame.Width / 8, image_frame.Height / 8)
            );

            string detected_username = acceso_concedido ? label1.Text : "Desconocido";

            foreach (var face in faces)
            {
                Cv2.Rectangle(image_frame, face, Scalar.Blue, 2);

                Mat face_region = new Mat(gray_frame, face);
                Mat resized_face = new Mat();
                Cv2.Resize(face_region, resized_face,
                    new OpenCvSharp.Size(model_width, model_height));

                try
                {
                    eigen_face_recognizer.Predict(resized_face,
                        out int predicted_id,
                        out double confidence);

                    if (predicted_id > 0 && confidence < threshold)
                    {
                        // Obtener datos del usuario
                        ClsAccionesDB db = new ClsAccionesDB();
                        var datos = db.ObtenerUsuarioReconocimiento(predicted_id);

                        string nombre = datos.nombre;
                        int rol_id = datos.rol_id;
                        int estado_cuenta = datos.estado_cuenta;
                        int parroquia_id = datos.parroquia_id;

                        Capa_de_acceso_de_datos.Sesion1.IniciarSesion(predicted_id, rol_id, parroquia_id, nombre);

                        if (!acceso_concedido)
                            detected_username = nombre;

                        // Validar estado de cuenta
                        if (estado_cuenta != 1)
=======
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
                        int rolId = datos.rol_id;
                        int estadoCuenta = datos.estado_cuenta;
                        int parroquiaId = datos.parroquia_id;

                        // Almacenar datos en la clase estática UsuarioLogueado
                        UsuarioLogueado.usuario_id = predictedId;
                        UsuarioLogueado.nombre = nombre;
                        UsuarioLogueado.rol_id = rolId;
                        UsuarioLogueado.parroquia_id = parroquiaId;

                        if (!accesoConcedido)
                            detectedUserName = nombre;

                        // Validar estado de cuenta
                        if (estadoCuenta != 1)
>>>>>>> v
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show("La cuenta del usuario está inactiva o bloqueada.",
                                                "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
<<<<<<< HEAD
                            acceso_concedido = false;
=======
>>>>>>> v
                            continue;
                        }

                        // Evitar abrir más de una vez
<<<<<<< HEAD
                        acceso_concedido = true;
=======
                        accesoConcedido = true;
>>>>>>> v

                        // Abrir formulario según el rol
                        Invoke(new Action(async () =>
                        {
                            Form f = null;

<<<<<<< HEAD
                            switch (rol_id)
                            {
                                case 1:
                                    // Para el rol de administrador
                                    f = new Ventana_Principal_Administrador(predicted_id, parroquia_id);
=======
                            switch (rolId)
                            {
                                case 1:
                                    // Para el rol de administrador
                                    f = new Ventana_Principal_Administrador(predictedId, parroquiaId);
>>>>>>> v
                                    break;

                                case 2:
                                case 3:
                                    // Para otros roles (empleado)
<<<<<<< HEAD
                                    f = new FRM_42(predicted_id, parroquia_id); // Pasa el ID y parroquiaId aquí
=======
                                    f = new FRM_42(predictedId, parroquiaId); // Pasa el ID y parroquiaId aquí
>>>>>>> v
                                    break;

                                default:
                                    MessageBox.Show("Rol no reconocido.", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
<<<<<<< HEAD
                                    acceso_concedido = false;
=======
                                    accesoConcedido = false;
>>>>>>> v
                                    return;
                            }

                            // Mostrar mensaje de bienvenida
<<<<<<< HEAD
                            if (!mensaje_mostrado)
                            {
                                mensaje_mostrado = true;
=======
                            if (!mensajeMostrado)
                            {
                                mensajeMostrado = true;
>>>>>>> v
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
<<<<<<< HEAD
                label1.Text = detected_username;
                pictureBox1.Image = BitmapConverter.ToBitmap(image_frame);
=======
                label1.Text = detectedUserName;
                pictureBox1.Image = BitmapConverter.ToBitmap(imageFrame);
>>>>>>> v
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