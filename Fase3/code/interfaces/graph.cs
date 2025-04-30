using System;
using System.Collections.Generic;
using System.Text;
using code.structures.double_list;
using code.structures.tree_avl;

namespace code.interfaces.graph
{
    
    public class NodoVehiculoRepuesto
    {
        public string IdVehiculo { get; set; } 
        public string IdRepuesto { get; set; } 

        
        public NodoVehiculoRepuesto(string idVehiculo, string idRepuesto)
        {
            IdVehiculo = idVehiculo;
            IdRepuesto = idRepuesto;
        }

        // Estos métodos ayudan a comparar nodos
        public override bool Equals(object obj)
        {
            if (obj is NodoVehiculoRepuesto otro)
            {
                return IdVehiculo == otro.IdVehiculo && IdRepuesto == otro.IdRepuesto;
            }
            return false;
        }

        // Este método genera un código único para cada nodo.
        public override int GetHashCode()
        {
            return HashCode.Combine(IdVehiculo, IdRepuesto);
        }
    }

    
    public class GrafoNoDirigido
    {
        
        private Dictionary<string, List<string>> listaAdyacencia;

        // crea un grafo vacío cuando lo iniciamos.
        public GrafoNoDirigido()
        {
            listaAdyacencia = new Dictionary<string, List<string>>();
        }


        public void Insertar(string idVehiculo, string idRepuesto)
        {
            if (string.IsNullOrEmpty(idVehiculo) || string.IsNullOrEmpty(idRepuesto))
            {
                Console.WriteLine("Error: Los IDs de vehículo y repuesto no pueden estar vacíos.");
                throw new ArgumentException("Los IDs de vehículo y repuesto no pueden estar vacíos.");
            }

            Console.WriteLine($"Insertando conexión: Vehículo={idVehiculo}, Repuesto={idRepuesto}");

            if (!listaAdyacencia.ContainsKey(idVehiculo))
            {
                listaAdyacencia[idVehiculo] = new List<string>();
            }

            if (!listaAdyacencia[idVehiculo].Contains(idRepuesto))
            {
                listaAdyacencia[idVehiculo].Add(idRepuesto);
                Console.WriteLine($"Conexión agregada: {idVehiculo} -> {idRepuesto}");
            }

            if (!listaAdyacencia.ContainsKey(idRepuesto))
            {
                listaAdyacencia[idRepuesto] = new List<string>();
            }

            if (!listaAdyacencia[idRepuesto].Contains(idVehiculo))
            {
                listaAdyacencia[idRepuesto].Add(idVehiculo);
                Console.WriteLine($"Conexión agregada: {idRepuesto} -> {idVehiculo}");
            }
        }
        public string GenerarDot()
        {
            StringBuilder dot = new StringBuilder();

            // Encabezado del archivo DOT
            dot.AppendLine("graph GrafoVehiculosRepuestos {");
            dot.AppendLine("    node [shape=ellipse];"); 
            dot.AppendLine("    graph [rankdir=LR];");  

            // HashSet para evitar duplicar conexiones
            HashSet<string> conexiones = new HashSet<string>();

            // Recorremos todas las claves del diccionario
            foreach (var nodo in listaAdyacencia)
            {
                string idActual = nodo.Key; 
                List<string> conexionesNodo = nodo.Value; 

                // Agregar nodos al grafo
                dot.AppendLine($"    \"{idActual}\";");

                foreach (var idConectado in conexionesNodo)
                {
                    // Creamos una clave única para la conexión
                    string claveConexion = idActual.CompareTo(idConectado) < 0
                        ? $"{idActual} -- {idConectado}"
                        : $"{idConectado} -- {idActual}";

                    // Agregamos la conexión al grafo si no está repetida
                    if (!conexiones.Contains(claveConexion))
                    {
                        dot.AppendLine($"    \"{idActual}\" -- \"{idConectado}\";");
                        conexiones.Add(claveConexion);
                    }
                }
            }

            // Cierre del archivo DOT
            dot.AppendLine("}");

            return dot.ToString();
        }
    }
}