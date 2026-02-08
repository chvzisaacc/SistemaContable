using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;

namespace Capa_de_Presentación.RECONOCIMIENTO_FACIAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class RECONOCER : Form
    {
        /// <summary>
        /// 
        /// </summary>
        enum RecordingType
        {
            /// <summary>
            /// The training
            /// </summary>
            training = 0,
            /// <summary>
            /// The recognition
            /// </summary>
            recognition = 1
        }
        /// <summary>
        /// The recording type
        /// </summary>
        RecordingType recording_type;
        /// <summary>
        /// The cam
        /// </summary>
        VideoCapture cam;
        /// <summary>
        /// The frame
        /// </summary>
        Mat frame;
        /// <summary>
        /// The face detector
        /// </summary>
        CascadeClassifier face_detector;
        /// <summary>
        /// The eigen face recognizer
        /// </summary>
        EigenFaceRecognizer eigen_face_recognizer;
        /// <summary>
        /// The running
        /// </summary>
        bool running = false;

        /// <summary>
        /// The model width
        /// </summary>
        int model_width = 100;
        /// <summary>
        /// The model height
        /// </summary>
        int model_height = 100;
        /// <summary>
        /// The threshold
        /// </summary>
        int threshold = 2500;
        /// <summary>
        /// The acceso concedido
        /// </summary>
        private bool acceso_concedido = false;
        /// <summary>
        /// The mensaje mostrado
        /// </summary>
        private bool mensaje_mostrado = false;
        /// <summary>
        /// The acceso enproceso
        /// </summary>
        private bool acceso_enproceso = false;



        /// <summary>
        /// The path trained face model
        /// </summary>
        string path_trained_faceModel = $"{Application.StartupPath}\\Faces\\stateModel.yaml";
        /// <summary>
        /// The path reconzier faces model
        /// </summary>
        string path_reconzier_faces_model = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="RECONOCER"/> class.
        /// </summary>
        public RECONOCER()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            frame = new Mat();

            // Cargar HaarCascade
            if (!File.Exists(path_reconzier_faces_model))
                MessageBox.Show("No se encontró haarcascade_frontalface_default.xml");
            else
                face_detector = new CascadeClassifier(path_reconzier_faces_model);

            // Crear el recognizer
            eigen_face_recognizer = EigenFaceRecognizer.Create(80, threshold);

            // Cargar modelo entrenado
            if (File.Exists(path_trained_faceModel))
                eigen_face_recognizer.Read(path_trained_faceModel);
            TurnOffCamera(); ;

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            TurnOnCamera();
        }

        /// <summary>
        /// Turns the on camera.
        /// </summary>
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


        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
        }

        /// <summary>
        /// Turns the off camera.
        /// </summary>
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
        /// <summary>
        /// Recognizes the face.
        /// </summary>
        private void RecognizeFace()
        {
            if (cam == null || acceso_concedido) return;

            Mat image_frame = new Mat();
            cam.Read(image_frame);

            if (image_frame.Empty()) return;

            Mat gray_frame = new Mat();
            Cv2.CvtColor(image_frame, gray_frame, ColorConversionCodes.BGR2GRAY);

            var faces = face_detector.DetectMultiScale(
                gray_frame, 1.4, 4, HaarDetectionTypes.ScaleImage,
                new OpenCvSharp.Size(image_frame.Width / 8, image_frame.Height / 8)
            );

            // Por defecto el nombre es "Desconocido"
            string detected_username = "Desconocido";

            foreach (var face in faces)
            {
                Cv2.Rectangle(image_frame, face, Scalar.Blue, 2);

                Mat face_region = new Mat(gray_frame, face);
                Mat resized_face = new Mat();
                Cv2.Resize(face_region, resized_face, new OpenCvSharp.Size(model_width, model_height));

                try
                {
                    eigen_face_recognizer.Predict(resized_face, out int predicted_id, out double confidence);

                    if (predicted_id > 0 && confidence < threshold)
                    {
                        // 1. Buscamos los datos en la DB
                        ClsAccionesDB db = new ClsAccionesDB();
                        var datos = db.ObtenerUsuarioReconocimiento(predicted_id);

                        // 2. ACTUALIZAMOS EL NOMBRE PARA QUE SE VEA EN PANTALLA
                        detected_username = datos.nombre;

                        if (datos.estado_cuenta == 1 && !acceso_concedido)
                        {
                            acceso_concedido = true; // Bloqueamos nuevas detecciones

                            // 3. GUARDAMOS EN SESIÓN
                            Capa_de_acceso_de_datos.Sesion1.IniciarSesion(predicted_id, datos.rol_id, datos.parroquia_id, datos.nombre);
                            db.RegistrarInicioSesionBiometrico(predicted_id);

                            // 4. PROCESO DE CIERRE SEGURO
                            Invoke(new Action(async () =>
                            {
                                if (this.IsDisposed) return;

                                // Actualizamos el label una última vez con el nombre real
                                label1.Text = datos.nombre;

                                if (!mensaje_mostrado)
                                {
                                    mensaje_mostrado = true;
                                    MessageBox.Show("Bienvenido " + datos.nombre, "Acceso concedido");
                                }

                                running = false;
                                TurnOffCamera();

                                await Task.Delay(500); // Pausa de medio segundo para que el usuario vea su nombre

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error durante reconocimiento facial: " + ex.Message);
                }
            }

            // 5. ACTUALIZACIÓN CONSTANTE DE LA CÁMARA Y EL LABEL
            if (!this.IsDisposed)
            {
                Invoke(new Action(() =>
                {
                    label1.Text = detected_username; // Aquí se mostrará "Desconocido" o el "Nombre"
                    pictureBox1.Image = BitmapConverter.ToBitmap(image_frame);
                }));
            }
        }




        /// <summary>
        /// Handles the Load event of the RECONOCER control.
        /// </summary>
        /// <param name="sender">The source of the event.</paramf
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RECONOCER_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the RECONOCER_Load control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RECONOCER_Load_1(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            FRM_PG1 fRM_PG1 = new();
            fRM_PG1.Show();
            this.Hide();
        }
    }
}