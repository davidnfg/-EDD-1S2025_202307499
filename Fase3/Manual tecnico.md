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


### BlockChain

## Estructura Básica
Implementa una **lista enlazada simple** para administrar usuarios con sus datos personales.

### Componentes Principales

#### 1. Clase `User`

- `Id:`Identificador único
- `Nombres:` Nombre(s) del usuario
- `Apellidos:`   Apellidos del usuario
- `Correo:`  Correo electrónico
- `Edad :` Edad en años
- `Contrasenia:` Contraseña del


### 2. Clase `Block`
Representa un bloque en la cadena.

Propiedades:
- `Index`: Número del bloque en la cadena.
- `Timestamp`: Fecha y hora de creación del bloque.
- `Data`: Información del usuario en formato JSON.
- `Nonce`: Número utilizado para la prueba de trabajo.
- `PreviousHash`: Hash del bloque anterior.
- `Hash`: Hash único del bloque actual.
- `Next`: Referencia al siguiente bloque en la cadena.

Métodos:

- `CalculateHash()`: Calcula el hash del bloque usando SHA-256.
- `MineBlock()`: Realiza la prueba de trabajo para encontrar un hash que comience con "0000".

### 3. Clase `Blockchain`

Representa la cadena de bloques como una lista simplemente enlazada.
Propiedades:
- `Head`: Referencia al primer bloque de la cadena.

Métodos principales:
- `AddBlock()`: Agrega un nuevo bloque a la cadena.
- `GenerateJson()`: Genera una representación JSON de toda la cadena.
- `GenerateDot()`: Genera un archivo DOT para visualizar la cadena con Graphviz.
- `AnalyzeBlockchain()`: Verifica la integridad de la cadena.
- `Buscar()`: Busca un usuario por ID.
- `ActualizarUsuario()`: Actualiza los datos de un usuario en la cadena.
- `ObtenerMaxID()`: Obtiene el ID más alto en la cadena.
- `ExisteCorreo()`: Verifica si un correo ya existe en la cadena.
- `ValidarContrasenia()`: Valida la contraseña de un usuario.
- `BuscarPorCorreo()`: Busca un usuario por correo.

#### Funcionamiento Principal
- **Agregar Bloques**:
Se crea un nuevo bloque con los datos del usuario.
Si es el primer bloque, se establece como la cabeza de la cadena.
Si no, se recorre la cadena hasta el último bloque y se enlaza el nuevo bloque.

- **Prueba de Trabajo**:
Cada bloque realiza una prueba de trabajo para encontrar un hash que comience con "0000".
Esto asegura la integridad y seguridad de la cadena.

- **Búsqueda y Actualización**:
Los usuarios pueden buscarse por ID o correo.
Los datos de un usuario pueden actualizarse, recalculando los hashes de los bloques afectados.

- **Validación de la Cadena**:
Se verifica que los hashes de los bloques sean válidos y que los enlaces entre bloques sean consistentes.

- **Visualización**:
La cadena puede representarse en formato JSON o como un grafo DOT para su visualización con Graphviz.

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

### ARBOL DE MERKLE

#### Estructura Básica
Implementa un **árbol B** de orden 5 para almacenar y gestionar facturas con operaciones eficientes de inserción, búsqueda y eliminación.

###  Componentes Principales

#### 1. Clase `Factura`
- `Id`: Identificador único
- `Id_Servicio`: ID del servicio asociado
- `Total`:  Monto total de la factura
- `Fecha`: Fecha de emisión de la factura.
- `MetodoPago`: Método de pago utilizado.

#### 2. Clase `MerkleNode`

Representa un nodo en el árbol de Merkle.

Propiedades:
- `Hash`: Hash del nodo (calculado a partir de los datos o de los hashes de los hijos).
- `Left`: Referencia al hijo izquierdo.
- `Right`: Referencia al hijo derecho.
- `Factura`: Factura asociada (solo para nodos hoja).

Constructores:
- `Nodo hoja`: Se crea a partir de una factura.
- `Nodo interno`: Se crea combinando los hashes de los hijos izquierdo y derecho.

Métodos:
- `CalculateHash()`: Calcula el hash combinado de los hijos izquierdo y derecho.
 
#### 3. Clase `MerkleTree`
Representa el árbol de Merkle completo.

Propiedades:
- `Leaves`: Lista de nodos hoja (facturas).
- `Root`: Nodo raíz del árbol.

Métodos principales:
- `Insert()`: Inserta una nueva factura en el árbol.
- `BuildTree()`: Reconstruye el árbol a partir de las hojas.
- `GenerateDot()`: Genera un archivo DOT para visualizar el árbol con Graphviz.
- `Buscar()`: Busca una factura por su ID.
- `ObtenerFacturasPorServicios()`: Obtiene facturas asociadas a una lista de IDs de servicios.
- `Eliminar()`: Elimina una factura por su ID y reconstruye el árbol.

#### Funcionamiento Principal
- **Inserción de Facturas:**
Se crea una factura y un nodo hoja con su hash.
La factura se agrega a la lista de hojas.
El árbol se reconstruye para actualizar los hashes de los nodos internos y la raíz.

- **Construcción del Árbol**:
A partir de las hojas, se agrupan nodos en pares y se crean nodos internos combinando sus hashes.
Este proceso se repite hasta que solo queda un nodo, que se convierte en la raíz.

- **Verificación de Integridad**:
Cada nodo contiene un hash que depende de sus hijos. Si los datos de una factura cambian, el hash de la raíz también cambia, lo que permite detectar modificaciones.

- **Búsqueda y Eliminación:**
Las facturas pueden buscarse por su ID recorriendo la lista de hojas.
Para eliminar una factura, se elimina su nodo hoja y se reconstruye el árbol.

- **Visualización:**
El árbol puede representarse en formato DOT para su visualización con Graphviz. Los nodos muestran información como el ID de la factura, el total y el hash.

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