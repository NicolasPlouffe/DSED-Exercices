USE master;
GO

USE master 
GO

IF 1 = 0
BEGIN
	DECLARE @kill varchar(8000) = '';  
	SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
	FROM sys.dm_exec_sessions
	WHERE database_id  = db_id('municipalite')

	EXEC(@kill);

	DROP DATABASE municipalite;
END
GO

IF DB_ID('municipalite') IS NULL
	CREATE DATABASE municipalite;
GO

USE municipalite;
GO

-- DROP TABLE municipalite;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE [name]='ClefAPI' AND xtype='U')
	CREATE TABLE ClefAPI(
    ClefAPIId UNIQUEIDENTIFIER  PRIMARY KEY,
	);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE [name]='municipalite' AND xtype='U')
CREATE TABLE municipalite(
                             municipaliteId INT PRIMARY KEY,
                             nomMunicipalite VARCHAR(150) NOT NULL,
                             adresseCourriel VARCHAR(150) NULL,
                             adresseWeb VARCHAR(1024) NULL,
                             dateProchaineElection DATE NULL,
                             actif BIT
);


IF 1 = 0
BEGIN
	UPDATE municipalite
	SET actif = 0
	WHERE municipaliteId = 2047
END

BEGIN 
SELECT * FROM municipalite;
SELECT * FROM ClefAPI;
END

INSERT INTO ClefAPI (ClefAPIId) VALUES (NEWID());

        E9721D8B-B42B-484A-9119-ED653464D113