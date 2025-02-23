using System;
using System.Runtime.InteropServices;

namespace AutoGestPro.Models
{
    public unsafe class Usuario
    {
        public int ID;
        public string Nombres;
        public string Apellidos;
        public string Correo;
        public string Contrasenia;
        public Usuario* Siguiente;
    }

    public unsafe class ListaUsuarios
    {
        private Usuario* cabeza;

        public void AgregarUsuario(int id, string nombres, string apellidos, string correo, string contrasenia)
        {
            Usuario* nuevo = (Usuario*)Marshal.AllocHGlobal(sizeof(Usuario));
            nuevo->ID = id;
            nuevo->Nombres = nombres;
            nuevo->Apellidos = apellidos;
            nuevo->Correo = correo;
            nuevo->Contrasenia = contrasenia;
            nuevo->Siguiente = null;

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Usuario* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevo;
            }
        }

        public Usuario* ObtenerCabeza()
        {
            return cabeza;
        }

        public bool EliminarUsuario(int id)
        {
            if (cabeza == null) return false;
            if (cabeza->ID == id)
            {
                Usuario* temp = cabeza;
                cabeza = cabeza->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                return true;
            }

            Usuario* actual = cabeza;
            while (actual->Siguiente != null && actual->Siguiente->ID != id)
            {
                actual = actual->Siguiente;
            }

            if (actual->Siguiente == null) return false;
            Usuario* temp2 = actual->Siguiente;
            actual->Siguiente = actual->Siguiente->Siguiente;
            Marshal.FreeHGlobal((IntPtr)temp2);
            return true;
        }

        public Usuario* BuscarUsuario(int id)
        {
            Usuario* actual = cabeza;
            while (actual != null)
            {
                if (actual->ID == id)
                    return actual;
                actual = actual->Siguiente;
            }
            return null;
        }

        public void MostrarUsuarios()
        {
            Usuario* actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual->ID}, Nombre: {actual->Nombres} {actual->Apellidos}, Correo: {actual->Correo}");
                actual = actual->Siguiente;
            }
        }
    }
}