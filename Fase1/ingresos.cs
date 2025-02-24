using Gtk;
using ListaDobleUnsafe;

public class IngresoUsuario : Window
{
    private Entry entryId, entryNombres, entryApellidos, entryCorreo, entryContrasenia;
    private ListaDoblementeEnlazada listaUsuarios;

    public IngresoUsuario(ListaDoblementeEnlazada lista) : base("Ingreso de Usuario")
    {
        listaUsuarios = lista;
        SetDefaultSize(300, 250);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        Label labelTitulo = new Label("Ingreso de Usuario");
        vbox.PackStart(labelTitulo, false, false, 5);

        entryId = AgregarCampo(vbox, "ID:");
        entryNombres = AgregarCampo(vbox, "Nombres:");
        entryApellidos = AgregarCampo(vbox, "Apellidos:");
        entryCorreo = AgregarCampo(vbox, "Correo:");
        entryContrasenia = AgregarCampo(vbox, "Contraseña:");

        Button btnGuardar = new Button("Guardar");
        btnGuardar.Clicked += GuardarUsuario;
        vbox.PackStart(btnGuardar, false, false, 5);

        ShowAll();
    }

    private Entry AgregarCampo(VBox contenedor, string etiqueta)
    {
        HBox hbox = new HBox(false, 5);
        Label label = new Label(etiqueta);
        Entry entry = new Entry();

        hbox.PackStart(label, false, false, 5);
        hbox.PackStart(entry, true, true, 5);
        contenedor.PackStart(hbox, false, false, 5);

        return entry;
    }

    private void GuardarUsuario(object sender, EventArgs e)
{
    int id;
    if (!int.TryParse(entryId.Text, out id))
    {
        Console.WriteLine("El ID debe ser un número entero.");
        return;
    }

    string nombres = entryNombres.Text;
    string apellidos = entryApellidos.Text;
    string correo = entryCorreo.Text;
    string contrasenia = entryContrasenia.Text;

    if (listaUsuarios == null)
    {
        Console.WriteLine("listaUsuarios es nulo.");
        return;
    }

    listaUsuarios.Insertar(id, nombres, apellidos, correo, contrasenia);
    Console.WriteLine("Usuario agregado correctamente.");
    Destroy();
}
}


public class IngresoVehiculo : Window
{
    public IngresoVehiculo() : base("Ingreso de Vehículo")
    {
        SetDefaultSize(300, 250);
        SetPosition(WindowPosition.Center);
        VBox vbox = new VBox(false, 5);

        Label titleLabel = new Label("<b>Ingreso de Vehículo</b>");
        titleLabel.UseMarkup = true;
        vbox.PackStart(titleLabel, false, false, 5);

        string[] labels = { "Id", "Id Usuario", "Marca", "Modelo", "Placa" };
        Entry[] entries = new Entry[labels.Length];

        for (int i = 0; i < labels.Length; i++)
        {
            HBox hbox = new HBox(false, 5);
            Label lbl = new Label(labels[i]);
            lbl.WidthRequest = 80;
            entries[i] = new Entry();
            hbox.PackStart(lbl, false, false, 5);
            hbox.PackStart(entries[i], true, true, 5);
            vbox.PackStart(hbox, false, false, 5);
        }

        Button btnGuardar = new Button("Guardar");
        vbox.PackStart(btnGuardar, false, false, 5);

        Add(vbox);
    }
}

public class IngresoRepuesto : Window
{
    public IngresoRepuesto() : base("Ingreso de Repuesto")
    {
        SetDefaultSize(300, 250);
        SetPosition(WindowPosition.Center);
        VBox vbox = new VBox(false, 5);

        Label titleLabel = new Label("<b>Ingreso de Repuesto</b>");
        titleLabel.UseMarkup = true;
        vbox.PackStart(titleLabel, false, false, 5);

        string[] labels = { "Id", "Repuesto", "Detalles", "Costo" };
        Entry[] entries = new Entry[labels.Length];

        for (int i = 0; i < labels.Length; i++)
        {
            HBox hbox = new HBox(false, 5);
            Label lbl = new Label(labels[i]);
            lbl.WidthRequest = 80;
            entries[i] = new Entry();
            hbox.PackStart(lbl, false, false, 5);
            hbox.PackStart(entries[i], true, true, 5);
            vbox.PackStart(hbox, false, false, 5);
        }

        Button btnGuardar = new Button("Guardar");
        vbox.PackStart(btnGuardar, false, false, 5);

        Add(vbox);
    }
}
