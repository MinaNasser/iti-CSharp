using System.ComponentModel.Design;
using System.Dynamic;
using ITI.OOPs.Models;

namespace ITI.OOPs.Managers
{
    public class TraineeManager
    {
        List<Trainee> list
             = new List<Trainee>();

        public TraineeManager()
        {
            list.Add(new Trainee() { Id = 0, Name = "Ahmed", Gender = Geneder.Male });
            list.Add(new Trainee() { Id = 2, Name = "Aly", Gender= Geneder.Male});
            list.Add(new Trainee() { Id = 3, Name = "Hany", Gender = Geneder.Male });
        }

        public List<Trainee> Get()
        {
            return list;
        }
        public Trainee? GetByID(int id)
        {
            foreach (Trainee t in list)
            {
                if (t.Id == id) { return t; }

            }
            return null;
        }
    }
}