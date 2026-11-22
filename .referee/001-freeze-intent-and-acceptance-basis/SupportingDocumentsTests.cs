using System.Text.RegularExpressions;
using Xunit;

namespace Referee.Spec001;

/// <summary>
/// Verifies the supporting documentation deliverables: plan.md, data-model.md,
/// quickstart.md, and research.md.
/// Tasks: T003, T005, T032–T037.
/// </summary>
public class SupportingDocumentsTests
{
    // ── plan.md ───────────────────────────────────────────────────────────────

    [Fact(DisplayName = "plan.md must exist")]
    public void PlanMd_Exists()
    {
        Assert.True(File.Exists(Paths.PlanMd),
            "plan.md must exist as a required deliverable of spec 001.");
    }

    [Fact(DisplayName = "plan.md Constitution Check table names all five principles")]
    public void PlanMd_ConstitutionCheck_NamesAllFivePrinciples()
    {
        var text = File.ReadAllText(Paths.PlanMd);
        Assert.Contains("Behavioral Fidelity", text);
        Assert.Contains("Parity Testing", text);
        Assert.Contains("Source Traceability", text);
        Assert.Contains("Target Stack Constraints", text);
        Assert.Contains("No Scope Creep", text);
    }

    [Fact(DisplayName = "plan.md Constitution Check table shows PASS for every principle")]
    public void PlanMd_ConstitutionCheck_AllFivePrinciplesPass()
    {
        var text = File.ReadAllText(Paths.PlanMd);
        // Expect at least five PASS entries — one per principle.
        var passCount = Regex.Matches(text, @"\bPASS\b").Count;
        Assert.True(passCount >= 5,
            $"plan.md Constitution Check table must show PASS for all five principles; " +
            $"found {passCount} occurrence(s) of 'PASS'.");
    }

    [Fact(DisplayName = "plan.md records .NET 8 LTS as the target framework")]
    public void PlanMd_RecordsTargetFramework()
    {
        var text = File.ReadAllText(Paths.PlanMd);
        Assert.True(
            text.Contains(".NET 8") || text.Contains("net8"),
            "plan.md must record .NET 8 LTS as the target framework.");
    }

    [Fact(DisplayName = "plan.md records xUnit as the testing framework")]
    public void PlanMd_RecordsXUnit()
    {
        var text = File.ReadAllText(Paths.PlanMd);
        Assert.True(
            text.Contains("xUnit") || text.Contains("xunit"),
            "plan.md must record xUnit as the testing framework (required by constitution principle II).");
    }

    // ── data-model.md ─────────────────────────────────────────────────────────

    [Fact(DisplayName = "data-model.md must exist")]
    public void DataModelMd_Exists()
    {
        Assert.True(File.Exists(Paths.DataModelMd),
            "data-model.md must exist as a required deliverable of spec 001.");
    }

    [Theory(DisplayName = "data-model.md contains each domain entity")]
    [InlineData("MenuItem")]
    [InlineData("TimingPreference")]
    [InlineData("PaymentMethod")]
    [InlineData("ReviewRating")]
    [InlineData("OrderStatus")]
    [InlineData("OrderPlacement")]
    [InlineData("PaymentOptions")]
    [InlineData("CustomerReview")]
    public void DataModelMd_ContainsEntity(string entity)
    {
        var text = File.ReadAllText(Paths.DataModelMd);
        Assert.True(text.Contains(entity),
            $"data-model.md must document the '{entity}' entity.");
    }

    [Fact(DisplayName = "data-model.md maps entities to spec.md source citations")]
    public void DataModelMd_ContainsSourceCitations()
    {
        var text = File.ReadAllText(Paths.DataModelMd);
        // At least one commit-hash fragment must appear, confirming traceability.
        Assert.True(
            text.Contains("c9ba3a61329a") ||  // CoffeeShopMenu
            text.Contains("a10c504e0538") ||  // OrderStatus initial
            text.Contains("050a6c51f1fd") ||  // OrderPlacement G1
            text.Contains("29e7ac8a9646"),    // CustomerReview
            "data-model.md must include at least one source citation from spec.md.");
    }

    // ── quickstart.md ─────────────────────────────────────────────────────────

    [Fact(DisplayName = "quickstart.md must exist")]
    public void QuickstartMd_Exists()
    {
        Assert.True(File.Exists(Paths.QuickstartMd),
            "quickstart.md must exist as a required deliverable of spec 001.");
    }

    [Fact(DisplayName = "quickstart.md contains validation scenarios for each user story")]
    public void QuickstartMd_ContainsValidationScenarios()
    {
        var text = File.ReadAllText(Paths.QuickstartMd);
        Assert.True(
            text.Contains("Scenario", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("Validation", StringComparison.OrdinalIgnoreCase),
            "quickstart.md must contain validation scenarios for the user stories.");
    }

    [Fact(DisplayName = "quickstart.md contains a 'Done When' completion checklist")]
    public void QuickstartMd_ContainsDoneWhenChecklist()
    {
        var text = File.ReadAllText(Paths.QuickstartMd);
        Assert.True(
            text.Contains("Done When", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("done when", StringComparison.OrdinalIgnoreCase),
            "quickstart.md must contain a 'Done When' section to mark spec 001 as complete.");
    }

    [Fact(DisplayName = "quickstart.md covers the source language reconciliation scenario")]
    public void QuickstartMd_CoversSourceLanguageReconciliation()
    {
        var text = File.ReadAllText(Paths.QuickstartMd);
        Assert.True(
            text.Contains("Java/Swing", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("swift", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("Swing", StringComparison.OrdinalIgnoreCase),
            "quickstart.md must include a scenario validating the Java/Swing reconciliation.");
    }

    // ── research.md ───────────────────────────────────────────────────────────

    [Fact(DisplayName = "research.md must exist")]
    public void ResearchMd_Exists()
    {
        Assert.True(File.Exists(Paths.ResearchMd),
            "research.md must exist as a required deliverable of spec 001.");
    }

    [Fact(DisplayName = "research.md references the legacy evidence source (brd.md or old/docs)")]
    public void ResearchMd_ReferencesLegacyEvidence()
    {
        var text = File.ReadAllText(Paths.ResearchMd);
        Assert.True(
            text.Contains("brd.md", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("old/docs", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("CoffeeShopMenu", StringComparison.OrdinalIgnoreCase),
            "research.md must reference the legacy evidence files (brd.md / old/docs).");
    }
}
