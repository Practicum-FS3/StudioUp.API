using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.Models;


namespace StudioUp.Repo
{
    public interface Itrainer
    {
        public  List<Trainer> GetTrainers();

          Trainer GetById(int id);

        //Trainer GetByCategoryName(string trainerName);
        void AddTrainer(Trainer trainer);
        void DeleteTrainer(Trainer trainer);
        void UpdateTrainer(Trainer trainer);
    }
}
