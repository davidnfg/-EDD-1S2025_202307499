using System;
using System.IO;
using Newtonsoft.Json;
using Gtk;
using AutoGestPro.Models;
using System.Collections.Generic;

namespace AutoGestPro
{
    public class CargaMasivaWindow : Window
    {
        private FileChooserButton fileChooser;
        private ComboBoxText entidadSelector;
        private Label statusLabel;

        private List<Usuario> usuariosList = new List<Usuario>();
        private List<Vehiculo> vehiculosList = new List<Vehiculo>();
        private List<Repuesto> repuestosList = new List<Repuesto>();

        public CargaMasivaWindow() : base("Carga Masiva")
        {
            SetDefaultSize(400, 200);
            SetPosition(WindowPosition.Center);

            VBox vbox = new VBox(false, 5);
            Label titleLabel = new Label("Seleccione el archivo JSON y la entidad");

            fileChooser = new FileChooserButton("Seleccionar Archivo", FileChooserAction.Open);
            entidadSelector = new ComboBoxText();
            entidadSelector.AppendText("Usuarios");
            entidadSelector.AppendText("Vehículos");
            entidadSelector.AppendText("Repuestos");
            Button cargarButton = new Button("Cargar Datos");
            statusLabel = new Label("");

            cargarButton.Clicked += OnCargaMasivaClicked;

            vbox.PackStart(titleLabel, false, false, 5);
            vbox.PackStart(fileChooser, false, false, 5);
            vbox.PackStart(entidadSelector, false, false, 5);
            vbox.PackStart(cargarButton, false, false, 5);
            vbox.PackStart(statusLabel, false, false, 5);

            Add(vbox);
            DeleteEvent += delegate { Destroy(); };
        }

        private void OnCargaMasivaClicked(object sender, EventArgs e)
        {
            string filePath = fileChooser.Filename;
            string entidad = entidadSelector.ActiveText;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(entidad))
            {
                statusLabel.Text = "Seleccione archivo y entidad.";
                return;
            }

            string jsonData = File.ReadAllText(filePath);

            switch (entidad)
            {
                case "Usuarios":
                    var usuarios = JsonConvert.DeserializeObject<Usuario[]>(jsonData);
                    foreach (var user in usuarios)
                    {
                        usuariosList.Add(user);
                        Console.WriteLine($"ID: {user.ID}, Nombres: {user.Nombres}, Apellidos: {user.Apellidos}, Correo: {user.Correo}");
                    }
                    statusLabel.Text = "Usuarios cargados correctamente.";
                    break;

                case "Vehículos":
                    var vehiculos = JsonConvert.DeserializeObject<Vehiculo[]>(jsonData);
                    foreach (var veh in vehiculos)
                    {
                        vehiculosList.Add(veh);
                        Console.WriteLine($"ID: {veh.ID}, Marca: {veh.Marca}, Modelo: {veh.Modelo}, Placa: {veh.Placa}");
                    }
                    statusLabel.Text = "Vehículos cargados correctamente.";
                    break;

                case "Repuestos":
                    var repuestos = JsonConvert.DeserializeObject<Repuesto[]>(jsonData);
                    foreach (var rep in repuestos)
                    {
                        repuestosList.Add(rep);
                        Console.WriteLine($"ID: {rep.ID}, Repuesto: {rep.RepuestoNombre}, Detalles: {rep.Detalles}, Costo: {rep.Costo}");
                    }
                    statusLabel.Text = "Repuestos cargados correctamente.";
                    break;

                default:
                    statusLabel.Text = "Entidad no válida.";
                    break;
            }
        }
    }
}