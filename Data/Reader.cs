namespace LibraryApp.Data;

/// <summary>
/// Reader base class with basic implementations.
/// </summary>
public class Reader
{
    /// <summary>
    /// Properties
    /// </summary>
    public string? Name { get; set; }
    public List<Book>? BorrowedBooks { get; set; }
}
