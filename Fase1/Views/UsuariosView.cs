using System;
using Gtk;
using AutoGestPro.Models;

namespace AutoGestPro
{
    public unsafe class UsuariosView : Window
    {
        private ListaUsuarios listaUsuarios;
        private TreeView treeView;
        private ListStore listStore;

        public UsuariosView(ListaUsuarios listaUsuarios) : base("Gestión de Usuarios")
        {
            this.listaUsuarios = listaUsuarios;

            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);

            VBox vbox = new VBox();
            HBox hbox = new HBox();

            treeView = new TreeView();
            listStore = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string));

            treeView.Model = listStore;
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Nombres", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Apellidos", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Correo", new CellRendererText(), "text", 3);

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);

            Button btnVer = new Button("Ver Usuario");
            Button btnEditar = new Button("Editar Usuario");
            Button btnEliminar = new Button("Eliminar Usuario");

            btnVer.Clicked += OnVerUsuarioClicked;
            btnEditar.Clicked += OnEditarUsuarioClicked;
            btnEliminar.Clicked += OnEliminarUsuarioClicked;

            hbox.PackStart(btnVer, false, false, 5);
            hbox.PackStart(btnEditar, false, false, 5);
            hbox.PackStart(btnEliminar, false, false, 5);

            vbox.PackStart(scrolledWindow, true, true, 5);
            vbox.PackStart(hbox, false, false, 5);

            Add(vbox);
            DeleteEvent += delegate { Application.Quit(); };

            MostrarUsuarios();
        }

        private void MostrarUsuarios()
        {
            listStore.Clear();
            Usuario* actual = listaUsuarios.ObtenerCabeza();
            while (actual != null)
            {
                listStore.AppendValues(actual->ID, actual->Nombres, actual->Apellidos, actual->Correo);
                actual = actual->Siguiente;
            }
        }

        private void OnVerUsuarioClicked(object sender, EventArgs e)
        {
            if (treeView.Selection.GetSelected(out TreeIter iter))
            {
                int id = (int)listStore.GetValue(iter, 0);
                Usuario* usuario = listaUsuarios.BuscarUsuario(id);
                if (usuario != null)
                {
                    MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok,
                        $"ID: {usuario->ID}\nNombres: {usuario->Nombres}\nApellidos: {usuario->Apellidos}\nCorreo: {usuario->Correo}");
                    md.Run();
                    md.Destroy();
                }
            }
        }

        private void OnEditarUsuarioClicked(object sender, EventArgs e)
        {
            if (treeView.Selection.GetSelected(out TreeIter iter))
            {
                int id = (int)listStore.GetValue(iter, 0);
                Usuario* usuario = listaUsuarios.BuscarUsuario(id);
                if (usuario != null)
                {
                    EditarUsuarioDialog dialog = new EditarUsuarioDialog(this, usuario);
                    if (dialog.Run() == (int)ResponseType.Ok)
                    {
                        usuario->Nombres = dialog.Nombres;
                        usuario->Apellidos = dialog.Apellidos;
                        usuario->Correo = dialog.Correo;
                        MostrarUsuarios();
                    }
                    dialog.Destroy();
                }
            }
        }

        private void OnEliminarUsuarioClicked(object sender, EventArgs e)
        {
            if (treeView.Selection.GetSelected(out TreeIter iter))
            {
                int id = (int)listStore.GetValue(iter, 0);
                if (listaUsuarios.EliminarUsuario(id))
                {
                    MostrarUsuarios();
                }
            }
        }
    }

    public unsafe class EditarUsuarioDialog : Dialog
    {
        private Entry entryNombres;
        private Entry entryApellidos;
        private Entry entryCorreo;

        public string Nombres => entryNombres.Text;
        public string Apellidos => entryApellidos.Text;
        public string Correo => entryCorreo.Text;

        public EditarUsuarioDialog(Window parent, Usuario* usuario) : base("Editar Usuario", parent, DialogFlags.Modal)
        {
            VBox vbox = new VBox();

            entryNombres = new Entry(usuario->Nombres);
            entryApellidos = new Entry(usuario->Apellidos);
            entryCorreo = new Entry(usuario->Correo);

            vbox.PackStart(new Label("Nombres:"), false, false, 5);
            vbox.PackStart(entryNombres, false, false, 5);
            vbox.PackStart(new Label("Apellidos:"), false, false, 5);
            vbox.PackStart(entryApellidos, false, false, 5);
            vbox.PackStart(new Label("Correo:"), false, false, 5);
            vbox.PackStart(entryCorreo, false, false, 5);

            AddButton("Cancelar", ResponseType.Cancel);
            AddButton("Guardar", ResponseType.Ok);

            ShowAll();
        }
    }
}