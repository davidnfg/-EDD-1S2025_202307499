## MANUAL TECNICO
# Listas
**Lista de Usuarios (Lista Enlazada)**
ListaEusuarios.cs
1. *Definición y uso de unsafe*
    - La clase ListaEnlazadaSimple está dentro de un espacio de nombres ListaDobleUnsafe.
    - Utiliza punteros (Node*) y asignación manual de memoria con NativeMemory.Alloc y NativeMemory.Free, lo que significa que la administración de memoria es responsabilidad del programador.
    El código debe compilarse con la opción unsafe activada.
2. *Variables de la clase:*
- head: Apunta al primer nodo de la lista.
- tail: Apunta al último nodo, permitiendo inserciones rápidas al final.
3. *Constructor*
    - public ListaEnlazadaSimple()
{
    head = null;
    tail = null;
}
- Inicializa la lista vacía(head y tail apuntan a null).
4. *Metodo Insertar*
- Asigna manualmente memoria para un nuevo nodo (NativeMemory.Alloc).
- Usa *nuevoNodo = new Node(...) para inicializarlo.
- Si la lista está vacía, head y tail apuntan al nuevo nodo.
- Si no, se enlaza el último nodo (tail) al nuevo y se actualiza tail.
5. *Metodo Eliminar*
Busca el nodo con id, si id coincide:
 - Si hay un nodo anterior, lo enlaza con el siguiente.
- Si el nodo es head, actualiza head.
- Si el nodo es tail, actualiza tail.
- Libera la memoria del nodo eliminado (NativeMemory.Free).
6. *Metodo Mostrar*
- Recorre la lista e imprime cada nodo usando su método ToString().

7. *Metodo BuscarUsuario*
- Busca un nodo con id y devuelve un puntero al nodo.
- Si no lo encuentra, retorna null.

8. *ActualizarUsuario*
- Usa BuscarUsuario(id) para localizar el nodo.
- Si existe, copia los nuevos valores en memoria (CopiarCadena).

**Lista de Vehiculos (Lista Enlazada)**
- *Metodo insertar:* 
Reserva memoria para un nuevo nodo usando NativeMemory.Alloc().
Convierte la memoria reservada en un puntero de tipo NodeVehi*.
Asigna valores al nodo recién creado.
Si la lista está vacía, el nuevo nodo es head y tail.
Si ya hay elementos:
Conecta el nuevo nodo con el último (tail).
Ajusta la referencia Prev en el nuevo nodo.
Mueve tail al nuevo nodo.

- Se repiten los metodos, excepto el de buscar y actualizar usuario.

**Lista de Repuestos (Lista Circular)**
- *Metodo insertar:* 
Reserva memoria para un nuevo nodo (NativeMemory.Alloc).
Convierte la memoria reservada en un puntero de tipo NodeRep*.
Asigna valores al nodo recién creado.
Si la lista está vacía, el nuevo nodo se convierte en head y tail, apuntando a sí mismo (circular).
Si ya hay elementos:
El nuevo nodo se coloca al final.
Se ajustan las referencias Next y Prev.
Se mantiene la estructura circular.

**Cola de servicios**
- *Metodo Enqueue:* 
Reserva memoria para un nuevo nodo (NativeMemory.Alloc).
Convierte la memoria reservada en un puntero de tipo NodoServi*.
Inicializa el nodo con los datos proporcionados.
Si la cola está vacía, el nuevo nodo es tanto head como tail.
Si ya hay elementos, se agrega al final de la cola (tail->Next = nuevoNodo).
 Se actualiza tail para apuntar al nuevo nodo.

 - *Metodo Dequeue:* 
Si la cola está vacía, no hay nada que eliminar.
Guarda el puntero del nodo actual (head).
Mueve head al siguiente nodo.
Si la cola queda vacía después de la eliminación, también actualiza tail a null.
Libera la memoria del nodo eliminado para evitar fugas.

**Pila Facturas**
- *Metodo Push:*
 Reserva memoria para una nueva factura (NativeMemory.Alloc).
Convierte la memoria reservada en un puntero de tipo NodoFac*.
Inicializa el nodo con los valores id, idOrden y total.
El nuevo nodo apunta al nodo anterior (top).
top se actualiza al nuevo nodo.
- *Metodo Pop:*
Si la pila está vacía (top == null), retorna null.
Guarda el puntero del nodo superior (top).
Actualiza top al siguiente nodo (top->Next).
Retorna el nodo eliminado (pero no libera su memoria).

# Nodos
Los nodos son lo mismo, solo cambian los atributos.
Definire el de usuario solamente.

- *Definicion de Node*
    Se usa struct en lugar de class porque la estructura Node representa un bloque de datos fijo en memoria.
Se usa unsafe porque se manejan punteros (Node*) y arreglos fijos (fixed char[]).
- *Atributos de node*
    ID → Identificador del nodo.
Nombres[50] → Arreglo de caracteres de tamaño fijo para almacenar el nombre (hasta 49 caracteres + '\0').
Apellidos[50] → Apellidos con el mismo concepto.
Correo[100] → Dirección de correo con un tamaño máximo de 99 caracteres + '\0'.
Contrasenia[50] → Almacena la contraseña.
Next → Apunta al siguiente nodo en la lista doblemente enlazada.
Prev → Apunta al nodo anterior en la lista doblemente enlazada.
Uso de fixed: Se usa fixed char[] para trabajar con bloques de memoria contiguos sin depender del recolector de basura.
- *Constructor*
Inicializa ID, Next y Prev con valores predeterminados.

# GenerarServicio.cs
Se define una ventana GTK (GenerarServicio).
- Contiene entradas (Entry) para ingresar:
ID del servicio
ID del repuesto
ID del vehículo
Detalles
Costo del servicio
- Al presionar "Guardar", se realiza lo siguiente:
Validación de entradas (deben ser números válidos).
Verificación de existencia del vehículo y repuesto.
Cálculo del costo total (Costo servicio + Costo repuesto).
Se inserta el servicio en una cola de servicios.
Se genera una factura en una pila de facturas.
Muestra un mensaje de confirmación.

# GestionUsuarios.cs
- *Buscar usuario*
Uso de punteros: La función busca un usuario en la lista de usuarios y, si lo encuentra, extrae sus datos de tipo char[] usando punteros (en la variable usuario), los convierte a strings y los muestra en los campos correspondientes.
Conversión de char[] a string: Utiliza el método ConvertirCharArrayAString.
Convertir char* a string

- *Actualizar Usuario*
Si se proporciona un ID válido, actualiza los datos del usuario en la lista usando el método ActualizarUsuario.

- *Eliminar Usuario*
Si se proporciona un ID válido, elimina el usuario de la lista y limpia los campos de entrada.