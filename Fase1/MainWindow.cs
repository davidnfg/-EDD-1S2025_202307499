using Gtk;
using ListaDobleUnsafe;

public class Menu : Window
{
    public Menu() : base("Fase 1")
    {
        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        ModifyBg(StateType.Normal, new Gdk.Color(200, 200, 200));

        VBox vbox = new VBox(false, 10);
        vbox.BorderWidth = 20;

        Label titleLabel = new Label("<b><span foreground='black' size='15000'>FASE 1</span></b>");
        titleLabel.UseMarkup = true;
        vbox.PackStart(titleLabel, false, false, 5);

        Frame menuFrame = new Frame("Menu");
        VBox menuVBox = new VBox(false, 5);
        menuFrame.Add(menuVBox);
        vbox.PackStart(menuFrame, false, false, 5);

        Button Btn_CargaMasiva = new Button("Cargas Masivas");
        Btn_CargaMasiva.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        Btn_CargaMasiva.Clicked += GoCargaMasiva;
        menuVBox.PackStart(Btn_CargaMasiva, false, false, 5);

        Button Btn_IngresoManual = new Button("Ingreso Individual");
        Btn_IngresoManual.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        Btn_IngresoManual.Clicked += GoIngresoManual;
        menuVBox.PackStart(Btn_IngresoManual, false, false, 5);

        Button Btn_GestionUsuarios = new Button("Gestión de Usuarios");
        Btn_GestionUsuarios.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        Btn_GestionUsuarios.Clicked += GoGestionUsuarios;
        menuVBox.PackStart(Btn_GestionUsuarios, false, false, 5);

        Button Btn_GenerarServicio = new Button("Generar Servicio");
        Btn_GenerarServicio.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        Btn_GenerarServicio.Clicked += GoGenerarServicio;
        menuVBox.PackStart(Btn_GenerarServicio, false, false, 5);

        Button Btn_CancelarFactura = new Button("Cancelar Factura");
        Btn_CancelarFactura.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        Btn_CancelarFactura.Clicked += GoFactura;
        menuVBox.PackStart(Btn_CancelarFactura, false, false, 5);

        Button Btn_GenerarReporte = new Button("Generar Reporte");
        Btn_GenerarReporte.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        menuVBox.PackStart(Btn_GenerarReporte, false, false, 5);

        Add(vbox);
    }

    private void GoCargaMasiva(object sender, EventArgs e)
    {
        CargaMasiva cargaMasiva = new CargaMasiva();
        cargaMasiva.ShowAll();
    }

    private void GoGenerarServicio(object sender, EventArgs e)
    {
        GenerarServicio generarServicio = new GenerarServicio(ListaGlobal.Lista_Vehiculos, ListaGlobal.Lista_Repuestos, ListaGlobal.Lista_Servicios , ListaGlobal.Pila_Facturas);
        generarServicio.ShowAll();
    }

    private void GoGestionUsuarios(object sender, EventArgs e)
    {
        GestionUsuarios gestionUsuarios = new GestionUsuarios();
        gestionUsuarios.ShowAll();
    }

    private void GoFactura(object sender, EventArgs e)
    {
        Factura factura = new Factura(ListaGlobal.Pila_Facturas);
        factura.ShowAll();
    }
    private void GoIngresoManual(object sender, EventArgs e)
    {   
        IngresoIndividual ingresoIndividual = new IngresoIndividual();
        ingresoIndividual.ShowAll();
        ListaGlobal.Lista_Usuarios.Mostrar();
        ListaGlobal.Lista_Vehiculos.Mostrar();
        ListaGlobal.Lista_Repuestos.Mostrar();
        ListaGlobal.Lista_Servicios.Mostrar();
        ListaGlobal.Pila_Facturas.Mostrar();

    }
}
