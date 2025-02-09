using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedIO.Klasy
{
    public class WynikGry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }  // Automatyczny ID
        public string ImieGracza { get; set; }  // Imię gracza
        public string CzasReakcji { get; set; }  // Czas reakcji
        public int Milisekundy { get; set; }  // Czas w milisekundach
    }
}
