using LibraryApp.Services;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Data
{
    // Library class
    public class Library
    {
        /// <summary>
        ///  Ctor and Properties
        /// </summary>
        public List<Book>? Books { get; set; } = new List<Book>();
        public List<Reader>? Readers { get; set; }  = new List<Reader>();

        #region Public Methods

        /// Method used to display all books from library
        public void ShowAllBooks()
        {
            try
            {
                DataUpdate();
                if (Books == null || Books?.Count <= 0) { Console.WriteLine("No book found"); return; };
                for (int i = 0; i < Books?.Count; i++)
                {
                    Console.WriteLine($"{i}. Title: {Books[i].Title, -50} Author: {Books[i].Author, -20} ISBN: {Books[i].ISBN, -15} Available: {Books[i].IsAvailable, -15}");
                }
                Console.WriteLine("Open console in full-screen for  better experience");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// Method used to add book from library
        public void AddBook(Book newBook)
        {
            try
            {
                DataUpdate();

                if (Books?.Find(book => book.ISBN == newBook.ISBN) != null)
                {
                    Console.WriteLine("Book with this ISBN already exists");
                    return;
                }
                Books?.Add(newBook);
                WriteData();
                Console.WriteLine("Book was successfully added ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }    
        }

        /// Method used to delete book from library
        public void RemoveBooks(string? isbnBookAsString)
        {
            try
            {
                Console.WriteLine("Are you sure you want to delete book? Y/N");
                if (Console.ReadLine()?.ToLower() != "y") return;
                DataUpdate();
                long isbn = long.Parse(isbnBookAsString);
                Book? book = Books?.Find(book => book.ISBN == isbn);
                if (book == null) { Console.WriteLine("Book with this ISBN not found"); return; }
                Books?.Remove(book);
                WriteData();
                Console.WriteLine("Book was successfully deleted ");
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }


        }

        /// Method used to lend book from library
        public void LendBook(string? isbnBookAsString)
        {
            try
            {
                DataUpdate();
                long? isbn = long.Parse(isbnBookAsString);
                Book? book = Books?.Find(book => book.ISBN == isbn);
                Console.Write("Enter the reader's name: ");
                string? readerName = Console.ReadLine();

                if (Books == null) { new NullReferenceException(); return; }
                if (isbn == null) { new NullReferenceException(); return; }
                if (book == null || book.IsAvailable == false) { Console.WriteLine("Book with this ISBN not found"); return; }
                if (readerName == null || readerName == "") { new NullReferenceException() ; return; }

                Reader reader = new Reader() { Name = readerName, BorrowedBooks = new List<Book>() };
                if (reader == null) { new NullReferenceException(); return; }
                int readerId = Readers.FindIndex(reader => reader.Name == readerName);
                if (readerId == -1) Readers.Add(reader);
                Readers[Readers.FindIndex(reader => reader.Name == readerName)].BorrowedBooks.Add(book);
                Books[Books.FindIndex(book => book.ISBN == isbn)].IsAvailable = false;

                WriteData();
                Console.WriteLine($"Book has been successfully lended by {readerName}");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        /// Method used to return book from library
        public void ReturnBook(string? isbnBookAsString)
        {
            try
            {
                DataUpdate();
                long? isbn = long.Parse(isbnBookAsString);
                Book book = Books?.Find(book => book.ISBN == isbn);
                Console.Write("Enter the reader's name: ");
                string? readerName = Console.ReadLine();

                if (Books == null) { new NullReferenceException(); return; }
                if (isbn == null) { new NullReferenceException(); return; }
                if (book == null || book.IsAvailable != false) { Console.WriteLine("Book with this ISBN not found"); return; }
                if (readerName == null || readerName == "") { new NullReferenceException(); return; }

                Reader reader = Readers.Find(reader => reader.Name == readerName);

                if (reader == null) { Console.WriteLine("Reader not fount"); }

                Book? borrowedBook = reader?.BorrowedBooks?.Find(book => book.ISBN == isbn);

                if (reader?.BorrowedBooks?.Remove(borrowedBook) != true) { Console.WriteLine("Book not fount"); return; }

                Books[Books.FindIndex(book => book.ISBN == isbn)].IsAvailable = true;

                Console.WriteLine($"Book has been successfully returned by {readerName}");

                WriteData();
            }
            catch (Exception)
            {

            }

        }

        #endregion // Public MethodsЧ

        #region Private Methods

        // Methods for update and saving books and readers
        private void DataUpdate()
        {
            try
            {
                Books = DataService.Read("Books.json").Books ?? new List<Book>();
                Readers = DataService.Read("Books.json").Readers ?? new List<Reader>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void WriteData()
        {
            try
            {
                DataService.Write(this, "Books.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        #endregion // Private Methods
    }
}
