# ❄️ ICE BAR - SISTEMA DE CONTROL Y VENTAS

Sistema de escritorio desarrollado en **Visual Studio Windows Forms (C# / .NET)** conectado a **Microsoft SQL Server**, diseñado para la administración integral, punto de venta y control de inventario de **ICE BAR**.

---

## 🚀 Inicio Rápido

### 1. Abrir en Visual Studio
- Abre el archivo del proyecto [IceBarSystem.csproj](file:///c:/Users/Daxer/Downloads/ICE%20BAR%20SYSTEM/IceBarSystem.csproj) directamente en **Visual Studio 2022**.
- Presiona **F5** o haz clic en **Iniciar (Debug)**.

### 2. O Ejecutar desde la Consola
En la carpeta del proyecto, ejecuta:
```bash
dotnet run
```

---

## 🔑 Credenciales de Acceso Iniciales

> ⚠️ **Regla del Negocio:** Solo existe un Administrador principal inicial, creado previamente en la base de datos. El Administrador es quien da de alta a los trabajadores.

| Rol | Usuario | Contraseña | Permisos |
| :--- | :--- | :--- | :--- |
| **Administrador** | `admin` | `admin123` | **Acceso Total**: Ventas, Consulta y Control Total de Stock, Historial de Ventas, Reportes de Incidencias y Gestión de Trabajadores. |
| **Trabajador (Demo)** | `trabajador1` | `1234` | **Operativo**: Registrar Ventas, Consultar Stock y Registrar Incidencias. |

---

## 🗄️ Base de Datos SQL Server

El sistema incluye el script SQL completo [IceBar_Database.sql](file:///c:/Users/Daxer/Downloads/ICE%20BAR%20SYSTEM/IceBar_Database.sql).

- **Nombre de la Base de Datos:** `IceBarDB`
- **Cadena de Conexión predeterminada:**
  ```text
  Server=(localdb)\MSSQLLocalDB;Database=IceBarDB;Integrated Security=True;TrustServerCertificate=True;
  ```
- **Auto-Configuración y Resiliencia:** 
  - La aplicación verifica y crea automáticamente la base de datos y tablas si aún no existen.
  - Cuenta con un botón de **⚙️ Configurar Base de Datos / SQL** en la pantalla de Login para cambiar fácilmente entre `(localdb)\MSSQLLocalDB`, `localhost` o `.\SQLEXPRESS` con prueba de conexión en tiempo real.

---

## 📋 Módulos y Casos de Uso Implementados

### 1. 🔐 Inicio de Sesión (`FormLogin.cs`)
- Acceso seguro mediante nombre de usuario y contraseña.
- Control de roles (`Administrador` vs `Trabajador`) que adapta dinámicamente los menús y permisos del sistema.

### 2. 🛒 Registrar Venta (`FormVenta.cs`)
- Vista de catálogo con buscador en tiempo real.
- Panel de venta con cuadrícula detallada:
  - Nombre del producto (ej. `Coca Cola 600ml`, `Cerveza Corona Extra 355ml`).
  - Precio unitario.
  - Cantidad seleccionada.
  - Total acumulado de la venta.
  - Campo de **Precio Cancelado (Pago del cliente)**.
  - Cálculo instantáneo de **Cambio / Devuelta**.
  - Selector de **Tipo de Pago** (Efectivo, Tarjeta de Débito, Tarjeta de Crédito, Transferencia).
- Transacción SQL: reduce automáticamente el stock del producto y emite el comprobante / ticket digital.

### 3. 📦 Consulta de Stock (`FormStockConsulta.cs`)
- Vista de inventario para Trabajadores y Administrador.
- Barra de búsqueda interactiva por nombre o descripción.
- Indicadores visuales de estado: `Disponible` (verde), `Bajo Stock` (ámbar), `Agotado` (rojo).

### 4. 📊 Control Total de Stock - Administrador (`FormStockAdmin.cs`)
- **Agregar Producto:** Nombre con descripción (ej: `Coca Cola 600ml`), precio unitario, stock inicial y categoría.
- **Editar Producto:** Modificación de nombres, precios y categorías.
- **Eliminar Producto:** Borrado seguro con confirmación.
- **Actualizar Stock Acumulativo:** Vista específica para ingresar cantidad a sumar, la cual se acumula de inmediato al stock anterior (`Stock = Stock + Cantidad`).

### 5. 📑 Historial de Ventas - Administrador (`FormHistorialVentas.cs`)
- Registro cronológico de todas las ventas con fecha, hora exacta, total recaudado, método de pago y trabajador que atendió.
- Filtro por rango de fechas (Desde / Hasta) o botón directo "Ventas de Hoy".
- Desglose de productos de cada ticket al seleccionar una venta.

### 6. ⚠️ Reporte de Incidencias (`FormIncidencias.cs`)
- Vista de incidencias y panel de registro.
- Campos para título, suceso o problema detallado, fecha/hora automática y usuario responsable del reporte.

### 7. 👥 Administrar Cuentas de Trabajadores (`FormUsuarios.cs`)
- Exclusivo para el Administrador.
- Alta de nuevos trabajadores con nombre completo, usuario y contraseña.
- Activación, desactivación o baja de cuentas de trabajadores (con protección de la cuenta principal del Administrador).

---

## 🎨 Diseño y Experiencia de Usuario
- Interfaz gráfica moderna inspirada en la temática **ICE BAR**: paleta azul hielo/cian neón (`#6FFFE9`, `#00C2CB`), paneles oscuros elegantes y tipografía limpia.
- Componentes reutilizables estilizados en [Theme.cs](file:///c:/Users/Daxer/Downloads/ICE%20BAR%20SYSTEM/UI/Theme.cs).
