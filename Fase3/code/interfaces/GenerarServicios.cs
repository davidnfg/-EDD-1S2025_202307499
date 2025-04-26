using Gtk;
using System;
using code.data;
using code.structures.tree_binary;
using code.structures.merkle;

namespace code.interfaces
{
    public class GenerarServicios : Window
    {
        private Entry idEntry;
        private Entry idRepuestoEntry;
        private Entry idVehiculoEntry;
        private Entry detallesEntry;
        private Entry costoEntry;
        private ListStore metodosPagoStore;
        private TreeView metodosPagoView;
        private string metodoPagoSeleccionado;

        // Variable estática para llevar el control del ID de la factura
        private static int idFacturaActual = 1;

        public GenerarServicios() : base("Crear Servicio")
        {
            SetDefaultSize(400, 400); // Aumentamos el tamaño para acomodar el ListBox
            SetPosition(WindowPosition.Center);

            var vbox = new VBox(false, 5);

            var idLabel = new Label("ID");
            idEntry = new Entry();

            var idRepuestoLabel = new Label("Id_Repuesto");
            idRepuestoEntry = new Entry();

            var idVehiculoLabel = new Label("Id_Vehículo");
            idVehiculoEntry = new Entry();

            var detallesLabel = new Label("Detalles");
            detallesEntry = new Entry();

            var costoLabel = new Label("Costo Servicio");
            costoEntry = new Entry();

            // Crear ListBox para métodos de pago
            var metodosPagoLabel = new Label("Método de Pago:");
            
            // Crear modelo para el TreeView
            metodosPagoStore = new ListStore(typeof(string));
            
            // Añadir métodos de pago
            metodosPagoStore.AppendValues("Efectivo");
            metodosPagoStore.AppendValues("Tarjeta de Débito");
            metodosPagoStore.AppendValues("Tarjeta de Crédito");
            
            // Crear TreeView
            metodosPagoView = new TreeView(metodosPagoStore);
            var cell = new CellRendererText();
            var column = new TreeViewColumn("Métodos de Pago", cell, "text", 0);
            metodosPagoView.AppendColumn(column);
            
            // Configurar selección única
            metodosPagoView.Selection.Mode = SelectionMode.Single;
            metodosPagoView.Selection.Changed += OnMetodoPagoSeleccionado;

            // Crear contenedor con scroll para el TreeView
            var scrolledWindow = new ScrolledWindow();
            scrolledWindow.SetSizeRequest(-1, 100);
            scrolledWindow.Add(metodosPagoView);

            var guardarButton = new Button("Guardar");
            guardarButton.Clicked += OnGuardarClicked;

            vbox.PackStart(idLabel, false, false, 0);
            vbox.PackStart(idEntry, false, false, 0);
            vbox.PackStart(idRepuestoLabel, false, false, 0);
            vbox.PackStart(idRepuestoEntry, false, false, 0);
            vbox.PackStart(idVehiculoLabel, false, false, 0);
            vbox.PackStart(idVehiculoEntry, false, false, 0);
            vbox.PackStart(detallesLabel, false, false, 0);
            vbox.PackStart(detallesEntry, false, false, 0);
            vbox.PackStart(costoLabel, false, false, 0);
            vbox.PackStart(costoEntry, false, false, 0);
            vbox.PackStart(metodosPagoLabel, false, false, 0);
            vbox.PackStart(scrolledWindow, true, true, 0);
            vbox.PackStart(guardarButton, false, false, 0);

            Add(vbox);
            ShowAll();
        }

       private void OnMetodoPagoSeleccionado(object sender, EventArgs e)
        {
            var selection = metodosPagoView.Selection;
            if (selection.CountSelectedRows() > 0)
            {
                TreePath[] paths = selection.GetSelectedRows();
                var model = metodosPagoView.Model;
                
                if (model.GetIter(out TreeIter iter, paths[0]))
                {
                    metodoPagoSeleccionado = model.GetValue(iter, 0).ToString();
                    Console.WriteLine($"Método de pago seleccionado: {metodoPagoSeleccionado}");
                }
            }
            else
            {
                metodoPagoSeleccionado = null;
            }
        }


        void OnGuardarClicked(object sender, EventArgs e)
        {
            int id, idRepuesto, idVehiculo;
            double costoServicio;

            if (!int.TryParse(idEntry.Text, out id) ||
                !int.TryParse(idRepuestoEntry.Text, out idRepuesto) ||
                !int.TryParse(idVehiculoEntry.Text, out idVehiculo) ||
                !double.TryParse(costoEntry.Text, out costoServicio))
            {
                MostrarMensajeError("Datos inválidos");
                return;
            }

            // Verificar que se haya seleccionado un método de pago
            if (string.IsNullOrEmpty(metodoPagoSeleccionado))
            {
                MostrarMensajeError("Por favor seleccione un método de pago");
                return;
            }

            // Verificar si el ID del servicio ya existe
            if (Variables.arbolServicios.Buscar(id) != null)
            {
                MostrarMensajeError("El ID del servicio ya existe. Por favor, use un ID diferente.");
                return;
            }

            var repuesto = Variables.arbolRepuestos.Buscar(idRepuesto);
            var vehiculo = Variables.listaVehiculos.Buscar(idVehiculo);

            if (repuesto == null)
            {
                MostrarMensajeError("Repuesto no encontrado");
                return;
            }

            if (vehiculo == null)
            {
                MostrarMensajeError("Vehículo no encontrado");
                return;
            }

            // Crear el servicio
            Variables.arbolServicios.Insertar(id, idRepuesto, idVehiculo, detallesEntry.Text, costoServicio, metodoPagoSeleccionado);
            Console.WriteLine($"Servicio generado: ID={id}, Id_Repuesto={idRepuesto}, Id_Vehículo={idVehiculo}, Detalles={detallesEntry.Text}, Costo Servicio={costoServicio}");

            // Calcular el total
            double total = costoServicio + repuesto.Costo;

            // Generar la factura
            int idFactura = GenerarIdFactura();
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Variables.arbolFacturas.Insert(idFactura, id, total, fecha, metodoPagoSeleccionado);
            Console.WriteLine($"Factura generada: ID={idFactura}, Id_Servicio={id}, Total={total}, Método Pago={metodoPagoSeleccionado}");

            MostrarMensaje($"Servicio y factura generados correctamente.\nTotal: {total:C}\nMétodo de pago: {metodoPagoSeleccionado}");
        }

        private int GenerarIdFactura()
        {
            // Incrementar el ID de la factura
            return idFacturaActual++;
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