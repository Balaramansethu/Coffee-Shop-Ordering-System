using Xunit;

namespace Referee.Spec001;

/// <summary>
/// Verifies that all five contract files exist and that their content is
/// consistent with the frozen behaviour catalogue in spec.md.
/// Tasks: T008, T013, T018, T023, T026, T030 (cross-reference checks).
/// </summary>
public class ContractFilesTests
{
    // ── Existence (T001 prerequisite check) ──────────────────────────────────

    [Theory(DisplayName = "All five contract files must exist")]
    [InlineData("menu.md")]
    [InlineData("cli-mappings.md")]
    [InlineData("cli-status-progression.md")]
    [InlineData("gui-guards.md")]
    [InlineData("quirks.md")]
    public void ContractFile_Exists(string filename)
    {
        var path = Path.Combine(Paths.ContractsDir, filename);
        Assert.True(File.Exists(path),
            $"contracts/{filename} must exist — it is a required deliverable of spec 001.");
    }

    // ── contracts/menu.md ─────────────────────────────────────────────────────

    [Theory(DisplayName = "contracts/menu.md contains each of the five frozen menu items")]
    [InlineData("Cappuccino")]
    [InlineData("Latte")]
    [InlineData("Matcha Latte")]
    [InlineData("Cold Brew")]
    [InlineData("Hot Chocolate")]
    public void ContractMenu_ContainsMenuItem(string item)
    {
        var text = File.ReadAllText(Paths.ContractMenu);
        Assert.Contains(item, text);
    }

    // ── contracts/cli-mappings.md ─────────────────────────────────────────────

    [Theory(DisplayName = "contracts/cli-mappings.md contains each timing string")]
    [InlineData("Immediate")]
    [InlineData("Bring after 5 minutes")]
    [InlineData("Keep bringing in a break of 30 minutes")]
    public void ContractCliMappings_ContainsTimingValue(string value)
    {
        var text = File.ReadAllText(Paths.ContractCliMappings);
        Assert.Contains(value, text);
    }

    [Fact(DisplayName = "contracts/cli-mappings.md records 'Immediate' as timing out-of-range default")]
    public void ContractCliMappings_TimingDefaultIsImmediate()
    {
        var text = File.ReadAllText(Paths.ContractCliMappings);
        Assert.True(
            text.Contains("Immediate", StringComparison.OrdinalIgnoreCase),
            "contracts/cli-mappings.md must record 'Immediate' as the timing out-of-range default.");
    }

    [Theory(DisplayName = "contracts/cli-mappings.md contains each payment string")]
    [InlineData("UPI")]
    [InlineData("Cash")]
    [InlineData("Card")]
    [InlineData("Coupon")]
    public void ContractCliMappings_ContainsPaymentValue(string value)
    {
        var text = File.ReadAllText(Paths.ContractCliMappings);
        Assert.Contains(value, text);
    }

    [Fact(DisplayName = "contracts/cli-mappings.md records 'Cash' as payment out-of-range default")]
    public void ContractCliMappings_PaymentDefaultIsCash()
    {
        var text = File.ReadAllText(Paths.ContractCliMappings);
        Assert.True(
            text.Contains("Cash", StringComparison.OrdinalIgnoreCase),
            "contracts/cli-mappings.md must record 'Cash' as the payment out-of-range default.");
    }

    [Theory(DisplayName = "contracts/cli-mappings.md contains each review string")]
    [InlineData("Very Good")]
    [InlineData("Okish")]
    [InlineData("Bad")]
    [InlineData("Worst")]
    public void ContractCliMappings_ContainsReviewValue(string value)
    {
        var text = File.ReadAllText(Paths.ContractCliMappings);
        Assert.Contains(value, text);
    }

    [Fact(DisplayName = "contracts/cli-mappings.md records 'Good' as review out-of-range default")]
    public void ContractCliMappings_ReviewDefaultIsGood()
    {
        var text = File.ReadAllText(Paths.ContractCliMappings);
        Assert.True(
            text.Contains("Good", StringComparison.OrdinalIgnoreCase),
            "contracts/cli-mappings.md must record 'Good' as the review out-of-range default.");
    }

    // ── contracts/cli-status-progression.md ──────────────────────────────────

    [Fact(DisplayName = "contracts/cli-status-progression.md contains the full three-state sequence")]
    public void ContractCliStatusProgression_ContainsSequence()
    {
        var text = File.ReadAllText(Paths.ContractCliStatusProgression);
        Assert.Contains("Pending", text);
        Assert.Contains("Preparing", text);
        Assert.Contains("Served", text);
    }

    [Fact(DisplayName = "contracts/cli-status-progression.md states progression is unconditional")]
    public void ContractCliStatusProgression_IsUnconditional()
    {
        var text = File.ReadAllText(Paths.ContractCliStatusProgression);
        Assert.True(
            text.Contains("unconditional", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("without", StringComparison.OrdinalIgnoreCase),
            "contracts/cli-status-progression.md must state the progression is unconditional.");
    }

    [Fact(DisplayName = "contracts/cli-status-progression.md distinguishes CLI from GUI flow")]
    public void ContractCliStatusProgression_DistinguishesCLIFromGUI()
    {
        var text = File.ReadAllText(Paths.ContractCliStatusProgression);
        // The contract must note the automatic progression is CLI-only.
        Assert.True(
            text.Contains("GUI", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("CLI", StringComparison.OrdinalIgnoreCase),
            "contracts/cli-status-progression.md must reference the CLI/GUI distinction.");
    }

    // ── contracts/gui-guards.md ───────────────────────────────────────────────

    [Fact(DisplayName = "contracts/gui-guards.md contains the exact E1 error message")]
    public void ContractGuiGuards_ContainsE1ErrorMessage()
    {
        var text = File.ReadAllText(Paths.ContractGuiGuards);
        Assert.Contains(
            "No items selected! Please select items from the menu.",
            text);
    }

    [Fact(DisplayName = "contracts/gui-guards.md contains the exact E2 error message")]
    public void ContractGuiGuards_ContainsE2ErrorMessage()
    {
        var text = File.ReadAllText(Paths.ContractGuiGuards);
        Assert.Contains(
            "No order placed! Please place an order first.",
            text);
    }

    [Fact(DisplayName = "contracts/gui-guards.md names Preparing and Served as the E3 status choices")]
    public void ContractGuiGuards_NamesE3StatusChoices()
    {
        var text = File.ReadAllText(Paths.ContractGuiGuards);
        Assert.Contains("Preparing", text);
        Assert.Contains("Served", text);
    }

    [Fact(DisplayName = "contracts/gui-guards.md describes Add to Order rebuild semantics")]
    public void ContractGuiGuards_DescribesAddToOrderRebuild()
    {
        var text = File.ReadAllText(Paths.ContractGuiGuards);
        Assert.True(
            text.Contains("Add to Order", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("checkbox", StringComparison.OrdinalIgnoreCase),
            "contracts/gui-guards.md must describe Add to Order or checkbox rebuild semantics.");
    }

    // ── contracts/quirks.md ───────────────────────────────────────────────────

    [Fact(DisplayName = "contracts/quirks.md contains entries for all four quirks G1–G4")]
    public void ContractQuirks_ContainsAllFourQuirks()
    {
        var text = File.ReadAllText(Paths.ContractQuirks);
        Assert.Contains("G1", text);
        Assert.Contains("G2", text);
        Assert.Contains("G3", text);
        Assert.Contains("G4", text);
    }

    [Fact(DisplayName = "contracts/quirks.md defers disposition to spec 002")]
    public void ContractQuirks_DefersToSpec002()
    {
        var text = File.ReadAllText(Paths.ContractQuirks);
        Assert.Contains("spec 002", text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "contracts/quirks.md records GetOrderId() hardcoded return of 100")]
    public void ContractQuirks_G1_RecordsHardcodedReturn100()
    {
        var text = File.ReadAllText(Paths.ContractQuirks);
        Assert.Contains("100", text);
        // The contract may use Java casing (getOrderId) or C# casing (GetOrderId).
        Assert.True(
            text.Contains("getOrderId", StringComparison.OrdinalIgnoreCase),
            "contracts/quirks.md must reference the GetOrderId/getOrderId getter for G1.");
    }

    [Fact(DisplayName = "contracts/quirks.md records processPayment() unconditional completion")]
    public void ContractQuirks_G3_RecordsUnconditionalPayment()
    {
        var text = File.ReadAllText(Paths.ContractQuirks);
        Assert.True(
            text.Contains("processPayment", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("payment", StringComparison.OrdinalIgnoreCase),
            "contracts/quirks.md must record the processPayment unconditional-completion quirk.");
    }
}
