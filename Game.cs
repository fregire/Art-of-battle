using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Art_of_battle.Model;

namespace Art_of_battle
{
    class Game
    {
        public GoldInfo Gold { get; private set; }
        public List<Card> AllCards = new List<Card>();
        public Settings GameSettings { get; set; }
        public Card[] PlayerCardsInGame
        {
            get => GameSettings.Slots.Select(slot => slot.Card).ToArray();
        }

        private HashSet<Creature> playerCreaturesInGame { get; set; }
        private HashSet<Creature> enemyCreaturesInGame { get; set; }

        public void DeleteCreature(Creature creature, bool isPlayerCreature)
        {
            if (isPlayerCreature)
                playerCreaturesInGame.Remove(creature);
            else
                enemyCreaturesInGame.Remove(creature);
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
