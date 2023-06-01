----------------------------------------------------------------------------------
/*
    Creaci�n de indices para la
    plataforma de streaming.
    
    Hecho por:
    Elkin Ariel Morillo Quenguan
    Jes�s David Cardenas Sandoval
    Jesus Gabriel Parra Dugarte
*/
----------------------------------------------------------------------------------
SET SERVEROUTPUT ON;
SET VERIFY OFF;
SET ECHO OFF;
----------------------------------------------------------------------------------
/*
    1. Este �ndice mejorar� el rendimiento de las consultas que busquen registros
    de CLIENTE Y ADMINISTRADOR basados en el nombre de usuario
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_cliente_nombre_usuario ON CLIENTE(NOMBRE_USUARIO_CLIENTE);

CREATE INDEX idx_administrador_nombre_usuario ON ADMINISTRADOR(NOMBRE_USUARIO_ADMIN);

--Nota: estos �ndices se crean impl�citamente desde el momento de creaci�n de la tabla,
--      ya que estas columnas tienen la restricci�n de unique

SELECT * FROM USER_IND_COLUMNS WHERE TABLE_NAME = 'CLIENTE';
SELECT * FROM USER_IND_COLUMNS WHERE TABLE_NAME = 'ADMINISTRADOR';

----------------------------------------------------------------------------------
/*
    2. Este �ndice mejorar� el rendimiento de las consultas que busquen registros
    de PRODUCTO basados en la fecha de estreno
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_producto_fecha_estreno ON PRODUCTO(FECHAESTRENO);

----------------------------------------------------------------------------------
/*
    3. indexar la tabla ACTOR_PRODUCTO que ordene por el c�digo del actor
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_actor_producto_nombreActor ON ACTOR_PRODUCTO(CODIGO_ACTOR);

----------------------------------------------------------------------------------
/*
    4. indexar la tabla CLIENTE_PLAN que ordene por el c�digo del plan
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_cliente_plan_codigo_plan ON CLIENTE_PLAN(CODIGO_PLAN);

----------------------------------------------------------------------------------
/*
    5. indexar la tabla CLIENTE_PRODUCTO que ordene por el c�digo del cliente
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_cliente_producto_codigoCliente ON CLIENTE_PRODUCTO(CODIGO_CLIENTE);
