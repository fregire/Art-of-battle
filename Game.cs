using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Art_of_battle.Model;

namespace Art_of_battle
{
    class Game
    {
        public GoldInfo GoldInfo { get; }
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }
        public Size BattleFieldSize { get; set; }

        public List<Card> AllCards = new List<Card>();
        public Settings GameSettings { get; set; }

        private Dictionary<Player, HashSet<Creature>> playerCreaturesInGame;


        public void DeleteCreature(Creature creature, Player player)
        {
            //TODO: Implement exception if player doesnt exist
            playerCreaturesInGame[player].Remove(creature);
        }

        public void PlaceCreature(Creature creature, Player player)
        {
            //TODO: Implement exception if player doesnt exist
            playerCreaturesInGame[player].Add(creature);
        }

        public void CreatePlayer(string name)
        {
            var defaultCards = AllCards.Take(4).ToArray();

            if (FirstPlayer == null)
                FirstPlayer = new Player(name, defaultCards);
            else
                SecondPlayer = new Player(name, defaultCards);
        }

        public Game(Settings settings)
        {
            GameSettings = settings;
        }


        public void Start()
        {

        }
    }
}
