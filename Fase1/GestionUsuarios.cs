using System;
using Gtk;
using ListaDobleUnsafe;

public class GestionUsuarios : Window
{
    private Entry entryId, entryNombre, entryApellido, entryCorreo;
    private Button btnBuscar, btnActualizar, btnEliminar;
    
    public GestionUsuarios() : base("Gestión de Usuarios")
    {
        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        ModifyBg(StateType.Normal, new Gdk.Color(200, 200, 200));

        VBox vbox = new VBox(false, 10);
        vbox.BorderWidth = 20;

        Label titleLabel = new Label("<b><span foreground='black' size='12000'>Editar de Usuario</span></b>");
        titleLabel.UseMarkup = true;
        vbox.PackStart(titleLabel, false, false, 5);

        Table table = new Table(4, 2, false);
        vbox.PackStart(table, false, false, 5);

        table.Attach(new Label("Id"), 0, 1, 0, 1);
        entryId = new Entry();
        table.Attach(entryId, 1, 2, 0, 1);
        
        btnBuscar = new Button("Buscar");
        btnBuscar.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        btnBuscar.Clicked += OnBuscarClicked;
        table.Attach(btnBuscar, 2, 3, 0, 1);
        
        table.Attach(new Label("Nombres"), 0, 1, 1, 2);
        entryNombre = new Entry();
        table.Attach(entryNombre, 1, 3, 1, 2);
        
        table.Attach(new Label("Apellidos"), 0, 1, 2, 3);
        entryApellido = new Entry();
        table.Attach(entryApellido, 1, 3, 2, 3);
        
        table.Attach(new Label("Correo"), 0, 1, 3, 4);
        entryCorreo = new Entry();
        table.Attach(entryCorreo, 1, 3, 3, 4);
        
        btnActualizar = new Button("Actualizar");
        btnActualizar.ModifyBg(StateType.Normal, new Gdk.Color(50, 205, 50));
        btnActualizar.Clicked += OnActualizarClicked;
        vbox.PackStart(btnActualizar, false, false, 5);
        
        btnEliminar = new Button("Eliminar Usuario");
        btnEliminar.ModifyBg(StateType.Normal, new Gdk.Color(255, 0, 0));
        btnEliminar.Clicked += OnEliminarClicked;
        vbox.PackStart(btnEliminar, false, false, 5);
        
        Add(vbox);
    }
    
    private void OnBuscarClicked(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse(entryId.Text, out id))
        {
            var usuario = ListaGlobal.Lista_Usuarios.Buscar(id);
            if (usuario != null)
            {
                entryNombre.Text = usuario->Nombre;
                entryApellido.Text = usuario->Apellido;
                entryCorreo.Text = usuario->Correo;
            }
            else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Usuario no encontrado.");
                md.Run();
                md.Destroy();
            }
        }
    }
    
    private void OnActualizarClicked(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse(entryId.Text, out id))
        {
            ListaGlobal.Lista_Usuarios.Actualizar(id, entryNombre.Text, entryApellido.Text, entryCorreo.Text);
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario actualizado correctamente.");
            md.Run();
            md.Destroy();
        }
    }
    
    private void OnEliminarClicked(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse(entryId.Text, out id))
        {
            ListaGlobal.Lista_Usuarios.Eliminar(id);
            entryNombre.Text = "";
            entryApellido.Text = "";
            entryCorreo.Text = "";
            
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario eliminado correctamente.");
            md.Run();
            md.Destroy();
        }
    }
}
