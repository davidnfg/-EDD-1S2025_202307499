using System;
using Gtk;
using System.Text;
using System.Security.Cryptography;
using code.data;

namespace code.interfaces
{
    public class GestionEntidades : Window
    {
        private ComboBoxText comboSeleccion;
        private Entry entryID;
        private Entry entryIDUsuario;
        private Entry entryMarca;
        private Entry entryModelo;
        private Entry entryPlaca;
        private Entry entryContrasenia;
        private Button buscarButton;
        private Button eliminarButton;
        private VBox mainContainer;
        private string tipoSeleccionado;

        public GestionEntidades() : base("Seleccionar Tipo de Edición")
        {
            SetDefaultSize(300, 150);
            SetPosition(WindowPosition.Center);

            VBox vbox = new VBox(false, 5);
            Label lblSeleccion = new Label("Seleccione qué desea editar:");

            comboSeleccion = new ComboBoxText();
            comboSeleccion.AppendText("Usuarios");
            comboSeleccion.AppendText("Vehículos");
            comboSeleccion.Active = 0;

            Button continuarButton = new Button("Continuar");
            continuarButton.Clicked += OnContinuarClicked;

            vbox.PackStart(lblSeleccion, false, false, 5);
            vbox.PackStart(comboSeleccion, false, false, 5);
            vbox.PackStart(continuarButton, false, false, 5);

            Add(vbox);
            ShowAll();
        }

        private void OnContinuarClicked(object sender, EventArgs e)
        {
            tipoSeleccionado = comboSeleccion.ActiveText;
            AbrirVentanaEdicion(tipoSeleccionado);
        }

        private void AbrirVentanaEdicion(string tipo)
        {
            Window ventanaEdicion = new Window("Editar " + tipo);
            ventanaEdicion.SetDefaultSize(350, 250);
            ventanaEdicion.SetPosition(WindowPosition.Center);

            mainContainer = new VBox(false, 5);
            Frame frame = new Frame("Editar " + tipo);
            VBox frameBox = new VBox(false, 5);

            entryID = CrearCampo("Id", true);
            frameBox.PackStart(entryID, false, false, 2);

            if (tipo == "Vehículos")
            {
                entryIDUsuario = CrearCampo("Id_Usuario", false);
                entryMarca = CrearCampo("Marca", false);
                entryModelo = CrearCampo("Modelo", false);
                entryPlaca = CrearCampo("Placa", false);

                frameBox.PackStart(entryIDUsuario, false, false, 2);
                frameBox.PackStart(entryMarca, false, false, 2);
                frameBox.PackStart(entryModelo, false, false, 2);
                frameBox.PackStart(entryPlaca, false, false, 2);
            }
            else // Usuarios
            {
                entryIDUsuario = CrearCampo("Nombres", true);
                entryMarca = CrearCampo("Apellidos", true);
                entryModelo = CrearCampo("Correo", true);
                entryPlaca = CrearCampo("Edad", true);
                entryContrasenia = CrearCampo("Contraseña", true);

                frameBox.PackStart(entryIDUsuario, false, false, 2);
                frameBox.PackStart(entryMarca, false, false, 2);
                frameBox.PackStart(entryModelo, false, false, 2);
                frameBox.PackStart(entryPlaca, false, false, 2);
                frameBox.PackStart(entryContrasenia, false, false, 2);
            }

            buscarButton = new Button("Buscar");
            buscarButton.ModifyBg(StateType.Normal, new Gdk.Color(100, 200, 100)); // Verde
            buscarButton.Clicked += OnBuscarClicked;

            Button agregarButton = new Button("Agregar");
            agregarButton.ModifyBg(StateType.Normal, new Gdk.Color(50, 150, 250)); // Azul
            agregarButton.Clicked += OnAgregarClicked;

            HBox buttonBox = new HBox(true, 5);
            buttonBox.PackStart(buscarButton, true, true, 5);
            buttonBox.PackStart(agregarButton, true, true, 5);

            frameBox.PackStart(buttonBox, false, false, 5);
            frame.Add(frameBox);
            mainContainer.PackStart(frame, false, false, 10);

            ventanaEdicion.Add(mainContainer);
            ventanaEdicion.ShowAll();
        }

        private void OnBuscarClicked(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(entryID.Text, out id))
            {
                MostrarMensajeError("ID inválido");
                return;
            }

            if (tipoSeleccionado == "Vehículos")
            {
                var vehiculo = Variables.listaVehiculos.Buscar(id);
                if (vehiculo != null)
                {
                    entryIDUsuario.Text = vehiculo.IdUsuario.ToString();
                    entryMarca.Text = vehiculo.Marca;
                    entryModelo.Text = vehiculo.Modelo.ToString();
                    entryPlaca.Text = vehiculo.Placa;
                }
                else
                {
                    MostrarMensajeError("Vehículo no encontrado");
                }
            }
            else if (tipoSeleccionado == "Usuarios")
            {
                var usuario = Variables.listaUsuarios.Buscar(id);
                if (usuario != null)
                {
                    entryIDUsuario.Text = usuario.Nombres;
                    entryMarca.Text = usuario.Apellidos;
                    entryModelo.Text = usuario.Correo;
                    entryPlaca.Text = usuario.Edad.ToString();

                    // Mostrar la contraseña hasheada
                    entryContrasenia.Text = CalcularHash(usuario.Contrasenia);
                }
                else
                {
                    MostrarMensajeError("Usuario no encontrado");
                }   
            }
        }

        private void OnAgregarClicked(object sender, EventArgs e)
        {
            int id, edad;
            if (!int.TryParse(entryID.Text, out id) || !int.TryParse(entryPlaca.Text, out edad))
            {
                MostrarMensajeError("ID o Edad inválidos");
                return;
            }

            string nombres = entryIDUsuario.Text;
            string apellidos = entryMarca.Text;
            string correo = entryModelo.Text;
            string contrasenia = entryContrasenia.Text;
            if (string.IsNullOrEmpty(nombres) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(correo))
            {
                MostrarMensajeError("Todos los campos deben estar llenos");
                return;
            }

            if (Variables.listaUsuarios.ExisteCorreo(correo))
            {
                MostrarMensajeError("El correo ya está registrado");
                return;
            }

            Variables.listaUsuarios.AddBlock(id, nombres, apellidos, correo, edad, contrasenia);
            MostrarMensaje($"Usuario agregado correctamente:\nID: {id}\nNombre: {nombres} {apellidos}");
            LimpiarCampos();
        }
        private void MostrarMensajeError(string mensaje)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, mensaje);
            md.Run();
            md.Destroy();
        }

        private void MostrarMensaje(string mensaje)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, mensaje);
            md.Run();
            md.Destroy();
        }

        private void LimpiarCampos()
        {
            entryID.Text = "";
            entryIDUsuario.Text = "";
            entryMarca.Text = "";
            entryModelo.Text = "";
            entryPlaca.Text = "";
            entryContrasenia.Text = "";
        }

        private Entry CrearCampo(string labelText, bool editable = true, bool isPassword = false)
        {
            HBox hbox = new HBox(false, 5);
            Label label = new Label(labelText);
            label.SetSizeRequest(80, 30);
            Entry entry = new Entry();
            entry.IsEditable = editable; // Configurar si el campo es editable o no
            entry.WidthRequest = 150;

            if (isPassword)
            {
                entry.Visibility = false; // Ocultar el texto ingresado
            }

            hbox.PackStart(label, false, false, 5);
            hbox.PackStart(entry, false, false, 5);

            mainContainer?.PackStart(hbox, false, false, 5);
            return entry;
        }

        private string CalcularHash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convertir a hexadecimal
                }
                return builder.ToString();
            }
        }
    }
}