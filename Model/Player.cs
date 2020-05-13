using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Art_of_battle.Model.Creatures;

namespace Art_of_battle.Model
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Cards { get; }
        private int currGold;

        public int CurrentGold
        {
            get
            {
                return currGold;
            }
            set
            {
                currGold = value;
                GoldChanged?.Invoke(value);
            }
        }

        public event Action<int> GoldChanged;

        public Point CreaturesSpawnPoint;
        public Direction CreaturesDirection { get; }
        public Building Castle { get; set; }

        public Player(string name, Direction creaturesDirection, List<Card> defaultCards)
        {
            Name = name;
            Cards = defaultCards;
            CreaturesDirection = creaturesDirection;
        }
    }
}
