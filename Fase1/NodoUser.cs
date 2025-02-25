using System;

namespace ListaDobleUnsafe
{
    public unsafe struct Node
{
    public int ID;
    public fixed char Nombres[50];
    public fixed char Apellidos[50];
    public fixed char Correo[100];
    public fixed char Contrasenia[50];
    public Node* Next;
    public Node* Prev;

    public Node(int id, string nombres, string apellidos, string correo, string contrasenia)
    {
        ID = id;
        Next = null;
        Prev = null;

        fixed (char* nl = Nombres)
        {
            for (int i = 0; i < nombres.Length && i < 50; i++)
                nl[i] = nombres[i];
        }

        fixed (char* np = Apellidos)
        {
            for (int i = 0; i < apellidos.Length && i < 50; i++)
                np[i] = apellidos[i];
        }

        fixed (char* d = Correo)
        {
            for (int i = 0; i < correo.Length && i < 100; i++)
                d[i] = correo[i];
        }

        fixed (char* c = Contrasenia)
        {
            for (int i = 0; i < contrasenia.Length && i < 50; i++)
                c[i] = contrasenia[i];
        }
    }

        public override string ToString()
{
    fixed (char* nl = Nombres, np = Apellidos, d = Correo, c = Contrasenia)
    {
        string nombresStr = new string(nl);
        string apellidosStr = new string(np);
        string correoStr = new string(d);
        string contraseniaStr = new string(c);

        int nombresLength = nombresStr.IndexOf('\0');
        int apellidosLength = apellidosStr.IndexOf('\0');
        int correoLength = correoStr.IndexOf('\0');
        int contraseniaLength = contraseniaStr.IndexOf('\0');

        if (nombresLength == -1) nombresLength = nombresStr.Length;
        if (apellidosLength == -1) apellidosLength = apellidosStr.Length;
        if (correoLength == -1) correoLength = correoStr.Length;
        if (contraseniaLength == -1) contraseniaLength = contraseniaStr.Length;

        return $"ID: {ID}, Nombres: {nombresStr.Substring(0, nombresLength)}, Apellidos: {apellidosStr.Substring(0, apellidosLength)}, Correo: {correoStr.Substring(0, correoLength)}, Contrasenia: {contraseniaStr.Substring(0, contraseniaLength)}";
    }
}
    }
    
    

}

