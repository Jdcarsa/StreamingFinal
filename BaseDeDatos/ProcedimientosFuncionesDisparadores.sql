----------------------------------------------------------------------------------
/*
    Creaci�n de procedimientos, funciones y triggers para el funcionamiento de la
    plataforma de streaming.
    
    Hecho por:
    Elkin Ariel Morillo Quenguan
    Jes�s David Cardenas Sandoval
    Jesus Gabriel Parra Dugarte
*/
----------------------------------------------------------------------------------

SET SERVEROUTPUT ON;
SET ECHO OFF;
SET VERIFY OFF;



----------------------------------------------------------------------------------
/*
    Procedimientos:
    
    1. Realizar un procedimiento que me permita conocer cu�l ha sido el producto m�s 
    visto durante la �ltima semana hasta la fecha.
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE PRODUCTO_MAS_VISTO AS
    V_FECHA_IN DATE;
    V_MAS_VISTO PRODUCTO.NOMBRE%TYPE;
    V_TOTAL_VISTAS NUMBER;
BEGIN
    V_FECHA_IN := SYSDATE - 7;

    SELECT P.NOMBRE, COUNT(*) AS TOTAL_REPRODUCCIONES
    INTO V_MAS_VISTO, V_TOTAL_VISTAS
    FROM CLIENTE_PRODUCTO CP INNER JOIN PRODUCTO P 
    ON P.CODIGO = CP.CODIGO_PRODUCTO
    WHERE CP.FECHA_REPRODUCCION BETWEEN V_FECHA_IN AND SYSDATE
    GROUP BY P.NOMBRE
    ORDER BY TOTAL_REPRODUCCIONES DESC
    FETCH FIRST 1 ROWS ONLY;

    DBMS_OUTPUT.PUT_LINE('PRODUCTO MAS VISTO: ' || V_MAS_VISTO);
    DBMS_OUTPUT.PUT_LINE('TOTAL DE REPRODUCCIONES: ' || V_TOTAL_VISTAS);
END;

EXECUTE PRODUCTO_MAS_VISTO;

----------------------------------------------------------------------------------
/*
    2. Procedimiento que me permita saber cu�l es el plan de suscripci�n con mayor 
    tendencia durante el �ltimo mes hasta la fecha.
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE PLAN_MAS_COMPRADO AS
    V_FECHA_IN DATE;
    V_MAS_COMPRADO PLANSUSCRIPCION.NOMBRE%TYPE;
    V_TOTAL_COMPRAS NUMBER;
BEGIN
    V_FECHA_IN := ADD_MONTHS(SYSDATE, -1);

    SELECT PS.NOMBRE, COUNT(*) AS TOTAL_COMPRAS
    INTO V_MAS_COMPRADO, V_TOTAL_COMPRAS
    FROM CLIENTE_PLAN CP INNER JOIN PLANSUSCRIPCION PS
    ON CP.CODIGO_PLAN = PS.CODIGO
    WHERE CP.FECHA_COMPRA BETWEEN V_FECHA_IN AND SYSDATE
    GROUP BY PS.NOMBRE
    ORDER BY TOTAL_COMPRAS DESC
    FETCH FIRST 1 ROWS ONLY;

    DBMS_OUTPUT.PUT_LINE('PLAN MAS COMPRADO EN EL ULTIMO MES: ' || V_MAS_COMPRADO);
    DBMS_OUTPUT.PUT_LINE('TOTAL DE COMPRAS: ' || V_TOTAL_COMPRAS);
END;

EXECUTE PLAN_MAS_COMPRADO;

----------------------------------------------------------------------------------
/*
    3. Crear un procedimiento que me permita obtener el monto total que ha gastado 
    un cliente en planes de suscripci�n
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE MONTO_TOTAL_GASTADO(P_CODIGO_CLIENTE IN NUMBER) AS
    V_MONTO_TOTAL NUMBER(11);
BEGIN
    SELECT SUM(PS.PRECIO) INTO V_MONTO_TOTAL
    FROM CLIENTE_PLAN CP JOIN PLANSUSCRIPCION PS 
    ON CP.CODIGO_PLAN = PS.CODIGO
    WHERE CP.CODIGO_CLIENTE = P_CODIGO_CLIENTE;

    DBMS_OUTPUT.PUT_LINE('EL CLIENTE CON CODIGO ' || P_CODIGO_CLIENTE || ' HA GASTADO UN TOTAL DE: $' || V_MONTO_TOTAL);
END;

EXECUTE MONTO_TOTAL_GASTADO(1);

----------------------------------------------------------------------------------
/*
    4.  Crear un procedimiento almacenado donde se cree una tabla anidada con un 
    registro el cual va a tener (nombre del plan , total recaudado) 
    sumar todo lo que ha sido reacaudado en el mes y mostrarlo por pantalla
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE recaudoMes 
    IS        
        TYPE registroGanancias_record IS RECORD
        (
            nombrePlan PlanSuscripcion.nombre%TYPE,
            total NUMBER(20)
        );
        TYPE tblGanacias IS TABLE OF registroGanancias_record;
        
        v_tblGanancias tblGanacias;
        
        v_total NUMBER := 0;
    BEGIN
        SELECT PlanSuscripcion.nombre, SUM(PlanSuscripcion.precio) AS totalVendido
        BULK COLLECT INTO v_tblGanancias
        FROM PlanSuscripcion JOIN Cliente_Plan ON (plansuscripcion.codigo = cliente_plan.codigo_plan)
        WHERE EXTRACT(MONTH FROM cliente_plan.fecha_compra) = EXTRACT(MONTH FROM SYSDATE)
        GROUP BY (PlanSuscripcion.nombre);
            
        FOR i in v_tblGanancias.FIRST .. v_tblGanancias.LAST LOOP
            v_total := v_total + v_tblGanancias(i).total;
        END LOOP;
                
        DBMS_OUTPUT.PUT_LINE('Total de ventas en el mes: ' || v_total);
END recaudoMes;

EXECUTE recaudoMes;

----------------------------------------------------------------------------------
/*
    5. Crear un procedimiento almacenado en que con un curso almacene los  planes de
    suscripcion que se encuentran vigentes y los muestre por pantalla   
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE planesVigentes
    IS
        CURSOR cur_planes IS
            SELECT codigo_cliente, nombre , fecha_vencimiento
            FROM Cliente_plan JOIN PlanSuscripcion ON (cliente_plan.codigo_plan = PlanSuscripcion.codigo )
            WHERE   SYSDATE < cliente_plan.fecha_vencimiento  ;
        BEGIN
            FOR planes IN cur_planes LOOP
                DBMS_OUTPUT.PUT_LINE('Codigo cliente: ' || planes.codigo_cliente || 
                ', Plan: ' || planes.nombre || ' , Fecha de vencimiento: ' ||planes.fecha_vencimiento);
            END LOOP;
END planesVigentes;        

EXECUTE planesVigentes;

----------------------------------------------------------------------------------
/*
    6. Crear un procedimiento que contenga un cursor que almacene todas las
    tarjetas d�bitos registradas y muestre por pantalla los datos de las tarjetas
    y el nombre del cliente.
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE datosTajertasDebito
    IS 
        v_numTarjetas NUMBER:= 0;
        CURSOR cur_tarjetas IS
                SELECT  Cliente.primerNombre || '  ' || Cliente.primerApellido as nombreCliente
                , Tarjeta.numeroTarjeta ,Tarjeta.tipoTarjeta , Tarjeta.cvv , Tarjeta.fechaExp 
                FROM Cliente JOIN Tarjeta ON (Cliente.nombre_usuario_cliente = 
                Tarjeta.nombre_usuario_cliente )
                WHERE tipoTarjeta = 'Débito';
        
                BEGIN
                    FOR datos IN cur_tarjetas LOOP
                        DBMS_OUTPUT.PUT_LINE('Nombre Cliente: ' || datos.nombreCliente || 
                        ', Numero de la tarejta: ' || datos.numeroTarjeta || ' 
                        , Tipo de la tarjeta: ' || datos.tipoTarjeta
                        || ' , CVV: ' || datos.cvv || ' , Fecha de expiracion: ' || datos.fechaExp);
                        v_numTarjetas := v_numTarjetas +1;
                    END LOOP;
    DBMS_OUTPUT.PUT_LINE('Numero de tarjetas debito registras : ' || v_numTarjetas);
END datosTajertasDebito;

EXECUTE datosTajertasDebito;
----------------------------------------------------------------------------------
/*
    Funciones:
    
    1. Realizar una funci�n que pida el c�digo de un actor y devuelva las pel�culas 
    en las que ha sido protagonista
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION PELICULAS_PROTAGONIZADAS(P_CODIGO_ACTOR IN NUMBER)
RETURN SYS_REFCURSOR
AS
  V_CURSOR SYS_REFCURSOR;
BEGIN
  OPEN V_CURSOR FOR
    SELECT P.NOMBRE FROM PRODUCTO P INNER JOIN ACTOR_PRODUCTO AP 
    ON P.CODIGO = AP.CODIGO_PRODUCTO
    WHERE AP.CODIGO_ACTOR = P_CODIGO_ACTOR
    AND AP.PAPEL = 'PRINCIPAL' OR AP.PAPEL = 'SECUNDARIO';

  RETURN V_CURSOR;
END;

DECLARE
  V_RESULTADO SYS_REFCURSOR;
  V_NOMBRE_PELICULA PRODUCTO.NOMBRE%TYPE;
BEGIN
  V_RESULTADO := PELICULAS_PROTAGONIZADAS(5); 

  LOOP
    FETCH V_RESULTADO INTO V_NOMBRE_PELICULA;
    EXIT WHEN V_RESULTADO%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE(V_NOMBRE_PELICULA);
  END LOOP;

  CLOSE V_RESULTADO;
END;

----------------------------------------------------------------------------------
/*
    2. Realizar una funci�n que retorne los datos de la pel�cula con m�s actores
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION PELICULA_MAS_ACTORES
RETURN SYS_REFCURSOR
AS
  V_CURSOR SYS_REFCURSOR;
BEGIN
  OPEN V_CURSOR FOR
    SELECT P.NOMBRE FROM PRODUCTO P INNER JOIN 
    (SELECT CODIGO_PRODUCTO, COUNT(*) AS CONTADOR_ACTORES
     FROM ACTOR_PRODUCTO
     GROUP BY CODIGO_PRODUCTO
     ORDER BY COUNT(*) DESC) A ON P.CODIGO = A.CODIGO_PRODUCTO
     FETCH FIRST 1 ROWS ONLY;

  RETURN V_CURSOR;
END;

DECLARE
  V_RESULTADO SYS_REFCURSOR;
  V_NOMBRE_PELICULA PRODUCTO.NOMBRE%TYPE;
BEGIN
  V_RESULTADO := PELICULA_MAS_ACTORES();

  FETCH V_RESULTADO INTO V_NOMBRE_PELICULA;

  DBMS_OUTPUT.PUT_LINE('LA PELICULA CON MAS ACTORES ES: ' || V_NOMBRE_PELICULA);

  CLOSE V_RESULTADO;
END;

----------------------------------------------------------------------------------
/*
    3. Realizar una función que retorne los datos del admin que más 
    productos ha ingresado a la plataforma
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION ADMIN_MAS_PRODUCTOS
RETURN SYS_REFCURSOR
AS
  V_CURSOR SYS_REFCURSOR;
BEGIN
  OPEN V_CURSOR FOR
    SELECT *
    FROM ADMINISTRADOR A
    WHERE A.CODIGO = (SELECT P.CODIGO_ADMIN
                      FROM PRODUCTO P
                      GROUP BY P.CODIGO_ADMIN
                      ORDER BY COUNT(P.CODIGO) DESC
                      FETCH FIRST 1 ROWS ONLY);

  RETURN V_CURSOR;
END;

DECLARE
  V_ADMIN_CURSOR SYS_REFCURSOR;
  V_ADMIN ADMINISTRADOR%ROWTYPE;
BEGIN
  V_ADMIN_CURSOR := ADMIN_MAS_PRODUCTOS();

  FETCH V_ADMIN_CURSOR INTO V_ADMIN;

  DBMS_OUTPUT.PUT_LINE('ADMINISTRADOR CON MAS PRODUCTOS REGISTRADOS:');
  DBMS_OUTPUT.PUT_LINE('Código: ' || V_ADMIN.CODIGO);
  DBMS_OUTPUT.PUT_LINE('Nombre: ' || V_ADMIN.PRIMERNOMBRE || ' ' || V_ADMIN.SEGUNDONOMBRE || ' ' || V_ADMIN.PRIMERAPELLIDO || ' ' || V_ADMIN.SEGUNDOAPELLIDO);

  CLOSE V_ADMIN_CURSOR;
END;

----------------------------------------------------------------------------------
/*
4. Crear una funcion que retorne la fecha donde se han generado mas
reproducciones de los productos
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION masReproducciones 
    RETURN DATE 
        IS
            v_fechaMasReproducciones Cliente_producto.fecha_reproduccion%TYPE;
            v_numeroReproducciones NUMBER;
        BEGIN
            SELECT COUNT(codigo_reproduccion) AS numero_reproduccion, fecha_reproduccion
            INTO v_numeroReproducciones, v_fechaMasReproducciones
            FROM cliente_producto
            GROUP BY fecha_reproduccion
            ORDER BY numero_reproduccion DESC
            FETCH FIRST 1 ROWS ONLY;   
    RETURN v_fechaMasReproducciones;
END masReproducciones ; 

DECLARE 
         v_fechaMasReproducciones Cliente_producto.fecha_reproduccion%TYPE;
BEGIN
        v_fechaMasReproducciones:= masReproducciones ();
        DBMS_OUTPUT.PUT_LINE('Fecha con mas reproducciones de videos: ' ||v_fechaMasReproducciones);
END;

----------------------------------------------------------------------------------
/*
    5. El numero de productos registrados en la base de datos ,  mostrar por pantalla 
    los administrados que han registrado productos , el usuario y el nombre , ademas
    de la cantidad de cada uno ha registrado
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION numPeliculas
    RETURN NUMBER
        IS
        v_nombreAdmin VARCHAR2 (40);
        v_numPeliculas NUMBER:= 0;
            CURSOR cur_admin IS
                SELECT nombre_usuario_admin , COUNT(Producto.codigo) as productosRegistrados
                FROM Administrador JOIN Producto ON (Administrador.codigo = Producto.codigo_Admin)
                GROUP BY nombre_usuario_admin
                ORDER BY productosRegistrados DESC;
            BEGIN
                FOR admini IN cur_admin LOOP
                    SELECT primerNombre || ' '|| primerApellido
                    INTO v_nombreAdmin
                    FROM administrador
                    WHERE nombre_usuario_admin = admini.nombre_usuario_admin;
                    DBMS_OUTPUT.PUT_LINE('Nombre del administrador: ' ||
                    v_nombreAdmin || ' , Usuario del administrador: ' || admini.nombre_usuario_admin 
                    || ' , Cantidad de productos registrados: ' || admini.productosRegistrados);
                    v_numPeliculas := v_numpeliculas + admini.productosRegistrados;
                END LOOP;    
            RETURN v_numPeliculas;
    END numPeliculas;

DECLARE 
         v_numeroPeliculas NUMBER;
BEGIN
        v_numeroPeliculas:= numPeliculas();
        DBMS_OUTPUT.PUT_LINE('Numero de peliculas registradas: ' || v_numeroPeliculas);
END;

----------------------------------------------------------------------------------
/*
    6. Crear un funcion  que contenga un cursor que almacene todas las tarjetas de
    credito  registradas y muestre por pantalla los datos de  las tarjetas y a
    quien le pertenece, ademas de retornar cuantas existen.
*/
----------------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION datosTajertasCredio 
RETURN NUMBER
IS 
    v_numTarjetas NUMBER:= 0;
    CURSOR cur_tarjetas IS
            SELECT  Cliente.primerNombre || '  ' || Cliente.primerApellido as nombreCliente
            , Tarjeta.numeroTarjeta ,Tarjeta.tipoTarjeta , Tarjeta.cvv , Tarjeta.fechaExp 
            FROM Cliente JOIN Tarjeta ON (Cliente.nombre_usuario_cliente = Tarjeta.nombre_usuario_cliente )
            WHERE tipoTarjeta = 'Cr�dito';
    BEGIN  
        FOR datos IN cur_tarjetas LOOP
            DBMS_OUTPUT.PUT_LINE('Nombre Cliente: ' || datos.nombreCliente || 
            ', Numero de la tarejta: ' || datos.numeroTarjeta || ' , Tipo de la tarjeta: ' 
            || datos.tipoTarjeta || ' , CVV: ' || datos.cvv || 
            ' , Fecha de expiracion: ' || datos.fechaExp);
            v_numTarjetas := v_numTarjetas +1;
        END LOOP;
    RETURN v_numTarjetas;
END datosTajertasCredio;
        
DECLARE 
         v_numeroTarjetas NUMBER;
BEGIN
        v_numeroTarjetas:= datosTajertasCredio();
        DBMS_OUTPUT.PUT_LINE('Numero de tarjetas credito registras : ' || v_numeroTarjetas);
END;

----------------------------------------------------------------------------------
/*
    Disparadores
    
    1. Crear un trigger que nos permita evitar el registro de un 
    cliente que es menor de edad
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE TRIGGER VERIFICAR_EDAD
BEFORE
INSERT OR UPDATE ON CLIENTE
FOR EACH ROW
DECLARE
    V_EDAD NUMBER(2);
    --V_VALIDAR BOOLEAN;
BEGIN
    --V_VALIDAR := VALIDAR_FECHA(:NEW.DEPANIONAC);
    V_EDAD := TRUNC(MONTHS_BETWEEN(SYSDATE, :NEW.FECHANACIMIENTO) / 12);

    IF(V_EDAD < 18) THEN
    RAISE_APPLICATION_ERROR(-20200, 'LA FECHA NO CUMPLE CON LOS REQUISITOS!');
    END IF;
END;

INSERT INTO CLIENTE(NOMBRE_USUARIO_CLIENTE, PRIMERNOMBRE, SEGUNDONOMBRE, PRIMERAPELLIDO, SEGUNDOAPELLIDO, FECHANACIMIENTO, CONTRASENIA, TELEFONO, CORREO) 
VALUES('ElkinMor', 'Elkin', 'Jose', 'Guitierrez', 'Martinez', '17/09/2008', 'Gutigod', '3125618193', 'GUT@UNICAUCAEDUCO');


----------------------------------------------------------------------------------
/*
    2. Crear un trigger que al momento de registrar una suscripci�n a un cliente, 
    dependiendo del plan seleccionado, la fecha de vencimiento del plan se adecue
    al tiempo l�mite del plan, ignorando la fecha de vencimiento preestablecida
    y actualizando a la correcta. Por ejemplo si el plan es semanal la fecha de
    vencimiento obligatoriamente debe sumarse 7 d�as m�s a la fecha de compra.
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE TRIGGER ACTUALIZAR_FECHA_VENCIMIENTO
BEFORE INSERT OR UPDATE ON CLIENTE_PLAN
FOR EACH ROW
DECLARE
    V_FECHA_COMPRA CLIENTE_PLAN.FECHA_COMPRA%TYPE;
    V_FECHA_VENCIMIENTO CLIENTE_PLAN.FECHA_VENCIMIENTO%TYPE;
BEGIN
    V_FECHA_COMPRA := NVL(:NEW.FECHA_COMPRA, SYSDATE);
    
    CASE :NEW.CODIGO_PLAN
        WHEN 1 THEN
            V_FECHA_VENCIMIENTO := V_FECHA_COMPRA + 7; 
        WHEN 2 THEN 
            V_FECHA_VENCIMIENTO := V_FECHA_COMPRA + 30; 
        WHEN 3 THEN 
            V_FECHA_VENCIMIENTO := V_FECHA_COMPRA + 365;
    END CASE;

    :NEW.FECHA_VENCIMIENTO := V_FECHA_VENCIMIENTO;
END;

INSERT INTO CLIENTE_PLAN(CODIGO, CODIGO_CLIENTE, CODIGO_PLAN, FECHA_COMPRA, FECHA_VENCIMIENTO) VALUES (13, 5, 2, '16/05/2023', '16/09/2023');


----------------------------------------------------------------------------------
/*
    3. Crear un trigger que se active antes de eliminar un registro en la tabla 
    ACTOR y verifique si el actor est� asociado a alg�n producto en la tabla
    ACTOR_PRODUCTO. Si el actor est� asociado a alg�n producto, el trigger debe
    evitar la eliminaci�n y mostrar un mensaje de error para evitar la eliminaci�n
    de los actores que est�n siendo utilizados.
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE TRIGGER VERIFICAR_PARTICIPACION
BEFORE DELETE ON ACTOR
FOR EACH ROW
DECLARE
    V_CONTADOR NUMBER;
BEGIN
    SELECT COUNT(*) INTO V_CONTADOR
    FROM ACTOR_PRODUCTO
    WHERE CODIGO_ACTOR = :OLD.CODIGO;

    IF V_CONTADOR > 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'No se puede eliminar al actor. Está asociado a una pelicula.');
    END IF;
END;

DELETE FROM ACTOR
WHERE CODIGO = 9;

----------------------------------------------------------------------------------
/*
    4. Triger que se dispara antes  crear un admin cuando se crea con un usuario
    ya registrado Genera mutacion
*/
----------------------------------------------------------------------------------

CREATE OR REPLACE PACKAGE gestionCuentas AS
    vUsuarioA   Administrador.nombre_usuario_admin%TYPE;
    vContadorAdmin NUMBER := 0;
    vContadorCliente NUMBER := 0;
    vUsuarioC   Cliente.nombre_usuario_cliente%TYPE;
END gestionCuentas;


CREATE OR REPLACE TRIGGER tr_existeAdmin
    FOR   INSERT  ON ADMINISTRADOR COMPOUND TRIGGER
    BEFORE EACH ROW IS 
        BEGIN
            SELECT nombre_usuario_admin
            INTO gestionCuentas.vUsuarioA
            FROM Administrador
            WHERE nombre_usuario_admin = :NEW.nombre_usuario_admin;
                                
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                NULL;
            END BEFORE EACH ROW;
        
    BEFORE STATEMENT IS
        BEGIN
            SELECT COUNT(nombre_usuario_admin) 
            INTO gestionCuentas.vContadorAdmin 
            FROM Administrador
            WHERE nombre_usuario_admin = gestionCuentas.vUsuarioA;

            IF gestionCuentas.vContadorAdmin = 1 THEN
                RAISE_APPLICATION_ERROR(-20024 , 'Ya existe un administrador registrado con ese nombre de usuario');
                gestionCuentas.vContadorAdmin := 0;
                gestionCuentas.vUsuarioA := NULL;
            END IF;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN 
                NULL;
            
END BEFORE STATEMENT;
END tr_existeAdmin;

EXECUTE CREAR_ADMINISTRADOR(15, 'AdminB', 'BRUNO','' ,'BARRRAGAN' , 'DIAZ', '19/12/1990', 'AdminB', '18/04/2023', '3135618190', 'TVPLUSS@TVPLUSCOM');


/* 5 Triger que se dispara antes de  crear un cliente cuando se crea con un usuario ya registrado
,este trigger genera tabla mutante , por lo tanto se debe realizar un trigger compuesto*/
CREATE OR REPLACE TRIGGER tr_existeCliente
    FOR   INSERT   ON CLIENTE COMPOUND TRIGGER
        BEFORE EACH ROW IS 
        BEGIN
                SELECT nombre_usuario_cliente
                INTO gestionCuentas.vUsuarioC
                FROM Cliente
                WHERE nombre_usuario_cliente = :NEW.nombre_usuario_cliente;
                
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                            NULL;
        END BEFORE EACH ROW;
        
        BEFORE STATEMENT IS
        BEGIN
                SELECT COUNT(nombre_usuario_cliente)
                INTO gestionCuentas.vContadorCliente
                FROM Cliente
                WHERE nombre_usuario_cliente =gestionCuentas. vUsuarioC;
                        
                IF gestionCuentas.vContadorCliente = 1 THEN
                        RAISE_APPLICATION_ERROR(-20024 , 'Ya existe un Cliente registrado con ese nombre de usuario');
                        gestionCuentas.vContadorCliente := 0;
                        gestionCuentas.vUsuarioC := NULL;
                        END IF;
                    EXCEPTION
                            WHEN NO_DATA_FOUND THEN
                                NULL;
        END BEFORE STATEMENT;
END  tr_existeCliente;


/*
6 Crear un trigger que cuando se actualice el nombre de usuario de un cliente ,
tambien se actualice en la tabla de tarjeta*/

CREATE OR REPLACE TRIGGER tr_actualizarUsuario
    AFTER UPDATE OF nombre_usuario_cliente ON CLIENTE 
        FOR EACH ROW
        BEGIN
                UPDATE Tarjeta
                SET  nombre_usuario_cliente = :NEW.nombre_usuario_cliente
                WHERE nombre_usuario_cliente = :OLD.nombre_usuario_cliente;
                
                DBMS_OUTPUT.PUT_LINE('El nombre de usuario de: ' ||:OLD.nombre_usuario_cliente 
                || ' Se ha actualizado a '|| :NEW.nombre_usuario_cliente );
                EXCEPTION 
                    WHEN NO_DATA_FOUND THEN
                                NULL;   
                    WHEN OTHERS THEN
                                DBMS_OUTPUT.PUT_LINE('Ha ocurrido un error ' );
        END tr_actualizarUsuario;

UPDATE CLIENTE 
    SET nombre_usuario_cliente = 'ClienteL'
    WHERE nombre_usuario_cliente = 'ClienteE';





















