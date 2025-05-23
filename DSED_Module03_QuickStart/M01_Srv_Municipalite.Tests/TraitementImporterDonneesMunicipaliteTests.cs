using System;
using System.Collections.Generic;
using System.Linq;

using AutoFixture;
using Moq;
using Xunit;

using M01_Srv_Municipalite;
using M01_Entite;

namespace M01_Srv_Municipalite.Tests
{
    public class TraitementImporterDonneesMunicipaliteTests
    {
        [Fact]
        public void Executer_BDVide_10Importations_10ElementsAjoutes()
        {
            // Arranger
            Fixture fixture = new Fixture();
            fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
            StatistiquesImportationDonnees sidAttendues = new StatistiquesImportationDonnees()
            {
                NombreEnregistrementsAjoutes = 10,
                NombreEnregistrementsModifies = 0,
                NombreEnregistrementsDesactives = 0,
                NombreEnregistrementsNonModifies = 0,
                NombreMunicipalitesImportees = 10
            };
            List<MunicipaliteEntite> municipalitesAImporter =
            Enumerable.Range(0, sidAttendues.NombreMunicipalitesImportees)
            .Select(i => fixture.Create<MunicipaliteEntite>())
            .ToList();
            List<MunicipaliteEntite> municipalitesActuelles = new List<MunicipaliteEntite>();

            Mock<IDepotImportationMunicipalites> mockDepotImportationMunicipalites = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportationMunicipalites
            .Setup(dim => dim.LireMunicipalites())
            .Returns(municipalitesAImporter);

            Mock<IDepotMunicipalites> mockDepotMunicipalites = new Mock<IDepotMunicipalites>();
            //mockDepotMunicipalites
            //    .Setup(dm => dm.ChercherMunicipaliteParCodeGeographique(It.IsAny<int>()))
            //    .Returns<Municipalite>(null);
            mockDepotMunicipalites
            .Setup(dm => dm.ListerMunicipalitesActives())
            .Returns(municipalitesActuelles);

            IDepotImportationMunicipalites depotImportationMunicipalites = mockDepotImportationMunicipalites.Object;
            IDepotMunicipalites depotMunicipalites = mockDepotMunicipalites.Object;
            TraitementImporterDonneesMunicipalite tidm = new TraitementImporterDonneesMunicipalite(depotImportationMunicipalites, depotMunicipalites);

            // Agir
            StatistiquesImportationDonnees sid = tidm.Executer();

            // Auditer
            Assert.Equal(sidAttendues.NombreEnregistrementsAjoutes, sid.NombreEnregistrementsAjoutes);
            Assert.Equal(sidAttendues.NombreEnregistrementsDesactives, sid.NombreEnregistrementsDesactives);
            Assert.Equal(sidAttendues.NombreEnregistrementsModifies, sid.NombreEnregistrementsModifies);
            Assert.Equal(sidAttendues.NombreEnregistrementsNonModifies, sid.NombreEnregistrementsNonModifies);
            Assert.Equal(sidAttendues.NombreMunicipalitesImportees, sid.NombreMunicipalitesImportees);

            mockDepotImportationMunicipalites.Verify(dim => dim.LireMunicipalites(), Times.Once);
            mockDepotMunicipalites.Verify(dm => dm.AjouterMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Exactly(sidAttendues.NombreEnregistrementsAjoutes));
            mockDepotMunicipalites.Verify(dm => dm.ListerMunicipalitesActives(), Times.Once);
            mockDepotMunicipalites.VerifyNoOtherCalls();
        }

        [Fact]
        public void Executer_BD3Municipalites_10Importations_7ElementsAjoutes()
        {
            // Arranger
            Fixture fixture = new Fixture();
            fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
            StatistiquesImportationDonnees sidAttendues = new StatistiquesImportationDonnees()
            {
                NombreEnregistrementsAjoutes = 7,
                NombreEnregistrementsModifies = 0,
                NombreEnregistrementsDesactives = 0,
                NombreEnregistrementsNonModifies = 3,
                NombreMunicipalitesImportees = 10
            };

            List<MunicipaliteEntite> municipalitesAImporter =
            Enumerable.Range(0, sidAttendues.NombreMunicipalitesImportees)
            .Select(i => fixture.Create<MunicipaliteEntite>()).ToList();

            List<MunicipaliteEntite> municipalitesActuelles = new List<MunicipaliteEntite>()
            {
                municipalitesAImporter[2],
                municipalitesAImporter[9],
                municipalitesAImporter[1],
            };

            Mock<IDepotImportationMunicipalites> mockDepotImportationMunicipalites = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportationMunicipalites
            .Setup(dim => dim.LireMunicipalites())
            .Returns(municipalitesAImporter);

            Mock<IDepotMunicipalites> mockDepotMunicipalites = new Mock<IDepotMunicipalites>();
            //mockDepotMunicipalites
            //    .Setup(dm => dm.ChercherMunicipaliteParCodeGeographique(It.IsAny<int>()))
            //    .Returns((int cg) => municipalitesActuelles
            //                             .Where(ma => ma.CodeGeographique == cg)
            //                             .SingleOrDefault()
            //    );
            mockDepotMunicipalites
                .Setup(dm => dm.ListerMunicipalitesActives())
                .Returns(municipalitesActuelles);

            IDepotImportationMunicipalites depotImportationMunicipalites = mockDepotImportationMunicipalites.Object;
            IDepotMunicipalites depotMunicipalites = mockDepotMunicipalites.Object;
            TraitementImporterDonneesMunicipalite tidm = new TraitementImporterDonneesMunicipalite(depotImportationMunicipalites, depotMunicipalites);

            // Agir
            StatistiquesImportationDonnees sid = tidm.Executer();

            // Auditer
            Assert.Equal(sidAttendues.NombreEnregistrementsAjoutes, sid.NombreEnregistrementsAjoutes);
            Assert.Equal(sidAttendues.NombreEnregistrementsDesactives, sid.NombreEnregistrementsDesactives);
            Assert.Equal(sidAttendues.NombreEnregistrementsModifies, sid.NombreEnregistrementsModifies);
            Assert.Equal(sidAttendues.NombreEnregistrementsNonModifies, sid.NombreEnregistrementsNonModifies);
            Assert.Equal(sidAttendues.NombreMunicipalitesImportees, sid.NombreMunicipalitesImportees);

            mockDepotImportationMunicipalites.Verify(dim => dim.LireMunicipalites(), Times.Once);
            mockDepotMunicipalites.Verify(dm => dm.AjouterMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Exactly(sidAttendues.NombreEnregistrementsAjoutes));
            mockDepotMunicipalites.Verify(dm => dm.ListerMunicipalitesActives(), Times.Once);
            mockDepotMunicipalites.VerifyNoOtherCalls();
        }

        [Fact]
        public void Executer_BD5Municipalites_10Importations_7ElementsAjoutes2Desactives()
        {
            // Arranger
            Fixture fixture = new Fixture();
            fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
            StatistiquesImportationDonnees sidAttendues = new StatistiquesImportationDonnees()
            {
                NombreEnregistrementsAjoutes = 7,
                NombreEnregistrementsModifies = 0,
                NombreEnregistrementsDesactives = 2,
                NombreEnregistrementsNonModifies = 3,
                NombreMunicipalitesImportees = 10
            };

            List<MunicipaliteEntite> municipalitesAImporter =
            Enumerable.Range(0, sidAttendues.NombreMunicipalitesImportees)
                .Select(i => fixture.Create<MunicipaliteEntite>())
                .ToList();

            List<MunicipaliteEntite> municipalitesActuelles = new List<MunicipaliteEntite>()
            {
                municipalitesAImporter[2],
                municipalitesAImporter[9],
                municipalitesAImporter[1],
                fixture.Create<MunicipaliteEntite>(),
                fixture.Create<MunicipaliteEntite>()
            };

            Mock<IDepotImportationMunicipalites> mockDepotImportationMunicipalites = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportationMunicipalites
            .Setup(dim => dim.LireMunicipalites())
            .Returns(municipalitesAImporter);

            Mock<IDepotMunicipalites> mockDepotMunicipalites = new Mock<IDepotMunicipalites>();
            //mockDepotMunicipalites
            //.Setup(dm => dm.ChercherMunicipaliteParCodeGeographique(It.IsAny<int>()))
            //.Returns((int cg) => municipalitesActuelles
            //.Where(ma => ma.CodeGeographique == cg)
            //.SingleOrDefault()
            //);
            mockDepotMunicipalites
                .Setup(dm => dm.ListerMunicipalitesActives())
                .Returns(municipalitesActuelles);

            IDepotImportationMunicipalites depotImportationMunicipalites = mockDepotImportationMunicipalites.Object;
            IDepotMunicipalites depotMunicipalites = mockDepotMunicipalites.Object;
            TraitementImporterDonneesMunicipalite tidm = new TraitementImporterDonneesMunicipalite(depotImportationMunicipalites, depotMunicipalites);

            // Agir
            StatistiquesImportationDonnees sid = tidm.Executer();

            // Auditer
            Assert.Equal(sidAttendues.NombreEnregistrementsAjoutes, sid.NombreEnregistrementsAjoutes);
            Assert.Equal(sidAttendues.NombreEnregistrementsDesactives, sid.NombreEnregistrementsDesactives);
            Assert.Equal(sidAttendues.NombreEnregistrementsModifies, sid.NombreEnregistrementsModifies);
            Assert.Equal(sidAttendues.NombreEnregistrementsNonModifies, sid.NombreEnregistrementsNonModifies);
            Assert.Equal(sidAttendues.NombreMunicipalitesImportees, sid.NombreMunicipalitesImportees);

            mockDepotImportationMunicipalites.Verify(dim => dim.LireMunicipalites(), Times.Once);
            mockDepotMunicipalites.Verify(dm => dm.AjouterMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Exactly(sidAttendues.NombreEnregistrementsAjoutes));
            mockDepotMunicipalites.Verify(dm => dm.DesactiverMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Exactly(sidAttendues.NombreEnregistrementsDesactives));
            mockDepotMunicipalites.Verify(dm => dm.ListerMunicipalitesActives(), Times.Once);
            mockDepotMunicipalites.VerifyNoOtherCalls();
        }

        [Fact]
        public void Executer_BD6Municipalites_10Importations_6ElementsAjoutes1Modifies2Desactives()
        {
            // Arranger
            Fixture fixture = new Fixture();
            fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
            StatistiquesImportationDonnees sidAttendues = new StatistiquesImportationDonnees()
            {
                NombreEnregistrementsAjoutes = 6,
                NombreEnregistrementsModifies = 1,
                NombreEnregistrementsDesactives = 2,
                NombreEnregistrementsNonModifies = 3,
                NombreMunicipalitesImportees = 10
            };

            List<MunicipaliteEntite> municipalitesAImporter =
            Enumerable.Range(0, sidAttendues.NombreMunicipalitesImportees)
                .Select(i => fixture.Create<MunicipaliteEntite>())
                .ToList();

            List<MunicipaliteEntite> municipalitesActuelles = new List<MunicipaliteEntite>()
            {
                municipalitesAImporter[2],
                municipalitesAImporter[0],
                municipalitesAImporter[9],
                new MunicipaliteEntite(
                    municipalitesAImporter[1].CodeGeographique,
                    municipalitesAImporter[1].NomMunicipalite,
                    municipalitesAImporter[1].AdresseCourriel,
                    municipalitesAImporter[1].AdresseWeb,
                    municipalitesAImporter[1].DateProchaineElection?.AddDays(10)
                ),
                fixture.Create<MunicipaliteEntite>(),
                fixture.Create<MunicipaliteEntite>()
            };

            Mock<IDepotImportationMunicipalites> mockDepotImportationMunicipalites = new Mock<IDepotImportationMunicipalites>();
            mockDepotImportationMunicipalites
            .Setup(dim => dim.LireMunicipalites())
            .Returns(municipalitesAImporter);

            Mock<IDepotMunicipalites> mockDepotMunicipalites = new Mock<IDepotMunicipalites>();
            //mockDepotMunicipalites
            //.Setup(dm => dm.ChercherMunicipaliteParCodeGeographique(It.IsAny<int>()))
            //.Returns((int cg) => municipalitesActuelles
            //.Where(ma => ma.CodeGeographique == cg)
            //.SingleOrDefault()
            //);
            mockDepotMunicipalites
                .Setup(dm => dm.ListerMunicipalitesActives())
                .Returns(municipalitesActuelles);

            IDepotImportationMunicipalites depotImportationMunicipalites = mockDepotImportationMunicipalites.Object;
            IDepotMunicipalites depotMunicipalites = mockDepotMunicipalites.Object;
            TraitementImporterDonneesMunicipalite tidm = new TraitementImporterDonneesMunicipalite(depotImportationMunicipalites, depotMunicipalites);

            // Agir
            StatistiquesImportationDonnees sid = tidm.Executer();

            // Auditer
            Assert.Equal(sidAttendues.NombreEnregistrementsAjoutes, sid.NombreEnregistrementsAjoutes);
            Assert.Equal(sidAttendues.NombreEnregistrementsDesactives, sid.NombreEnregistrementsDesactives);
            Assert.Equal(sidAttendues.NombreEnregistrementsModifies, sid.NombreEnregistrementsModifies);
            Assert.Equal(sidAttendues.NombreEnregistrementsNonModifies, sid.NombreEnregistrementsNonModifies);
            Assert.Equal(sidAttendues.NombreMunicipalitesImportees, sid.NombreMunicipalitesImportees);

            mockDepotImportationMunicipalites.Verify(dim => dim.LireMunicipalites(), Times.Once);
            mockDepotMunicipalites.Verify(dm => dm.AjouterMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Exactly(sidAttendues.NombreEnregistrementsAjoutes));
            mockDepotMunicipalites.Verify(dm => dm.DesactiverMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Exactly(sidAttendues.NombreEnregistrementsDesactives));
            mockDepotMunicipalites.Verify(dm => dm.ListerMunicipalitesActives(), Times.Once);
            mockDepotMunicipalites.Verify(dm => dm.MAJMunicipalite(It.IsAny<MunicipaliteEntite>()), Times.Once);
            mockDepotMunicipalites.VerifyNoOtherCalls();
        }
    }
}
