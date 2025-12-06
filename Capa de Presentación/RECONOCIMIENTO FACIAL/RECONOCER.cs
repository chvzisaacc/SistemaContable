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
                        db.RegistrarInicioSesionBiometrico(predicted_id);

                        if (!acceso_concedido)
                            detected_username = nombre;

                        // Validar estado de cuenta
                        if (estado_cuenta != 1)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show("La cuenta del usuario está inactiva o bloqueada.",
                                                "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                            acceso_concedido = false;
                            continue;
                        }

                        // Evitar abrir más de una vez
                        acceso_concedido = true;

                        // Abrir formulario según el rol
                        Invoke(new Action(async () =>
                        {
                            Form f = null;

                            switch (rol_id)
                            {
                                case 1:
                                    // Para el rol de administrador
                                    f = new Ventana_Principal_Administrador(predicted_id, parroquia_id);
                                    break;

                                case 2:
                                case 3:
                                    // Para otros roles (empleado)
                                    f = new FRM_42(predicted_id, parroquia_id); // Pasa el ID y parroquiaId aquí
                                    break;

                                default:
                                    MessageBox.Show("Rol no reconocido.", "Error",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    acceso_concedido = false;
                                    return;
                            }

                            // Mostrar mensaje de bienvenida
                            if (!mensaje_mostrado)
                            {
                                mensaje_mostrado = true;
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
                label1.Text = detected_username;
                pictureBox1.Image = BitmapConverter.ToBitmap(image_frame);
            }));
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