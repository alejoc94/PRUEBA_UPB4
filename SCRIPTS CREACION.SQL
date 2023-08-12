create database PRUEBA_UPB;

create table USUARIOS
(
	ID INT NOT NULL CONSTRAINT PK_USUARIOS PRIMARY KEY,
    NOMBRES VARCHAR(80) NOT NULL,
	APELLIDOS VARCHAR(80) NOT NULL,
	EMAIL VARCHAR(40) NOT NULL,
	USERNAME VARCHAR(10) NOT NULL,
	PWD VARCHAR(10) NOT NULL
);

/*SP CONSULTA*/
CREATE OR REPLACE FUNCTION sp_consultar_usuarios(
    p_id INTEGER DEFAULT NULL
)
RETURNS TABLE (
    ID INTEGER,
    NOMBRES VARCHAR,
    APELLIDOS VARCHAR,
    EMAIL VARCHAR,
    USERNAME VARCHAR,
    PWD VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
       USUARIOS.ID ,
		USUARIOS.NOMBRES ,
		USUARIOS.APELLIDOS ,
		USUARIOS.EMAIL ,
		USUARIOS.USERNAME ,
		USUARIOS.PWD 
    FROM USUARIOS
    WHERE USUARIOS.ID = p_id OR p_id IS NULL;
END;
$$ LANGUAGE plpgsql;
/*SP ELIMINACION*/
CREATE OR REPLACE FUNCTION sp_eliminar_usuarios(IN p_id INTEGER)
RETURNS VOID AS $$
DECLARE
    v_existe_prod INTEGER;
BEGIN
    SELECT ID INTO v_existe_prod
    FROM PRODUCTOS
    WHERE ID = p_id;

    IF v_existe_prod < 1 OR v_existe_prod IS NULL THEN
        DELETE FROM USUARIOS WHERE USUARIOS.ID = v_existe_prod;
    END IF;
END;
$$ LANGUAGE plpgsql;

/*SP CREACION*/
CREATE OR REPLACE FUNCTION sp_crear_actualizar_usuarios(
    IN p_id INTEGER, 
    IN p_nombre VARCHAR(80), 
    IN p_apellidos VARCHAR(80), 
    IN p_email VARCHAR(40), 
    IN p_username VARCHAR(10),
	IN p_pdw  VARCHAR(10)
)
RETURNS VOID AS $$
DECLARE
    v_existe_usu INTEGER;
BEGIN
    SELECT ID INTO v_existe_usu
    FROM PRODUCTOS
    WHERE ID = p_id;

    IF v_existe_usu < 1 OR v_existe_usu IS NULL THEN
        BEGIN
            INSERT INTO USUARIOS (ID, NOMBRES, APELLIDOS, EMAIL, USERNAME, PWD)
            VALUES (p_id, p_nombre, p_apellidos, p_email, p_username, p_pwd);
        END;
    ELSE
        BEGIN
            UPDATE USUARIOS
            SET NOMBRES = p_nombre, 
				APELLIDOS = p_apellidos, 
				EMAIL = p_email, 
				USERNAME = p_username, 
				PWD = p_pwd
            WHERE ID = p_id;
        END;
    END IF;

END;
$$ LANGUAGE plpgsql;