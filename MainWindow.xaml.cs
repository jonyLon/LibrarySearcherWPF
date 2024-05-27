using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibrarySearcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibraryContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new LibraryContext();
            LoadAuthorsAsync();
        }

        private async void LoadAuthorsAsync()
        {
            var autors = await _context.Authors.ToListAsync();
            AuthorsComboBox.ItemsSource = autors;
            AuthorsComboBox.DisplayMemberPath = "Name";
            AuthorsComboBox.SelectedValuePath = "AuthorId";
        }

        private async void AuthorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AuthorsComboBox.SelectedItem != null)
            {
                var author_id = (int)AuthorsComboBox.SelectedValue;
                var books = await _context.Books
                    .Where(b => b.AuthorId == author_id)
                    .ToListAsync();
                BooksListBox.ItemsSource = books;
                BooksListBox.DisplayMemberPath = "DisplayInfo";
            }
        }



        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text.Length >= 3)
            {
                var books = await _context.Books
                    .Where(b => b.Title.Contains(SearchTextBox.Text))
                    .ToListAsync();
                BooksListBox.ItemsSource = books;
                BooksListBox.DisplayMemberPath = "Title";
            } else
            {
                MessageBox.Show("Please enter at least 3 characters for the search.");
            }
        }
    }
}