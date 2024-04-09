using NUnit.Framework;
using System;
using TestApp.Library;

namespace TestApp.Tests;

[TestFixture]
public class LibraryCatalogTests
{
    private LibraryCatalog _catalog = null!;

    [SetUp]
    public void SetUp()
    {
        this._catalog = new();
    }

    [Test]
    public void Test_AddBook_BookAddedToCatalog()
    {
        // Arrange
        string isbn = "861-3-16-122510-0";
        string title = "Gomor";
        string author = "Roberto Saviano";

        // Act
        _catalog.AddBook(isbn, title, author);
        Book addedBook = _catalog.GetBook(isbn);

        // Assert
        Assert.IsNotNull(addedBook);
        Assert.AreEqual(title, addedBook.Title);
        Assert.AreEqual(author, addedBook.Author);
        Assert.AreEqual(isbn, addedBook.Isbn);
    }

    [Test]
    public void Test_GetBook_BookExists_ReturnsBook()
    {
        // Arrange
        string isbn = "861-3-16-122510-0";
        string title = "Gomor";
        string author = "Roberto Saviano";
        _catalog.AddBook(isbn, title, author);

        // Act
        Book retrievedBook = _catalog.GetBook(isbn);

        // Assert
        Assert.IsNotNull(retrievedBook);
        Assert.AreEqual(title, retrievedBook.Title);
        Assert.AreEqual(author, retrievedBook.Author);
        Assert.AreEqual(isbn, retrievedBook.Isbn);
    }

    [Test]
    public void Test_GetBook_BookDoesNotExist_ThrowsArgumentException()
    {
        // Arrange
       string isbn = "861-3-16-122510-0";

        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => _catalog.GetBook(isbn));
    }

    [Test]
    public void Test_DisplayCatalog_NoBooks_ReturnsEmptyString()
    {
        // Arrange
        // No books are added to the catalog

        // Act
        string result = _catalog.DisplayCatalog();

        // Assert
        Assert.AreEqual(string.Empty, result);
    }

    [Test]
    public void Test_DisplayCatalog_WithBooks_ReturnsFormattedCatalog()
    {
        // Arrange
        string isbn1 = "861-3-16-12251-0";
        string title1 = "Gomor";
        string author1 = "Roberto Saviano";
        _catalog.AddBook(isbn1, title1, author1);

        string isbn2 = "123-5-67-12341-0";
        string title2 = "First Steps in C# ";
        string author2 = "Svetlin Nakov";
        _catalog.AddBook(isbn2, title2, author2);

        string expectedCatalog = $"Library Catalog:{Environment.NewLine}" +
                                 $"{title1} by {author1} (ISBN: {isbn1}){Environment.NewLine}" +
                                 $"{title2} by {author2} (ISBN: {isbn2})";

        // Act
        string actualCatalog = _catalog.DisplayCatalog();

        // Assert
        Assert.That(actualCatalog, Is.EqualTo(expectedCatalog));
    }
}
