using System;
using Gtk;
using System.Collections.Generic;

namespace AutoGestPro
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            LoginWindow login = new LoginWindow();
            login.ShowAll();
            Application.Run();
        }
    }

    public class Bitacora
    {
        private Dictionary<(int, int), string> matrizDispersa = new Dictionary<(int, int), string>();

        public void AgregarEntrada(int idVehiculo, int idRepuesto, string detalles)
        {
            if (detalles == null)
            {
                throw new ArgumentNullException(nameof(detalles), "Detalles cannot be null");
            }
            matrizDispersa[(idVehiculo, idRepuesto)] = detalles;
        }

        public string ObtenerEntrada(int idVehiculo, int idRepuesto)
        {
            return matrizDispersa.TryGetValue((idVehiculo, idRepuesto), out string detalles) ? detalles ?? "No encontrado" : "No encontrado";
        }

        public void MostrarBitacora()
        {
            foreach (var entry in matrizDispersa)
            {
                Console.WriteLine($"Vehículo ID: {entry.Key.Item1}, Repuesto ID: {entry.Key.Item2}, Detalles: {entry.Value}");
            }
        }
    }
}