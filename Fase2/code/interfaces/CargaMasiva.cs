using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Gtk;
using code.data;

public class CargaMasivaGTK : Window
{
    private ComboBoxText comboTipo;
    private Label statusLabel;

    public CargaMasivaGTK() : base("Carga Masiva desde JSON")
    {
        SetDefaultSize(400, 250);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);
        
        // Selector de tipo de carga
        comboTipo = new ComboBoxText();
        comboTipo.AppendText("Vehículos");
        comboTipo.AppendText("Usuarios");
        comboTipo.AppendText("Repuestos");
        comboTipo.Active = 0;

        Button cargarButton = new Button("Cargar JSON");
        statusLabel = new Label("Seleccione una categoría y cargue un archivo...");

        // Suscribirse al evento Clicked
        cargarButton.Clicked += OnCargarClicked;

        vbox.PackStart(comboTipo, false, false, 5);
        vbox.PackStart(cargarButton, false, false, 5);
        vbox.PackStart(statusLabel, false, false, 5);

        Add(vbox);
        ShowAll();
    }

    private void OnCargarClicked(object sender, EventArgs e)
    {
        FileChooserDialog fileChooser = new FileChooserDialog("Seleccionar archivo JSON",
            this, FileChooserAction.Open,
            "Cancelar", ResponseType.Cancel,
            "Abrir", ResponseType.Accept);

        if (fileChooser.Run() == (int)ResponseType.Accept)
        {
            string filePath = fileChooser.Filename;
            fileChooser.Destroy();
            string tipoSeleccionado = comboTipo.ActiveText;
            CargarDesdeJSON(filePath, tipoSeleccionado);
        }
        else
        {
            fileChooser.Destroy();
        }
    }

    private void CargarDesdeJSON(string filePath, string tipo)
    {
        try
        {
            string jsonData = File.ReadAllText(filePath);
            JArray jsonArray = JArray.Parse(jsonData);

            switch (tipo)
            {
                case "Vehículos":
                    foreach (var item in jsonArray)
                    {
                        int id = (int)item["ID"];
                        int idUsuario = (int)item["ID_Usuario"];
                        string marca = (string)item["Marca"];
                        int modelo = (int)item["Modelo"];
                        string placa = (string)item["Placa"];
                        code.data.Variables.listaVehiculos.Insertar(id, idUsuario, marca, modelo, placa);
                
                    }
                    statusLabel.Text = $"Cargados {jsonArray.Count} vehículos.";
                    break;

                case "Usuarios":
                    foreach (var item in jsonArray)
                    {
                        int id = (int)item["ID"];
                        string nombres = (string)item["Nombres"];
                        string apellidos = (string)item["Apellidos"];
                        string correo = (string)item["Correo"];
                        int edad = (int)item["Edad"];
                        string contrasenia = (string)item["Contrasenia"];
                        code.data.Variables.listaUsuarios.Agregar(id, nombres, apellidos, correo, edad, contrasenia);
                        Console.WriteLine($"Usuario agregado: ID={id}, Nombres={nombres}, Apellidos={apellidos}, Correo={correo}, Edad={edad}, Contrasenia={contrasenia}");
                    }
                    statusLabel.Text = $"Cargados {jsonArray.Count} usuarios.";
                    break;

                case "Repuestos":
                    foreach (var item in jsonArray)
                    {
                        int id = (int)item["ID"];
                        string repuesto = (string)item["Repuesto"];
                        string detalles = (string)item["Detalles"];
                        double costo = (double)item["Costo"];
                        code.data.Variables.arbolRepuestos.Insertar(id, repuesto, detalles, costo);
                    }
                    statusLabel.Text = $"Cargados {jsonArray.Count} repuestos.";
                    break;

                default:
                    statusLabel.Text = "Tipo no reconocido.";
                    break;
            }
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Error al cargar JSON: " + ex.Message;
        }
    }

}