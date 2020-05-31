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
        public PlayerLevel PlayerLevelInfo { get; set; }
        public int GameGoldAmount { get; set; }
        public string Name { get; }
        public List<Card> Cards { get; }
        public List<Card> ChoosedCardsForGame { get; set; }
        private int battleGoldAmount;

        public int BattleGoldAmount
        {
            get
            {
                return battleGoldAmount;
            }
            set
            {
                battleGoldAmount = value;
                GoldChanged?.Invoke(value);
            }
        }

        public event Action<int> GoldChanged;

        public Point CreaturesSpawnPoint;
        public Direction CreaturesDirection { get; }
        public Building Castle { get; set; }

        public Player(
            string name, 
            Direction creaturesDirection,
            List<Card> cards, 
            Dictionary<int, int> playerLevelsInfo)
        {
            Name = name;
            Cards = cards;
            CreaturesDirection = creaturesDirection;
            PlayerLevelInfo = new PlayerLevel(playerLevelsInfo);
            ChoosedCardsForGame = new List<Card>();
        }
    }
}
