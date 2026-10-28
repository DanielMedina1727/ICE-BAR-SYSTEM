# 🧊 ICE BAR SYSTEM

Sistema de Punto de Venta (POS) para bares, desarrollado en **C# / Windows Forms** con **.NET 10** y **SQL Server**.

> Interfaz inspirada en el proyecto de referencia `xdnt123 / MarlenyFarma`, con panel lateral Navy, botones de acción con código de color y tarjetas KPI en el Dashboard.

---

## ✨ Características

| Módulo | Descripción |
|---|---|
| 🔐 **Login** | Autenticación con opción "Recordar usuario", control de acceso por rol |
| 📊 **Dashboard** | Tarjetas KPI (ventas del día, tickets, productos, incidencias) + tabla de últimas 10 ventas y stock bajo mínimo |
| 🛒 **Terminal POS** | Selección de producto, carrito, método de pago, emisión de ticket y descuento automático de stock |
| 📦 **Administración de Stock** | CRUD completo de productos (nombre, categoría, precio, stock mínimo) |
| 🔍 **Consulta de Stock** | Catálogo de solo lectura con filtros por nombre y categoría |
| 📜 **Historial de Ventas** | Filtro por rango de fechas y número de ticket, con detalle de cada venta |
| 👥 **Gestión de Usuarios** | Crear, editar y desactivar usuarios (solo Administrador) |
| ⚠️ **Incidencias** | Registro y seguimiento de incidentes del bar |
| ⚙️ **Configuración de Conexión** | Configurar, probar y guardar la cadena de conexión + inicializar la BD desde la UI |
| 🗒️ **Auditoría** | Tabla `Auditoria` creada automáticamente en tiempo de ejecución; registra cada acción crítica |

---

## 🖥️ Prerrequisitos

- **Visual Studio 2022** (v17.8 o superior) con carga de trabajo *Desktop .NET*
- **.NET 10 SDK** — [descargar](https://dotnet.microsoft.com/download/dotnet/10.0)
- **SQL Server** — LocalDB, SQL Server Express o instancia completa

---

## 🚀 Configuración e instalación

### 1. Clonar / abrir el proyecto

```
git clone <repo-url>
```

Abrir `IceBarSystem.sln` en Visual Studio 2022.

### 2. Configurar la conexión a la base de datos

**Opción A — desde la aplicación (recomendado)**

1. Ejecutar la aplicación.
2. En la pantalla de Login, hacer clic en **"Configurar conexión"**.
3. Ingresar Server, Database, usuario y contraseña.
4. Pulsar **Probar conexión** y luego **Inicializar Tablas** para crear el esquema completo.

**Opción B — SQL manual**

Ejecutar el script `IceBar_Database.sql` (en la raíz del proyecto) contra tu instancia de SQL Server.

**Opción C — App.config**

Editar `App.config` y ajustar la cadena de conexión `IceBarDB`:

```xml
<connectionStrings>
  <add name="IceBarDB"
       connectionString="Server=.\SQLEXPRESS;Database=IceBar;Integrated Security=True;TrustServerCertificate=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

La cadena de conexión activa se guarda en `icebar_db_config.txt` en el directorio de la aplicación para persistir entre sesiones.

---

## 🗄️ Esquema de base de datos

| Tabla | Descripción |
|---|---|
| `Usuarios` | Credenciales y rol (Administrador / Trabajador) |
| `Productos` | Catálogo de productos con precio, categoría y stock mínimo |
| `Ventas` | Encabezado de venta (fecha, usuario, total, método de pago) |
| `DetalleVentas` | Líneas de cada venta (producto, cantidad, precio unitario) |
| `Incidencias` | Registro de incidentes (descripción, estado, fecha) |
| `Auditoria` | Creada automáticamente en runtime; registra INSERT / UPDATE / DELETE críticos |

---

## 🔑 Credenciales por defecto

| Usuario | Contraseña | Rol |
|---|---|---|
| `admin` | `admin123` | Administrador |
| `trabajador1` | `1234` | Trabajador |

> [!CAUTION]
> Cambiar las contraseñas por defecto antes de usar en producción.

---

## 🔐 Permisos por rol

| Módulo | Administrador | Trabajador |
|---|---|---|
| Dashboard | ✅ | ✅ |
| Terminal POS | ✅ | ✅ |
| Consulta de Stock | ✅ | ✅ |
| Historial de Ventas | ✅ | ✅ |
| Administración de Stock | ✅ | ❌ |
| Gestión de Usuarios | ✅ | ❌ |
| Incidencias | ✅ | ✅ |
| Configuración de Conexión | ✅ | ❌ |

---

## 🏗️ Estructura del proyecto

```
ICE BAR SYSTEM/
├── IceBarSystem.sln                  ← Solución Visual Studio
└── ICE BAR SYSTEM/
    ├── IceBarSystem.csproj           ← Proyecto SDK-style (net10.0-windows)
    ├── App.config                    ← Cadena de conexión por defecto
    ├── Program.cs                    ← Punto de entrada (Application.Run)
    ├── RoundedPanel.cs               ← Control personalizado con esquinas redondeadas
    │
    ├── Forms/                        ← Un directorio, todos los formularios
    │   ├── FormLogin.cs / .Designer.cs / .resx
    │   ├── FormMain.cs  / .Designer.cs / .resx   ← Shell MDI / contenedor principal
    │   ├── FormDashboard.cs / .Designer.cs / .resx
    │   ├── FormVenta.cs / .Designer.cs / .resx
    │   ├── FormStockAdmin.cs / .Designer.cs / .resx
    │   ├── FormStockConsulta.cs / .Designer.cs / .resx
    │   ├── FormHistorialVentas.cs / .Designer.cs / .resx
    │   ├── FormUsuarios.cs / .Designer.cs / .resx
    │   ├── FormIncidencias.cs / .Designer.cs / .resx
    │   └── FormConfigConexion.cs / .Designer.cs / .resx
    │
    ├── UI/
    │   └── Theme.cs                  ← Paleta de colores y fuentes centralizada
    │
    ├── Data/
    │   └── DatabaseHelper.cs         ← Conexión, queries y EnsureDatabaseSetup()
    │
    ├── Helpers/
    │   └── AuditoriaHelper.cs        ← Registro de auditoría
    │
    └── IceBar_Database.sql           ← Script completo de creación de BD + datos semilla
```

> [!NOTE]
> Cada formulario tiene **tres archivos**: `.cs` (lógica), `.Designer.cs` (controles) y `.resx` (recursos). Los tres están declarados explícitamente en el `.csproj` con `<SubType>Form</SubType>` y `<DependentUpon>`, lo que permite abrir el **Diseñador de Windows Forms** en Visual Studio.

---

## 🎨 Guía de estilo visual

La interfaz sigue la misma paleta del proyecto de referencia `xdnt123 / MarlenyFarma`:

| Token | Color (Hex) | Uso |
|---|---|---|
| `Navy` | `#1A237E` | Sidebar, encabezados de panel |
| `BtnBlue` | `#1976D2` | Botones Nuevo / Buscar |
| `BtnGreen` | `#388E3C` | Botones Guardar / Confirmar |
| `BtnYellow` | `#F57F17` | Botones Limpiar / Editar |
| `BtnRed` | `#C62828` | Botones Eliminar / Cancelar |
| `Background` | `#F5F5F5` | Fondo general del formulario |
| `CardWhite` | `#FFFFFF` | Tarjetas KPI y paneles de contenido |
| `TextDark` | `#212121` | Texto principal |
| `TextLight` | `#FFFFFF` | Texto sobre fondos Navy |

La clase [`Theme.cs`](file:///c:/Users/Daxer/Downloads/ICE%20BAR%20SYSTEM/ICE%20BAR%20SYSTEM/UI/Theme.cs) expone todos los colores y fuentes como propiedades estáticas. Usar siempre `Theme.*` en lugar de valores en línea.

---

## 🛠️ Compilar

```bash
# Desde la raíz de la solución
dotnet build IceBarSystem.sln
```

Build limpio: **0 errores, 0 advertencias**.

```bash
# Si necesitas restaurar paquetes desde caché local
dotnet restore IceBarSystem.sln --source "C:\Users\<tu_usuario>\.nuget\packages"
dotnet build IceBarSystem.sln --no-restore
```

---

## 🖌️ Abrir el Diseñador de Windows Forms

1. En el **Explorador de soluciones**, expandir `Forms/`.
2. Hacer clic derecho sobre cualquier `.cs` (p. ej. `FormLogin.cs`).
3. Seleccionar **"Ver diseñador"** (o presionar `Shift + F7`).

> [!IMPORTANT]
> Para que el diseñador funcione, los tres archivos del formulario deben existir y estar declarados en el `.csproj`. Si agregas un formulario nuevo, cópialo del patrón existente y declara las tres entradas en `IceBarSystem.csproj`.

---

## 📦 Dependencias NuGet

| Paquete | Versión | Propósito |
|---|---|---|
| `Microsoft.Data.SqlClient` | 7.1.1 | Acceso a SQL Server |

---

## 🤝 Contribuir

1. Crear rama: `git checkout -b feature/nombre-caracteristica`
2. Commit: `git commit -m "feat: descripción"`
3. Pull Request contra `main`

---

## 📄 Licencia

Proyecto educativo / privado. Todos los derechos reservados.
