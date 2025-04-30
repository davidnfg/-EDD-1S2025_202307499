using Gtk;
using System;
using code.structures.double_list;

namespace code.interfaces
{
    public class VisualizacionVehiculos : Window
    {
        private TreeView treeView;
        private ListStore vehiculosStore;
        private int usuarioId; // Variable de instancia para almacenar el ID del usuario

        public VisualizacionVehiculos(int idUsuario) : base("Visualización de Vehículos")
        {
            usuarioId = idUsuario; // Almacenar el ID del usuario
            SetDefaultSize(800, 400);
            SetPosition(WindowPosition.Center);
            BorderWidth = 10;

            // Crear el contenedor principal
            VBox vbox = new VBox(false, 5);

            // Crear el TreeView
            CreateVehiculosTreeView();

            // Crear el ScrolledWindow para el TreeView
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.SetSizeRequest(-1, 300);
            scrolledWindow.Add(treeView);

            // Botón de actualizar
            Button btnActualizar = new Button("Actualizar");
            btnActualizar.Clicked += OnActualizarClicked;

            // Agregar controles al contenedor
            vbox.PackStart(new Label("Lista de Vehículos Registrados"), false, false, 0);
            vbox.PackStart(scrolledWindow, true, true, 0);
            vbox.PackStart(btnActualizar, false, false, 0);

            Add(vbox);

            // Cargar datos iniciales
            CargarDatosVehiculos(usuarioId);

            ShowAll();
        }

        private void CargarDatosVehiculos(int idUsuario)
        {
            // Limpiar datos existentes
            vehiculosStore.Clear();

            // Obtener los vehículos asociados al usuario actual
            var listaVehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);

            // Agregar los vehículos al TreeView
            foreach (var idVehiculo in listaVehiculos)
            {
                var vehiculo = code.data.Variables.listaVehiculos.Buscar(idVehiculo);
                if (vehiculo != null)
                {
                    AgregarVehiculo(vehiculo.Id, vehiculo.IdUsuario, vehiculo.Marca, vehiculo.Modelo.ToString(), vehiculo.Placa);
                }
            }
        }

        private void CreateVehiculosTreeView()
        {
            // Crear el TreeView
            treeView = new TreeView();

            // Crear las columnas para el TreeView
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("ID Usuario", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Marca", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Modelo", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Placa", new CellRendererText(), "text", 4);

            // Crear el ListStore para almacenar los datos
            vehiculosStore = new ListStore(typeof(int), typeof(int), typeof(string), typeof(string), typeof(string));
            treeView.Model = vehiculosStore;
        }

        private void AgregarVehiculo(int id, int idUsuario, string marca, string modelo, string placa)
        {
            vehiculosStore.AppendValues(id, idUsuario, marca, modelo, placa);
        }

        private void OnActualizarClicked(object sender, EventArgs e)
        {
            CargarDatosVehiculos(usuarioId); // Usar la variable de instancia
        }

        public static void Mostrar(int idUsuario)
        {
            Application.Init();
            new VisualizacionVehiculos(idUsuario); // Pasar el ID del usuario directamente
            Application.Run();
        }
    }
}