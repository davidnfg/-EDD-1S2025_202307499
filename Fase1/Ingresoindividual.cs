using Gtk;
using ListaDobleUnsafe;

public class IngresoIndividual : Window
{
    private ListaDoblementeEnlazada listaUsuarios = ListaGlobal.Lista_Usuarios;
    private ListaDEVehiculos listaVehiculos = ListaGlobal.Lista_Vehiculos;
    private ListaDERep listaRepuestos = ListaGlobal.Lista_Repuestos;

    public IngresoIndividual() : base("Ingreso Individual")
    {
        SetDefaultSize(350, 300);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);
        
        Label titleLabel = new Label("<b>Seleccione el tipo de ingreso</b>");
        titleLabel.UseMarkup = true;
        vbox.PackStart(titleLabel, false, false, 5);

        ComboBoxText comboBox = new ComboBoxText();
        comboBox.AppendText("Usuario");
        comboBox.AppendText("Vehículo");
        comboBox.AppendText("Repuesto");
        comboBox.Active = 0;
        vbox.PackStart(comboBox, false, false, 5);

        Button btnContinuar = new Button("Continuar");
        btnContinuar.Clicked += (sender, e) => OnSeleccionado(comboBox.ActiveText);
        vbox.PackStart(btnContinuar, false, false, 5);
        
        Add(vbox);
    }

    private void OnSeleccionado(string seleccion)
    {
        if (seleccion == "Usuario")
        {
            IngresoUsuario ingresoUsuario = new IngresoUsuario(listaUsuarios);
            ingresoUsuario.ShowAll();
        }
        else if (seleccion == "Vehículo")
        {
            IngresoVehiculo ingresoVehiculo = new IngresoVehiculo(listaVehiculos);
            ingresoVehiculo.ShowAll();
        }
        else if (seleccion == "Repuesto")
        {
            IngresoRepuesto ingresoRepuesto = new IngresoRepuesto(listaRepuestos);
            ingresoRepuesto.ShowAll();
        }
    }
}

