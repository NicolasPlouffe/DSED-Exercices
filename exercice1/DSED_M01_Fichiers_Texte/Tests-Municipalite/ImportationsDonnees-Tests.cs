global using Xunit;
using M01_Entite;
using M01_Entite.IDepot;
using M01_Srv_Municipalite;
using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
namespace Tests_Municipalite;

public class ImportationDonneesTests
{
    // On declare les Mock pour tous ce qui est non déterministe pour les tests
    private readonly Mock<IDepotImportationMunicipalites> _mockDepotImportation;
    private readonly Mock<IDepotMunicipalites> _mockDepotMunicipalite;
    private readonly TraitementImporterDonneesMunicipalite _traitement;
    
    // Utilisation des type Mock pour les objets de test
    public ImportationDonneesTests()
    {
        _mockDepotImportation = new Mock<IDepotImportationMunicipalites>();
        _mockDepotMunicipalite = new Mock<IDepotMunicipalites>();
        _traitement = new TraitementImporterDonneesMunicipalite(_mockDepotImportation.Object, _mockDepotMunicipalite.Object);
    }
    
    // On déclare les variables globales pour l'ensemble des tests
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
        NomMunicipalite = "St-Clin-Clin des Meux-Meux",
        AdresseCourriel = "St-Clin-Clin@Meux-Meux.org",
        AdresseWeb = "https://St-ClinClinDesMeuxMeux.org",
        DateProchaineElection = new DateTime(2029, 1, 1)
    };
    
    [Fact]
    public void Executer_DevraitAjouterNouvelleMunicipalite_SiAbsenteEnBD()
    {
        // Arrange
        var csvData = new List<MunicipaliteEntite> { MunicipaliteBase };
        _mockDepotImportation.Setup(d => d.LireMunicipalites()).Returns(csvData);
        _mockDepotMunicipalite.Setup(d => d.ChercherMunicipaliteParCodeGeographique(MunicipaliteBase.CodeGeographique)).Returns((MunicipaliteEntite?)null);

        // Act
        var resultat = _traitement.Executer();

        // Assert
        _mockDepotMunicipalite.Verify(d => d.AjouterMunicipalite(It.Is<MunicipaliteEntite>(m => m.Equals(MunicipaliteBase))), Times.Once);
        resultat.NombreEnregistrementsAjoutes.Should().Be(1);
        resultat.NombreEnregistrementsModifies.Should().Be(0);
        resultat.NombreEnregistrementsNonModifies.Should().Be(0);
    }

    [Fact]
    public void Executer_NeDevraitPasAjouter_SiMunicipaliteDejaPresente()
    {
        // Arrange
        var donneesCSV = new List<MunicipaliteEntite> { MunicipaliteBase };
        _mockDepotImportation.Setup(d => d.LireMunicipalites()).Returns(donneesCSV);
        _mockDepotMunicipalite.Setup(d => d.ChercherMunicipaliteParCodeGeographique(MunicipaliteBase.CodeGeographique)).Returns(MunicipaliteBase);

        // Act
        var resultat = _traitement.Executer();

        // Assert
        _mockDepotMunicipalite.Verify(d => d.AjouterMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Never);
        _mockDepotMunicipalite.Verify(d => d.MAJMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Never);
        resultat.NombreEnregistrementsNonModifies.Should().Be(1);
        resultat.NombreEnregistrementsAjoutes.Should().Be(0);
        resultat.NombreEnregistrementsModifies.Should().Be(0);
    }

    [Fact]
    public void Executer_DevraitModifier_SiMunicipaliteDifferente()
    {
        // Arrange
        var donneesCSV = new List<MunicipaliteEntite> { MunicipaliteModifie };
        _mockDepotImportation.Setup(d => d.LireMunicipalites()).Returns(donneesCSV);
        _mockDepotMunicipalite.Setup(d => d.ChercherMunicipaliteParCodeGeographique(MunicipaliteModifie.CodeGeographique)).Returns(MunicipaliteBase);

        // Act
        var resultat = _traitement.Executer();

        // Assert
        _mockDepotMunicipalite.Verify(d => d.MAJMunicipalite(It.Is<MunicipaliteEntite>(m => 
            m.AdresseCourriel == MunicipaliteModifie.AdresseCourriel &&
            m.AdresseWeb == MunicipaliteModifie.AdresseWeb &&
            m.DateProchaineElection == MunicipaliteModifie.DateProchaineElection)), Times.Once);
        resultat.NombreEnregistrementsModifies.Should().Be(1);
        resultat.NombreEnregistrementsAjoutes.Should().Be(0);
    }
    
    [Fact]
    public void Executer_AvecEntiteIdentique_NeDevraitPasModifier()
    {
        // Arrange
        _mockDepotImportation.Setup(d => d.LireMunicipalites()).Returns(new List<MunicipaliteEntite> { MunicipaliteBase });
        _mockDepotMunicipalite.Setup(d => d.ChercherMunicipaliteParCodeGeographique(1)).Returns(MunicipaliteBase);

        // Act
        var resultat = _traitement.Executer();

        // Assert
        _mockDepotMunicipalite.Verify(d => d.MAJMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Never);
        resultat.NombreEnregistrementsNonModifies.Should().Be(0);
    }

    [Fact]
    public void Executer_DevraitDesactiverMunicipaliteAbsenteDuCSV()
    {
        // Arrange
        var dbEntites = new List<MunicipaliteEntite> { MunicipaliteBase };
        _mockDepotImportation.Setup(d => d.LireMunicipalites()).Returns(new List<MunicipaliteEntite>());
        _mockDepotMunicipalite.Setup(d => d.ListerMunicipalitesActives()).Returns(dbEntites);

        // Act
        _traitement.Executer();

        // Assert
        _mockDepotMunicipalite.Verify(d => d.DesactiverMunicipalite(It.Is<MunicipaliteEntite>(m => m.Equals(MunicipaliteBase))), Times.Once);
    }
    
}