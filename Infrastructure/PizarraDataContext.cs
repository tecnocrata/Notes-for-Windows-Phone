using System;
using System.Data.Linq;
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
    public class PizarraDataContext:DataContext
    {
        public PizarraDataContext(string connectionString):base(connectionString)
        {
            
        }

        private static PizarraDataContext dataContext;

        public static PizarraDataContext CurrentDataContext
        {
            get
            {
                if (dataContext == null)
                {
                    dataContext= new PizarraDataContext("isostore:/pizarra.sdf");
                    if (!dataContext.DatabaseExists())
                    {
                        dataContext.CreateDatabase();
                    }
                }
                return dataContext;
            }
        }

        public Table<Nota> Notas;
    }
}
