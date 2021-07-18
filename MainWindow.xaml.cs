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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt2v2.ScaffoldModel;

namespace Projekt2v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
               //uhunnn
            }
        }
            public void CheckLogin()
        {
            using var db = new WypozyczalniaFilmowDBContext();
          
            var user = db.KontaktKlients.Where(i => i.Login == this.LoginTextBox.Text).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Niepoprawny login i/lub hasło!");
            }
            else if (this.LoginTextBox.Text == user.Login || this.passwordBox.Password == user.Haslo)
            {
                MessageBox.Show("Zalogowano jako " + user.Login + "");
            }
            else
            {
                MessageBox.Show("Niepoprawny login i/lub hasło");
            }
        }
        public void rejestracja()
        {
            using var db = new WypozyczalniaFilmowDBContext();
            if (RejCheck.IsChecked==false)
            {
                MessageBox.Show("Musisz zaakceptować regulamin");
            }
            else
            {
                var count = db.KontaktKlients.Count();
                count = count + 1;
                string liczba = count.ToString();
               // MessageBox.Show(liczba);
                db.KontaktKlients.Add(new KontaktKlient() { Login = LoginRej.Text, Haslo = passwordBox1.Password, Imie = ImieRej.Text, Nazwisko = NazwiskoRej.Text, IdKlienta=liczba });
                MessageBox.Show("Konto utworzone pomyslnie");
                db.SaveChanges();
            }  
        }
        private void Haslo_TextChanged(object sender, TextChangedEventArgs e)
        {
            //zaloguj haslo
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            //zaloguj login
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Zaloguj button
            CheckLogin();
        }
        private void LoginRej_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Rejestracja login
        }

        private void HasloRej_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Rejestracja haslo
        }

        private void RejCheck_Checked(object sender, RoutedEventArgs e)
        {
            //rej check
        }

        public void RejButton_Click(object sender, RoutedEventArgs e)
        {
            //utworz konto
            rejestracja();
        }

        private void ImieRej_TextChanged(object sender, TextChangedEventArgs e)
        {
            //rej imie
        }

        private void NazwiskoRej_TextChanged(object sender, TextChangedEventArgs e)
        {
            //rej nazwisko
        }

        private void Wypozyczone_Click(object sender, RoutedEventArgs e)
        {
            //wypozyczone
            using var db = new WypozyczalniaFilmowDBContext();
           
            var wynajem = from p in db.Wynajems
                          select new
                          {
                              Wypozyczenie = p.IdWypozyczenia,
                              FilmID = p.IdFilmu,
                              DataWypozyczenia = p.DataWypozyczenia,
                              DataZwrotu = p.DataZwrotu
                          };
            var wynajem2 = db.Wynajems.Join(db.KontaktKlients, p => p.IdKlienta, mp => mp.IdKlienta, (p, mp) => new
            {
                Wypozyczenie = p.IdWypozyczenia,
                FilmID = p.IdFilmu,
                DataWypozyczenia = p.DataWypozyczenia,
                DataZwrotu = p.DataZwrotu,
                Wypozyczyl = mp.Login
            }).Where(i => i.Wypozyczyl == this.LoginTextBox.Text).ToList();

            dataGrid.ItemsSource = wynajem2.ToList();

        }

        private void Oferta_Click(object sender, RoutedEventArgs e)
        {
            //oferta
            using var db = new WypozyczalniaFilmowDBContext();
            var filmy = from p in db.Films
                        select new
                        {
                            //FilmID = p.IdFilmu,
                            Nazwa = p.Nazwa,
                            Gatunek = p.Gatunek,
                            Wydawca = p.Wydawca,
                            CenaZaDobe = p.CenaZaDobe,
                            NosnikID = p.IdNosnika
                        };
          
            dataGrid.ItemsSource = filmy.ToList();
        }
     
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
