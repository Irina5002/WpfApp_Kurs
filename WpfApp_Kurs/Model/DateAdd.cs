using System.Linq;
using System.Collections.Generic;

namespace WpfApp_Kurs.Model
{
    public static class DateAdd //Не будет использоваться в качестве объекта. Хрнанит в себе список методов, которые позволяют добавлять, изменять и редактировать записи "Красной книги"
    {
        //Добавить заповедник
        public static string CreateReserve(string name, int founded)
        {
            string result = "Данный заповедник уже существует";
            using (Context db = new Context())
            {
                //Проверка на существование
                bool checkThis = db.Reserves.Any(el => el.Name == name && el.Founded == founded);
                if (!checkThis)
                {
                    Reserve newReserve = new Reserve 
                    { 
                        Name = name,
                        Founded = founded 
                    };
                    db.Reserves.Add(newReserve);
                    db.SaveChanges();
                    result = "Заповедник добавлен";
                }
                return result;
            }

        }
        //Добавить вид
        public static string CreateAnimal(string name)
        {
            string result = "Данный вид уже существует";
            using (Context db = new Context())
            {
                //Проверка на существование
                bool checkThis = db.Animals.Any(el => el.Name == name);
                if (!checkThis)
                {
                    Animal newAnimal = new Animal { Name = name };
                    db.Animals.Add(newAnimal);
                    db.SaveChanges();
                    result = "Вид добавлен";
                }
                return result;
            }

        }
        //Добавить особь 
        public static string CreateIndividual(string name, string sex, int age, Animal animal, Reserve reserve)
        {
            string result = "Данная особь уже существует";
            using (Context db = new Context())
            {
                //Проверка на существование
                bool checkThis = db.Individuals.Any(el => el.Name == name && el.Age == age && el.Sex == sex);
                if (!checkThis)
                {
                    Individual newIndividual = new Individual
                    {
                        Name = name,
                        Sex = sex,
                        Age = age,
                        AnimalId = animal.Id,
                        ReserveId = reserve.Id
                    };
                    db.Individuals.Add(newIndividual);
                    db.SaveChanges();
                    result = "Особь добавлена";
                }
                return result;
            }

        }
        //Удалить заповедник
        public static string DeleteReserve(Reserve reserve)
        {
            string result = "Данный заповедник не существует";
            using (Context db = new Context())
            {
                db.Reserves.Remove(reserve);
                db.SaveChanges();
                result = "Заповедник удален";
            }
            return result;
        }
        //Удалить вид
        public static string DeleteAnimal(Animal animal)
        {
            string result = "Данного вида не существует";
            using (Context db = new Context())
            {
                db.Animals.Remove(animal);
                db.SaveChanges();
                result = "Вид удален";
            }
            return result;
        }
        //Удалить особь 
        public static string DeleteIndividual(Individual individual)
        {
            string result = "Данной особи не существует";
            using (Context db = new Context())
            {
                db.Individuals.Remove(individual);
                db.SaveChanges();
                result = "Особь удалена";
            }
            return result;
        }
        //Редактировать заповедник
        public static string EditReserve(Reserve oldReserve, string newName, int newFounded)
        {
            string result = "Данного заповедника не существует";
            using (Context db = new Context())
            {
                Reserve reserve = db.Reserves.FirstOrDefault(d => d.Id == oldReserve.Id);
                reserve.Name = newName;
                reserve.Founded = newFounded;
                db.SaveChanges();
                result = "Заповедник обновлен";
            }
            return result;
        }
        //Редактировать вид
        public static string EditAnimal(Animal oldAnimal, string newName)
        {
            string result = "Данного вида не существует";
            using (Context db = new Context())
            {
                Animal animal = db.Animals.FirstOrDefault(d => d.Id == oldAnimal.Id);
                animal.Name = newName;
                db.SaveChanges();
                result = "Вид обновлен";
            }
            return result;
        }
        //Редактировать особь
        public static string EditIndividual(Individual oldIndividual, string newName, string newSex, int newAge, Animal newAnimal, Reserve newReserve)
        {
            string result = "Данной особи не существует";
            using (Context db = new Context())
            {
                Individual individual = db.Individuals.FirstOrDefault(d => d.Id == oldIndividual.Id);
                individual.Name = newName;
                individual.Sex = newSex;
                individual.Age = newAge;
                individual.AnimalId = newAnimal.Id;
                individual.ReserveId = newReserve.Id;
                db.SaveChanges();
                result = "Особь обновлена";
            }
            return result;
        }

        //Следующие три статика нужны для списка всех компонентов в ManageMV
        //Заповедники
        public static List<Reserve> GetAllReserves()
        {
            using (Context db = new Context())
            {
                var result = db.Reserves.ToList();
                return result;
            }
        }
        //Виды
        public static List<Animal> GetAllAnimals()
        {
            using (Context db = new Context())
            {
                var result = db.Animals.ToList();
                return result;
            }
        }
        //Особи
        public static List<Individual> GetAllIndividuals()
        {
            using (Context db = new Context())
            {
                var result = db.Individuals.ToList();
                return result;
            }
        }

        //Получение вида по id
        public static Animal GetAnimalById(int id)
        {
            using (Context db = new Context())
            {
                Animal anim = db.Animals.FirstOrDefault(a => a.Id == id);
                return anim;
            }
        }

        //Получение заповедника по id
        public static Reserve GetReserveById(int id)
        {
            using (Context db = new Context())
            {
                Reserve res = db.Reserves.FirstOrDefault(r => r.Id == id);
                return res;
            }
        }

        //Получение всех особей для вида
        public static List<Individual> GetIndividualByIdAnimal(int id)
        {
            using (Context db = new Context())
            {
                List<Individual> individuals = (from individual in GetAllIndividuals() where individual.AnimalId == id select individual).ToList();
                return individuals;
            }
        }

        //Получение всех особей для заповедника
        public static List<Individual> GetAllIndividualByIdReserve(int id)
        {
            using (Context db = new Context())
            {
                List<Individual> individuals = (from individual in GetAllIndividuals() where individual.ReserveId == id select individual).ToList();
                return individuals;
            }
        }
    }
}
