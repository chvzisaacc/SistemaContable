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
        int threshold = 5000;

        string pathTrainedFaceModel = $"{Application.StartupPath}\\Faces\\stateModel.yaml";
        string pathReconzierFacesModel = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";

        // Variables para control de reconocimiento
        private DateTime lastRecognitionTime = DateTime.MinValue;
        private string lastRecognizedUser = "";
        private readonly TimeSpan recognitionCooldown = TimeSpan.FromSeconds(3); // 3 segundos entre reconocimientos

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
            else
                MessageBox.Show("No existe un modelo entrenado (stateModel.yaml).");

            TurnOffCamera();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TurnOnCamera();
        }

        private void TurnOnCamera()
        {
            try
            {
                cam = new VideoCapture(2);

                if (!cam.IsOpened())
                {
                    MessageBox.Show("No se pudo abrir la cámara.");
                    return;
                }

                running = true;

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
            if (cam == null) return;

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

            string detectedUserName = "Desconocido";
            int detectedUserId = -1;

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
                        // OBTENER DATOS DEL USUARIO USANDO TUS MÉTODOS EXISTENTES
                        ClsAccionesDB db = new ClsAccionesDB();
                        Usuario usuario = db.ObtenerUsuarioCompleto(predictedId);

                        if (usuario != null)
                        {
                            detectedUserName = usuario.usuario_nombre;
                            detectedUserId = usuario.Usuario_id;

                            // Verificar si es un nuevo reconocimiento y procesarlo
                            ProcessUserRecognition(usuario);
                        }
                        else
                        {
                            // Si no se encuentra usuario completo, obtener solo el nombre
                            detectedUserName = db.ObtenerNombreUsuario(predictedId);
                            detectedUserId = predictedId;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en reconocimiento: {ex.Message}");
                }
            }

            // Mostrar nombre en la interfaz
            Invoke(new Action(() =>
            {
                label1.Text = detectedUserName;
                pictureBox1.Image = BitmapConverter.ToBitmap(imageFrame);
            }));
        }

        private void ProcessUserRecognition(Usuario usuario)
        {
            // Control para evitar múltiples reconocimientos consecutivos del mismo usuario
            if (DateTime.Now - lastRecognitionTime < recognitionCooldown &&
                lastRecognizedUser == usuario.usuario_nombre)
            {
                return;
            }

            lastRecognitionTime = DateTime.Now;
            lastRecognizedUser = usuario.usuario_nombre;

            // Invocar en el hilo de la UI
            Invoke(new Action(() =>
            {
                // Verificar estado de cuenta (Id_estado_cuenta = 1 para activo)
                if (usuario.Id_estado_cuenta != 1)
                {
                    string estadoTexto = usuario.Id_estado_cuenta == 2 ? "Inactivo" : "Bloqueado";
                    MessageBox.Show($"Cuenta {estadoTexto}. Contacte al administrador.",
                        "Estado de Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir formulario según el rol (Rol_id)
                AbrirFormularioPorRol(usuario);
            }));
        }

        private void AbrirFormularioPorRol(Usuario usuario)
        {
            // Cerrar cámara antes de abrir nuevo formulario
            TurnOffCamera();

            Form formulario = null;

            // SI EL Rol_id ES 1 ABRE FRM_PG5, SINO FRM_PG42
            if (usuario.Rol_id == 1)
            {
                formulario = new FRM_PG5(usuario.usuario_nombre, usuario.Usuario_id);
            }
            else
            {
                formulario = new FRM_42(usuario.usuario_nombre, usuario.Usuario_id);
            }

            // Mostrar mensaje de bienvenida
            string rolTexto = usuario.Rol_id == 1 ? "Administrador" : "Usuario";
            MessageBox.Show($"Bienvenido: {usuario.usuario_nombre}\nRol: {rolTexto}",
                "Acceso Permitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Mostrar el formulario correspondiente
            if (formulario != null)
            {
                formulario.Show();
                this.Hide(); // Ocultar el formulario de reconocimiento

                // Evento para cuando se cierre el formulario secundario
                formulario.FormClosed += (s, args) =>
                {
                    this.Show(); // Mostrar nuevamente el formulario de reconocimiento
                    TurnOnCamera(); // Reactivar la cámara
                };
            }
        }

        // Método alternativo si solo tienes el ID y nombre
        private void ProcessUserRecognitionAlternative(int userId, string userName)
        {
            ClsAccionesDB db = new ClsAccionesDB();

            // Obtener usuario completo
            Usuario usuario = db.ObtenerUsuarioCompleto(userId);

            if (usuario != null)
            {
                ProcessUserRecognition(usuario);
            }
            else
            {
                // Si no se puede obtener usuario completo, mostrar mensaje
                MessageBox.Show($"Usuario {userName} no encontrado en la base de datos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RECONOCER_Load(object sender, EventArgs e)
        {
            // Configuración inicial adicional si es necesaria
        }

        // Para manejar el cierre del formulario
        private void RECONOCER_FormClosing(object sender, FormClosingEventArgs e)
        {
            TurnOffCamera();
        }

        private void RECONOCER_Load_1(object sender, EventArgs e)
        {

        }
    }
}