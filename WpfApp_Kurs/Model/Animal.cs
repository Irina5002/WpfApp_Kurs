using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Kurs.Model
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Individual> Individuals { get; set; }

        [NotMapped]
        public List<Individual> AnimalIndividuals
        {
            get
            {
                return DateAdd.GetIndividualByIdAnimal(Id);
            }
        }
    }
}
