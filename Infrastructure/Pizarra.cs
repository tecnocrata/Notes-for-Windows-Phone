using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    public class Pizarra: INotifyPropertyChanged
    {
        private ObservableCollection<Nota> notas;

        public ObservableCollection<Nota> Notas
        {
            get
            {
                if (notas == null)
                {
                    notas = new ObservableCollection<Nota>();
                    if (DesignerProperties.IsInDesignTool)
                    {
                        notas.Add(new Nota() {Titulo = "Examen", Contenido = "No olvidar el examen de calculo"});
                        notas.Add(new Nota() {Titulo = "Cumple Patty", Contenido = "El cumpleanios de Patty se acerca"});
                    }
                    else
                    {
                        CargarNotas();
                    }
                }
                return notas;
            }
            set
            {
                notas = value;
                OnProperyChanged("Notas");
            }
        }

        public void Agregar(Nota nota)
        {
            Notas.Add(nota);
            PizarraDataContext.CurrentDataContext.Notas.InsertOnSubmit(nota);
            PizarraDataContext.CurrentDataContext.SubmitChanges();
        }

        public Nota Recuperar(int id)
        {
            var query = (from n in PizarraDataContext.CurrentDataContext.Notas
                         where n.NotaId == id
                         select n).Single();
            return query;
        }

        public void Update(Nota nota)
        {
            var notaActual = Recuperar(nota.NotaId);
            notaActual.Titulo = nota.Titulo;
            notaActual.Contenido = nota.Contenido;
            PizarraDataContext.CurrentDataContext.SubmitChanges();
            //Notas[Notas.IndexOf(Notas.Where(n => n.NotaId == notaActual.NotaId).FirstOrDefault())] = notaActual;
        }

        public void Eliminar (int id)
        {
            var query = (from n in PizarraDataContext.CurrentDataContext.Notas
                         where n.NotaId == id
                         select n).Single();
            PizarraDataContext.CurrentDataContext.Notas.DeleteOnSubmit(query);
            PizarraDataContext.CurrentDataContext.SubmitChanges();
            Notas.Remove(query);
        }

        private void CargarNotas()
        {
            var query = from n in PizarraDataContext.CurrentDataContext.Notas
                        orderby n.NotaId
                        select n;
            Notas = new ObservableCollection<Nota>(query.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
