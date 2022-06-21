using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Kurs.Model
{
    public class Individual
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
        public int ReserveId { get; set; }
        public virtual Reserve Reserve { get; set; }

        [NotMapped]
        public Animal IndividualAnimal
        {
            get
            {
                return DateAdd.GetAnimalById(AnimalId);
            }
        }

        public Reserve IndividualReserve
        {
            get
            {
                return DateAdd.GetReserveById(ReserveId);
            }
        }

    }
}
