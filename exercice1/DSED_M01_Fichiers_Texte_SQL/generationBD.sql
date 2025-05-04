USE master;
Go

IF DB_ID ('municipalites') IS NULL
CREATE DATABASE municipalites;
GO

USE municipalites;
GO

CREATE TABLE municipalites (
    CodeGeographique INT PRIMARY KEY
    ,NomMunicipalite NVARCHAR(255) NOT NULL
    ,AdresseCourriel NVARCHAR(255) NULL
    ,AdresseWeb NVARCHAR(255) NULL
    ,DateProchaineElection DATETIME NULL
    ,Actif BIT NOT NULL DEFAULT 1
);

SELECT * FROM municipalites m ;