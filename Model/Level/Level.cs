using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art_of_battle.Model
{
    public class Level
    {
        public int ReceivedGoldAmount { get; set; }
        public int ReceivedExperienceAmount { get; set; }
        public LevelName LevelName { get; }
        public bool IsLocked { get; set; }
        public int LevelToUnlock { get; set; }

        public Level(LevelName levelName, int recvExp, int recvGold, int lvlToUnlock)
        {
            this.LevelName = levelName;
            this.ReceivedExperienceAmount = recvExp;
            this.ReceivedGoldAmount = recvGold;
            this.LevelToUnlock = lvlToUnlock;
        }
    }
}
