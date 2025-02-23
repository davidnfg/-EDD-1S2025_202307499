using System;
using Gtk;

namespace AutoGestPro
{
    public class LoginWindow : Window
    {
        public LoginWindow() : base("Inicio de Sesión")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);

            VBox vbox = new VBox(false, 5);
            Label labelUser = new Label("Usuario:");
            Entry entryUser = new Entry();
            Label labelPassword = new Label("Contraseña:");
            Entry entryPassword = new Entry { Visibility = false };
            Button btnLogin = new Button("Login");
            Button btnCancel = new Button("Cancelar");

            btnLogin.Clicked += (sender, e) =>
            {
                string user = entryUser.Text;
                string password = entryPassword.Text;

                if (user == "root@gmail.com" && password == "root123")
                {
                    new Menu().ShowAll();
                    this.Destroy();
                }
                else
                {
                    MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Credenciales incorrectas");
                    md.Run();
                    md.Destroy();
                }
            };

            btnCancel.Clicked += (sender, e) => Application.Quit();

            vbox.PackStart(labelUser, false, false, 5);
            vbox.PackStart(entryUser, false, false, 5);
            vbox.PackStart(labelPassword, false, false, 5);
            vbox.PackStart(entryPassword, false, false, 5);
            vbox.PackStart(btnLogin, false, false, 5);
            vbox.PackStart(btnCancel, false, false, 5);

            Add(vbox);
            DeleteEvent += delegate { Application.Quit(); };
        }
    }
}