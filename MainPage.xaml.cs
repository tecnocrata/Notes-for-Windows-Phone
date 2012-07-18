using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Modulo6.Actividad2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Detail.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            var notaSeleccionada = lbNotas.SelectedItem as Nota;
            if (notaSeleccionada != null)
                NavigationService.Navigate(new Uri("/Detail.xaml?id=" + notaSeleccionada.NotaId,
                                                   UriKind.RelativeOrAbsolute));
        }

        private void lbNotas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = true;
        }
    }
}