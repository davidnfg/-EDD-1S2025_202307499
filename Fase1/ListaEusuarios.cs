using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaEnlazadaSimple
    {
        private Node* head;
        private Node* tail;

        public ListaEnlazadaSimple()
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
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            Node* actual = head;
            Node* anterior = null;

            while (actual != null)
            {
                if (actual->ID == id)
                {
                    if (anterior != null)
                        anterior->Next = actual->Next;
                    else
                        head = actual->Next;

                    if (actual == tail)
                        tail = anterior;

                    NativeMemory.Free(actual);
                    return;
                }
                anterior = actual;
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

        public unsafe Node* GetHead()
        {
            return head;
        }

        public string GenerarDot()
        {
            if (head == null)
                return "";

            string dot = "";
            Node* actual = head;

            while (actual != null)
            {
                string nombres = new string(actual->Nombres);
                string apellidos = new string(actual->Apellidos);

                dot += $"    {actual->ID} [label=\"ID: {actual->ID}\\n{nombres} {apellidos}\"];\n";
                if (actual->Next != null)
                {
                    dot += $"    {actual->ID} -> {actual->Next->ID};\n";
                }
                actual = actual->Next;
            }

            return dot;
        }

        ~ListaEnlazadaSimple()
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