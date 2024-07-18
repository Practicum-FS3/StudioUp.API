using Microsoft.EntityFrameworkCore;
using StudioUp.Models;


namespace StudioUp.Repo
{
    public class Trainer : Itrainer
    {
        DataContext _context;

        public Trainer(DataContext _context)
        {
            this._context = _context;
        }
        public  List<Trainer> GetTrainers()
        {
            return _context.Trainers.ToList();
        }

        public async Task<Trainer> GetById(int id)
        {
            return await _context.Trainer.FindAsync(id);  
        }
        //public Trainer GetByTrainerName(string trainerName)
        //{
        //    Trainer trainer = _context.Trainers.Where(x => x.FirstName == trainerName).First();
        //    return trainer;

        //}
        public void AddTrainer(Trainer trainer)
        {

            _context.Trainers.Add(trainer);
            _context.SaveChanges();
        }
        public void DeleteTrainer(Trainer trainer)
        {
            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
        }
        public void UpdateTrainer(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
            _context.SaveChanges();
        }
    }
}
}
