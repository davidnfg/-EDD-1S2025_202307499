using System;
using Gtk;
using AutoGestPro.Models;
using System.Collections.Generic;

namespace AutoGestPro
{
    public class MainWindow : Window
    {
        private ListaUsuarios listaUsuarios = new ListaUsuarios();

        public MainWindow() : base("AutoGest Pro")
        {
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            VBox vbox = new VBox(false, 5);
            Label label = new Label("Bienvenido a AutoGest Pro");
            Button btnCargar = new Button("Carga Masiva");
            Button btnUsuarios = new Button("Ingreso Individual");
            Button btnServicios = new Button("Generar Servicios");
            Button btnFacturacion = new Button("Cancelar Factura");
            Button btnSalir = new Button("Salir");

            btnUsuarios.Clicked += (sender, e) => new UsuariosView(listaUsuarios).ShowAll();
            btnCargar.Clicked += (sender, e) => new CargaMasivaWindow().ShowAll();
            btnServicios.Clicked += (sender, e) => new ServiciosView().ShowAll();
            btnFacturacion.Clicked += (sender, e) => new FacturaView().ShowAll();
            btnSalir.Clicked += (sender, e) => Application.Quit();

            vbox.PackStart(label, false, false, 5);
            vbox.PackStart(btnCargar, false, false, 5);
            vbox.PackStart(btnUsuarios, false, false, 5);
            vbox.PackStart(btnServicios, false, false, 5);
            vbox.PackStart(btnFacturacion, false, false, 5);
            vbox.PackStart(btnSalir, false, false, 5);

            Add(vbox);
            DeleteEvent += delegate { Application.Quit(); };
        }
    }
}