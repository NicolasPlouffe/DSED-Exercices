use master;
    
    -- Création de la base de données avec paramètres recommandés
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Transaction_Bancaire')
BEGIN
    CREATE DATABASE [Transaction_Bancaire]
END
GO

USE [Transaction_Bancaire]
GO

-- Table Compte (correspond à CompteEntite)
CREATE TABLE dbo.Compte (
                            NumeroCompte UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                            TypeCompte VARCHAR(20) NOT NULL CHECK (TypeCompte IN ('Courrant', 'Epargne', 'Entreprise'))
)
    GO

-- Table Transaction (correspond à TransactionEntite)
CREATE TABLE dbo.Transaction (
                                 TransactionId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                                 Type VARCHAR(20) NOT NULL CHECK (Type IN ('Dépot', 'Retrait', 'Virement')),
                                 DateTransaction DATE NOT NULL,
                                 Montant DECIMAL(18,2) NOT NULL,
                                 NumeroCompte UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES dbo.Compte(NumeroCompte) ON DELETE CASCADE
)
    GO

