using Gtk;
using System;
using code.data;

public class ActualizarRepuestos : Window
{
    private Entry idEntry;
    private Entry repuestoEntry;
    private Entry detallesEntry;
    private Entry costoEntry;

    public ActualizarRepuestos() : base("Actualización de Repuestos")
    {
        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);

        var vbox = new VBox(false, 5);

        var idLabel = new Label("Id");
        idEntry = new Entry();

        var repuestoLabel = new Label("Repuesto");
        repuestoEntry = new Entry();

        var detallesLabel = new Label("Detalles");
        detallesEntry = new Entry();

        var costoLabel = new Label("Costo");
        costoEntry = new Entry();

        var buscarButton = new Button("Buscar");
        var actualizarButton = new Button("Actualizar");

        buscarButton.Clicked += OnBuscarClicked;
        actualizarButton.Clicked += OnActualizarClicked;

        vbox.PackStart(idLabel, false, false, 0);
        vbox.PackStart(idEntry, false, false, 0);
        vbox.PackStart(repuestoLabel, false, false, 0);
        vbox.PackStart(repuestoEntry, false, false, 0);
        vbox.PackStart(detallesLabel, false, false, 0);
        vbox.PackStart(detallesEntry, false, false, 0);
        vbox.PackStart(costoLabel, false, false, 0);
        vbox.PackStart(costoEntry, false, false, 0);
        vbox.PackStart(buscarButton, false, false, 0);
        vbox.PackStart(actualizarButton, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    void OnBuscarClicked(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse(idEntry.Text, out id))
        {
            var repuesto = Variables.arbolRepuestos.Buscar(id);
            if (repuesto != null)
            {
                repuestoEntry.Text = repuesto.Repuesto;
                detallesEntry.Text = repuesto.Detalles;
                costoEntry.Text = repuesto.Costo.ToString();
            }
            else
            {
                MostrarMensajeError("Repuesto no encontrado");
            }
        }
        else
        {
            MostrarMensajeError("ID inválido");
        }
    }

    void OnActualizarClicked(object sender, EventArgs e)
    {
        int id;
        double costo;
        if (int.TryParse(idEntry.Text, out id) && double.TryParse(costoEntry.Text, out costo))
        {
            bool actualizado = Variables.arbolRepuestos.Actualizar(id, repuestoEntry.Text, detallesEntry.Text, costo);
            if (actualizado)
            {
                MostrarMensaje("Repuesto actualizado correctamente");
            }
            else
            {
                MostrarMensajeError("Repuesto no encontrado");
            }
        }
        else
        {
            MostrarMensajeError("Datos inválidos");
        }
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