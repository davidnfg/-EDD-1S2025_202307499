using Gtk;

namespace code.interfaces
{
    public class RootWindow : Window
    {
        private Button cargaMasivaButton;
        private Button Entidades;
        private Button ActualizarV;
        private Button GenerarS;
        private Button reportesButton;
        private Button Vrepuestos;
        private Button cerrarSesionButton;

        public RootWindow() : base("Root Window")
        {
            SetDefaultSize(300, 250);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            cargaMasivaButton = new Button("Carga Masiva");
            cargaMasivaButton.Clicked += OnCargaMasivaButtonClicked;
            vbox.PackStart(cargaMasivaButton, false, false, 0);

            Entidades = new Button("Gestion de Entidades");
            Entidades.Clicked += OnEntidadesButtonClicked;
            vbox.PackStart(Entidades, false, false, 0);

            ActualizarV = new Button("Actualizacion de Repuestos");
            ActualizarV.Clicked += OnActualizarVButtonClicked;
            vbox.PackStart(ActualizarV, false, false, 0);

            GenerarS = new Button("Generar servicios");
            GenerarS.Clicked += OnGenerarSButtonClicked;
            vbox.PackStart(GenerarS, false, false, 0);

            Vrepuestos = new Button("Visualizar repuestos");
            Vrepuestos.Clicked += OnVrepuestosButtonClicked;
            vbox.PackStart(Vrepuestos, false, false, 0);

            reportesButton = new Button("Reportes");
            reportesButton.Clicked += OnReportesButtonClicked;
            vbox.PackStart(reportesButton, false, false, 0);

            cerrarSesionButton = new Button("Cerrar Sesión");
            cerrarSesionButton.Clicked += OnCerrarSesionButtonClicked;
            vbox.PackStart(cerrarSesionButton, false, false, 0);

            Add(vbox);
            ShowAll();
        }

        private void OnCargaMasivaButtonClicked(object sender, EventArgs e)
        {
                CargaMasivaGTK cargaMasivaWindow = new CargaMasivaGTK();
                cargaMasivaWindow.Show();

        }

        private void OnEntidadesButtonClicked(object sender, EventArgs e)
        {
            GestionEntidades gestionEntidades = new GestionEntidades();
            gestionEntidades.Show();

        }

        private void OnActualizarVButtonClicked(object sender, EventArgs e)
        {
            ActualizarRepuestos actualizarRepuestos = new ActualizarRepuestos();
            actualizarRepuestos.Show();
        }

        private void OnGenerarSButtonClicked(object sender, EventArgs e)
        {
            GenerarServicios generarServicios = new GenerarServicios();
            generarServicios.Show();
        }

        private void OnVrepuestosButtonClicked(object sender, EventArgs e)
        {
            RootWindowTables rootWindowTables = new RootWindowTables();
            rootWindowTables.Show();
        }

        private void OnReportesButtonClicked(object sender, EventArgs e)
        {

            //Usuarios
            string CodigoDot_Usuarios = code.data.Variables.listaUsuarios.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Usuarios", CodigoDot_Usuarios);
            code.utils.Utilidades.ConvertirDotAImagen("Usuarios.dot");


            //Vehiculos
            string CodigoDot_Vehiculos = code.data.Variables.listaVehiculos.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Vehiculos", CodigoDot_Vehiculos);
            code.utils.Utilidades.ConvertirDotAImagen("Vehiculos.dot");

            //Repuestos
            string CodigoDot_Repuestos = code.data.Variables.arbolRepuestos.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Repuestos", CodigoDot_Repuestos);
            code.utils.Utilidades.ConvertirDotAImagen("Repuestos.dot");


            //Servicios
            string CodigoDot_Servicios = code.data.Variables.arbolServicios.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Servicios", CodigoDot_Servicios);
            code.utils.Utilidades.ConvertirDotAImagen("Servicios.dot");

            //Facturas
            string CodigoDot_Facturas = code.data.Variables.arbolFacturas.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Facturas", CodigoDot_Facturas);
            code.utils.Utilidades.ConvertirDotAImagen("Facturas.dot");
        }


        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Destroy();
        }
    }
}




            



                   
