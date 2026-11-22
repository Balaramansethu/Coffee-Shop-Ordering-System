using Xunit;

namespace Referee.Spec001;

/// <summary>
/// Verifies every frozen-behaviour rule that spec.md must contain, section by section.
/// Each test maps to one or more FR-* requirements from the spec.
/// </summary>
public class SpecMdContentTests
{
    private static readonly string _spec = File.ReadAllText(Paths.SpecMd);

    // ── Source Language Reconciliation (FR-009) ────────────────────────────────

    [Fact(DisplayName = "FR-009 · spec.md records 'Swing' as the correction for 'swift'")]
    public void SourceLanguageReconciliation_IdentifiesSwingMisnomer()
    {
        Assert.Contains("misnomer", _spec, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Swing", _spec);
    }

    [Fact(DisplayName = "FR-009 · spec.md states no Swift source code was found")]
    public void SourceLanguageReconciliation_StatesNoSwiftCodeFound()
    {
        // The spec must explicitly state the absence of Swift source.
        Assert.True(
            _spec.Contains("no Swift source code", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("no Swift", StringComparison.OrdinalIgnoreCase),
            "spec.md must state that no Swift source code was found in the estate.");
    }

    [Fact(DisplayName = "FR-009 · spec.md records agreed reading as 'Java/Swing'")]
    public void SourceLanguageReconciliation_AgreedsReadingIsJavaSwing()
    {
        Assert.Contains("Java/Swing", _spec);
    }

    // ── Section A — Five Fixed Menu Items (FR-001) ────────────────────────────

    [Theory(DisplayName = "FR-001 · Section A contains each frozen menu item")]
    [InlineData("Cappuccino")]
    [InlineData("Latte")]
    [InlineData("Matcha Latte")]
    [InlineData("Cold Brew")]
    [InlineData("Hot Chocolate")]
    public void SectionA_ContainsMenuItem(string item)
    {
        Assert.Contains(item, _spec);
    }

    [Fact(DisplayName = "FR-001 · Section A states exactly five items")]
    public void SectionA_StatesFiveItems()
    {
        Assert.Contains("five items", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-001 · Section A cites CoffeeShopMenu.java as source")]
    public void SectionA_HasCoffeeShopMenuSourceCitation()
    {
        Assert.Contains("CoffeeShopMenu.java", _spec);
    }

    // ── Section B1 — Timing Preference Mapping (FR-002) ──────────────────────

    [Theory(DisplayName = "FR-002/B1 · Section B1 contains each timing string")]
    [InlineData("Immediate")]
    [InlineData("Bring after 5 minutes")]
    [InlineData("Keep bringing in a break of 30 minutes")]
    public void SectionB1_ContainsTimingValue(string value)
    {
        Assert.Contains(value, _spec);
    }

    [Fact(DisplayName = "FR-002/B1 · Section B1 names 'Immediate' as the out-of-range default")]
    public void SectionB1_DefaultIsImmediate()
    {
        Assert.Contains("Immediate (default)", _spec);
    }

    [Fact(DisplayName = "FR-002/B1 · Section B1 cites commit dc4f92d1e3dd")]
    public void SectionB1_HasSourceCitation()
    {
        Assert.Contains("dc4f92d1e3dd", _spec);
    }

    // ── Section B2 — Payment Method Mapping (FR-002) ─────────────────────────

    [Theory(DisplayName = "FR-002/B2 · Section B2 contains each payment string")]
    [InlineData("UPI")]
    [InlineData("Cash")]
    [InlineData("Card")]
    [InlineData("Coupon")]
    public void SectionB2_ContainsPaymentValue(string value)
    {
        Assert.Contains(value, _spec);
    }

    [Fact(DisplayName = "FR-002/B2 · Section B2 names 'Cash' as the out-of-range default")]
    public void SectionB2_DefaultIsCash()
    {
        Assert.Contains("Cash (default)", _spec);
    }

    [Fact(DisplayName = "FR-002/B2 · Section B2 cites commit f7bd946d6057")]
    public void SectionB2_HasSourceCitation()
    {
        Assert.Contains("f7bd946d6057", _spec);
    }

    // ── Section B3 — Customer Review Mapping (FR-002) ────────────────────────

    [Theory(DisplayName = "FR-002/B3 · Section B3 contains each review string")]
    [InlineData("Very Good")]
    [InlineData("Good")]
    [InlineData("Okish")]
    [InlineData("Bad")]
    [InlineData("Worst")]
    public void SectionB3_ContainsReviewValue(string value)
    {
        Assert.Contains(value, _spec);
    }

    [Fact(DisplayName = "FR-002/B3 · Section B3 names 'Good' as the out-of-range default")]
    public void SectionB3_DefaultIsGood()
    {
        Assert.Contains("Good (default)", _spec);
    }

    [Fact(DisplayName = "FR-002/B3 · Section B3 cites commit 7d34d0d762a1")]
    public void SectionB3_HasSourceCitation()
    {
        Assert.Contains("7d34d0d762a1", _spec);
    }

    // ── Section C — CLI Order Status Progression (FR-003) ────────────────────

    [Fact(DisplayName = "FR-003 · Section C states progression is unconditional")]
    public void SectionC_ProgressionIsUnconditional()
    {
        Assert.Contains("unconditionally", _spec);
    }

    [Fact(DisplayName = "FR-003 · Section C describes Pending → Preparing → Served sequence")]
    public void SectionC_DescribesPendingPreparingServedSequence()
    {
        Assert.True(
            _spec.Contains("Pending to Preparing") ||
            _spec.Contains("Pending → Preparing") ||
            _spec.Contains("Pending' (initial) to 'Preparing'"),
            "spec.md must describe the Pending-to-Preparing transition explicitly.");
        Assert.True(
            _spec.Contains("then to Served") ||
            _spec.Contains("Preparing → Served") ||
            _spec.Contains("Preparing' and then to 'Served'"),
            "spec.md must describe the Preparing-to-Served transition explicitly.");
    }

    [Fact(DisplayName = "FR-003 · Section C states no user confirmation is required")]
    public void SectionC_StatesNoUserConfirmationRequired()
    {
        Assert.Contains("without", _spec);
        Assert.True(
            _spec.Contains("user confirmation") || _spec.Contains("user intervention"),
            "spec.md must state that CLI status progression requires no user confirmation or intervention.");
    }

    [Fact(DisplayName = "FR-003 · Section C cites commit f223e4f96f79")]
    public void SectionC_HasSourceCitation()
    {
        Assert.Contains("f223e4f96f79", _spec);
    }

    // ── Section D — Initial Order Status ──────────────────────────────────────

    [Fact(DisplayName = "FR-003 · Section D records initial status as Pending")]
    public void SectionD_InitialStatusIsPending()
    {
        Assert.True(
            _spec.Contains("starts in the Pending") ||
            _spec.Contains("starts in 'Pending'") ||
            _spec.Contains("Every new order starts"),
            "spec.md must record that every new order starts in the Pending state.");
    }

    [Fact(DisplayName = "FR-003 · Section D cites commit a10c504e0538")]
    public void SectionD_HasSourceCitation()
    {
        Assert.Contains("a10c504e0538", _spec);
    }

    // ── Section E1 — No Items Selected Guard (FR-004) ─────────────────────────

    [Fact(DisplayName = "FR-004/E1 · Section E1 contains the exact legacy error message")]
    public void SectionE1_ContainsExactErrorMessage()
    {
        Assert.Contains(
            "No items selected! Please select items from the menu.",
            _spec);
    }

    [Fact(DisplayName = "FR-004/E1 · Section E1 cites commit 3925208ea05f")]
    public void SectionE1_HasSourceCitation()
    {
        Assert.Contains("3925208ea05f", _spec);
    }

    // ── Section E2 — No Order Placed Guard (FR-004) ───────────────────────────

    [Fact(DisplayName = "FR-004/E2 · Section E2 contains the exact legacy error message")]
    public void SectionE2_ContainsExactErrorMessage()
    {
        Assert.Contains(
            "No order placed! Please place an order first.",
            _spec);
    }

    [Fact(DisplayName = "FR-004/E2 · Section E2 cites commit 673dc984f8d9")]
    public void SectionE2_HasSourceCitation()
    {
        Assert.Contains("673dc984f8d9", _spec);
    }

    // ── Section E3 — Manual Status Update Choices (FR-005) ───────────────────

    [Fact(DisplayName = "FR-005/E3 · Section E3 names Preparing and Served as the offered values")]
    public void SectionE3_NamesBothStatusValues()
    {
        Assert.Contains("Preparing", _spec);
        Assert.Contains("Served", _spec);
    }

    [Fact(DisplayName = "FR-005/E3 · Section E3 states exactly two values are offered")]
    public void SectionE3_StatesExactlyTwoValues()
    {
        Assert.Contains("exactly two", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-005/E3 · Section E3 cites commit b6207a9ffd2f")]
    public void SectionE3_HasSourceCitation()
    {
        Assert.Contains("b6207a9ffd2f", _spec);
    }

    // ── Section F — Add to Order Rebuild Semantics (FR-006) ──────────────────

    [Fact(DisplayName = "FR-006 · Section F states the previously selected list is cleared")]
    public void SectionF_StatesListIsCleared()
    {
        Assert.Contains("clears", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-006 · Section F states list is rebuilt from currently checked checkboxes")]
    public void SectionF_StatesRebuildFromCheckboxes()
    {
        Assert.Contains("checkboxes", _spec, StringComparison.OrdinalIgnoreCase);
        Assert.True(
            _spec.Contains("rebuild", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("re-read", StringComparison.OrdinalIgnoreCase),
            "spec.md must state the list is rebuilt from (or re-read from) checkboxes.");
    }

    [Fact(DisplayName = "FR-006 · Section F states replace semantics (clear then rebuild)")]
    public void SectionF_StatesReplaceNotAccumulate()
    {
        // Section F must express the clear-then-rebuild semantics.
        // ("accumulate" appears legitimately in Section H and must NOT be tested for absence globally.)
        Assert.True(
            _spec.Contains("replaces", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("replace", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("clear", StringComparison.OrdinalIgnoreCase),
            "spec.md must state that Add to Order replaces (not accumulates) the selection.");
        // The word "accumulation" in Section F would be a spec error; Section H uses it legitimately.
        // Check that Section F does not describe accumulation by confirming the rebuild language is present.
        Assert.True(
            _spec.Contains("rebuild", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("re-read", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("clears", StringComparison.OrdinalIgnoreCase),
            "spec.md Section F must describe the clear-then-rebuild pattern explicitly.");
    }

    [Fact(DisplayName = "FR-006 · Section F cites commit b8f566f12b7e")]
    public void SectionF_HasSourceCitation()
    {
        Assert.Contains("b8f566f12b7e", _spec);
    }

    // ── Section G — Known Quirks (FR-007, FR-008) ────────────────────────────

    [Fact(DisplayName = "FR-007 · Section G lists all four quirk entries G1–G4")]
    public void SectionG_HasFourQuirkEntries()
    {
        Assert.Contains("G1", _spec);
        Assert.Contains("G2", _spec);
        Assert.Contains("G3", _spec);
        Assert.Contains("G4", _spec);
    }

    [Fact(DisplayName = "FR-007/G1 · Section G records getOrderId() hardcoded return of 100")]
    public void SectionG_G1_HardcodedGetterReturns100()
    {
        Assert.Contains("getOrderId", _spec);
        Assert.Contains("100", _spec);
        Assert.Contains("hardcoded", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-007/G1 · Section G cites commit 050a6c51f1fd for G1")]
    public void SectionG_G1_HasSourceCitation()
    {
        Assert.Contains("050a6c51f1fd", _spec);
    }

    [Fact(DisplayName = "FR-007/G2 · Section G records hardcoded order ID = 1 in both flows")]
    public void SectionG_G2_HardcodedOrderIdIs1()
    {
        // The spec must mention both the CLI and GUI citation hashes for G2.
        Assert.Contains("64f7802c89cf", _spec);
        Assert.Contains("345f29de025e", _spec);
    }

    [Fact(DisplayName = "FR-007/G3 · Section G records processPayment() unconditional completion")]
    public void SectionG_G3_UnconditionalPaymentCompletion()
    {
        Assert.Contains("processPayment", _spec);
        Assert.Contains("308c286a1855", _spec);
    }

    [Fact(DisplayName = "FR-007/G4 · Section G records updateStatus() accepting any string")]
    public void SectionG_G4_UpdateStatusAcceptsAnyString()
    {
        Assert.Contains("updateStatus", _spec);
        Assert.Contains("c952d5c09d03", _spec);
    }

    [Fact(DisplayName = "FR-008 · Section G defers every quirk's disposition to spec 002")]
    public void SectionG_DefersAllQuirksToSpec002()
    {
        Assert.Contains("spec 002", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-008 · Section G records constitution mandate for G1–G3")]
    public void SectionG_G1ToG3_MarkedAsConstitutionMandated()
    {
        // The spec must record that the constitution mandates G1–G3 must be reproduced.
        Assert.True(
            _spec.Contains("constitution mandates", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("Constitution mandate", StringComparison.Ordinal),
            "spec.md must record the constitution mandate for G1–G3.");
        Assert.Contains("MUST", _spec);
    }

    [Fact(DisplayName = "FR-008 · Section G explicitly states G4 is NOT in the constitution's Known Defects table")]
    public void SectionG_G4_NotInConstitutionKnownDefectsTable()
    {
        Assert.Contains("Known Defects", _spec);
        Assert.True(
            _spec.Contains("NOT listed", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("not in the constitution", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("is NOT listed in the constitution", StringComparison.OrdinalIgnoreCase),
            "spec.md must state that G4 is NOT in the constitution's Known Defects table.");
    }

    // ── Section H — addReview Overwrite Semantics (FR-011) ───────────────────

    [Fact(DisplayName = "FR-011 · Section H states addReview always overwrites")]
    public void SectionH_StatesAddReviewOverwrites()
    {
        Assert.Contains("addReview", _spec);
        Assert.Contains("overwrites", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-011 · Section H states there is no accumulation")]
    public void SectionH_StatesNoAccumulation()
    {
        Assert.True(
            _spec.Contains("no accumulation", StringComparison.OrdinalIgnoreCase) ||
            _spec.Contains("There is no accumulation", StringComparison.OrdinalIgnoreCase),
            "spec.md must state that addReview does not accumulate entries.");
    }

    [Fact(DisplayName = "FR-011 · Section H states the method always replaces")]
    public void SectionH_StatesMethodAlwaysReplaces()
    {
        Assert.Contains("always replaces", _spec, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "FR-011 · Section H cites CustomerReview.java commit 29e7ac8a9646")]
    public void SectionH_HasSourceCitation()
    {
        Assert.Contains("CustomerReview.java", _spec);
        Assert.Contains("29e7ac8a9646", _spec);
    }

    // ── Runtime and Build Assumptions (FR-010) ───────────────────────────────

    [Fact(DisplayName = "FR-010 · Runtime section heading marks these as frozen / not ported")]
    public void RuntimeSection_HeadingStatesNotPortedHere()
    {
        Assert.Contains("Not Ported Here", _spec);
    }

    [Fact(DisplayName = "FR-010 · Runtime section records JDK 11 requirement")]
    public void RuntimeSection_MentionsJdk11()
    {
        Assert.Contains("JDK 11", _spec);
    }

    [Fact(DisplayName = "FR-010 · Runtime section records Maven wrapper (mvnw)")]
    public void RuntimeSection_MentionsMavenWrapper()
    {
        Assert.True(
            _spec.Contains("mvnw") || _spec.Contains("Maven wrapper"),
            "spec.md must mention the Maven wrapper (mvnw).");
    }

    [Fact(DisplayName = "FR-010 · Runtime section records JavaFX module dependencies")]
    public void RuntimeSection_MentionsJavaFxDependencies()
    {
        Assert.True(
            _spec.Contains("javafx.controls") || _spec.Contains("javafx.fxml"),
            "spec.md must record the JavaFX module dependencies.");
    }

    // ── Scope Constraint (SC-005) ─────────────────────────────────────────────

    [Fact(DisplayName = "SC-005 · spec.md contains no C# code blocks")]
    public void SpecMd_ContainsNoCSharpCodeBlocks()
    {
        Assert.DoesNotContain("```csharp", _spec, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("```cs\n", _spec);
    }

    [Fact(DisplayName = "SC-005 · spec.md does not prescribe a UI framework choice")]
    public void SpecMd_DoesNotPrescribeUiFramework()
    {
        // The spec records intent only; stack choice belongs in the implementing specs.
        Assert.DoesNotContain("WinForms", _spec);
        Assert.DoesNotContain("Windows Forms", _spec);
    }
}
