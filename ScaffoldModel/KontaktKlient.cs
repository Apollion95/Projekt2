using System;
using System.Collections.Generic;

#nullable disable

namespace Projekt2v2.ScaffoldModel
{
    public partial class KontaktKlient
    {
        public KontaktKlient()
        {
            Wynajems = new HashSet<Wynajem>();
        }

        public string IdKlienta { get; set; }
        public string NumerTelefonu { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Nazwa { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }

        public virtual ICollection<Wynajem> Wynajems { get; set; }
    }
}
