using code.interfaces;
using code.utils.json;
using Gtk;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Manejar el evento de cierre del programa
        AppDomain.CurrentDomain.ProcessExit += OnApplicationExit;

        Application.Init();
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Show();

        Application.Run();
    }

    private static void OnApplicationExit(object? sender, EventArgs e)
    {
        // Limpiar el archivo JSON al cerrar la aplicación
        Jsons.LimpiarJson("registros");
        Console.WriteLine("El archivo JSON ha sido limpiado al cerrar la aplicación.");
    }
}