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
using System.Windows.Shapes;
using PostgreWpf.DataSubscribtion;

namespace PostgreWpf.Views
{
    /// <summary>
    /// Interaction logic for DirectorInput.xaml
    /// </summary>
    public partial class DirectorInput : Window, IAddFilmObservable
    {
        private Director director;
        IAddFilmObserver observer;
        public DirectorInput()
        {
            InitializeComponent();
            director = new Director();
        }

        public DirectorInput(Director director)
        {
            InitializeComponent();
            this.director = director;
            FirstNameField.Text = director.FirstName;
            SecondNameField.Text = director.SecondName;
            if (director.IsCertified == "true") IsCertifiedCheckBox.IsChecked = true;
            else IsCertifiedCheckBox.IsChecked = false;
        }

        public void Subscribe(IAddFilmObserver observer)
        {
            this.observer = observer;
        }

        public void Unsubscribe(IAddFilmObserver observer)
        {
            this.observer.OnDirectorAdded(director);
        }

        private void CreateDirector_Clicked(object sender, RoutedEventArgs e)
        {
            String isChecked = "FALSE";
            if ((bool)this.IsCertifiedCheckBox.IsChecked)
            {
                isChecked = "TRUE";
            }
            director = new Director(
                this.director.Did,
                this.FirstNameField.Text,
                this.SecondNameField.Text,
                isChecked
                );
            Unsubscribe(observer);
            this.Close();
           
        }
    }
}
