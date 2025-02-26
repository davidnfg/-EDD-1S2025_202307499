using System;
using Gtk;
using ListaDobleUnsafe;

public class GenerarServicio : Window
{
    private Entry entryId, entryIdRepuesto, entryIdVehiculo, entryDetalles, entryCostoS;
    private Button btnGuardar;
    private ListaDEVehiculos listaVehiculos;
    private ListaDEservicios listaServicios;
    private ListaDERep listaRepuestos;
    private PilaFacturas pilaFacturas; // NUEVO: Agregamos la pila de facturas

    public GenerarServicio(ListaDEVehiculos vehiculos, ListaDERep repuestos, ListaDEservicios servicios, PilaFacturas facturas) : base("Crear Servicio")
    {
        listaVehiculos = vehiculos;
        listaRepuestos = repuestos;
        listaServicios = servicios;
        pilaFacturas = facturas; // Asignamos la pila

        SetDefaultSize(300, 250);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);

        vbox.PackStart(new Label("ID"), false, false, 0);
        entryId = new Entry();
        vbox.PackStart(entryId, false, false, 0);

        vbox.PackStart(new Label("Id_Repuesto"), false, false, 0);
        entryIdRepuesto = new Entry();
        vbox.PackStart(entryIdRepuesto, false, false, 0);

        vbox.PackStart(new Label("Id_Vehiculo"), false, false, 0);
        entryIdVehiculo = new Entry();
        vbox.PackStart(entryIdVehiculo, false, false, 0);

        vbox.PackStart(new Label("Detalles"), false, false, 0);
        entryDetalles = new Entry();
        vbox.PackStart(entryDetalles, false, false, 0);

        vbox.PackStart(new Label("Costo Servicio"), false, false, 0);
        entryCostoS = new Entry();
        vbox.PackStart(entryCostoS, false, false, 0);

        btnGuardar = new Button("Guardar");
        btnGuardar.Clicked += OnGuardarClicked;
        vbox.PackStart(btnGuardar, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    private void OnGuardarClicked(object sender, EventArgs e)
    {
        int id, idRepuesto, idVehiculo;
        double costoS;
        
        if (!int.TryParse(entryId.Text, out id) ||
            !int.TryParse(entryIdRepuesto.Text, out idRepuesto) ||
            !int.TryParse(entryIdVehiculo.Text, out idVehiculo) ||
            !double.TryParse(entryCostoS.Text, out costoS))
        {
            ShowError("Los valores ingresados no son válidos.");
            return;
        }

        if (!ExisteVehiculo(idVehiculo))
        {
            ShowError("El vehículo no existe en la base de datos.");
            return;
        }
        
        if (!ExisteRepuesto(idRepuesto))
        {
            ShowError("El repuesto no existe en la base de datos.");
            return;
        }

        // Calcular costo total
        double costoRepuesto = ObtenerCostoRepuesto(idRepuesto);
        double costoTotal = costoS + costoRepuesto;

        // Insertar servicio en la lista
        listaServicios.Insertar(id, idRepuesto, idVehiculo, entryDetalles.Text, costoS);

        // Insertar factura en la pila
        pilaFacturas.Push(id, id, costoTotal);

        Console.WriteLine($"Servicio generado: ID={id}, Vehículo={idVehiculo}, Repuesto={idRepuesto}, Costo={costoS}");
        Console.WriteLine($"Factura generada: ID_Factura={id}, ID_Orden={id}, Costo Total=Q{costoTotal}");

        MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Servicio y Factura guardados correctamente");
        dialog.Run();
        dialog.Destroy();
    }

    private bool ExisteVehiculo(int idVehiculo)
    {
        unsafe
        {
            NodeVehi* actual = listaVehiculos.GetHead();
            while (actual != null)
            {
                if (actual->ID == idVehiculo) return true;
                actual = actual->Next;
            }
            return false;
        }
    }

    private bool ExisteRepuesto(int idRepuesto)
    {
        unsafe
        {
            NodeRep* actual = listaRepuestos.GetHead();
            while (actual != null)
            {
                if (actual->ID == idRepuesto) return true;
                actual = actual->Next;
            }
            return false;
        }
    }

    private double ObtenerCostoRepuesto(int idRepuesto)
    {
        unsafe
        {
            NodeRep* actual = listaRepuestos.GetHead();
            while (actual != null)
            {
                if (actual->ID == idRepuesto) return actual->Costo;
                actual = actual->Next;
            }
            return 0.0;
        }
    }

    private void ShowError(string message)
    {
        MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
        dialog.Run();
        dialog.Destroy();
    }
}
