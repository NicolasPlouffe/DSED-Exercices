using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using M01_DAL_Municipalite_SQLServer;
using M01_Entite;

namespace Tests_Municipalite;

public class DepotMunicipaliteSQLServer_Tests : IDisposable
{
    private readonly MunicipaliteContextSQLServer _contextSQLServer;
    private readonly DepotMunicipalitesSQLServer _depotSQLServer;

    public DepotMunicipaliteSQLServer_Tests()
    {
        var options = new DbContextOptionsBuilder<MunicipaliteContextSQLServer>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _contextSQLServer = new MunicipaliteContextSQLServer(options);
        _depotSQLServer = new DepotMunicipalitesSQLServer(_contextSQLServer);
    }
    
    private MunicipaliteEntite MunicipaliteBase => new MunicipaliteEntite 
    { 
        CodeGeographique = 1,
        NomMunicipalite = "St-Clin-Clin des Meux-Meux",
        AdresseCourriel = "St-Clin-Clin@Meux-Meux.com",
        AdresseWeb = "https://St-ClinClinDesMeuxMeux.com",
        DateProchaineElection = new DateTime(2025, 1, 1)
    };
    
    private MunicipaliteEntite MunicipaliteModifie => new MunicipaliteEntite 
    { 
        CodeGeographique = 1,
        NomMunicipalite = "St-Clin-Clin des Mieux-Mieux",
        AdresseCourriel = "St-Clin-Clin@Meux-Meux.org",
        AdresseWeb = "https://St-ClinClinDesMeuxMeux.org",
        DateProchaineElection = new DateTime(2029, 1, 1)
    };
    
    [Fact]
    public void AjouterMunicipalite_DevraitPersisterEnBD()
    {
        // Act
        _depotSQLServer.AjouterMunicipalite(MunicipaliteBase);
    
        // Assert
        var resultat = _contextSQLServer.Municipalites.Find(1);
        resultat.Should().NotBeNull();
        resultat.NomMunicipalite.Should().Be(MunicipaliteBase.NomMunicipalite);
    }
    
    [Fact]
    public void ChercherMunicipaliteParCodeGeographique_RetourneNullSiInexistant()
    {
        // Act
        var resultat = _depotSQLServer.ChercherMunicipaliteParCodeGeographique(999);

        // Assert
        resultat.Should().BeNull();
    }
    
    [Fact]
    public void ChercherMunicipaliteParCodeGeographique_RetourneSiPresent()
    {
        // Arrange
        _depotSQLServer.AjouterMunicipalite(MunicipaliteBase);

        // Act
        var resultat = _depotSQLServer.ChercherMunicipaliteParCodeGeographique(MunicipaliteBase.CodeGeographique);

        // Assert
        resultat.Should().BeEquivalentTo(MunicipaliteBase);
    }

    [Fact]
    public void MAJMunicipalite_DevraitModifierLesChamps()
    {
        // Arrange
        _depotSQLServer.AjouterMunicipalite(MunicipaliteBase);
        
        // Act
        _depotSQLServer.MAJMunicipalite(MunicipaliteModifie);

        // Assert
        
        var resultatDTO = _contextSQLServer.Municipalites.Find(MunicipaliteModifie.CodeGeographique);
        var resultatEntite = resultatDTO.VerEntite(); 
    
        resultatEntite.Should().BeEquivalentTo(MunicipaliteModifie);
        resultatDTO.Actif.Should().BeTrue();
    }

    [Fact]
    public void DesactiverMunicipalite_DevraitMarquerInactif()
    {
        // Arrange
        _depotSQLServer.AjouterMunicipalite(MunicipaliteBase);
        
        // Act
        _depotSQLServer.DesactiverMunicipalite(MunicipaliteBase);

        // Assert
        var resultat = _contextSQLServer.Municipalites.Find(1);
        resultat.Actif.Should().BeFalse();
    }

    public void Dispose()
    {
        _contextSQLServer?.Dispose();
    }
}
