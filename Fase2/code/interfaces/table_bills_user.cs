using Gtk;
using code.structures.tree_b;
using System;
using System.Collections.Generic;

namespace code.interfaces
{
    public class UserWindowTableInvoices : Window
    {
        private TreeView treeViewFacturas;
        private ListStore listStoreFacturas;

        public UserWindowTableInvoices(List<Factura> listaFacturas) : base("Facturas")
        {
            SetDefaultSize(600, 400); // Ajustar el tamaño de la ventana
            SetPosition(WindowPosition.Center);

            // Crear un contenedor vertical
            Box vbox = new Box(Orientation.Vertical, 5);

            // Crear el TreeView para mostrar las facturas
            treeViewFacturas = new TreeView();
            vbox.PackStart(treeViewFacturas, true, true, 0);

            // Crear las columnas para el TreeView
            treeViewFacturas.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeViewFacturas.AppendColumn("Servicio", new CellRendererText(), "text", 1);
            treeViewFacturas.AppendColumn("Total", new CellRendererText(), "text", 2);

            // Crear el ListStore para almacenar los datos
            listStoreFacturas = new ListStore(typeof(int), typeof(int), typeof(double));
            treeViewFacturas.Model = listStoreFacturas;

            // Agregar las facturas al ListStore
            MostrarFacturas(listaFacturas);

            // Agregar el TreeView al contenedor principal
            Add(vbox);
            ShowAll();
        }

        private void MostrarFacturas(List<Factura> facturas)
        {
            // Limpiar el ListStore antes de agregar nuevos datos
            listStoreFacturas.Clear();

            // Agregar cada factura al ListStore
            foreach (var factura in facturas)
            {
                listStoreFacturas.AppendValues(factura.Id, factura.Id_Servicio, factura.Total);
            }
        }
    }
}