using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_of_battle.Model
{
    public class PlayerLevel
    {
        public int CurrentExperienceAmount { get; set; }

        public int CurrentLevel => RequiredExperienceForEachLevel
                    .FirstOrDefault(pair => CurrentExperienceAmount < pair.Value).Key - 1;

        public Dictionary<int, int> RequiredExperienceForEachLevel;

        public PlayerLevel(Dictionary<int, int> requiredExperience)
        {
            this.RequiredExperienceForEachLevel = requiredExperience;
        }
    }
}
