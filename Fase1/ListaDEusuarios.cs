using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDoblementeEnlazada
    {
        private Node* head;
        private Node* tail;

        public ListaDoblementeEnlazada()
        {
            head = null;
            tail = null;
        }

        public void Insertar(int id, string nombres, string apellidos, string correo, string contrasenia)
        {
            Node* nuevoNodo = (Node*)NativeMemory.Alloc((nuint)sizeof(Node));
            *nuevoNodo = new Node(id, nombres, apellidos, correo, contrasenia);

            if (head == null)
            {
                head = tail = nuevoNodo;
            }
            else
            {
                tail->Next = nuevoNodo;
                nuevoNodo->Prev = tail;
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            Node* actual = head;
            while (actual != null)
            {
                if (actual->ID == id)
                {
                    if (actual->Prev != null)
                        actual->Prev->Next = actual->Next;
                    else
                        head = actual->Next;

                    if (actual->Next != null)
                        actual->Next->Prev = actual->Prev;
                    else
                        tail = actual->Prev;

                    NativeMemory.Free(actual);
                    return;
                }
                actual = actual->Next;
            }
        }

        public void Mostrar()
        {
            Node* actual = head;
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            }
        }

        public unsafe Node* BuscarUsuario(int id)
        {
            Node* actual = head;
            while (actual != null)
            {
                if (actual->ID == id)
                    return actual;
                actual = actual->Next;
            }
            return null;
        }   

    public unsafe void ActualizarUsuario(int id, string nombres, string apellidos, string correo)
        {
            Node* actual = BuscarUsuario(id);
            if (actual != null)
            {
                CopiarCadena(actual->Nombres, nombres, 50);
                CopiarCadena(actual->Apellidos, apellidos, 50);
                CopiarCadena(actual->Correo, correo, 100);
            }
        }

        private unsafe void CopiarCadena(char* destino, string origen, int maxLength)
        {
            int length = Math.Min(origen.Length, maxLength - 1); // Máximo permitido sin el terminador
            for (int i = 0; i < length; i++)
            {
                destino[i] = origen[i];
            }
            destino[length] = '\0'; // Agregar terminador nulo para evitar basura en la memoria
        }

        public void MostrarReversa()
        {
            Node* actual = tail;
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Prev;
            }
        }


        ~ListaDoblementeEnlazada()
        {
            Node* actual = head;
            while (actual != null)
            {
                Node* temp = actual;
                actual = actual->Next;
                NativeMemory.Free(temp);
            }
        }
    }
}
