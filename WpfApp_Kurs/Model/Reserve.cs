using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Kurs.Model
{
    public class Reserve
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Founded { get; set; }
        public List<Individual> Individuals { get; set; }

        [NotMapped]
        public List<Individual> ReserveIndividuals
        {
            get
            {
                return DateAdd.GetAllIndividualByIdReserve(Id);
            }
        }
    }
}
