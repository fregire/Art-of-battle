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

        public List<Creature> AllCards = new List<Creature>();
        public Settings GameSettings { get; set; }

        public Dictionary<Player, HashSet<Creature>> playerCreaturesInGame;

        public void DeleteCreature(Creature creature, Player player)
        {
            //TODO: Implement exception if player doesnt exist
            playerCreaturesInGame[player].Remove(creature);
        }

        public Action<Creature> GenerateCreature = (creature) => creature.GenerateSameCreature();
            
        public void PlaceCreatureOnField(Creature creature, Player player)
        {
            playerCreaturesInGame[player].Add(creature);
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
