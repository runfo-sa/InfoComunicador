CREATE DATABASE IF NOT EXISTS InfoComunicador;

USE InfoComunicador;

DROP TABLE IF EXISTS InfoComunicador.cartel; 
CREATE TABLE InfoComunicador.cartel (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Texto VARCHAR(600),
    Fecha DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

DROP TABLE IF EXISTS InfoComunicador.color;
CREATE TABLE InfoComunicador.color (
	ColorRGB CHAR(20)
) ENGINE=InnoDB;

DROP PROCEDURE IF EXISTS InfoComunicador.getColor;
DELIMITER //
CREATE PROCEDURE InfoComunicador.getColor(out Color VARCHAR(20))
BEGIN
    SELECT ColorRGB INTO Color FROM InfoComunicador.color LIMIT 1;
    COMMIT;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS InfoComunicador.getUltimoCartel;
DELIMITER //
CREATE PROCEDURE InfoComunicador.getUltimoCartel(OUT Cartel VARCHAR(600))
BEGIN
    SELECT Texto INTO Cartel FROM InfoComunicador.cartel ORDER BY Id DESC LIMIT 1;
END //
DELIMITER ;

 
 DROP PROCEDURE IF EXISTS InfoComunicador.insertCartel;
DELIMITER //
CREATE PROCEDURE InfoComunicador.insertCartel(IN Cartel VARCHAR(600))
BEGIN
    INSERT INTO InfoComunicador.cartel (Texto) VALUES (Cartel);
    COMMIT;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS InfoComunicador.putColor;
DELIMITER //
CREATE PROCEDURE InfoComunicador.putColor(IN Color CHAR(25))
BEGIN
	SET SQL_SAFE_UPDATES = 0;
    UPDATE InfoComunicador.color
    SET ColorRGB = Color;
    SET SQL_SAFE_UPDATES = 1;
    COMMIT;
END //
DELIMITER ;

INSERT INTO InfoComunicador.color (ColorRGB) VALUES ('rgb(255,255,255)');