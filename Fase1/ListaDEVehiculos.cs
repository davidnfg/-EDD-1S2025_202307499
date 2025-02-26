using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDEVehiculos
{
    public NodeVehi* head;
    private NodeVehi* tail;

    public ListaDEVehiculos()
    {
        head = null;
        tail = null;
    }

    public void Insertar(int id, int id_usuario, string marca, int modelo, string placa)
    {
        NodeVehi* nuevoNodo = (NodeVehi*)NativeMemory.Alloc((nuint)sizeof(NodeVehi));
        *nuevoNodo = new NodeVehi(id, id_usuario, marca, modelo, placa);

        if (head == null)
        {
            head = tail = nuevoNodo;
        }
        else
        {
            tail->Next = (NodeVehi*)nuevoNodo;
            nuevoNodo->Prev = (NodeVehi*)tail;
            tail = nuevoNodo;
        }
    }

    public void Eliminar(int id)
    {
        NodeVehi* actual = head;
        while (actual != null)
        {
            if (actual->ID == id)
            {
                if (actual->Prev != null)
                    ((NodeVehi*)actual->Prev)->Next = actual->Next;
                else
                    head = (NodeVehi*)actual->Next;

                if (actual->Next != null)
                    ((NodeVehi*)actual->Next)->Prev = actual->Prev;
                else
                    tail = (NodeVehi*)actual->Prev;

                NativeMemory.Free(actual);
                return;
            }
            actual = (NodeVehi*)actual->Next;
        }
    }

    public void Mostrar()
    {
        NodeVehi* actual = head;
        while (actual != null)
        {
            Console.WriteLine(actual->ToString());
            actual = (NodeVehi*)actual->Next;
        }
    }

    public void MostrarReversa()
    {
        NodeVehi* actual = tail;
        while (actual != null)
        {
            Console.WriteLine(actual->ToString());
            actual = (NodeVehi*)actual->Prev;
        }
    }
    public unsafe NodeVehi* GetHead()
    {
        return head;
    }

    public string GenerarDot()
        {
            if (head == null)
                return "";

            string dot = "";
            NodeVehi* actual = head;

            while (actual != null)
            {
                string marca = new string(actual->Marca, 0, 50).TrimEnd('\0');
                string placa = new string(actual->Placa, 0, 50).TrimEnd('\0');

                dot += $"    {actual->ID} [label=\"ID: {actual->ID}\\nID_Usuario: {actual->ID_Usuario}\\nMarca: {marca}\\nModelo: {actual->Modelo}\\nPlaca: {placa}\"];\n";

                if (actual->Next != null)
                {
                    dot += $"    {actual->ID} -> {actual->Next->ID};\n";
                    dot += $"    {actual->Next->ID} -> {actual->ID};\n"; // Conexión doble
                }

                actual = actual->Next;
            }

            return dot;
        }
    ~ListaDEVehiculos()
    {
        NodeVehi* actual = head;
        while (actual != null)
        {
            NodeVehi* temp = actual;
            actual = (NodeVehi*)actual->Next;
            NativeMemory.Free(temp);
        }
    }
}
}
