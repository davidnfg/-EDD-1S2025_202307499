using code.structures.blockchain;
using code.structures.tree_avl;
using code.structures.tree_binary;
using code.structures.double_list;
using code.structures.merkle;
using System.Net.NetworkInformation;
using Pango;


namespace code.data
{
    public static class Variables
    {
        //Usuario Logueado
        public static User usuarioActual = new User(0, "", "", "", 0, "");
        public static string entrada = "";
        public static string salida = "";
        // Usuarios
        public static Blockchain listaUsuarios = new Blockchain(); 

        // Vehiculos
        public static ListaDoble listaVehiculos = new ListaDoble();


        //Repuestos
        public static ArbolAVL arbolRepuestos = new ArbolAVL();
        

        //Servicios
        public static ArbolBinario arbolServicios = new ArbolBinario();

        //Facturas
        public static MerkleTree arbolFacturas = new MerkleTree();
        
    }
}
     