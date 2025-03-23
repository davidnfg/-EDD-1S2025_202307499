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
            code.data.Variables.arbolServicios.Insertar(5, 1005, 2005, "Filtro de aire", 20.00);    
            code.data.Variables.arbolServicios.Insertar(3, 1003, 2003, "Pastillas de freno", 45.99);
            code.data.Variables.arbolServicios.Insertar(7, 1007, 2007, "Batería", 75.00);           
            code.data.Variables.arbolServicios.Insertar(1, 1001, 2001, "Filtro de aceite", 25.50);  
            code.data.Variables.arbolServicios.Insertar(4, 1004, 2004, "Aceite motor", 35.00);      
            code.data.Variables.arbolServicios.Insertar(6, 1006, 2006, "Líquido de frenos", 10.50); 
            code.data.Variables.arbolServicios.Insertar(9, 1009, 2009, "Neumático", 100.00);        
            code.data.Variables.arbolServicios.Insertar(2, 1002, 2002, "Bujía", 15.75);             
            code.data.Variables.arbolServicios.Insertar(8, 1008, 2008, "Lámpara", 5.25);            
            code.data.Variables.arbolServicios.Insertar(10, 1010, 2010, "Escobillas", 12.00);
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
        }


        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Destroy();
        }
    }
}




            



                   
