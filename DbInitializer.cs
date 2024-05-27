using LibrarySearcher.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibrarySearcher
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            try
            {
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                // Обробка винятків, які можуть виникнути при створенні бази даних
                MessageBox.Show($"An error occurred while creating the database: {ex.Message}");
                return;
            }

            if (context.Authors.Any()) // Перевірка чи було ініціалізовано базу раніше
            {
                return;
            }

            var authors = new Author[]
            {
            new Author{Name="Jane Austen"},
            new Author{Name="Charles Dickens"},
            new Author{Name="Mark Twain"},
            new Author{Name="Ernest Hemingway"},
            new Author{Name="F. Scott Fitzgerald"}
            };

            foreach (var author in authors)
            {
                context.Authors.Add(author);
            }

            context.SaveChanges();


            var books = new Book[]
            {
                new Book{Title="Pride and Prejudice", AuthorId=authors.Single(a => a.Name == "Jane Austen").AuthorId},
                new Book{Title="Sense and Sensibility", AuthorId=authors.Single(a => a.Name == "Jane Austen").AuthorId},
                new Book{Title="Great Expectations", AuthorId=authors.Single(a => a.Name == "Charles Dickens").AuthorId},
                new Book{Title="A Tale of Two Cities", AuthorId=authors.Single(a => a.Name == "Charles Dickens").AuthorId},
                new Book{Title="Adventures of Huckleberry Finn", AuthorId=authors.Single(a => a.Name == "Mark Twain").AuthorId},
                new Book{Title="The Adventures of Tom Sawyer", AuthorId=authors.Single(a => a.Name == "Mark Twain").AuthorId},
                new Book{Title="The Old Man and the Sea", AuthorId=authors.Single(a => a.Name == "Ernest Hemingway").AuthorId},
                new Book{Title="For Whom the Bell Tolls", AuthorId=authors.Single(a => a.Name == "Ernest Hemingway").AuthorId},
                new Book{Title="The Great Gatsby", AuthorId=authors.Single(a => a.Name == "F. Scott Fitzgerald").AuthorId},
                new Book{Title="Tender Is the Night", AuthorId=authors.Single(a => a.Name == "F. Scott Fitzgerald").AuthorId}
            };

            foreach (var b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();


        }

    }
}
