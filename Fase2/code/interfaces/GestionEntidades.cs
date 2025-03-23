using System;
using Gtk;
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
                entryIDUsuario = CrearCampo("Nombres", false);
                entryMarca = CrearCampo("Apellidos", false);
                entryModelo = CrearCampo("Correo", false);
                entryPlaca = CrearCampo("Edad", false);

                frameBox.PackStart(entryIDUsuario, false, false, 2);
                frameBox.PackStart(entryMarca, false, false, 2);
                frameBox.PackStart(entryModelo, false, false, 2);
                frameBox.PackStart(entryPlaca, false, false, 2);
            }

            buscarButton = new Button("Buscar");
            buscarButton.ModifyBg(StateType.Normal, new Gdk.Color(100, 200, 100)); // Verde
            buscarButton.Clicked += OnBuscarClicked;

            eliminarButton = new Button("Eliminar");
            eliminarButton.ModifyBg(StateType.Normal, new Gdk.Color(200, 50, 50)); // Rojo
            eliminarButton.Clicked += OnEliminarClicked;

            HBox buttonBox = new HBox(true, 5);
            buttonBox.PackStart(buscarButton, true, true, 5);
            buttonBox.PackStart(eliminarButton, true, true, 5);

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
                }
                else
                {
                    MostrarMensajeError("Usuario no encontrado");
                }
            }
        }

        private void OnEliminarClicked(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(entryID.Text, out id))
            {
                MostrarMensajeError("ID inválido");
                return;
            }

            bool eliminado = false;
            if (tipoSeleccionado == "Vehículos")
            {
                eliminado = Variables.listaVehiculos.Eliminar(id);
            }
            else if (tipoSeleccionado == "Usuarios")
            {
                eliminado = Variables.listaUsuarios.Eliminar(id);
            }

            if (eliminado)
            {
                MostrarMensaje("Eliminado correctamente");
                LimpiarCampos();
            }
            else
            {
                MostrarMensajeError("No se encontró el ID para eliminar");
            }
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
        }

        private Entry CrearCampo(string labelText, bool editable)
        {
            HBox hbox = new HBox(false, 5);
            Label label = new Label(labelText);
            label.SetSizeRequest(80, 30);
            Entry entry = new Entry();
            entry.IsEditable = editable;
            entry.WidthRequest = 150;

            hbox.PackStart(label, false, false, 5);
            hbox.PackStart(entry, false, false, 5);

            mainContainer?.PackStart(hbox, false, false, 5);
            return entry;
        }
    }
}