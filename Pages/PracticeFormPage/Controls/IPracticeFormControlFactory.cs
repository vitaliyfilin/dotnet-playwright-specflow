namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

public interface IPracticeFormControlFactory
{
    /// <summary>
    /// Factory method for creating <see cref="PracticeFormControlBase"/> instances based on the given label and optional value.
    /// 
    /// This pattern is useful in UI automation because:
    /// <list type="bullet">
    ///     <item>Encapsulates control creation logic, promoting separation of concerns.</item>
    ///     <item>Allows for easy addition of new control types or labels without modifying existing code.</item>
    ///     <item>Provides a centralized place to manage control instantiation, improving maintainability and scalability.</item>
    /// </list>
    /// </summary>
    /// <param name="label">The label that determines the control type.</param>
    /// <param name="value">An optional value used for controls requiring it, such as checkboxes.</param>
    /// <returns>A <see cref="PracticeFormControlBase"/> instance based on the label.</returns>
    /// <exception cref="ArgumentException">Thrown if the label is "hobbies" or "gender" and the value is null or empty.</exception>
    public PracticeFormControlBase Create(string label, string? value);
}