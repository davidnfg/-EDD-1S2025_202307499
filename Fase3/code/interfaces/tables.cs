using Gtk;
using code.structures.tree_avl;
using System;

namespace code.interfaces
{
    public class RootWindowTables : Window
    {
        private Button inOrdenButton;
        private Button preOrdenButton;
        private Button postOrdenButton;
        private TreeView treeViewRecorridos;
        private ListStore listStore;

        public RootWindowTables() : base("Recorridos Árbol AVL")
        {
            SetDefaultSize(600, 400); // Aumentar el tamaño de la ventana
            SetPosition(WindowPosition.Center);

            // Crear una caja vertical para organizar los widgets
            Box vbox = new Box(Orientation.Vertical, 5);

            // Crear los botones para seleccionar el tipo de recorrido
            inOrdenButton = new Button("InOrden");
            inOrdenButton.Clicked += OnInOrdenButtonClicked;
            vbox.PackStart(inOrdenButton, false, false, 0);

            preOrdenButton = new Button("PreOrden");
            preOrdenButton.Clicked += OnPreOrdenButtonClicked;
            vbox.PackStart(preOrdenButton, false, false, 0);

            postOrdenButton = new Button("PostOrden");
            postOrdenButton.Clicked += OnPostOrdenButtonClicked;
            vbox.PackStart(postOrdenButton, false, false, 0);

            // Crear el TreeView para mostrar los resultados
            treeViewRecorridos = new TreeView();
            vbox.PackStart(treeViewRecorridos, true, true, 0);

            // Crear las columnas para el TreeView
            treeViewRecorridos.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeViewRecorridos.AppendColumn("Repuesto", new CellRendererText(), "text", 1);
            treeViewRecorridos.AppendColumn("Detalles", new CellRendererText(), "text", 2);
            treeViewRecorridos.AppendColumn("Costo", new CellRendererText(), "text", 3);

            // Crear el ListStore para almacenar los datos
            listStore = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string));
            treeViewRecorridos.Model = listStore;

            // Agregar la caja al contenedor principal
            Add(vbox);
            ShowAll();
        }

        // Acción para el botón InOrden
        private void OnInOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido("InOrden");
        }

        // Acción para el botón PreOrden
        private void OnPreOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido("PreOrden");
        }

        // Acción para el botón PostOrden
        private void OnPostOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido("PostOrden");
        }

        // Método para mostrar el recorrido seleccionado
        private void MostrarRecorrido(string tipoRecorrido)
        {
            // Limpiar el ListStore antes de agregar nuevos datos
            listStore.Clear();

            // Obtener el árbol de repuestos de las variables
            var arbol = code.data.Variables.arbolRepuestos;

            Nodo_Repuesto[] recorrido = null;

            // Seleccionar el tipo de recorrido
            switch (tipoRecorrido)
            {
                case "InOrden":
                    recorrido = arbol.TablaInOrden();
                    break;

                case "PreOrden":
                    recorrido = arbol.TablaPreOrden();
                    break;

                case "PostOrden":
                    recorrido = arbol.TablaPostOrden();
                    break;
            }

            // Agregar los datos al ListStore
            foreach (var nodo in recorrido)
            {
                listStore.AppendValues(nodo.Id, nodo.Repuesto, nodo.Detalles, nodo.Costo.ToString("0.##"));
            }
        }
    }
}