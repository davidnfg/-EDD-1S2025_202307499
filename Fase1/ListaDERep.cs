using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDERep
{
    private NodeRep* head;
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
        }
        else
        {
            tail->Next = (NodeRep*)nuevoNodo;
            nuevoNodo->Prev = (NodeRep*)tail;
            tail = nuevoNodo;
        }
    }

    public void Eliminar(int id)
    {
        NodeRep* actual = head;
        while (actual != null)
        {
            if (actual->ID == id)
            {
                if (actual->Prev != null)
                    ((NodeRep*)actual->Prev)->Next = actual->Next;
                else
                    head = (NodeRep*)actual->Next;

                if (actual->Next != null)
                    ((NodeRep*)actual->Next)->Prev = actual->Prev;
                else
                    tail = (NodeRep*)actual->Prev;

                NativeMemory.Free(actual);
                return;
            }
            actual = (NodeRep*)actual->Next;
        }
    }

    public void Mostrar()
    {
        NodeRep* actual = head;
        while (actual != null)
        {
            Console.WriteLine(actual->ToString());
            actual = (NodeRep*)actual->Next;
        }
    }

    public void MostrarReversa()
    {
        NodeRep* actual = tail;
        while (actual != null)
        {
            Console.WriteLine(actual->ToString());
            actual = (NodeRep*)actual->Prev;
        }
    }

    ~ListaDERep()
    {
        NodeRep* actual = head;
        while (actual != null)
        {
            NodeRep* temp = actual;
            actual = (NodeRep*)actual->Next;
            NativeMemory.Free(temp);
        }
    }
}
}
