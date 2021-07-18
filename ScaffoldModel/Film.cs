using System;
using System.Collections.Generic;

#nullable disable

namespace Projekt2v2.ScaffoldModel
{
    public partial class Film
    {
        public Film()
        {
            Wynajems = new HashSet<Wynajem>();
        }

        public string IdFilmu { get; set; }
        public string Nazwa { get; set; }
        public string Gatunek { get; set; }
        public string Wydawca { get; set; }
        public string CenaZaDobe { get; set; }
        public string IdNosnika { get; set; }

        public virtual Dostepnosc IdNosnikaNavigation { get; set; }
        public virtual ICollection<Wynajem> Wynajems { get; set; }
    }
}
