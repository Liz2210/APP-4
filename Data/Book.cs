namespace LibraryApp.Data;

/// <summary>
/// Book class.
/// </summary>
public class Book
{
    /// <summary>
    /// Properties
    /// </summary>
    #region Properties
    public string? Title { get; set; }
    public string? Author { get; set; }
    public long? ISBN { get; set; }
    public bool? IsAvailable { get; set; }

    #endregion // Ctor
}
