using LibraryApp.Data;
using LibraryApp.Enums;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Threading.Tasks.Sources;

namespace LibraryApp.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    /// 
    private readonly Library _library = new();

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public void Show()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Show all books");
            Console.WriteLine("2. Add book");
            Console.WriteLine("3. Remove book");
            Console.WriteLine("4. Lend book");
            Console.WriteLine("5. Return book");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                MainScreenChoices choice = (MainScreenChoices)int.Parse(choiceAsString);
                switch (choice)
                {
                    case MainScreenChoices.ShowAllBooks:
                        ShowAllBooks();
                        break;
                    case MainScreenChoices.AddBook:
                        AddBook();
                        break;
                    case MainScreenChoices.RemoveBook:
                        RemoveBook();
                        break;
                    case MainScreenChoices.LendBook:
                        LendBook();
                        break;
                    case MainScreenChoices.ReturnBook:
                        ReturnBook();
                        break;
                    case MainScreenChoices.Exit:
                        Console.WriteLine("Goodbye.");
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// Method used to display all books from library
    private void ShowAllBooks()
    {
        _library.ShowAllBooks();
    }

    /// Method used to add book from library
    private void AddBook()
    {
        try
        {
            Console.WriteLine("Please enter information about the book:");
            Console.Write("Title: ");
            string? title = Console.ReadLine();
            Console.Write("Author: ");
            string? author = Console.ReadLine();
            Console.Write("ISBN: ");
            long? isbn = long.Parse(Console.ReadLine() ?? "0");

            if (title == null || author == null) { throw new NullReferenceException(); } 
            if (isbn == null || isbn <= 0 || isbn?.ToString().Length != 13 ) { Console.WriteLine("ISBN must be more than 13 digits"); throw new DataException();  }

            _library.AddBook(new Book { Author = author, Title = title, ISBN = isbn, IsAvailable = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
      
    }
    /// Method used to display delete book from library
    private void RemoveBook()
    {
        try
        {
            Console.Write("Write ISBN: ");
            string? isbnBook = Console.ReadLine();
            if (isbnBook == null ) { new NullReferenceException(); }
            _library.RemoveBooks(isbnBook);
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }


    }
    /// Method used to lend book from library
    private void LendBook()
    {
        Console.WriteLine("Enter the IBN of the Book");
        string? isbn = Console.ReadLine();
        _library.LendBook(isbn);
    }
    /// Method used to return book from library
    private void ReturnBook()
    {
        Console.WriteLine("Enter the IBN of the Book");
        string? isbn = Console.ReadLine();
        _library.ReturnBook(isbn);
    }

    #endregion // Private Methods 
}