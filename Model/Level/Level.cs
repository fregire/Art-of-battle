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
        public int RequiredLevel { get; set; }
        public Level(LevelName levelName, int recvExp, int recvGold, int requiredLevel)
        {
            this.LevelName = levelName;
            this.ReceivedExperienceAmount = recvExp;
            this.ReceivedGoldAmount = recvGold;
            this.RequiredLevel = requiredLevel;
        }

        public bool IsLocked(Player player)
        {
            return player.PlayerLevelInfo.CurrentLevel < RequiredLevel;
        }
    }
}
