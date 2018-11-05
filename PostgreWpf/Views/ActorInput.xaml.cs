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

namespace PostgreWpf.Views
{
    /// <summary>
    /// Interaction logic for ActorInput.xaml
    /// </summary>
    public partial class ActorInput : Window, IAddFilmObservable
    {
        private Actor actor;
        IAddFilmObserver observer;

        public ActorInput()
        {
            InitializeComponent();
            actor = new Actor();
        }

        public ActorInput(Actor actor)
        {
            InitializeComponent();
            this.actor = actor;
            FirstNameField.Text = actor.FirstName;
            SecondNameField.Text = actor.SecondName;
            ExperienceField.Text = actor.Experience.ToString();
        }

        public void Subscribe(IAddFilmObserver observer)
        {
            this.observer = observer;
        }

        public void Unsubscribe(IAddFilmObserver observer)
        {
            this.observer.OnActorAdded(actor);
        }

        public void CreateActor_Clicked(object sender, RoutedEventArgs e)
        {
            actor = new Actor(
                this.actor.Aid,
                this.FirstNameField.Text,
                this.SecondNameField.Text, Int32.Parse(this.ExperienceField.Text)
                );
            Unsubscribe(observer);
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
