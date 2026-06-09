using Microsoft.EntityFrameworkCore;
using Normative.Models;
using Normative.Models.Config;
using Normative.Models.Table;

namespace Normative.Data;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {




        // ─────────────────────────────────────────
        // ProductLine – výrobní linky
        // ─────────────────────────────────────────
        modelBuilder.Entity<ProductLine>().HasData(
            new ProductLine { ProductLineId = 1, Name = "Potrubí", IsDeleted = false },
            new ProductLine { ProductLineId = 2, Name = "Nádoby", IsDeleted = false },
            new ProductLine { ProductLineId = 3, Name = "Výměníky tepla", IsDeleted = false }
        );

        // ─────────────────────────────────────────
        // ProductType – typy výrobků
        // ─────────────────────────────────────────
        modelBuilder.Entity<ProductType>().HasData(
            new ProductType { ProductTypeId = 1, Name = "Standardní", IsDeleted = false },
            new ProductType { ProductTypeId = 2, Name = "Tlakový", IsDeleted = false },
            new ProductType { ProductTypeId = 3, Name = "Nerezový", IsDeleted = false }
        );

        // ─────────────────────────────────────────
        // ProductSize – velikosti / dimenze
        // ─────────────────────────────────────────
        modelBuilder.Entity<ProductSize>().HasData(
            new ProductSize { ProductSizeId = 1, Name = "DN 50", Size = "DN50  / PN16", IsDeleted = false },
            new ProductSize { ProductSizeId = 2, Name = "DN 100", Size = "DN100 / PN16", IsDeleted = false },
            new ProductSize { ProductSizeId = 3, Name = "DN 200", Size = "DN200 / PN25", IsDeleted = false },
            new ProductSize { ProductSizeId = 4, Name = "DN 300", Size = "DN300 / PN25", IsDeleted = false },
            new ProductSize { ProductSizeId = 5, Name = "DN 500", Size = "DN500 / PN40", IsDeleted = false }
        );

        // ─────────────────────────────────────────
        // PreparationType – typy příprav
        // ─────────────────────────────────────────
        modelBuilder.Entity<PreparationType>().HasData(
            new PreparationType { Id = 1, Name = "Příprava materiálu", IsDeleted = false },
            new PreparationType { Id = 2, Name = "Příprava svarových ploch", IsDeleted = false },
            new PreparationType { Id = 3, Name = "Příprava povrchu", IsDeleted = false },
            new PreparationType { Id = 4, Name = "Příprava přírub", IsDeleted = false }
        );

        // ─────────────────────────────────────────
        // Preparation – časy přípravy [hod] per velikost
        //   Fitter  = hodiny montéra
        //   Welder  = hodiny svářeče
        //   Platí:  čím větší DN, tím více času
        // ─────────────────────────────────────────
        modelBuilder.Entity<Preparation>().HasData(
            // --- Příprava materiálu ---
            new Preparation { Id = 1, ProductSizeId = 1, PreparationTypeId = 1, Fitter = 0.25m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 2, ProductSizeId = 2, PreparationTypeId = 1, Fitter = 0.50m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 3, ProductSizeId = 3, PreparationTypeId = 1, Fitter = 1.00m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 4, ProductSizeId = 4, PreparationTypeId = 1, Fitter = 1.50m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 5, ProductSizeId = 5, PreparationTypeId = 1, Fitter = 2.50m, Welder = 0.00m, IsDeleted = false },
            // --- Příprava svarových ploch ---
            new Preparation { Id = 6, ProductSizeId = 1, PreparationTypeId = 2, Fitter = 0.25m, Welder = 0.25m, IsDeleted = false },
            new Preparation { Id = 7, ProductSizeId = 2, PreparationTypeId = 2, Fitter = 0.50m, Welder = 0.50m, IsDeleted = false },
            new Preparation { Id = 8, ProductSizeId = 3, PreparationTypeId = 2, Fitter = 0.75m, Welder = 0.75m, IsDeleted = false },
            new Preparation { Id = 9, ProductSizeId = 4, PreparationTypeId = 2, Fitter = 1.00m, Welder = 1.00m, IsDeleted = false },
            new Preparation { Id = 10, ProductSizeId = 5, PreparationTypeId = 2, Fitter = 1.50m, Welder = 1.50m, IsDeleted = false },
            // --- Příprava povrchu ---
            new Preparation { Id = 11, ProductSizeId = 1, PreparationTypeId = 3, Fitter = 0.50m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 12, ProductSizeId = 2, PreparationTypeId = 3, Fitter = 0.75m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 13, ProductSizeId = 3, PreparationTypeId = 3, Fitter = 1.25m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 14, ProductSizeId = 4, PreparationTypeId = 3, Fitter = 2.00m, Welder = 0.00m, IsDeleted = false },
            new Preparation { Id = 15, ProductSizeId = 5, PreparationTypeId = 3, Fitter = 3.00m, Welder = 0.00m, IsDeleted = false },
            // --- Příprava přírub ---
            new Preparation { Id = 16, ProductSizeId = 1, PreparationTypeId = 4, Fitter = 0.50m, Welder = 0.50m, IsDeleted = false },
            new Preparation { Id = 17, ProductSizeId = 2, PreparationTypeId = 4, Fitter = 0.75m, Welder = 0.75m, IsDeleted = false },
            new Preparation { Id = 18, ProductSizeId = 3, PreparationTypeId = 4, Fitter = 1.25m, Welder = 1.25m, IsDeleted = false },
            new Preparation { Id = 19, ProductSizeId = 4, PreparationTypeId = 4, Fitter = 2.00m, Welder = 2.00m, IsDeleted = false },
            new Preparation { Id = 20, ProductSizeId = 5, PreparationTypeId = 4, Fitter = 3.50m, Welder = 3.50m, IsDeleted = false }
        );

        // ─────────────────────────────────────────
        // Operation – operace (hlavičky)
        //   Každá operace patří k jedné ProductLine + ProductType
        // ─────────────────────────────────────────
        modelBuilder.Entity<Operation>().HasData(
            // Potrubí / Standardní
            new Operation
            {
                OperationId = 1,
                ProductLineId = 1,
                ProductTypeId = 1,
                OperationNumber = 10,
                OperationDescription = "Řezání a tvarování potrubí",
                WorkCenter = "WC-10",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 2,
                ProductLineId = 1,
                ProductTypeId = 1,
                OperationNumber = 20,
                OperationDescription = "Svařování potrubí",
                WorkCenter = "WC-20",
                IsDeleted = false
            },
            // Potrubí / Tlakový
            new Operation
            {
                OperationId = 3,
                ProductLineId = 1,
                ProductTypeId = 2,
                OperationNumber = 10,
                OperationDescription = "Řezání tlakového potrubí",
                WorkCenter = "WC-10",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 4,
                ProductLineId = 1,
                ProductTypeId = 2,
                OperationNumber = 20,
                OperationDescription = "Svařování tlakového potrubí",
                WorkCenter = "WC-20",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 5,
                ProductLineId = 1,
                ProductTypeId = 2,
                OperationNumber = 30,
                OperationDescription = "Tlaková zkouška",
                WorkCenter = "WC-30",
                IsDeleted = false
            },
            // Nádoby / Tlakový
            new Operation
            {
                OperationId = 6,
                ProductLineId = 2,
                ProductTypeId = 2,
                OperationNumber = 10,
                OperationDescription = "Příprava pláště nádoby",
                WorkCenter = "WC-10",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 7,
                ProductLineId = 2,
                ProductTypeId = 2,
                OperationNumber = 20,
                OperationDescription = "Svařování dna a víka",
                WorkCenter = "WC-20",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 8,
                ProductLineId = 2,
                ProductTypeId = 2,
                OperationNumber = 40,
                OperationDescription = "Kontrola a certifikace",
                WorkCenter = "WC-40",
                IsDeleted = false
            },
            // Nádoby / Nerezový
            new Operation
            {
                OperationId = 9,
                ProductLineId = 2,
                ProductTypeId = 3,
                OperationNumber = 10,
                OperationDescription = "Příprava nerezového pláště",
                WorkCenter = "WC-10",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 10,
                ProductLineId = 2,
                ProductTypeId = 3,
                OperationNumber = 20,
                OperationDescription = "TIG svařování nerezových dílů",
                WorkCenter = "WC-20",
                IsDeleted = false
            },
            // Výměníky tepla / Tlakový
            new Operation
            {
                OperationId = 11,
                ProductLineId = 3,
                ProductTypeId = 2,
                OperationNumber = 10,
                OperationDescription = "Příprava trubkových svazků",
                WorkCenter = "WC-10",
                IsDeleted = false
            },
            new Operation
            {
                OperationId = 12,
                ProductLineId = 3,
                ProductTypeId = 2,
                OperationNumber = 20,
                OperationDescription = "Montáž trubkového svazku do pláště",
                WorkCenter = "WC-20",
                IsDeleted = false
            }
        );

        // ─────────────────────────────────────────
        // OperationStep – konkrétní kroky operací
        //   StandardHour = normočas v minutách
        //   Sequence     = pořadí kroku v operaci
        // ─────────────────────────────────────────
        modelBuilder.Entity<OperationStep>().HasData(

            // ── Op 1: Řezání a tvarování potrubí ──────────────────────────────
            new OperationStep { OperationStepId = 1, OperationId = 1, ProductSizeId = 1, Sequence = 1, Name = "Odměření a označení", DrawingPosition = "P-01", StandardHour = 5, Diameter = "DN50", IsDeleted = false },
            new OperationStep { OperationStepId = 2, OperationId = 1, ProductSizeId = 1, Sequence = 2, Name = "Řezání pásovou pilou", DrawingPosition = "P-02", StandardHour = 10, Diameter = "DN50", IsDeleted = false },
            new OperationStep { OperationStepId = 3, OperationId = 1, ProductSizeId = 2, Sequence = 1, Name = "Odměření a označení", DrawingPosition = "P-01", StandardHour = 7, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 4, OperationId = 1, ProductSizeId = 2, Sequence = 2, Name = "Řezání pásovou pilou", DrawingPosition = "P-02", StandardHour = 15, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 5, OperationId = 1, ProductSizeId = 3, Sequence = 1, Name = "Odměření a označení", DrawingPosition = "P-01", StandardHour = 10, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 6, OperationId = 1, ProductSizeId = 3, Sequence = 2, Name = "Plasma řezání", DrawingPosition = "P-02", StandardHour = 25, Diameter = "DN200", IsDeleted = false },

            // ── Op 2: Svařování potrubí ────────────────────────────────────────
            new OperationStep { OperationStepId = 7, OperationId = 2, ProductSizeId = 1, Sequence = 1, Name = "Stehování", DrawingPosition = "S-01", StandardHour = 15, Diameter = "DN50", IsDeleted = false },
            new OperationStep { OperationStepId = 8, OperationId = 2, ProductSizeId = 1, Sequence = 2, Name = "Svařování MIG", DrawingPosition = "S-02", StandardHour = 30, Diameter = "DN50", IsDeleted = false },
            new OperationStep { OperationStepId = 9, OperationId = 2, ProductSizeId = 2, Sequence = 1, Name = "Stehování", DrawingPosition = "S-01", StandardHour = 20, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 10, OperationId = 2, ProductSizeId = 2, Sequence = 2, Name = "Svařování MIG", DrawingPosition = "S-02", StandardHour = 50, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 11, OperationId = 2, ProductSizeId = 3, Sequence = 1, Name = "Stehování", DrawingPosition = "S-01", StandardHour = 30, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 12, OperationId = 2, ProductSizeId = 3, Sequence = 2, Name = "Svařování MIG", DrawingPosition = "S-02", StandardHour = 80, Diameter = "DN200", IsDeleted = false },

            // ── Op 3: Řezání tlakového potrubí ───────────────────────────────
            new OperationStep { OperationStepId = 13, OperationId = 3, ProductSizeId = 2, Sequence = 1, Name = "Kontrola materiálu", DrawingPosition = "T-01", StandardHour = 5, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 14, OperationId = 3, ProductSizeId = 2, Sequence = 2, Name = "Řezání + zkosení", DrawingPosition = "T-02", StandardHour = 20, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 15, OperationId = 3, ProductSizeId = 4, Sequence = 1, Name = "Kontrola materiálu", DrawingPosition = "T-01", StandardHour = 8, Diameter = "DN300", IsDeleted = false },
            new OperationStep { OperationStepId = 16, OperationId = 3, ProductSizeId = 4, Sequence = 2, Name = "Plasma řezání + zkosení", DrawingPosition = "T-02", StandardHour = 35, Diameter = "DN300", IsDeleted = false },

            // ── Op 4: Svařování tlakového potrubí ────────────────────────────
            new OperationStep { OperationStepId = 17, OperationId = 4, ProductSizeId = 2, Sequence = 1, Name = "Stehování", DrawingPosition = "S-01", StandardHour = 25, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 18, OperationId = 4, ProductSizeId = 2, Sequence = 2, Name = "Kořenový svar TIG", DrawingPosition = "S-02", StandardHour = 40, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 19, OperationId = 4, ProductSizeId = 2, Sequence = 3, Name = "Výplňové vrstvy MIG", DrawingPosition = "S-03", StandardHour = 60, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 20, OperationId = 4, ProductSizeId = 4, Sequence = 1, Name = "Stehování", DrawingPosition = "S-01", StandardHour = 40, Diameter = "DN300", IsDeleted = false },
            new OperationStep { OperationStepId = 21, OperationId = 4, ProductSizeId = 4, Sequence = 2, Name = "Kořenový svar TIG", DrawingPosition = "S-02", StandardHour = 70, Diameter = "DN300", IsDeleted = false },
            new OperationStep { OperationStepId = 22, OperationId = 4, ProductSizeId = 4, Sequence = 3, Name = "Výplňové vrstvy MIG", DrawingPosition = "S-03", StandardHour = 110, Diameter = "DN300", IsDeleted = false },

            // ── Op 5: Tlaková zkouška ─────────────────────────────────────────
            new OperationStep { OperationStepId = 23, OperationId = 5, ProductSizeId = 2, Sequence = 1, Name = "Instalace přístrojů", DrawingPosition = "Z-01", StandardHour = 15, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 24, OperationId = 5, ProductSizeId = 2, Sequence = 2, Name = "Tlaková zkouška 1.5x PN", DrawingPosition = "Z-02", StandardHour = 30, Diameter = "DN100", IsDeleted = false },
            new OperationStep { OperationStepId = 25, OperationId = 5, ProductSizeId = 4, Sequence = 1, Name = "Instalace přístrojů", DrawingPosition = "Z-01", StandardHour = 20, Diameter = "DN300", IsDeleted = false },
            new OperationStep { OperationStepId = 26, OperationId = 5, ProductSizeId = 4, Sequence = 2, Name = "Tlaková zkouška 1.5x PN", DrawingPosition = "Z-02", StandardHour = 45, Diameter = "DN300", IsDeleted = false },

            // ── Op 6: Příprava pláště nádoby ─────────────────────────────────
            new OperationStep { OperationStepId = 27, OperationId = 6, ProductSizeId = 3, Sequence = 1, Name = "Válcování plechu", DrawingPosition = "N-01", StandardHour = 60, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 28, OperationId = 6, ProductSizeId = 3, Sequence = 2, Name = "Podélný svar pláště", DrawingPosition = "N-02", StandardHour = 90, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 29, OperationId = 6, ProductSizeId = 5, Sequence = 1, Name = "Válcování plechu", DrawingPosition = "N-01", StandardHour = 120, Diameter = "DN500", IsDeleted = false },
            new OperationStep { OperationStepId = 30, OperationId = 6, ProductSizeId = 5, Sequence = 2, Name = "Podélný svar pláště", DrawingPosition = "N-02", StandardHour = 180, Diameter = "DN500", IsDeleted = false },

            // ── Op 7: Svařování dna a víka ────────────────────────────────────
            new OperationStep { OperationStepId = 31, OperationId = 7, ProductSizeId = 3, Sequence = 1, Name = "Přivaření eliptického dna", DrawingPosition = "D-01", StandardHour = 80, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 32, OperationId = 7, ProductSizeId = 3, Sequence = 2, Name = "Přivaření hrdel", DrawingPosition = "D-02", StandardHour = 50, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 33, OperationId = 7, ProductSizeId = 5, Sequence = 1, Name = "Přivaření eliptického dna", DrawingPosition = "D-01", StandardHour = 150, Diameter = "DN500", IsDeleted = false },
            new OperationStep { OperationStepId = 34, OperationId = 7, ProductSizeId = 5, Sequence = 2, Name = "Přivaření hrdel", DrawingPosition = "D-02", StandardHour = 100, Diameter = "DN500", IsDeleted = false },

            // ── Op 8: Kontrola a certifikace ─────────────────────────────────
            new OperationStep { OperationStepId = 35, OperationId = 8, ProductSizeId = 3, Sequence = 1, Name = "RT / UT kontrola svarů", DrawingPosition = "K-01", StandardHour = 60, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 36, OperationId = 8, ProductSizeId = 3, Sequence = 2, Name = "Tlaková zkouška nádoby", DrawingPosition = "K-02", StandardHour = 90, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 37, OperationId = 8, ProductSizeId = 5, Sequence = 1, Name = "RT / UT kontrola svarů", DrawingPosition = "K-01", StandardHour = 120, Diameter = "DN500", IsDeleted = false },
            new OperationStep { OperationStepId = 38, OperationId = 8, ProductSizeId = 5, Sequence = 2, Name = "Tlaková zkouška nádoby", DrawingPosition = "K-02", StandardHour = 180, Diameter = "DN500", IsDeleted = false },

            // ── Op 9: Příprava nerezového pláště ─────────────────────────────
            new OperationStep { OperationStepId = 39, OperationId = 9, ProductSizeId = 3, Sequence = 1, Name = "Přesné řezání nerez", DrawingPosition = "NR-01", StandardHour = 45, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 40, OperationId = 9, ProductSizeId = 3, Sequence = 2, Name = "Moření a pasivace", DrawingPosition = "NR-02", StandardHour = 30, Diameter = "DN200", IsDeleted = false },

            // ── Op 10: TIG svařování nerezových dílů ─────────────────────────
            new OperationStep { OperationStepId = 41, OperationId = 10, ProductSizeId = 3, Sequence = 1, Name = "TIG kořenový svar", DrawingPosition = "NR-03", StandardHour = 60, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 42, OperationId = 10, ProductSizeId = 3, Sequence = 2, Name = "TIG výplňové vrstvy", DrawingPosition = "NR-04", StandardHour = 90, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 43, OperationId = 10, ProductSizeId = 3, Sequence = 3, Name = "Vizuální + rozměrová kontrola", DrawingPosition = "NR-05", StandardHour = 20, Diameter = "DN200", IsDeleted = false },

            // ── Op 11: Příprava trubkových svazků ────────────────────────────
            new OperationStep { OperationStepId = 44, OperationId = 11, ProductSizeId = 1, Sequence = 1, Name = "Řezání trubek na délku", DrawingPosition = "VS-01", StandardHour = 30, Diameter = "DN50", IsDeleted = false },
            new OperationStep { OperationStepId = 45, OperationId = 11, ProductSizeId = 1, Sequence = 2, Name = "Rozvrtání trubkovnice", DrawingPosition = "VS-02", StandardHour = 60, Diameter = "DN50", IsDeleted = false },
            new OperationStep { OperationStepId = 46, OperationId = 11, ProductSizeId = 1, Sequence = 3, Name = "Zaválcování trubek", DrawingPosition = "VS-03", StandardHour = 90, Diameter = "DN50", IsDeleted = false },

            // ── Op 12: Montáž trubkového svazku do pláště ────────────────────
            new OperationStep { OperationStepId = 47, OperationId = 12, ProductSizeId = 3, Sequence = 1, Name = "Zasunutí svazku do pláště", DrawingPosition = "VT-01", StandardHour = 45, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 48, OperationId = 12, ProductSizeId = 3, Sequence = 2, Name = "Přivaření trubkovnice", DrawingPosition = "VT-02", StandardHour = 120, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 49, OperationId = 12, ProductSizeId = 3, Sequence = 3, Name = "Montáž přírub a hrdel", DrawingPosition = "VT-03", StandardHour = 60, Diameter = "DN200", IsDeleted = false },
            new OperationStep { OperationStepId = 50, OperationId = 12, ProductSizeId = 3, Sequence = 4, Name = "Těsnostní zkouška výměníku", DrawingPosition = "VT-04", StandardHour = 90, Diameter = "DN200", IsDeleted = false }
        );
    }
}
