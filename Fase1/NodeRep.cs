using System;

namespace ListaDobleUnsafe
{
    public unsafe struct NodeRep
{
    public int ID;
    public fixed char Repuesto[50];
    public fixed char Detalles[350];
    public double Costo;
    public NodeRep* Next;
    public NodeRep * Prev;

    public NodeRep(int id, string repuesto, string detalles, double costo)
    {
        ID = id;
        Costo = costo;
        Next = null;
        Prev = null;

        fixed (char* re = Repuesto)
        {
            for (int i = 0; i < repuesto.Length && i < 50; i++)
                re[i] = repuesto[i];
        }

        fixed (char* de = Detalles)
        {
            for (int i = 0; i < detalles.Length && i < 50; i++)
                de[i] = detalles[i];
        }

    }

        public override string ToString()
{
    fixed (char* re = Repuesto, de = Detalles)
    {
        string repuestoStr = new string(de);
        string detallesStr = new string(re);

        int repuestoLength = repuestoStr.IndexOf('\0');
        int detallesLength = detallesStr.IndexOf('\0');

        if (repuestoLength == -1) repuestoLength = repuestoStr.Length;
        if (detallesLength == -1) detallesLength = detallesStr.Length;


        return $"ID: {ID}, Repuesto: {repuestoStr.Substring(0, repuestoLength)}, Detalles: {detallesStr.Substring(0, detallesLength)}, Costo: {Costo}";
    }
}
    }
}

