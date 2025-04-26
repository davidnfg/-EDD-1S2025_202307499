# MANUAL TECNICO
## ESTRUCTURAS
### LISTA DOBLEMENTE ENLAZADA.
Este código implementa una lista doblemente enlazada para gestionar vehículos en C#. La estructura permite almacenar, buscar, eliminar y visualizar información de vehículos de manera eficiente.

Clase`Nodo_Vehiculos`

**Propiedades:**
- `Id:` Identificador único del vehículo

- `IdUsuario:` ID del usuario asociado al vehículo

- `Marca:` Marca del vehículo

- `Modelo:` Año del modelo

- `Placa:` Número de placa

- `Siguiente:` Referencia al nodo siguiente

- `Anterior:` Referencia al nodo anterior

Clase`Lista Doble`

**Atributos:**

- `cabeza`: Puntero al primer nodo de la lista

- `cola`: Puntero al último nodo de la lista

#### Métodos principales
1. **Insertar**

*Funcionamiento:*

- Crea un nuevo nodo con los datos proporcionados

- Si la lista está vacía, establece cabeza y cola como el nuevo nodo

- Si no está vacía, enlaza el nuevo nodo al final de la lista
2. **Buscar**

*Funcionamiento:*

- Recorre la lista desde la cabeza hasta encontrar un nodo con el ID buscado

- Retorna el nodo si lo encuentra, o null si no existe
3. **Eliminar**

*Funcionamiento:*

Busca el nodo con el ID especificado

- Reconfigura los enlaces de los nodos adyacentes para "saltar" el nodo a eliminar

- Maneja casos especiales cuando el nodo a eliminar es la cabeza o la cola

4. **Listar Vehículos por Usuario**

*Funcionamiento:*

- Recorre toda la lista y recopila los IDs de los vehículos asociados a un usuario específico

- Retorna una lista de enteros con los IDs encontrados


5. **Graficar con Graphviz**

*Funcionamiento:*

- Genera código DOT para visualizar la lista usando Graphviz

- Crea nodos con la información completa de cada vehículo

- Establece conexiones bidireccionales entre nodos

- Es especialmente útil para depuración y visualización de la estructura


### Lista Enlazada Simple

## Estructura Básica
Implementa una **lista enlazada simple** para administrar usuarios con sus datos personales.

### Componentes Principales

#### 1. Clase `Nodo_Usuario`

- `Id:`Identificador único
- `Nombres:` Nombre(s) del usuario
- `Apellidos:`   Apellidos del usuario
- `Correo:`  Correo electrónico
- `Edad :` Edad en años
- `Contrasenia:` Contraseña del
- `Nodo_Usuario? Siguiente:` Enlace al siguiente nodo

### 2. Clase ListaEnlazada
#### Funcionamiento Principal
**Agregar Usuarios**

*Cómo funciona:*
- Crea un nuevo nodo con los datos

- Si la lista está vacía, lo establece como cabeza

- Si no, recorre hasta el final y lo añade

**Búsquedas**

*Características:*

- Busca por correo (case-insensitive) o por ID

- Retorna el nodo completo o null

- ExisteCorreo devuelve verdadero/falso.

**Eliminar Usuarios**

*Proceso:*
- Busca el nodo con el ID

- Reconfigura los enlaces:

   Si es la cabeza: mueve la cabeza al siguiente
 Si no: el nodo anterior apunta al siguiente del eliminado

### Árbol AVL

#### Estructura Básica
Implementa un **árbol AVL** (árbol binario balanceado) para almacenar y gestionar repuestos automotrices con búsquedas eficientes.

### 🔧 Componentes Principales

#### 1. Clase `Nodo_Repuesto`
- `Id`: ID único del repuesto
- `Repuesto`: Nombre del repuesto
- `Detalles`: Descripción detallada
- `Costo`: Precio del repuesto
- `Nodo_Repuesto Izquierda`: Hijo izquierdo (valores menores)
- `Nodo_Repuesto Derecha`: Hijo derecho (valores mayores)
- `Altura`: Altura del nodo (para balanceo)

#### **2. Clase ArbolAVL**
 **Funcionamiento Principal**
 **Insertar Repuestos**
 *Proceso:*

- Inserta como en un árbol binario normal

- Calcula el factor de balanceo

- Aplica rotaciones si es necesario:

- Rotación simple derecha (LL)

- Rotación simple izquierda (RR)

- Rotación doble izquierda-derecha (LR)

- Rotación doble derecha-izquierda (RL)

**Búsquedas**
 *Características:*

- Búsqueda por ID (O(log n) en árbol balanceado)

- Retorna el nodo completo o null

**Actualización**
 *Funcionamiento:*

- Busca el repuesto por ID

- Actualiza todos sus campos

- Retorna true si existía, false si no

#### Recorridos del Árbol
| Método | Orden | Recorrido |
|-----------|-----------|-----------|
| TablaInOrden() | Izq-Raíz-Der| Lista ordenada por ID |
| TablaPreOrden() | Raíz-Izq-Der| Útil para copiar árboles|
| TablaPostOrden() | Izq-Der-Raíz | Útil para eliminar nodos |

####  Métodos de Balanceo
**Rotaciones**
- Propósito: Mantener el árbol balanceado (altura máxima diferencia 1)

Casos:

- Desequilibrio izquierda-izquierda: Rotación simple derecha

- Desequilibrio derecha-derecha: Rotación simple izquierda

- Desequilibrios mixtos: Rotaciones dobles

**Visualización con Graphviz**
- Salida: Código DOT para visualizar el árbol

- Formato:

- Nodos rectangulares con: ID, Nombre, Detalles y Costo

- Flechas que muestran la jerarquía

**Métodos Auxiliares**
- ObtenerAltura()	Calcula altura de un nodo
- ObtenerBalance()	Calcula factor de balanceo (altura izquierda - altura derecha)

### Árbol B

#### Estructura Básica
Implementa un **árbol B** de orden 5 para almacenar y gestionar facturas con operaciones eficientes de inserción, búsqueda y eliminación.

####  Componentes Principales

#### 1. Clase `Factura`
- `Id`: Identificador único
- `Id_Servicio`: ID del servicio asociado
- `Total`:  Monto total de la factura

2. Clase NodoArbolB

    - `List<Factura> Claves:` Facturas almacenadas (máx. 4)
    - `List<NodoArbolB> Hijos:` Punteros a hijos (máx. 5)
    - `EsHoja:` Indica si es nodo hoja
    
    Métodos auxiliares:

    - `public bool EstaLleno()`:  Verifica si tiene 4 claves
    - `public bool TieneMinimoClaves()`: Verifica si tiene al menos 2 claves.

 
 3. Clase ArbolB

**Operaciones Principales**

- **Inserción**

 *Proceso:*

- Si la raíz está llena: Crea nueva raíz.

- Divide la raíz actual

- Inserta recursivamente manteniendo el orden

*Características:*

- Complejidad: O(log n)

Auto-balanceante
- **Búsqueda**
 *Funcionamiento:*

- Búsqueda binaria dentro de cada nodo

- Recorre hacia abajo según comparación de IDs

Retorna null si no exist
- **Eliminación**
*Estrategias:*

- Hoja: Remoción directa si no viola invariantes

- Nodo interno:

- Reemplaza con predecesor/sucesor

- Fusiona nodos si es necesario

*Casos especiales:*

- Préstamo de claves a hermanos

- Fusión de nodos

##### Recorridos del Árbol
| Método | Orden | Utilidad |
|-----------|-----------|-----------|
| RecorridoInOrden() | ID ascendente| Reportes ordenados |
| ObtenerFacturasPorServicios() | Personalizado| Consultas específicas|

#### Rotaciones y balanceo

| Metodo | Funcion |
|-----------|-----------|
| DividirHijo()| Divide nodos llenos|
| TomaPrestadoDelAnterior() | Rota claves desde hermano izquierdo |
| TomaPrestadoDelSiguiente() | Rota claves desde hermano derecho |
| TomaPrestadoDelSiguiente()| Combina nodos con pocas claves |

### Árbol Binario de Búsqueda 

#### Estructura Básica
Implementa un **árbol binario de búsqueda** para almacenar y gestionar servicios automotrices con operaciones eficientes.

###  Componentes Principales

#### 1. Clase `Nodo_Servicio`
```csharp
public class Nodo_Servicio {
    public int Id;            // Identificador único
    public int Id_Repuesto;   // ID del repuesto asociado
    public int Id_Vehiculo;   // ID del vehículo asociado
    public string Detalles;   // Descripción del servicio
    public double Costo;      // Costo del servicio
    public Nodo_Servicio Izquierda;  // Subárbol izquierdo (valores menores)
    public Nodo_Servicio Derecha;    // Subárbol derecho (valores mayores)
}
```
2. Clase ArbolBinario

**Operaciones Principales**

**Inserción**

*Proceso:*

- Crea nuevo nodo con los datos

- Si el árbol está vacío, lo establece como raíz

- Si no, recorre recursivamente:

- ID menor: va al subárbol izquierdo

- ID mayor o igual: va al subárbol derecho

*C- omplejidad:*

- Mejor caso: O(log n) (árbol balanceado)

- Peor caso: O(n) (árbol degenerado)

**Búsqueda**

*Funcionamiento:*

- Búsqueda binaria recursiva

- Retorna nodo completo o null si no existe

#### Recorridos del Árbol
| Método | Orden | Utilidad |
|-----------|-----------|-----------|
| TablaInOrden() | Izq-Raíz-Der| Listado ordenado por ID |
| TablaPreOrden() | Raíz-Izq-Der| Copia de estructura |
| TablaPostOrden() | Izq-Der-Raíz | Eliminación segura |

#### Recorridos Filtrados
| Método | Filtro | Descripción |
|-----------|-----------|-----------|
| TablaInOrden_Vehiculos() |Por vehículo | Servicios ordenados para vehículos específicos|
| TablaPreOrden_Vehiculos() |Por vehículo | Prioriza raíz antes de filtrar |
| TablaPostOrden_Vehiculos() |Por vehículo  | Procesa hijos antes de raíz |

#### Métodos Auxiliares

- Todos los métodos recursivos siguen el patrón:

- Verificar nodo actual

- Procesar subárbol izquierdo

- Procesar nodo actual

- Procesar subárbol derecho