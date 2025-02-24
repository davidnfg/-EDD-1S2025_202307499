using Gtk;
using System;
using System.IO;
using Newtonsoft.Json;  //Para instalar esto se utiliza el comando "dotnet add package Newtonsoft.Json"

public class CargaMasiva : Window
{
    private ComboBoxText comboBox;
    private FileChooserButton fileChooser;

    public CargaMasiva() : base("Carga Masiva")
    {
        SetDefaultSize(350, 300);
        SetPosition(WindowPosition.Center);
        ModifyBg(StateType.Normal, new Gdk.Color(200, 200, 200));

        VBox vbox = new VBox(false, 10);
        vbox.BorderWidth = 20;

        Label titleLabel = new Label("<b>Menu Carga masiva</b>");
        titleLabel.UseMarkup = true;
        vbox.PackStart(titleLabel, false, false, 5);
        
        comboBox = new ComboBoxText();
        comboBox.AppendText("Usuarios");
        comboBox.AppendText("Vehículos");
        comboBox.AppendText("Repuestos");
        comboBox.Active = 0;
        vbox.PackStart(comboBox, false, false, 5);

        fileChooser = new FileChooserButton("Seleccionar archivo JSON", FileChooserAction.Open);
        fileChooser.Filter = new FileFilter();
        fileChooser.Filter.AddPattern("*.json");
        vbox.PackStart(fileChooser, false, false, 5);

        Button btnCargar = new Button("Cargar");
        btnCargar.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        btnCargar.Clicked += OnCargarArchivoClicked;
        vbox.PackStart(btnCargar, false, false, 5);
        
        Add(vbox);
    }

    private void OnCargarArchivoClicked(object sender, EventArgs e)
    {
        string filePath = fileChooser.Filename;
        if (string.IsNullOrEmpty(filePath)) return;

        string selectedOption = comboBox.ActiveText;
        switch (selectedOption)
        {
            case "Usuarios":
                CargarJSONuser(filePath);
                break;
            case "Vehículos":
                CargarJSONvehiculos(filePath);
                break;
            case "Repuestos":
                CargarJSONrepuestos(filePath);
                break;
        }
    }

    private void CargarJSONuser(string filePath)
    {
        try
        {
            string jsonContent = File.ReadAllText(filePath);
            var Useres = JsonConvert.DeserializeObject<User[]>(jsonContent);

            Console.WriteLine("Datos cargados correctamente:");
            foreach (var User in Useres)
            {
                Console.WriteLine($"ID: {User.ID}, Nombres: {User.Nombres}, Apellidos: {User.Apellidos}, Correo: {User.Correo}, Contrasenia: {User.Contrasenia}");
                ListaGlobal.Lista_Usuarios.Insertar(User.ID, User.Nombres, User.Apellidos, User.Correo, User.Contrasenia);
            }

            MessageDialog successDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                "Archivo JSON cargado correctamente.");
            successDialog.Run();
            successDialog.Destroy();
        }
        catch (Exception ex)
        {
            MessageDialog errorDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Error,
                ButtonsType.Ok,
                $"Error al cargar el archivo JSON: {ex.Message}");
            errorDialog.Run();
            errorDialog.Destroy();
        }
    }

    private void CargarJSONvehiculos(string filePath)
    {
       try
        {
            string jsonContent = File.ReadAllText(filePath);
            var Vehis = JsonConvert.DeserializeObject<Vehi[]>(jsonContent);

            Console.WriteLine("Datos cargados correctamente:");
            foreach (var Vehi in Vehis)
            {
                Console.WriteLine($"ID: {Vehi.ID}, ID_Usuario: {Vehi.ID_Usuario}, Marca: {Vehi.Marca}, Modelo: {Vehi.Modelo}, Placa: {Vehi.Placa}");
                ListaGlobal.Lista_Vehiculos.Insertar(Vehi.ID, Vehi.ID_Usuario, Vehi.Marca, Vehi.Modelo, Vehi.Placa);
            }

            MessageDialog successDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                "Archivo JSON cargado correctamente.");
            successDialog.Run();
            successDialog.Destroy();
        }
        catch (Exception ex)
        {
            MessageDialog errorDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Error,
                ButtonsType.Ok,
                $"Error al cargar el archivo JSON: {ex.Message}");
            errorDialog.Run();
            errorDialog.Destroy();
        }
    
    }

    private void CargarJSONrepuestos(string filePath)
    {
        try
        {
            string jsonContent = File.ReadAllText(filePath);
            var Reps = JsonConvert.DeserializeObject<Rep[]>(jsonContent);

            Console.WriteLine("Datos cargados correctamente:");
            foreach (var Rep in Reps)
            {
                Console.WriteLine($"ID: {Rep.ID}, Repuesto: {Rep.Repuesto}, Detalles: {Rep.Detalles}, Costo: {Rep.Costo}");
                ListaGlobal.Lista_Repuestos.Insertar(Rep.ID, Rep.Repuesto, Rep.Detalles, Rep.Costo);
            }

            MessageDialog successDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                "Archivo JSON cargado correctamente.");
            successDialog.Run();
            successDialog.Destroy();
        }
        catch (Exception ex)
        {
            MessageDialog errorDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Error,
                ButtonsType.Ok,
                $"Error al cargar el archivo JSON: {ex.Message}");
            errorDialog.Run();
            errorDialog.Destroy();
        }
    }

    public class User
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
    }

     public class Vehi
    {
        public int ID { get; set; }
        public int ID_Usuario { get; set; }
        public string Marca { get; set; }
        public int Modelo { get; set; }
        public string Placa { get; set; }
    }

    public class Rep
    {
        public int ID { get; set; }
        public string Repuesto { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }
    }
}
