using Capa_de_acceso_de_datos;
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
    public partial class RECONOCIMIENTO_FACIAL : Form
    {

        enum RecordingType
        {
            training = 0,
            recognition = 1
        }
        RecordingType recordingType;

        // Tamaño para redimensionar rostros (si los quisieras almacenar)
        int modelWidth = 100;
        int modelHeight = 100;

        // Rutas de guardado
        string pathSavedFaces = $"{Application.StartupPath}\\Faces\\";
        string pathTrainedFaceModel = $"{Application.StartupPath}\\Faces\\stateModel.yaml";

        // HaarCascade (OpenCvSharp)
        string pathReconzierFacesModel = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";

        // Cámara y detector
        VideoCapture cam;
        Mat frame;
        CascadeClassifier faceDetector;
        bool running = false;

        List<Mat> trainedImages = new List<Mat>();
        List<string> labels = new List<string>();

        EigenFaceRecognizer eigenFaceRecognizer;
        //Indexar cada rostro empezando desde el 1
        int faceId = 1;
        string faceName = "";
        //Detectar si es una cara nueva si escribimos el nombre en la textbox
        bool isAnewFace = false;
        //componentes
        int EigenFaceRecognizerComponentes = 80;
        //margen de error o de fallo = 5000
        int threshold = 5000;



        public RECONOCIMIENTO_FACIAL()
        {
            InitializeComponent();
            frame = new Mat();

            //MessageBox.Show("StartupPath: " + Application.StartupPath);

            // Verificación del XML
            if (!File.Exists(pathReconzierFacesModel))
            {
                MessageBox.Show("No se encontró el archivo haarcascade_frontalface_default.xml");
            }
            else
            {
                faceDetector = new CascadeClassifier(pathReconzierFacesModel);
            }

            TurnOffCamera();

            CargarUsuariosCombo();

            //inicializamos el objeto que ejecutara el algoritmo EIGEN
            //Pero primero necesitamos especificar el numero de componentes y el margen de error
            eigenFaceRecognizer = EigenFaceRecognizer.Create(EigenFaceRecognizerComponentes, threshold);


        }

        //reseteamos
        private void ResetInitValues()
        {
            isAnewFace = true;
        }

        public void CargarUsuariosCombo()
        {
            ClsAccionesDB objacciones = new();
            List<Usuario> usuarios = objacciones.ObtenerUsuarios();

            comboBox1.DisplayMember = "usuario_nombre";
            comboBox1.ValueMember = "Usuario_id";
            comboBox1.DataSource = usuarios;
        }

        private string GetLocalUserFolder(int usuarioId)
        {
            string folder = Path.Combine(Application.StartupPath, "Faces", usuarioId.ToString());

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }





        void RecognizeFace()
        {
            string name = "unknown";

            // Capturar frame
            Mat imageFrame = new Mat();
            cam.Read(imageFrame);

            if (imageFrame.Empty())
                return;

            // Convertir a gris
            Mat grayFrame = new Mat();
            Cv2.CvtColor(imageFrame, grayFrame, ColorConversionCodes.BGR2GRAY);

            // Detectar rostros
            Rect[] faces = faceDetector.DetectMultiScale(
                grayFrame,
                1.4,
                4,
                OpenCvSharp.HaarDetectionTypes.ScaleImage,
                new OpenCvSharp.Size(imageFrame.Width / 8, imageFrame.Height / 8)
            );

            foreach (var face in faces)
            {
                // Recorte del rostro
                Mat faceRegion = new Mat(grayFrame, face);

                // Redimensionar
                Mat imageToCompare = new Mat();
                Cv2.Resize(faceRegion, imageToCompare,
                    new OpenCvSharp.Size(modelWidth, modelHeight),
                    0, 0, InterpolationFlags.Cubic);

                // PROTECCIÓN CONTRA CRASH EN PREDICT()
                try
                {
                    eigenFaceRecognizer.Predict(imageToCompare,
                        out int predictedLabel,
                        out double confidence);

                    if (predictedLabel > 0 && confidence < threshold)
                        name = GetFacesName(predictedLabel);
                    else
                        name = "unknown";
                }
                catch (Exception ex)
                {
                    // EVITAR QUE LA APLICACIÓN SE CIERRE
                    Console.WriteLine("Error en Predict(): " + ex.Message);
                    name = "unknown";
                }

                // Dibujar contorno
                Cv2.Rectangle(imageFrame, face, Scalar.BurlyWood, 3);
            }

            // Mostrar imagen
            pictureBox1.Image = BitmapConverter.ToBitmap(imageFrame);
        }

        private int GetNextFaceId()
        {
            int faceId = 0;
            var paths = GetAllFacesPath();
            foreach (var p in paths)
            {
                int cId = int.Parse(GetfaceIdFromPath(p));
                if (cId > faceId)
                {
                    faceId = cId;
                }
            }
            return Math.Max(faceId, 1) + 1;
        }

        //distinguir nombres de las caras
        private KeyValuePair<string, int> GetItemListFace(string path)
        {
            var slices = path.Split('\\');
            var nameAndIndex = slices[slices.Length - 1].Replace(".bmp", "");

            var parts = nameAndIndex.Split('_');

            // si el archivo NO cumple el formato ID_Nombre_Indice → evitar crash
            if (parts.Length < 3)
                return new KeyValuePair<string, int>("INVALID", -1);

            // VALIDACIÓN: si el ID NO es un número → evitar crash
            if (!int.TryParse(parts[0], out int id))
                return new KeyValuePair<string, int>("INVALID", -1);

            string name = parts[1];

            return new KeyValuePair<string, int>(name, id);
        }

        private DateTime lastSave = DateTime.MinValue;

        private void Spotface()
        {
            if (!running || cam == null || !cam.IsOpened())
                return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                var faces = faceDetector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.Red, 2);

                    Mat faceCrop = new Mat(gray, face);
                    Cv2.Resize(faceCrop, faceCrop, new OpenCvSharp.Size(modelWidth, modelHeight));

                    // *** aseguro que se usan fotos del usuario correcto ***
                    trainedImages.Add(faceCrop.Clone());

                    // guardar cada 500ms
                    if ((DateTime.Now - lastSave).TotalMilliseconds >= 500)
                    {
                        ClsAccionesDB db = new ClsAccionesDB();

                        int fotosSQL = db.ContarFotosUsuario(faceId);

                        if (fotosSQL < 30)
                        {
                            try
                            {
                                byte[] data = MatToByteArray(faceCrop);
                                int newPhotoId = db.GuardarFotoRostro(faceId, data);

                                // Guardar también en carpeta local
                                string folder = GetLocalUserFolder(faceId);
                                string filePath = Path.Combine(folder, $"{newPhotoId}.bmp");
                                File.WriteAllBytes(filePath, data);

                                lastSave = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error SQL: " + ex.Message);
                            }
                        }
                        else
                        {
                            running = false;
                            TurnOffCamera();
                            MessageBox.Show("Entrenamiento completado (30 fotos).");
                        }
                    }
                }
            }

            pictureBox1.Image = BitmapConverter.ToBitmap(frame);
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
                        if (recordingType == RecordingType.training)
                            Spotface();
                        else if (recordingType == RecordingType.recognition)
                            RecognizeFace();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar cámara: " + ex.Message);
            }
        }

        private async void TurnOffCamera()
        {
            running = false;

            // Esperar un poco para que el loop se detenga
            await Task.Delay(120);

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
            catch (Exception ex)
            {
                MessageBox.Show("Error al apagar cámara: " + ex.Message);
            }
        }
        //obtenemos las imagenes de rostros
        private string[] GetAllFacesPath()
        {
            return Directory.GetFiles(pathSavedFaces, "*.bmp");
        }

        //obtenemos el id de un rostro existente
        private string GetfaceIdFromPath(string path)
        {
            // Extraer solo el nombre del archivo sin extensión
            var slices = path.Split("\\");
            var nameAndIndex = slices[slices.Length - 1].Replace(".bmp", "");

            // Separar por "_"
            var parts = nameAndIndex.Split('_');

            //si el archivo NO cumple el formato ID_Nombre_Indice
            // evitar crashear y devolver "-1" (así se ignora en el entrenamiento)
            if (parts.Length < 3)
                return "-1";

            //si el ID NO es un número, evitar crash
            if (!int.TryParse(parts[0], out int id))
                return "-1";

            return id.ToString();
        }

        //obtenemos el siguiente indice de unas secuencia del mismo rostro
        private string GetAIndexFaceFromPath(string path)
        {

            var slices = path.Split("\\");
            var nameAndIndex = slices[slices.Length - 1].Replace(".bmp", "");
            if (faceId == int.Parse(nameAndIndex.Split("_")[0]))
                return nameAndIndex.Split("_")[2];
            return "";

        }
        //se guardaran cuando la camara deje de grabar

        private void SaveFaces(string FacesName)
        {
            if (trainedImages.Any() && !string.IsNullOrEmpty(FacesName))
            {
                FacesName = FacesName.Replace("_", "");
            }

            int currentCount = CountFacesOfId(faceId);

            int indx = GetNextIndexFace();
            var facesToSave = trainedImages.ToList();

            foreach (var face in facesToSave)
            {
                if (currentCount >= 30)
                    break;

                face.SaveImage($"{pathSavedFaces}/{faceId}_{FacesName}_{indx}.bmp");

                indx++;
                currentCount++;
            }
        }

        //recuperar el actual indice del rostro si ya fue guardado si no vamos a guardarlo con el indice 1
        private int NextIndexFromAnExistingFace()
        {
            int index = -1;
            var allfaces = GetAllFacesPath();
            foreach (var p in allfaces)
            {
                var faceindex = GetAIndexFaceFromPath(p);
                if (!string.IsNullOrEmpty(faceindex))
                    if (int.Parse(faceindex) > index)
                        index = int.Parse(faceindex);
            }
            return index + 1;
        }

        int GetNextIndexFace()
        {
            if (!isAnewFace)
            {
                return NextIndexFromAnExistingFace();
            }
            else
            {
                return 1;
            }
        }


        //entrenar rostros
        private void TrainingFace()
        {
            //revisamos si el archivo que guarda el resultado del entrenamiento y se borra porque se
            //tiene que actualizar el resultado

            if (File.Exists(pathTrainedFaceModel))
            {
                File.Delete(pathTrainedFaceModel);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (recordingType == RecordingType.training)
            {
                Spotface();
            }
            if (recordingType == RecordingType.recognition)
            {
                RecognizeFace();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is Usuario seleccionado)
            {
                faceId = seleccionado.Usuario_id;
                faceName = seleccionado.usuario_nombre;

                // *** REINICIAR BUFERS ***
                trainedImages.Clear();
                isAnewFace = false;

                recordingType = RecordingType.training;
                TurnOnCamera();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario antes de iniciar el entrenamiento.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
            try
            {
                if (cam != null)
                {
                    if (cam.IsOpened())
                    {
                        cam.Release();   // Se libera solo si está abierta
                    }

                    cam.Dispose();        // Se libera memoria (solo si no está ya disposed)
                    cam = null;           // Evita llamar Release() dos veces
                }

                running = false;
                pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al apagar cámara: " + ex.Message);
            }
            //salvamos las imagenes
            //etiquetar las caras por las que no preguntamos nombres.

            SaveFaces(faceName);

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;

            isAnewFace = false;
            faceId = GetNextFaceId();
        }

        //Entrenamiento de rostros
        private bool TrainDataSetWithEigenFaceRecognizer()
        {
            //En caso de que exista un archivo de entrenamiento removerlo
            if (File.Exists(pathTrainedFaceModel))
            {
                File.Delete(pathTrainedFaceModel);
            }

            // Obtener TODAS las fotos desde SQL
            ClsAccionesDB db = new ClsAccionesDB();
            List<Usuario> usuarios = db.ObtenerUsuarios();

            List<Mat> images = new List<Mat>();
            List<int> labels = new List<int>();

            //si existe alguna
            foreach (var usuario in usuarios)
            {
                //obtener fotos del usuario directamente desde BD ***
                List<byte[]> fotos = db.ObtenerRostrosPorUsuario(usuario.Usuario_id);

                foreach (byte[] foto in fotos)
                {
                    // obtenemos el rostro en el mismo tamaño que hemos estado guardandolo
                    Mat faceImage = Mat.FromImageData(foto, ImreadModes.Grayscale);

                    // Redimensionar igual que en EmguCV
                    Cv2.Resize(faceImage, faceImage, new OpenCvSharp.Size(modelWidth, modelHeight),
                               0, 0, InterpolationFlags.Cubic);

                    //guardamos el rostro
                    images.Add(faceImage);

                    // Obtener ID desde la BD
                    labels.Add(usuario.Usuario_id);
                }
            }

            // listas que OpenCvSharp sí acepta
            if (images.Count == 0)
                return false;

            // entrenar
            eigenFaceRecognizer.Train(images, labels);

            // guardar
            eigenFaceRecognizer.Write(pathTrainedFaceModel);

            return true;
        }

        private void RECONOCIMIENTO_FACIAL_Load(object sender, EventArgs e) { }

        private void PictureBox1_Click(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usuario seleccionado = comboBox1.SelectedItem as Usuario;

            if (seleccionado != null)
            {
                faceId = seleccionado.Usuario_id;
                faceName = seleccionado.usuario_nombre;

                // esto indica que NO es una nueva cara
                isAnewFace = false;
            }
        }

        private string GetFacesName(int label)
        {
            string[] files = Directory.GetFiles(pathSavedFaces, "*.bmp");

            foreach (string f in files)
            {
                string file = Path.GetFileNameWithoutExtension(f);
                string[] parts = file.Split('_');

                if (parts.Length >= 2)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1];

                    if (id == label)
                        return name;
                }
            }

            return "unknown";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool wasTrained = TrainDataSetWithEigenFaceRecognizer();
            if (wasTrained)
            {
                MessageBox.Show("Entrenamiendo exitoso");
            }
            else
            {
                MessageBox.Show("Entrenamiento fallido");
            }
        }

        private int CountFacesOfId(int id)
        {
            int count = 0;
            var files = Directory.GetFiles(pathSavedFaces, "*.bmp");

            foreach (var f in files)
            {
                var file = Path.GetFileNameWithoutExtension(f);
                var parts = file.Split('_');

                if (parts.Length >= 3 && int.TryParse(parts[0], out int fileId))
                {
                    if (fileId == id)
                        count++;
                }
            }

            return count;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam = new VideoCapture();
            recordingType = RecordingType.recognition;


            //antes de encender la camara debemos decirle a eigen donde esta el archivo preentrenado sino
            //existe no existira el reconocimiento
            if (File.Exists(pathTrainedFaceModel))
            {
                eigenFaceRecognizer.Read(pathTrainedFaceModel);
                TurnOnCamera();

            }
            else
            {
                MessageBox.Show("Modelo entrenado invalido");
            }
        }

        //CONVERTIR A BYTE PARA INGRESAR A LA DB
        private byte[] MatToByteArray(Mat img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap bitmap = BitmapConverter.ToBitmap(img);
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private void BorrarFotosLocales(int usuarioId)
        {
            string folder = Path.Combine(Application.StartupPath, "Faces");

            if (!Directory.Exists(folder))
                return;

            // Buscar archivos del usuario (2_*.bmp)
            string patron = $"{usuarioId}_*.bmp";

            string[] archivos = Directory.GetFiles(folder, patron);

            foreach (var archivo in archivos)
            {
                try
                {
                    File.Delete(archivo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo borrar: " + archivo + "\n" + ex.Message);
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Detener cualquier reconocimiento o captura
                running = false;

                // 2. Apagar cámara si está en uso
                if (cam != null)
                {
                    try { cam.Release(); } catch { }
                    try { cam.Dispose(); } catch { }
                    cam = null;
                }

                // 3. Liberar imagen del PictureBox
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                // 4. Forzar liberación de archivos bloqueados
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // 5. Eliminar archivos locales
                BorrarFotosLocales(faceId);

                // 6. Eliminar fotos en BD
                ClsAccionesDB db = new ClsAccionesDB();
                db.BorrarFotosUsuario(faceId);

                // 7. REENTRENAR el sistema para eliminar el modelo del usuario
                if (TrainDataSetWithEigenFaceRecognizer())
                {
                    MessageBox.Show("Fotos eliminadas y modelo actualizado.");
                }
                else
                {
                    MessageBox.Show("Fotos eliminadas, pero no se pudo regenerar el modelo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar fotos: " + ex.Message);
            }
        }


    }

}