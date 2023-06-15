----------------------------------------------------------------------------------
/*
    Creación de vistas para la
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
    1. vista que muestre el nombre de un actor y las películas en las 
    que ha trabajado
*/
----------------------------------------------------------------------------------

--Nota: es importante que hallan actuaciones y productos registrados
--EXECUTE REGISTRAR_ACTUACION(P_PRODUCTO, P_ACTOR, P_PAPEL);
CREATE OR REPLACE VIEW VISTA_ACTOR_PELICULAS AS
SELECT A.CODIGO AS CODIGO_ACTOR, A.PRIMERNOMBRE || ' ' || A.PRIMERAPELLIDO AS NOMBRE_ACTOR, P.CODIGO AS CODIGO_PELÍCULA, P.NOMBRE AS NOMBRE_PELICULA
FROM ACTOR A
JOIN ACTOR_PRODUCTO AP ON A.CODIGO = AP.CODIGO_ACTOR
JOIN PRODUCTO P ON AP.CODIGO_PRODUCTO = P.CODIGO;

SELECT * FROM VISTA_ACTOR_PELICULAS;

UPDATE VISTA_ACTOR_PELICULAS SET NOMBRE_ACTOR = 'Daniel Meara' where CODIGO_ACTOR = 1;
----------------------------------------------------------------------------------
/*
    2. vista que muestre el nombre de un actor y las pel?culas en las 
    que ha trabajado
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE VIEW VISTA_PRODUCTO_ADMIN AS
SELECT P.CODIGO, P.NOMBRE, P.DESCRIPCION, P.FECHAESTRENO, P.DURACION, P.GENERO,
       A.CODIGO AS CODIGO_ADMIN, A.NOMBRE_USUARIO_ADMIN, A.ADM_ID
FROM PRODUCTO P
JOIN ADMINISTRADOR A ON P.CODIGO_ADMIN = A.CODIGO;

SELECT * FROM VISTA_PRODUCTO_ADMIN;

----------------------------------------------------------------------------------
/*
    3. vista que muestre  los datos de una tarjeta junto con el nombre e id del
    cliente al que le pertenece
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE VIEW VISTA_TARJETA_CLIENTE AS
SELECT T.CLIENTE, T.CODIGO, T.NUMEROTARJETA, T.FECHAEXP, T.NOMBRETARJETA, T.CVV, T.TIPOTARJETA,
       C.CODIGO AS CODIGO_CLIENTE, C.NOMBRE_USUARIO_CLIENTE
FROM TARJETA T
JOIN CLIENTE C ON T.CLIENTE = C.CODIGO;

SELECT * FROM VISTA_TARJETA_CLIENTE;

----------------------------------------------------------------------------------
/*
    4. vista que muestre el nombre de los planes junto con la cantidad de
    clientes que la han comprado
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE VIEW VISTA_PLANES_CLIENTES AS
SELECT P.CODIGO, P.NOMBRE, COUNT(C.CODIGO_CLIENTE) AS CANTIDAD_CLIENTES
FROM PLANSUSCRIPCION P
LEFT JOIN CLIENTE_PLAN C ON P.CODIGO = C.CODIGO_PLAN
GROUP BY P.CODIGO, P.NOMBRE;

SELECT * FROM VISTA_PLANES_CLIENTES;

----------------------------------------------------------------------------------
/*
    5. vista que muestre la lista de productos que  ha visto un cliente
*/
----------------------------------------------------------------------------------
-- EXECUTE REGISTRAR_REPRODUCCION(P_COD_CLIENTE IN NUMBER, P_COD_PRODUCTO IN NUMBER);

EXECUTE REGISTRAR_REPRODUCCION(1, 4);

CREATE OR REPLACE VIEW VISTA_PRODUCTOS_VISTOS AS
SELECT CP.CODIGO_CLIENTE, C.NOMBRE_USUARIO_CLIENTE, CP.CODIGO_PRODUCTO, P.NOMBRE
FROM CLIENTE_PRODUCTO CP
JOIN CLIENTE C ON CP.CODIGO_CLIENTE = C.CODIGO
JOIN PRODUCTO P ON CP.CODIGO_PRODUCTO = P.CODIGO;

SELECT * FROM VISTA_PRODUCTOS_VISTOS WHERE CODIGO_CLIENTE = 1;

----------------------------------------------------------------------------------
/*
    6. vista que muestre los diferentes g?neros de pel?culas junto con la
    cantidad total de pel?culas en cada g?nero
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE VIEW VISTA_GENEROS_PELICULAS AS 
SELECT GENERO, COUNT(*) AS CANTIDAD_PELICULAS
FROM PRODUCTO
GROUP BY GENERO;

SELECT * FROM VISTA_GENEROS_PELICULAS;

----------------------------------------------------------------------------------
/*
    6. vista que muestre los clientes que tienen una suscripci?n activa
    en el momento actual. 
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE VIEW VISTA_CLIENTES_SUSCRIPCION AS
SELECT C.CODIGO, C.NOMBRE_USUARIO_CLIENTE, P.NOMBRE AS PLAN_SUSCRIPCION, CP.FECHA_COMPRA, CP.FECHA_VENCIMIENTO
FROM CLIENTE C
JOIN CLIENTE_PLAN CP ON C.CODIGO = CP.CODIGO_CLIENTE
JOIN PLANSUSCRIPCION P ON CP.CODIGO_PLAN = P.CODIGO
WHERE CP.FECHA_COMPRA <= SYSDATE AND CP.FECHA_VENCIMIENTO >= SYSDATE;

----------------------------------------------------------------------------------
/*
    7. vista que muestre los clientes que tienen una suscripci?n activa
    en el momento actual. 
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE VIEW VISTA_CLIENTE_PLAN
AS
SELECT *
FROM CLIENTE_PLAN;
