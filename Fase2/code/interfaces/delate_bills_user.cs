using Gtk;
using code.structures.tree_b;
using System;
using System.Collections.Generic;
using System.Linq;

namespace code.interfaces
{
    public class UserWindowDeleteInvoice : Window
    {
        private Entry entryId;
        private Button searchButton;
        private Button deleteButton;
        private TreeView treeViewFacturas;
        private ListStore listStoreFacturas;

        public UserWindowDeleteInvoice() : base("Eliminar Factura")
        {
            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            entryId = new Entry();
            entryId.PlaceholderText = "Ingrese ID de la factura";
            vbox.PackStart(entryId, false, false, 5);

            searchButton = new Button("Buscar");
            searchButton.Clicked += OnSearchButtonClicked;
            vbox.PackStart(searchButton, false, false, 5);

            deleteButton = new Button("Eliminar");
            deleteButton.Clicked += OnDeleteButtonClicked;
            deleteButton.Sensitive = false;
            vbox.PackStart(deleteButton, false, false, 5);

            // Crear el TreeView para mostrar las facturas
            treeViewFacturas = new TreeView();
            treeViewFacturas.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeViewFacturas.AppendColumn("Servicio", new CellRendererText(), "text", 1);
            treeViewFacturas.AppendColumn("Total", new CellRendererText(), "text", 2);

            listStoreFacturas = new ListStore(typeof(int), typeof(int), typeof(double));
            treeViewFacturas.Model = listStoreFacturas;

            vbox.PackStart(treeViewFacturas, true, true, 5);

            Add(vbox);
            ShowAll();
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            if (int.TryParse(entryId.Text, out int id))
            {
                Factura factura = code.data.Variables.arbolFacturas.Buscar(id);

                if (factura != null)
                {
                    int idUsuario = code.data.Variables.usuarioActual.Id;
                    List<int> List_Ids_vehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);
                    List<int> Lista_Ids_Servicios = code.data.Variables.arbolServicios.Servicios_Vehiculos(List_Ids_vehiculos);
                    List<Factura> Lista_Facturas_Usuario = code.data.Variables.arbolFacturas.ObtenerFacturasPorServicios(Lista_Ids_Servicios);
                    List<int> Ids_Facturas_Usuario = Lista_Facturas_Usuario.Select(f => f.Id).ToList();

                    if (Ids_Facturas_Usuario.Contains(id))
                    {
                        MostrarFacturaEnTabla(factura);
                        deleteButton.Sensitive = true;
                    }
                    else
                    {
                        MostrarMensajeError("No tienes ninguna factura con este ID.");
                        deleteButton.Sensitive = false;
                    }
                }
                else
                {
                    MostrarMensajeError("Factura no encontrada.");
                    deleteButton.Sensitive = false;
                }
            }
            else
            {
                MostrarMensajeError("Por favor, ingrese un ID válido.");
                deleteButton.Sensitive = false;
            }
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (int.TryParse(entryId.Text, out int id))
            {
                int idUsuario = code.data.Variables.usuarioActual.Id;
                List<int> List_Ids_vehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);
                List<int> Lista_Ids_Servicios = code.data.Variables.arbolServicios.Servicios_Vehiculos(List_Ids_vehiculos);
                List<Factura> Lista_Facturas_Usuario = code.data.Variables.arbolFacturas.ObtenerFacturasPorServicios(Lista_Ids_Servicios);

                List<int> Ids_Facturas_Usuario = Lista_Facturas_Usuario.Select(f => f.Id).ToList();

                if (Ids_Facturas_Usuario.Contains(id))
                {
                    code.data.Variables.arbolFacturas.Eliminar(id);
                    MostrarMensaje("Factura eliminada correctamente.");
                    deleteButton.Sensitive = false;
                    entryId.Text = "";
                    listStoreFacturas.Clear();
                }
                else
                {
                    MostrarMensajeError("Error: No puedes eliminar esta factura porque no te pertenece.");
                }
            }
            else
            {
                MostrarMensajeError("Por favor, ingrese un ID válido.");
            }
        }

        private void MostrarFacturaEnTabla(Factura factura)
        {
            listStoreFacturas.Clear();
            listStoreFacturas.AppendValues(factura.Id, factura.Id_Servicio, factura.Total);
        }

        private void MostrarMensajeError(string mensaje)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, mensaje);
            md.Run();
            md.Destroy();
        }

        private void MostrarMensaje(string mensaje)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, mensaje);
            md.Run();
            md.Destroy();
        }
    }
}