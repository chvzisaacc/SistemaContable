using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
using Capa_de_Presentación.CLASES;
using System.Net.Http;
using System.Text.Json;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Formulario de inicio de sesión. Valida credenciales y establece la sesión en <c>Sesion1</c>.
    /// Contiene lógica de validación de campos y delega la autenticación a <see cref="ClsRecuperacion"/>.
    /// </summary>
    public partial class FRM_PG1 : Form
    {
        private HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Identificador del usuario autenticado. Se establece tras iniciar sesión correctamente.
        /// </summary>
        public int UsuarioId { get; private set; }

        /// <summary>
        /// Rol del usuario autenticado. Se obtiene de la lógica de inicio de sesión.
        /// </summary>
        public int Rol { get; private set; }

        /// <summary>
        /// Identificador de la parroquia asociada al usuario autenticado.
        /// </summary>
        public int ParroquiaId { get; private set; }

        /// <summary>
        /// Constructor: inicializa componentes y configura la ventana.
        /// </summary>
        public FRM_PG1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Configurar el boton de sincronizar si existe en el diseñador
            ConfigurarBotonSincronizar();
        }

        /// <summary>
        /// Configura las propiedades del boton de sincronizacion.
        /// </summary>
        private void ConfigurarBotonSincronizar()
        {
            // Si el boton no existe en el diseñador, crearlo programaticamente
            if (btnSincronizar == null)
            {
                btnSincronizar = new Button();
                btnSincronizar.Text = "Sincronizar";
                btnSincronizar.Size = new Size(100, 30);
                btnSincronizar.BackColor = Color.FromArgb(52, 73, 94);
                btnSincronizar.ForeColor = Color.White;
                btnSincronizar.FlatStyle = FlatStyle.Flat;
                btnSincronizar.Click += btnSincronizar_Click;

                // Posicionar al lado del boton de inicio de sesion
                // Asumiendo que button1 es el boton de inicio de sesion
                if (btn_iniciar_sesion != null)
                {
                    btnSincronizar.Location = new Point(btn_iniciar_sesion.Right + 10, btn_iniciar_sesion.Top);
                    this.Controls.Add(btnSincronizar);
                }
            }
        }

        /// <summary>
        /// Evento del botón de inicio de sesión. Valida campos, llama a la lógica de autenticación y
        /// en caso de éxito inicializa la sesión y cierra el formulario con DialogResult.OK.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            ClsValidaciones validaciones = new ClsValidaciones();

            if (string.IsNullOrWhiteSpace(txt_usuario.Text) || txt_usuario.Text == "Usuario")
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario.");
                return;
            }


            if (string.IsNullOrWhiteSpace(txt_contraseña.Text) || txt_contraseña.Text == "Contraseña")
            {
                MessageBox.Show("Por favor, ingrese una contraseña.");
                return;
            }



            if (txt_contraseña.Text.Contains(" "))
            {
                MessageBox.Show("La contraseña no puede contener espacios.");
                return;
            }

            // Lógica de autenticación: delega a ClsRecuperacion
            ClsRecuperacion login = new ClsRecuperacion();
            int rol = login.IniciarSesion(txt_usuario.Text, txt_contraseña.Text, 0, this, label1);

            if (rol <= 0)
                return;

            // Obtener identificador y parroquia del usuario autenticado
            ClsAccionesDB acciones = new ClsAccionesDB();
            var resultado = acciones.ValidarCredenciales(txt_usuario.Text, txt_contraseña.Text, 0);

            UsuarioId = resultado.usuario_id;
            ParroquiaId = resultado.id_parroquia;
            Rol = resultado.rol_id;

            // Iniciar sesión global
            Sesion1.IniciarSesion(UsuarioId, Rol, ParroquiaId);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Evento del boton de sincronizacion. Realiza la sincronizacion pull desde la API remota.
        /// </summary>
        private async void btnSincronizar_Click(object sender, EventArgs e)
        {
            // Deshabilitar el boton durante la sincronizacion
            btnSincronizar.Enabled = false;
            btnSincronizar.Text = "Sincronizando...";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10); // Timeout de 10 segundos

                    string urlApiRemota = "https://rozella-exanthematic-jeffrey.ngrok-free.dev";

                    var request = new { UrlRemota = urlApiRemota };

                    string apiLocal = "http://localhost:5145";
                    var jsonContent = new StringContent(
                        JsonSerializer.Serialize(request),
                        System.Text.Encoding.UTF8,
                        "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(
                        $"{apiLocal}/api/Data/jalar-cambios",
                        jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResultado = await response.Content.ReadAsStringAsync();
                        var resultado = JsonSerializer.Deserialize<JsonElement>(jsonResultado);
                        int cambiosAplicados = resultado.GetProperty("cambiosAplicados").GetInt32();

                        if (cambiosAplicados > 0)
                        {
                            MessageBox.Show($"Sincronizacion completada exitosamente.\n\nCambios aplicados: {cambiosAplicados}",
                                "Sincronizacion Exitosa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No hay cambios pendientes para sincronizar.\n\nLa base de datos se encuentra actualizada.",
                                "Sincronizacion",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No fue posible completar la sincronizacion.\n\nPor favor, verifique su conexion a internet e intente nuevamente.",
                            "Error de Sincronizacion",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No es posible establecer comunicación con el servidor central.\n\n" +
                               "El administrador no ha activado el servicio de sincronizacion.\n\n" +
                               "Por favor, comuníquese con el administrador para reportar esta situación.",
                               "Servidor No Disponible",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("La solicitud de sincronizacion ha excedido el tiempo de espera.\n\n" +
                               "El servidor central no responde. Verifique que el administrador haya activado el servicio.\n\n" +
                               "Si el problema persiste, contacte al administrador.",
                               "Tiempo de Espera Agotado",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado durante la sincronizacion.\n\n" +
                               $"Detalle técnico: {ex.Message}\n\n" +
                               $"Por favor, comuníquese con el administrado para asistencia técnica.",
                               "Error Inesperado",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            finally
            {
                // Restaurar el boton
                btnSincronizar.Enabled = true;
                btnSincronizar.Text = "Sincronizar";
            }
        }

        /// <summary>
        /// Muestra el formulario de recuperación de contraseña.
        /// </summary>
        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var frm = new Olvidaste_tu_contraseña())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
            this.Show();
        }

        /// <summary>
        /// Controlador para el clic en el placeholder del usuario: limpia el texto y ajusta el color.
        /// </summary>
        private void txtUsuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario.Text == "Usuario")
            {
                txt_usuario.Text = "";
                txt_usuario.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Restaurar placeholder si el campo usuario queda vacío al salir del control.
        /// </summary>
        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_usuario.Text))
            {
                txt_usuario.Text = "Usuario";
                txt_usuario.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Limpia el placeholder de contraseña al hacer foco.
        /// </summary>
        private void txtContraseña_Click(object sender, EventArgs e)
        {
            if (txt_contraseña.Text == "Contraseña")
            {
                txt_contraseña.Text = "";
                txt_contraseña.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Restaura el placeholder de contraseña si queda vacío.
        /// </summary>
        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_contraseña.Text))
            {
                txt_contraseña.Text = "Contraseña";
                txt_contraseña.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Controlador reservado para el PictureBox principal (sin lógica actual).
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Restringe la entrada en el campo usuario: evita espacio inicial y espacios en general.
        /// </summary>
        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones v = new ClsValidaciones();

            if (!v.NoPermitirEspacioInicial(txt_usuario.Text, e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == ' ')
                e.Handled = true;
        }

        /// <summary>
        /// Restringe la entrada en el campo contraseña: evita espacio inicial y espacios en general.
        /// </summary>
        private void txt_contraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones val = new ClsValidaciones();

            if (!val.NoPermitirEspacioInicial(txt_contraseña.Text, e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == ' ')
                e.Handled = true;
        }

        /// <summary>
        /// Muestra la contraseña (quita el enmascarado).
        /// </summary>
        private void pbMostrar_Click(object sender, EventArgs e)
        {
            pbOcultar.BringToFront();
            txt_contraseña.PasswordChar = '\0';
        }

        /// <summary>
        /// Oculta la contraseña (activa el enmascarado).
        /// </summary>
        private void pbOcultar_Click(object sender, EventArgs e)
        {
            pbMostrar.BringToFront();
            txt_contraseña.PasswordChar = '*';
        }

        /// <summary>
        /// Evento Load del formulario. Reservado para inicializaciones futuras.
        /// </summary>
        private void FRM_PG1_Load(object sender, EventArgs e)
        {

        }

        private void btnSincronizar_Click_1(object sender, EventArgs e)
        {

        }
    }
}