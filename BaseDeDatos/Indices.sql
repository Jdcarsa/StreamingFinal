----------------------------------------------------------------------------------
/*
    Creación de indices para la
    plataforma de streaming.
    
    Hecho por:
    Elkin Ariel Morillo Quenguan
    Jesús David Cardenas Sandoval
    Jesus Gabriel Parra Dugarte
*/
----------------------------------------------------------------------------------
SET SERVEROUTPUT ON;
SET VERIFY OFF;
SET ECHO OFF;
----------------------------------------------------------------------------------
/*
    1. Este índice mejorará el rendimiento de las consultas que busquen registros
    de CLIENTE Y ADMINISTRADOR basados en el nombre de usuario
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_cliente_nombre_usuario ON CLIENTE(NOMBRE_USUARIO_CLIENTE);

CREATE INDEX idx_administrador_nombre_usuario ON ADMINISTRADOR(NOMBRE_USUARIO_ADMIN);

--Nota: estos índices se crean implícitamente desde el momento de creación de la tabla,
--      ya que estas columnas tienen la restricción de unique

SELECT * FROM USER_IND_COLUMNS WHERE TABLE_NAME = 'CLIENTE';
SELECT * FROM USER_IND_COLUMNS WHERE TABLE_NAME = 'ADMINISTRADOR';

----------------------------------------------------------------------------------
/*
    2. Este índice mejorará el rendimiento de las consultas que busquen registros
    de PRODUCTO basados en la fecha de estreno
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_producto_fecha_estreno ON PRODUCTO(FECHAESTRENO);

----------------------------------------------------------------------------------
/*
    3. indexar la tabla ACTOR_PRODUCTO que ordene por el código del actor
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_actor_producto_nombreActor ON ACTOR_PRODUCTO(CODIGO_ACTOR);

----------------------------------------------------------------------------------
/*
    4. indexar la tabla CLIENTE_PLAN que ordene por el código del plan
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_cliente_plan_codigo_plan ON CLIENTE_PLAN(CODIGO_PLAN);

----------------------------------------------------------------------------------
/*
    5. indexar la tabla CLIENTE_PRODUCTO que ordene por el código del cliente
*/
----------------------------------------------------------------------------------

CREATE INDEX idx_cliente_producto_codigoCliente ON CLIENTE_PRODUCTO(CODIGO_CLIENTE);

---

--INDICES PRESENTACION
DROP INDEX IDX_NOMBRE_PORTADA;

SELECT *
FROM ALL_INDEXES
WHERE TABLE_NAME = 'PRODUCTO';

/*CREACION DEL INDICE BASADO EN LA TABLA 
PRODUCTO EN LA COLUMNA NOMBRE*/
CREATE INDEX IDX_NOMBRE_PORTADA
ON PRODUCTO(NOMBRE);

--CONSULTAR SU EXISTENCIA
SELECT * FROM USER_INDEXES
WHERE TABLE_NAME = 'PRODUCTO';

--ACTIVACION DEL MONITORING
ALTER INDEX IDX_NOMBRE_PORTADA MONITORING USAGE;

SELECT * FROM USER_IND_COLUMNS
WHERE TABLE_NAME  = 'PRODUCTO';

--VERIFICACION DE SU USO
SELECT * FROM V$OBJECT_USAGE
WHERE TABLE_NAME = 'PRODUCTO';

--MOSTRAR INFORMACION DE LOS PRODUCTOS
SELECT  NOMBRE, DESCRIPCION
FROM PRODUCTO PR
WHERE NOMBRE LIKE '%c%'
ORDER BY NOMBRE ASC;

-- VERIFICACION DE SU USO
SELECT * FROM V$OBJECT_USAGE
WHERE TABLE_NAME = 'PRODUCTO';

-----------------------------------------------------------------------------------------------------------------

DROP INDEX IDX_PELICULAS_PROTAGONIZADAS;
/*CREACION DEL INDICE BASADO EN LA FUNCION PELICULAS_PROTAGONIZADAS
EN LA TABLA ACTOR EN LA COLUMNA PRIMERNOMBRE*/
CREATE INDEX IDX_PELICULAS_PROTAGONIZADAS
ON ACTOR(PELICULAS_PROTAGONIZADAS(PRIMERNOMBRE));

--CONSULTAR SU EXISTENCIA
SELECT * FROM USER_INDEXES
WHERE TABLE_NAME = 'ACTOR';

--ACTIVACION DEL MONITORING
ALTER INDEX IDX_PELICULAS_PROTAGONIZADAS MONITORING USAGE;

--VERIFICACION DE SU USO
SELECT * FROM V$OBJECT_USAGE
WHERE TABLE_NAME = 'ACTOR';

--USO DEL INDICE
DECLARE
  V_RESULTADO SYS_REFCURSOR;
  V_NOMBRE_PELICULA PRODUCTO.NOMBRE%TYPE;
  V_DESCRIPCION_PELICULA PRODUCTO.DESCRIPCION%TYPE;
BEGIN
  V_RESULTADO := PELICULAS_PROTAGONIZADAS('Emma');
  LOOP
    FETCH V_RESULTADO INTO V_NOMBRE_PELICULA, V_DESCRIPCION_PELICULA;
    EXIT WHEN V_RESULTADO%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE(V_NOMBRE_PELICULA ||' -------'||V_DESCRIPCION_PELICULA);
  END LOOP;
  CLOSE V_RESULTADO;
END;
