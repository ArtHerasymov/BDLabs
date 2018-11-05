using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PostgreWpf.DataSubscribtion;

namespace PostgreWpf
{
    /// <summary>
    /// Interaction logic for FilmInput.xaml
    /// </summary>
    public partial class FilmInput : Window, IAddFilmObservable
    {
        private Film film;
        IAddFilmObserver observer;
        public FilmInput()
        {
            InitializeComponent();
            film = new Film();
        }

        public FilmInput(Film film)
        {
            InitializeComponent();
            this.film = film;
            TitleField.Text = film.Title;
            BudgetField.Text = film.Budget.ToString();
            ScriptField.Text = film.Script;
            DIDField.Text = film.Did.ToString();
        }

        public void CreateFilm_Clicked(object sender, RoutedEventArgs e)
        {
            film = new Film(
                this.film.Fid,
                this.TitleField.Text,
                Int32.Parse(BudgetField.Text), ScriptField.Text,
                Int32.Parse(DIDField.Text));
            Unsubscribe(observer);
            this.Close();
        }

        public void Subscribe(IAddFilmObserver observer)
        {
            this.observer = observer;
        }

        public void Unsubscribe(IAddFilmObserver observer)
        {
            this.observer.OnFilmAdded(film);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
