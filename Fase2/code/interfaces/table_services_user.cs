using Gtk;
using code.structures.tree_binary;
using System;
using System.Collections.Generic;

namespace code.interfaces
{
    public class UserWindowTableServices : Window
    {
        private Button inOrdenButton;
        private Button preOrdenButton;
        private Button postOrdenButton;
        private TreeView treeViewRecorridos;
        private ListStore listStoreRecorridos;
        private List<Nodo_Servicio> inOrdenList;
        private List<Nodo_Servicio> preOrdenList;
        private List<Nodo_Servicio> postOrdenList;

        public UserWindowTableServices(List<Nodo_Servicio> inOrden, List<Nodo_Servicio> preOrden, List<Nodo_Servicio> postOrden)
            : base("Servicios")
        {
            inOrdenList = inOrden;
            preOrdenList = preOrden;
            postOrdenList = postOrden;

            SetDefaultSize(600, 400); // Ajustar el tamaño de la ventana
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            inOrdenButton = new Button("InOrden");
            inOrdenButton.Clicked += OnInOrdenButtonClicked;
            vbox.PackStart(inOrdenButton, false, false, 0);

            preOrdenButton = new Button("PreOrden");
            preOrdenButton.Clicked += OnPreOrdenButtonClicked;
            vbox.PackStart(preOrdenButton, false, false, 0);

            postOrdenButton = new Button("PostOrden");
            postOrdenButton.Clicked += OnPostOrdenButtonClicked;
            vbox.PackStart(postOrdenButton, false, false, 0);

            // Crear el TreeView para mostrar los servicios
            treeViewRecorridos = new TreeView();
            vbox.PackStart(treeViewRecorridos, true, true, 0);

            // Crear las columnas para el TreeView
            treeViewRecorridos.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeViewRecorridos.AppendColumn("Repuesto", new CellRendererText(), "text", 1);
            treeViewRecorridos.AppendColumn("Vehículo", new CellRendererText(), "text", 2);
            treeViewRecorridos.AppendColumn("Detalles", new CellRendererText(), "text", 3);
            treeViewRecorridos.AppendColumn("Costo", new CellRendererText(), "text", 4);

            // Crear el ListStore para almacenar los datos
            listStoreRecorridos = new ListStore(typeof(int), typeof(int), typeof(int), typeof(string), typeof(double));
            treeViewRecorridos.Model = listStoreRecorridos;

            Add(vbox);
            ShowAll();
        }

        private void OnInOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido(inOrdenList);
        }

        private void OnPreOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido(preOrdenList);
        }

        private void OnPostOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido(postOrdenList);
        }

        private void MostrarRecorrido(List<Nodo_Servicio> recorrido)
        {
            // Limpiar el ListStore antes de agregar nuevos datos
            listStoreRecorridos.Clear();

            // Agregar cada servicio al ListStore
            foreach (var servicio in recorrido)
            {
                listStoreRecorridos.AppendValues(servicio.Id, servicio.Id_Repuesto, servicio.Id_Vehiculo, servicio.Detalles, servicio.Costo);
            }
        }
    }
}