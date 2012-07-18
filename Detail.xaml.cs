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
    public partial class Detail : PhoneApplicationPage
    {
        public Detail()
        {
            InitializeComponent();
        }
        private ScreenMode _currentScreenMode = ScreenMode.Undefined;

        private ScreenMode CurrentScreenMode
        {
            get
            {
                if (_currentScreenMode == ScreenMode.Undefined) _currentScreenMode = GetScreenMode();
                return _currentScreenMode;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (CurrentScreenMode == ScreenMode.New)
            {
                this.PageTitle.Text = "Captura";
                ApplicationBar = CrearAppBarNew();
            }
            else
            {
                PageTitle.Text = "Detalle";
                ApplicationBar = CrearAppBarEdit();
                RecuperarNota();
            }
        }

        private void RecuperarNota()
        {
            var pizarra = ((Application.Current as App).Resources["miPizarra"] as Pizarra);
            var notaEditable = pizarra.Recuperar(Convert.ToInt32(GetId()));
            (Resources["notaActual"] as Nota).NotaId = notaEditable.NotaId;
            (Resources["notaActual"] as Nota).Titulo = notaEditable.Titulo;
            (Resources["notaActual"] as Nota).Contenido = notaEditable.Contenido;
        }

        private IApplicationBar CrearAppBarEdit()
        {
            IApplicationBar bar = new ApplicationBar();
            ApplicationBarIconButton btnGuardar =
                new ApplicationBarIconButton(new Uri("/Images/appbar.save.rest.png", UriKind.RelativeOrAbsolute));
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += new EventHandler(btnGuardar_Click);
            bar.Buttons.Add(btnGuardar);

            ApplicationBarIconButton btnEliminar =
                new ApplicationBarIconButton(new Uri("/Images/appbar.delete.rest.png", UriKind.RelativeOrAbsolute));
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += new EventHandler(btnEliminar_Click);
            bar.Buttons.Add(btnEliminar);

            ApplicationBarIconButton btnAnclar =
                new ApplicationBarIconButton(new Uri("/Images/appbar.favs.addto.rest.png", UriKind.RelativeOrAbsolute));
            btnAnclar.Text = "Eliminar";
            btnAnclar.Click += new EventHandler(btnAnclar_Click);
            bar.Buttons.Add(btnAnclar);
            return bar;
        }

        private IApplicationBar CrearAppBarNew()
        {
            IApplicationBar bar = new ApplicationBar();
            ApplicationBarIconButton btnGuardar =
                new ApplicationBarIconButton(new Uri("/Images/appbar.save.rest.png", UriKind.RelativeOrAbsolute));
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += new EventHandler(btnGuardar_Click);
            bar.Buttons.Add(btnGuardar);
            return bar;
        }

        private string GetId()
        {
            string id = null;
            if (NavigationContext.QueryString.ContainsKey("id"))
                id = NavigationContext.QueryString["id"];
            return id;
        }

        private ScreenMode GetScreenMode()
        {
            if (GetId() == null) return ScreenMode.New;
            return ScreenMode.Edit;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var nuevaNota = Resources["notaActual"] as Nota;
            var pizarra = ((Application.Current as App).Resources["miPizarra"] as Pizarra);
            switch (CurrentScreenMode)
            {
                case ScreenMode.New:
                    pizarra.Agregar(nuevaNota);
                    break;
                case ScreenMode.Edit:
                    pizarra.Update(nuevaNota);
                    break;
            }
            NavigationService.GoBack();
        }

        void btnAnclar_Click(object sender, EventArgs e)
        {
            var notaActual = Resources["notaActual"] as Nota;
            if (ShellTile.ActiveTiles.Where(m => m.NavigationUri == NavigationService.Source).Count() == 0)
            {
                StandardTileData newTile = new StandardTileData()
                {
                    Title = notaActual.Titulo,
                    BackgroundImage = new Uri("/Images/tile.background.png", UriKind.RelativeOrAbsolute),
                    BackContent = notaActual.Contenido.Length < 10 ? "Nota de recordatorio" : notaActual.Contenido.Substring(0, 10) + "...",
                    BackTitle = notaActual.Titulo,
                    BackBackgroundImage = new Uri("/Images/tile.backbackground.png", UriKind.RelativeOrAbsolute)
                };
                ShellTile.Create(NavigationService.Source, newTile);
            }
        }

        void btnEliminar_Click(object sender, EventArgs e)
        {
            var nuevaNota = Resources["notaActual"] as Nota;
            var pizarra = ((Application.Current as App).Resources["miPizarra"] as Pizarra);
            pizarra.Eliminar(nuevaNota.NotaId);
            var currentTile =
                ShellTile.ActiveTiles.Where(m => m.NavigationUri == NavigationService.Source).FirstOrDefault();
            if (currentTile != null)
                currentTile.Delete();

            NavigationService.GoBack();
        }

        private void txtDetalle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                var tb = sender as TextBox;
                var binding = tb.GetBindingExpression(TextBox.TextProperty);
                if (binding != null) binding.UpdateSource();
            }
        }
    }

    public enum ScreenMode
    {
        Undefined = 999,
        New = 1,
        Edit = 2
    };
}