CREATE OR REPLACE DIRECTORY DIR_PORTADAS_PRODUCTOS AS 
'C:\Users\NUBIA PARRA\OneDrive\Documentos\UNIVERSIDAD\Quinto Semestre\Bases II\Streaming Final\StreamingFinal\BaseDeDatos\Imagenes';

CREATE OR REPLACE DIRECTORY DIR_VIDEOS_PRODUCTOS AS 
'C:\Users\NUBIA PARRA\OneDrive\Documentos\UNIVERSIDAD\Quinto Semestre\Bases II\Streaming Final\StreamingFinal\BaseDeDatos\Videos';


CREATE OR REPLACE DIRECTORY DIR_PORTADAS_PRODUCTOS AS 
'C:\Users\Elkin\OneDrive\Escritorio\TvPlus+\StreamingFinal\BaseDeDatos\Imagenes';

CREATE OR REPLACE DIRECTORY DIR_VIDEOS_PRODUCTOS AS 
'C:\Users\Elkin\OneDrive\Escritorio\TvPlus+\StreamingFinal\BaseDeDatos\Videos';


GRANT READ, WRITE ON DIRECTORY DIR_PORTADAS_PRODUCTOS TO proyecto_bases;
GRANT READ, WRITE ON DIRECTORY DIR_VIDEOS_PRODUCTOS TO ;
SELECT * FROM DBA_DIRECTORIES; 