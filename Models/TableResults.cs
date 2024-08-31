namespace PlaywrightSpecFlowDemo.WebUI.Tests.Models;

/// <summary>
/// Represents a record of student details captured in a table format.
/// </summary>
public sealed record TableResults
{
    public required string StudentName { get; init; }
    public required string StudentEmail { get; init; }
    public required string Gender { get; init; }
    public required string Mobile { get; init; }
    public required string DateOfBirth { get; init; }
    public required string Subjects { get; init; }
    public required string Hobbies { get; init; }
    public string? Picture { get; init; } = null;
    public required string Address { get; init; }
    public string? StateAndCity { get; init; } = null;
}