using System;
using System.Runtime.InteropServices;

namespace AutoGestPro.Models
{
    public unsafe class Vehiculo
    {
        public int ID;
        public int ID_Usuario;
        public string Marca;
        public string Modelo;
        public string Placa;
        public Vehiculo* Siguiente;
    }

    public unsafe class ListaVehiculos
    {
        private Vehiculo* cabeza;

        public void AgregarVehiculo(int id, int idUsuario, string marca, string modelo, string placa)
        {
            Vehiculo* nuevo = (Vehiculo*)Marshal.AllocHGlobal(sizeof(Vehiculo));
            nuevo->ID = id;
            nuevo->ID_Usuario = idUsuario;
            nuevo->Marca = marca;
            nuevo->Modelo = modelo;
            nuevo->Placa = placa;
            nuevo->Siguiente = null;

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Vehiculo* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevo;
            }
        }

        public bool EliminarVehiculo(int id)
        {
            if (cabeza == null) return false;
            if (cabeza->ID == id)
            {
                Vehiculo* temp = cabeza;
                cabeza = cabeza->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                return true;
            }

            Vehiculo* actual = cabeza;
            while (actual->Siguiente != null && actual->Siguiente->ID != id)
            {
                actual = actual->Siguiente;
            }

            if (actual->Siguiente == null) return false;
            Vehiculo* temp2 = actual->Siguiente;
            actual->Siguiente = actual->Siguiente->Siguiente;
            Marshal.FreeHGlobal((IntPtr)temp2);
            return true;
        }

        public Vehiculo* BuscarVehiculo(int id)
        {
            Vehiculo* actual = cabeza;
            while (actual != null)
            {
                if (actual->ID == id)
                    return actual;
                actual = actual->Siguiente;
            }
            return null;
        }

        public void MostrarVehiculos()
        {
            Vehiculo* actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual->ID}, ID Usuario: {actual->ID_Usuario}, Marca: {actual->Marca}, Modelo: {actual->Modelo}, Placa: {actual->Placa}");
                actual = actual->Siguiente;
            }
        }
    }
}