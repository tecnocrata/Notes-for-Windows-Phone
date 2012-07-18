using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Modulo6.Actividad2
{
    [Table]
    public class Nota : INotifyPropertyChanged
    {
        private int notaId;

        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int NotaId
        {
            get { return notaId; }
            set
            {
                notaId = value;
                OnProperyChanged("NotaId");
            }
        }

        private string titulo;

        [Column]
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnProperyChanged("Titulo");
            }
        }

        private string contenido;

        [Column]
        public string Contenido
        {
            get { return contenido; }
            set
            {
                contenido = value;
                OnProperyChanged("Contenido");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProperyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
