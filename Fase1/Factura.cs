using System;
using Gtk;
using ListaDobleUnsafe;

public class Factura : Window
{
    private Label lblId, lblIdOrden, lblTotal;
    private Button btnCancelar;
    private PilaFacturas pilaFacturas;

    public Factura(PilaFacturas pila) : base("Facturación")
    {
        pilaFacturas = pila;
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        
        VBox vbox = new VBox(false, 5);
        vbox.PackStart(new Label("Facturación"), false, false, 0);
        
        lblId = new Label("Id: ");
        vbox.PackStart(lblId, false, false, 0);
        
        lblIdOrden = new Label("Id_Orden: ");
        vbox.PackStart(lblIdOrden, false, false, 0);
        
        lblTotal = new Label("Total: ");
        vbox.PackStart(lblTotal, false, false, 0);
        
        btnCancelar = new Button("Cancelar Factura");
        btnCancelar.Clicked += OnCancelarFactura;
        vbox.PackStart(btnCancelar, false, false, 0);
        
        Add(vbox);
        MostrarFactura();
        ShowAll();
    }

    private unsafe void MostrarFactura()
    {
        NodoFac* factura = pilaFacturas.Peek();
        if (factura != null)
        {
            lblId.Text = $"Id: {factura->ID}";
            lblIdOrden.Text = $"Id_Orden: {factura->ID_orden}";
            lblTotal.Text = $"Total: Q{factura->CostoTotal}";
        }
        else
        {
            lblId.Text = "No hay facturas";
            lblIdOrden.Text = "";
            lblTotal.Text = "";
        }
    }

    private unsafe void OnCancelarFactura(object sender, EventArgs e)
    {
        pilaFacturas.Pop();
        MostrarFactura();
    }
}

