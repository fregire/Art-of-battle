using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Cards { get; }
        public int CurrentGold { get; set; }

        public Point CreaturesSpawnPoint;
        public Direction CreaturesDirection { get; }
        public ICreature Castle { get; set; }

        public Player(string name, Direction creaturesDirection, List<Card> defaultCards)
        {
            Name = name;
            Cards = defaultCards;
            CreaturesDirection = creaturesDirection;
        }
    }
}
