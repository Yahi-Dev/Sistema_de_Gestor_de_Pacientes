Para el uso de este proyecto: 

Configurar la conexión a la base de datos:

Abra el archivo appsettings.json en el proyecto WebApp.

Modifique el valor de DefaultConnection para que apunte a su base de datos local. Por ejemplo:

"ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Actualizar la base de datos:

Abra la Package Manager Console.

Asegúrese de que la opción Default Projects esté seleccionada en la capa de persistence.

En la consola, escriba el siguiente comando:
Update-Database -Context ApplicationContext
Este comando aplicará las migraciones y actualizará la base de datos según el contexto de ApplicationContext.



Descripción del Proyecto
Objetivo General
Desarrollar un sistema gestor de pacientes utilizando ASP.NET Core 6, 7, o 8 MVC, implementado bajo la arquitectura Onion.

Funcionalidad de la Aplicación
Login
Pantalla de Inicio de Sesión: Formulario para ingresar usuario y contraseña. Redirecciona a la pantalla de inicio si los datos son correctos; muestra un mensaje de error si son incorrectos.
Registro de Usuario: Formulario para crear un nuevo usuario administrador con validaciones de campos requeridos y coincidencia de contraseñas.
Menú (Home)
El menú principal incluye las siguientes opciones:

Mantenimiento de Usuarios: Solo accesible para usuarios administradores. Permite visualizar, crear, editar y eliminar usuarios.
Mantenimiento de Médicos: Solo accesible para usuarios administradores. Permite gestionar la información de los médicos.
Mantenimiento de Pruebas de Laboratorio: Solo accesible para usuarios administradores. Permite gestionar las pruebas de laboratorio.
Mantenimiento de Pacientes: Solo accesible para usuarios asistentes. Permite gestionar la información de los pacientes.
Mantenimiento de Citas: Solo accesible para usuarios asistentes. Permite gestionar las citas de los pacientes.
Mantenimiento de Resultados de Pruebas de Laboratorio: Solo accesible para usuarios asistentes. Permite gestionar y reportar los resultados de las pruebas de laboratorio.
Mantenimiento de Usuarios (Administrador)
Visualización de Usuarios: Listado de usuarios con opciones para editar y eliminar.
Creación y Edición de Usuarios: Formulario con validaciones para nombre, apellido, correo, nombre de usuario y contraseñas. Validación de unicidad del nombre de usuario.
Mantenimiento de Médicos (Administrador)
Visualización de Médicos: Listado de médicos con opciones para editar y eliminar.
Creación y Edición de Médicos: Formulario para gestionar nombre, apellido, correo, teléfono, cédula y foto del médico.
Mantenimiento de Pruebas de Laboratorio (Administrador)
Visualización de Pruebas de Laboratorio: Listado de pruebas con opciones para editar y eliminar.
Creación y Edición de Pruebas: Formulario para gestionar el nombre de la prueba.
Mantenimiento de Pacientes (Asistente)
Visualización de Pacientes: Listado de pacientes con opciones para editar y eliminar.
Creación y Edición de Pacientes: Formulario para gestionar nombre, apellido, teléfono, dirección, cédula, fecha de nacimiento, hábitos, alergias y foto del paciente.
Mantenimiento de Resultados de Laboratorio (Asistente)
Visualización de Resultados: Listado de resultados de laboratorio pendientes, con opción para reportar resultados.
Reporte de Resultados: Formulario para introducir y finalizar el resultado de una prueba.
Mantenimiento de Citas (Asistente)
Visualización de Citas: Listado de citas con opciones para gestionar el estado de la cita (pendiente, en consulta, completada).
Creación y Gestión de Citas: Formulario para crear nuevas citas y gestionar pruebas de laboratorio asociadas a una cita.
Tecnologías Utilizadas
ASP.NET Core 6, 7, o 8 MVC: Framework principal para el desarrollo de la aplicación web.
Arquitectura Onion: Diseño arquitectónico que promueve una separación clara de responsabilidades y una alta mantenibilidad.
Entity Framework (Code First): ORM utilizado para la persistencia de datos.
Bootstrap: Framework de CSS para el diseño visual de la aplicación.
Repositorios y Servicios Genéricos: Patrón de diseño para la gestión de datos y lógica de negocio de manera genérica y reutilizable.
ViewModels: Utilizados para gestionar los datos y validaciones en las vistas.
Requerimientos Técnicos
Uso de viewmodels y validaciones desde los mismos.
Implementación correcta de la arquitectura Onion.
Aplicación de repositorios y servicios genéricos.
Este proyecto proporciona una solución completa para la gestión de pacientes, médicos, pruebas de laboratorio, y citas, asegurando una experiencia de usuario eficiente y una estructura de código mantenible y escalable.
