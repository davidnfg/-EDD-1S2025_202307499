using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDERep
    {
        public NodeRep* head;
        private NodeRep* tail;

        public ListaDERep()
        {
            head = null;
            tail = null;
        }

        public void Insertar(int id, string repuesto, string detalles, double costo)
        {
            NodeRep* nuevoNodo = (NodeRep*)NativeMemory.Alloc((nuint)sizeof(NodeRep));
            *nuevoNodo = new NodeRep(id, repuesto, detalles, costo);

            if (head == null)
            {
                head = tail = nuevoNodo;
                head->Next = head;
                head->Prev = head;
            }
            else
            {
                tail->Next = nuevoNodo;
                nuevoNodo->Prev = tail;
                nuevoNodo->Next = head;
                head->Prev = nuevoNodo;
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            if (head == null) return;

            NodeRep* actual = head;
            do
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

                    if (actual == head)
                        head = actual->Next;

                    if (actual == tail)
                        tail = actual->Prev;

                    NativeMemory.Free(actual);
                    return;
                }
                actual = actual->Next;
            } while (actual != head);
        }

        public void Mostrar()
        {
            if (head == null) return;

            NodeRep* actual = head;
            do
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            } while (actual != head);
        }

        public void MostrarReversa()
        {
            if (tail == null) return;

            NodeRep* actual = tail;
            do
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Prev;
            } while (actual != tail);
        }

        public unsafe NodeRep* GetHead()
        {
            return head;
        }

        ~ListaDERep()
        {
            if (head == null) return;

            NodeRep* actual = head;
            do
            {
                NodeRep* temp = actual;
                actual = actual->Next;
                NativeMemory.Free(temp);
            } while (actual != head);
        }
    }
}