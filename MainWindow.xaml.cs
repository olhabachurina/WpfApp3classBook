using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace WpfApp3classBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Book> books;
        private int currentPage = 0;
        private const int booksPerPage = 3;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBooks();
            ShowBooks();
        }

        private void InitializeBooks()
        {
            
            books = new List<Book>
            {
                new Book("Отверженные", "https://example.com/book1.jpg", "Драма", "Описание 1", 2009),
                new Book("Собачье сердце", "https://example.com/book2.jpg", "Сатира", "Описание 2", 2015),
                new Book("Исчезнувшая", "https://example.com/book3.jpg", "Детектив", "Описание 3", 2021),
                
            };
        }

        private void ShowBooks()
        {
            
            BooksStackPanel.Children.Clear();
            int startIndex = currentPage * booksPerPage;
            int endIndex = Math.Min(startIndex + booksPerPage, books.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                Book book = books[i];
                StackPanel bookPanel = CreateBookPanel(book);
                BooksStackPanel.Children.Add(bookPanel);
            }
        }

        private StackPanel CreateBookPanel(Book book)
        {
            
            StackPanel panel = new StackPanel
            {
                Margin = new Thickness(10),
                Width = 250
            };

            
            Image image = new Image
            {
                Height = 300,
                Width = 200,
                Source = new BitmapImage(new Uri(book.ImageUrl))
            };
            panel.Children.Add(image);

            TextBlock titleBlock = new TextBlock
            {
                Text = book.Title,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 0)
            };
            panel.Children.Add(titleBlock);

            Button detailsButton = new Button
            {
                Content = "Подробнее",
                Tag = book, 
                Margin = new Thickness(0, 5, 0, 0)
            };
            detailsButton.Click += DetailsButton_Click;
            panel.Children.Add(detailsButton);

            return panel;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            
            Button detailsButton = sender as Button;
            if (detailsButton != null && detailsButton.Tag is Book)
            {
                Book book = (Book)detailsButton.Tag;
                ShowBookDetails(book);
            }
        }

        private void ShowBookDetails(Book book)
        {
            
            DetailsTitle.Text = book.Title;
            DetailsGenre.Text = "Жанр: " + book.Genre;
            DetailsYear.Text = "Год издания: " + book.Year.ToString();
            DetailsDescription.Text = "Описание: " + book.Description;

           
            DetailsStackPanel.Visibility = Visibility.Visible;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
           
            currentPage = Math.Max(0, currentPage - 1);
            ShowBooks();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            
            int totalPages = (int)Math.Ceiling((double)books.Count / booksPerPage);
            currentPage = Math.Min(currentPage + 1, totalPages - 1);
            ShowBooks();
        }
    }

    public class Book
    {
        public string Title { get; }
        public string ImageUrl { get; }
        public string Genre { get; }
        public string Description { get; }
        public int Year { get; }

        public Book(string title, string imageUrl, string genre, string description, int year)
        {
            Title = title;
            ImageUrl = imageUrl;
            Genre = genre;
            Description = description;
            Year = year;
        }
    }
}
