using Gtk;
using code.data;

namespace code.interfaces
{
    public class RegisterWindow : Window
    {
        // Elementos de la interfaz
        private Entry IDEntry;
        private Entry nombreEntry;
        private Entry apellidosEntry;
        private Entry correoEntry;
        private Entry edadEntry;
        private Entry contraseniaEntry;
        private Button registerButton;
        private Button cancelButton;

        public RegisterWindow() : base("Register Window")
        {
            // Establecer el tamaño de la ventana
            SetDefaultSize(300, 250);
            SetPosition(WindowPosition.Center);

            // Crear un contenedor de caja vertical para la ventana
            VBox vbox = new VBox(false, 5);

            Label IDLabel = new Label("ID:");
            vbox.PackStart(IDLabel, false, false, 0);
            IDEntry = new Entry();
            IDEntry.IsEditable = false; // El campo ID no será editable
            vbox.PackStart(IDEntry, false, false, 0);

            Label nombreLabel = new Label("Nombre:");
            vbox.PackStart(nombreLabel, false, false, 0);
            nombreEntry = new Entry();
            vbox.PackStart(nombreEntry, false, false, 0);

            Label apellidosLabel = new Label("Apellidos:");
            vbox.PackStart(apellidosLabel, false, false, 0);
            apellidosEntry = new Entry();
            vbox.PackStart(apellidosEntry, false, false, 0);

            Label correoLabel = new Label("Correo:");
            vbox.PackStart(correoLabel, false, false, 0);
            correoEntry = new Entry();
            vbox.PackStart(correoEntry, false, false, 0);

            Label edadLabel = new Label("Edad:");
            vbox.PackStart(edadLabel, false, false, 0);
            edadEntry = new Entry();
            vbox.PackStart(edadEntry, false, false, 0);

            Label contraseniaLabel = new Label("Contraseña:");
            vbox.PackStart(contraseniaLabel, false, false, 0);
            contraseniaEntry = new Entry();
            contraseniaEntry.Visibility = false; // Establecer para que no se vea la contraseña
            vbox.PackStart(contraseniaEntry, false, false, 0);

            registerButton = new Button("Registrar");
            registerButton.Clicked += OnRegisterButtonClicked;
            vbox.PackStart(registerButton, false, false, 0);

            cancelButton = new Button("Cancelar");
            cancelButton.Clicked += OnCancelButtonClicked;
            vbox.PackStart(cancelButton, false, false, 0);

            Add(vbox);

            // Asignar automáticamente el ID al abrir la ventana
            IDEntry.Text = GenerarNuevoID().ToString();

            ShowAll();
        }

        private void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string nombres = nombreEntry.Text;
            string apellidos = apellidosEntry.Text;
            string correo = correoEntry.Text;
            int edad;
            string contrasenia = contraseniaEntry.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasenia) ||
                !int.TryParse(edadEntry.Text, out edad))
            {
                MostrarMensajeError("Por favor, complete todos los campos correctamente.");
                return;
            }

            // Verificar si el correo ya existe
            if (Variables.listaUsuarios.ExisteCorreo(correo))
            {
                MostrarMensajeError("El correo ya está registrado.");
                return;
            }

            // Obtener el ID generado automáticamente
            int id = int.Parse(IDEntry.Text);

            // Registrar el usuario
            Variables.listaUsuarios.Agregar(id, nombres, apellidos, correo, edad, contrasenia);

            Console.WriteLine($"Usuario registrado: {nombres} {apellidos}, Correo: {correo}, ID: {id}");

            MostrarMensaje("Usuario registrado exitosamente.");
            this.Destroy();
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Registro cancelado.");
            this.Destroy();
        }

        private int GenerarNuevoID()
        {
            // Obtener el ID más alto registrado en la lista de usuarios
            int maxID = Variables.listaUsuarios.ObtenerMaxID();
            return maxID + 1; // Incrementar en 1
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
    }
}