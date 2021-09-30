//////////////////////////////////////////////////////////////////////////////////////
//** RestAPi para demostracion de conocimiento a practica de TO
//////////////////////////////////////////////////////////////////////////////////////

Definición : Se presenta un RestAPI donde expone recurso de PERSONAS y DOMICILIO de persona, creando las acciones de CRUD basica de una Entidad.

	1.- Se presnetan las operaciones basicas de CRUD
	2.- Se hacen validaciones en el DTO antes de entrar a la logica de negocio
	3.- Se comentan las funciones y metodos que expone el servicio.
	4.- Queda pendiente realizar los de error ( no se completan por no mencionarse en el requerimiento)


Refactorización:
	* Refactorizar la validacion de DNI y TELEFONO como datos unicos existentes, puede pasarse como validacion en el DTO (se dejo en la regla de neogocio como demostracion del ejemplo).
	* Refactorizar los tipos de objetos devueltos por los metodos Add, Udpate , de regla de negocio, se puede implementar una clase con en lugar de "string" en el Tuple que se retorna.
	* Refactorizar los diferentes mensaje de validaciones que puede retornar la regla de negocio como un Struct y clase de CustomErrorMessages, con codificacion de errores para mayor claridad.

Documentacion:
	Se expone documentacion con Swagger donde se puede implementar la ejecución de los metodos expuestos por el servicio, se pueden visualizar los tipos de resultados y los Modelos obtenidos por los mismos.
	Para acceder a swagger a travez del contenedor