namespace Referee.Spec001;

// Resolves the path to the spec-001 documentation package under test.
// Override with SPEC_DIR env var when running from unusual locations.
internal static class Paths
{
    private static readonly Lazy<string> _specDir = new(ResolveSpecDir);

    public static string SpecDir => _specDir.Value;
    public static string SpecMd => Path.Combine(SpecDir, "spec.md");
    public static string PlanMd => Path.Combine(SpecDir, "plan.md");
    public static string DataModelMd => Path.Combine(SpecDir, "data-model.md");
    public static string QuickstartMd => Path.Combine(SpecDir, "quickstart.md");
    public static string ResearchMd => Path.Combine(SpecDir, "research.md");
    public static string ContractsDir => Path.Combine(SpecDir, "contracts");
    public static string ContractMenu => Path.Combine(ContractsDir, "menu.md");
    public static string ContractCliMappings => Path.Combine(ContractsDir, "cli-mappings.md");
    public static string ContractCliStatusProgression => Path.Combine(ContractsDir, "cli-status-progression.md");
    public static string ContractGuiGuards => Path.Combine(ContractsDir, "gui-guards.md");
    public static string ContractQuirks => Path.Combine(ContractsDir, "quirks.md");

    private static string ResolveSpecDir()
    {
        var fromEnv = Environment.GetEnvironmentVariable("SPEC_DIR");
        if (fromEnv is not null)
            return Path.GetFullPath(fromEnv);

        // Walk up from the test binary looking for a sibling implementation workspace.
        // Expected layout:
        //   /…/new/
        //     .referee-*/              ← referee workspace (test binary lives here)
        //     Coffee-Shop-Ordering-System/specs/001-…/   ← implementation
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            // Prefer the named implementation workspace (most specific).
            var candidate = Path.Combine(dir.FullName,
                "Coffee-Shop-Ordering-System", "specs",
                "001-freeze-intent-and-acceptance-basis");
            if (File.Exists(Path.Combine(candidate, "spec.md")))
                return candidate;

            // Accept a bare specs/ tree only when the contracts/ subdirectory
            // is also present — this excludes the referee's sparse snapshot.
            var candidate2 = Path.Combine(dir.FullName,
                "specs", "001-freeze-intent-and-acceptance-basis");
            if (File.Exists(Path.Combine(candidate2, "spec.md")) &&
                Directory.Exists(Path.Combine(candidate2, "contracts")))
                return candidate2;

            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException(
            "Cannot locate specs/001-freeze-intent-and-acceptance-basis. " +
            "Set the SPEC_DIR environment variable to its absolute path.");
    }
}
