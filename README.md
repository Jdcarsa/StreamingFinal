# StreamingFinal

**StreamingFinal** es una aplicación de escritorio desarrollada en C# para la gestión y administración de servicios de streaming. Facilita la organización, control y consulta de información relevante para el usuario o administrador del sistema.

## Características Principales

- Gestión de usuarios y cuentas.
- Administración de servicios de streaming y suscripciones.
- Registro y consulta de historial de uso.
- Búsqueda de información por diferentes criterios.
- Generación de reportes básicos.

## Tecnologías Utilizadas

- ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
- ![Oracle](https://img.shields.io/badge/Oracle-CC0000?style=for-the-badge&logo=oracle&logoColor=white)
- **IDE recomendado:** Visual Studio

## Instalación y Ejecución

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/Jdcarsa/StreamingFinal.git
   ```
2. **Configura la base de datos Oracle:**
   - Crea una base de datos nueva para el sistema.
   - Importa el archivo SQL si está disponible (`/database/` o consulta el proyecto).
   - Configura los parámetros de conexión en la clase de conexión de C# (ejemplo: `ConexionOracle.cs`).

3. **Abre el proyecto en Visual Studio:**
   - Selecciona **Archivo > Abrir > Proyecto/Solución** y elige la carpeta clonada.
   - Asegúrate de tener instalado el proveedor de datos de Oracle para .NET.

4. **Compila y ejecuta la aplicación:**
   - Haz clic en "Iniciar" o presiona `F5`.

## Uso Básico

- Inicia sesión con tu usuario.
- Administra servicios de streaming y usuarios desde el menú principal.
- Consulta reportes y realiza búsquedas según tus necesidades.

## Roadmap / Próximas Funcionalidades

- [ ] Notificaciones automáticas para renovaciones de suscripciones.
- [ ] Exportación de reportes a PDF.
- [ ] Mejoras en la interfaz de usuario.

## Licencia

Distribuido bajo la Licencia MIT. Consulta el archivo `LICENSE` para más información.
```
