using System;
using System.Collections.Generic;
using System.Data;
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
using PostgreWpf.DataSubscribtion;
using PostgreWpf.Views;

namespace PostgreWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, IAddFilmObserver
    {
        private enum States { FILMS,ACTORS,DIRECTORS,NONE};
        private enum Operations { CREATE, EDIT}
        private States currentState = States.NONE;
        private Operations currentOperation = Operations.CREATE;
        private DBInterface dbInterface;
        
        public MainWindow()
        {
            InitializeComponent();
            dbInterface = new DBInterface("localhost", "5432", "postgres", "123qwe", "postgres");
        }

        private void CreateTables(object sender, RoutedEventArgs e)
        {
            dbInterface.CreateActorsTable();
            dbInterface.CreateDirectorsTable();
            dbInterface.CreateFilmsTable();
            dbInterface.CreateFilmstoActorsTable();
        }

        private void CreateFilm_Clicked(object sender, RoutedEventArgs e)
        {
            FilmInput filmInput = new FilmInput();
            filmInput.Subscribe(this);
            filmInput.Show();
        }

        private void CreateActor_Clicked(object sender, RoutedEventArgs e)
        {
            ActorInput actorInput = new ActorInput();
            actorInput.Subscribe(this);
            actorInput.Show();
        }

        private void CreateDirector_Clicked(object sender, RoutedEventArgs e)
        {
            DirectorInput dirInput = new DirectorInput();
            dirInput.Subscribe(this);
            dirInput.Show();
        }

        private void GetFilms_Clicked(object sender, RoutedEventArgs e)
        {
            var result = dbInterface.GetAllFilms();
            if (result != null)
            {
                DataGrid.ItemsSource = result.DefaultView;
                currentState = States.FILMS;
            }
        }

        private void GetActors_Clicked(object sender, RoutedEventArgs e)
        {
            var result = dbInterface.GetAllActors();
            if(result != null)
            {
                DataGrid.ItemsSource = result.DefaultView;
                currentState = States.ACTORS;
            }
        }

        private void GetDirectors_Clicked(object sender, RoutedEventArgs e)
        {
            var result = dbInterface.GetAllDirectors();
            if (result != null)
            {
                DataGrid.ItemsSource = result.DefaultView;
                currentState = States.DIRECTORS;
            }
        }

        private void GenerateRandom_Clicked(object sender, RoutedEventArgs e)
        {
            for(int i =0; i< 50; i++)
            {
                dbInterface.InsertDirector(new Director());
            }

            for(int i = 0; i < 50; i++)
            {
                dbInterface.InsertActor(new Actor());
            }

            for(int i =0; i < 50; i++)
            {
                dbInterface.InsertFilm(new Film());
            }

            var result = dbInterface.GetAllFilms();
            if(result != null)
            {
                DataGrid.ItemsSource = result.DefaultView;
            }
        }

        public void OnDropTables_Clicked(object sender, RoutedEventArgs e)
        {
            dbInterface.DropActors();
            dbInterface.DropRels();
            dbInterface.DropFilms();
            dbInterface.DropDirectors();
        }

        public void OnSearchScript_Clicked(object sender, RoutedEventArgs e)
        {
            var result = dbInterface.SearchScript(this.SearchScriptArea.Text);
            if(result != null)
            {
                DataGrid.ItemsSource = result.DefaultView;
            }
        }

        public void OnComplexSearch_Clicked(object sender, RoutedEventArgs e)
        {
            string certified = "FALSE";
            if ((bool)this.CertifiedCheckBox.IsChecked)
            {
                certified = "TRUE";
            }
            var result = dbInterface.SearchTitle_Director(
               Int32.Parse(this.BudgetInput.Text), certified
                );
            if(result != null)
            {
                DataGrid.ItemsSource = result.DefaultView;
            }
        }

 
        private void OnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            if (currentState == States.FILMS)
            {
                string filmId = ((DataRowView)DataGrid.SelectedItem).Row["fid"].ToString();
                dbInterface.DeleteFilm(Int32.Parse(filmId));
                DataGrid.ItemsSource = dbInterface.GetAllFilms().DefaultView;
            }
            else if (currentState == States.ACTORS)
            {
                string actorID = ((DataRowView)DataGrid.SelectedItem).Row["aid"].ToString();
                dbInterface.DeleteActor(Int32.Parse(actorID));
                DataGrid.ItemsSource = dbInterface.GetAllActors().DefaultView;
            }
            else if (currentState == States.DIRECTORS)
            {
                string directorID = ((DataRowView)DataGrid.SelectedItem).Row["did"].ToString();
                dbInterface.DeleteDirector(Int32.Parse(directorID));
                DataGrid.ItemsSource = dbInterface.GetAllDirectors().DefaultView;
            }
        }

        public void OnFilmAdded(Film film)
        {
            if(currentOperation == Operations.CREATE)
            {
                dbInterface.InsertFilm(film);
            }
            else
            {
                dbInterface.UpdateFilm(film);
            }
            DataGrid.ItemsSource = dbInterface.GetAllFilms().DefaultView;
            dbInterface.Close();
        }

        public void OnActorAdded(Actor actor)
        {
            if (currentOperation == Operations.CREATE)
            {
                dbInterface.InsertActor(actor);
            }
            else
            {
                dbInterface.UpdateActor(actor);
            }
            DataGrid.ItemsSource = dbInterface.GetAllActors().DefaultView;

            dbInterface.Close();
        }

        public void OnDirectorAdded(Director director)
        {
            if(currentOperation == Operations.CREATE)
            {
                dbInterface.InsertDirector(director);
            }
            else
            {
                dbInterface.UpdateDirector(director);
            }
            DataGrid.ItemsSource = dbInterface.GetAllDirectors().DefaultView;

            dbInterface.Close();
        }

        public void OnEdit_Clicked(object sender, RoutedEventArgs e)
        {
            currentOperation = Operations.EDIT;
            if (currentState == States.NONE)
                return;
            switch (currentState)
            {
                case States.FILMS:
                    Film film = new Film(
                        Int32.Parse(((DataRowView)DataGrid.SelectedItem).Row["fid"].ToString()),
                        ((DataRowView)DataGrid.SelectedItem).Row["title"].ToString(),
                        Int32.Parse(((DataRowView)DataGrid.SelectedItem).Row["budget"].ToString()),
                        ((DataRowView)DataGrid.SelectedItem).Row["script"].ToString(),
                        Int32.Parse(((DataRowView)DataGrid.SelectedItem).Row["did"].ToString())
                        );
                    FilmInput filmInput = new FilmInput(film);
                    filmInput.Subscribe(this);
                    filmInput.Show();
                    break;
                case States.ACTORS:
                    Actor actor = new Actor(
                        Int32.Parse(((DataRowView)DataGrid.SelectedItem).Row["aid"].ToString()),
                        ((DataRowView)DataGrid.SelectedItem).Row["firstname"].ToString(),
                        ((DataRowView)DataGrid.SelectedItem).Row["secondname"].ToString(),
                        Int32.Parse(((DataRowView)DataGrid.SelectedItem).Row["experience"].ToString())
                        );
                    ActorInput actorInput = new ActorInput(actor);
                    actorInput.Subscribe(this);
                    actorInput.Show();
                    break;
                case States.DIRECTORS:
                    Director director = new Director(
                        Int32.Parse(((DataRowView)DataGrid.SelectedItem).Row["did"].ToString()),
                        ((DataRowView)DataGrid.SelectedItem).Row["firstname"].ToString(),
                        ((DataRowView)DataGrid.SelectedItem).Row["secondname"].ToString(),
                        ((DataRowView)DataGrid.SelectedItem).Row["isCertified"].ToString().ToLower()
                        );
                    DirectorInput directorInput = new DirectorInput(director);
                    directorInput.Subscribe(this);
                    directorInput.Show();
                    break;
            }
        }

    }
}
