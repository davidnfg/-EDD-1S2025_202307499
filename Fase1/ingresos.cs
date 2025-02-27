using Gtk;
using ListaDobleUnsafe;

public class IngresoUsuario : Window
{
    private Entry entryId, entryNombres, entryApellidos, entryCorreo, entryContrasenia;
    private ListaEnlazadaSimple listaUsuarios;
    private ListaDEVehiculos listaVehiculos;
    private ListaDERep listaRepuestos;

    public IngresoUsuario(ListaEnlazadaSimple lista) : base("Ingreso de Usuario")
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
    private Entry entryId, entryIdUsuario, entryMarca, entryModelo, entryPlaca;
    private ListaDEVehiculos listaVehiculos;

    public IngresoVehiculo(ListaDEVehiculos lista) : base("Ingreso de Vehículo")
    {
        listaVehiculos = lista;
        SetDefaultSize(300, 250);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        Label labelTitulo = new Label("Ingreso de Vehículo");
        vbox.PackStart(labelTitulo, false, false, 5);

        entryId = AgregarCampo(vbox, "ID:");
        entryIdUsuario = AgregarCampo(vbox, "ID Usuario:");
        entryMarca = AgregarCampo(vbox, "Marca:");
        entryModelo = AgregarCampo(vbox, "Modelo:");
        entryPlaca = AgregarCampo(vbox, "Placa:");

        Button btnGuardar = new Button("Guardar");
        btnGuardar.Clicked += GuardarVehiculo;
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

    private void GuardarVehiculo(object sender, EventArgs e)
    {
        int id, idUsuario, modelo;

        if (!int.TryParse(entryId.Text, out id))
        {
            Console.WriteLine("El ID debe ser un número entero.");
            return;
        }

        if (!int.TryParse(entryIdUsuario.Text, out idUsuario))
        {
            Console.WriteLine("El ID Usuario debe ser un número entero.");
            return;
        }

        if (!int.TryParse(entryModelo.Text, out modelo))
        {
            Console.WriteLine("El Modelo debe ser un número entero.");
            return;
        }

        string marca = entryMarca.Text;
        string placa = entryPlaca.Text;

        if (listaVehiculos == null)
        {
            Console.WriteLine("listaVehiculos es nulo.");
            return;
        }

        listaVehiculos.Insertar(id, idUsuario, marca, modelo, placa);
        Console.WriteLine("Vehículo agregado correctamente.");
        Destroy();
    }
}



public class IngresoRepuesto : Window
{
    private Entry entryId, entryRepuesto, entryDetalles, entryCosto;
    private ListaDERep listaRepuestos;

    public IngresoRepuesto(ListaDERep lista) : base("Ingreso de Repuesto")
    {
        listaRepuestos = lista;
        SetDefaultSize(300, 250);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        Label labelTitulo = new Label("Ingreso de Repuesto");
        vbox.PackStart(labelTitulo, false, false, 5);

        entryId = AgregarCampo(vbox, "ID:");
        entryRepuesto = AgregarCampo(vbox, "Repuesto:");
        entryDetalles = AgregarCampo(vbox, "Detalles:");
        entryCosto = AgregarCampo(vbox, "Costo:");

        Button btnGuardar = new Button("Guardar");
        btnGuardar.Clicked += GuardarRepuesto;
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

    private void GuardarRepuesto(object sender, EventArgs e)
    {
        int id;
        double costo;

        if (!int.TryParse(entryId.Text, out id))
        {
            Console.WriteLine("El ID debe ser un número entero.");
            return;
        }

        if (!double.TryParse(entryCosto.Text, out costo))
        {
            Console.WriteLine("El costo debe ser un número válido.");
            return;
        }

        string repuesto = entryRepuesto.Text;
        string detalles = entryDetalles.Text;

        if (listaRepuestos == null)
        {
            Console.WriteLine("listaRepuestos es nulo.");
            return;
        }

        listaRepuestos.Insertar(id, repuesto, detalles, costo);
        Console.WriteLine("Repuesto agregado correctamente.");
        Destroy();
    }
}


