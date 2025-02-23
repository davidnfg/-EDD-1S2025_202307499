using Gtk;
using System;
using System.IO;
using Newtonsoft.Json;  //Para instalar esto se utiliza el comando "dotnet add package Newtonsoft.Json"

public class CargaMasiva : Window
{
    public CargaMasiva() : base("Carga Masiva")
    {
        SetDefaultSize(400, 150);
        SetPosition(WindowPosition.Center);

        // Crear un contenedor para los elementos
        VBox vbox = new VBox(false, 5);

        // Label
        Label label = new Label("Seleccione un archivo JSON para cargar:");
        vbox.PackStart(label, false, false, 0);

        // Botón para cargar archivo JSON
        Button btnCargarArchivo = new Button("Cargar Archivo JSON");
        btnCargarArchivo.Clicked += OnCargarArchivoClicked;
        vbox.PackStart(btnCargarArchivo, false, false, 0);

        Add(vbox);
    }

    private void OnCargarArchivoClicked(object sender, EventArgs e)
    {
        // Crear un diálogo para seleccionar archivo
        FileChooserDialog fileChooser = new FileChooserDialog(
            "Seleccione un archivo JSON",
            this,
            FileChooserAction.Open,
            "Cancelar", ResponseType.Cancel,
            "Abrir", ResponseType.Accept);

        // Filtrar solo archivos JSON
        FileFilter filter = new FileFilter();
        filter.Name = "Archivos JSON";
        filter.AddPattern("*.json");
        fileChooser.AddFilter(filter);

        // Si el usuario selecciona un archivo
        if (fileChooser.Run() == (int)ResponseType.Accept)
        {
            string filePath = fileChooser.Filename;
            CargarJSON(filePath);
        }

        fileChooser.Destroy();
    }

    private void CargarJSON(string filePath)
{
    try
    {
        // Leer el contenido del archivo JSON
        string jsonContent = File.ReadAllText(filePath);

        // Deserializar el JSON a una lista de objetos
        var locales = JsonConvert.DeserializeObject<Local[]>(jsonContent);

        // Mostrar los datos en consola (o procesarlos como necesites)
        Console.WriteLine("Datos cargados correctamente:");
        foreach (var local in locales)
        {
            Console.WriteLine($"ID: {local.ID}, Nombres: {local.Nombres}, Apellidos: {local.Apellidos}, Correo: {local.Correo}, Contrasenia: {local.Contrasenia}");
            ListaGlobal.Lista_Usuarios.Insertar(local.ID, local.Nombres, local.Apellidos, local.Correo, local.Contrasenia);
        }

        // Mostrar mensaje de éxito
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
        // Mostrar mensaje de error si algo falla
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

    // Clase para representar la estructura del JSON
    public class Local
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
    }
}