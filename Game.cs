using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Art_of_battle.Model;

namespace Art_of_battle
{
    public class Game
    {
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }
        public Size BattleFieldSize { get; set; }
        public GameSettings GameSettings { get; set; }

        public List<Card> AllCards { get; }
        public GoldInfo GoldInfo { get; }

        public Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;

        public void PlaceCreatureOnField(ICreature creature, Player player)
        {
            playerCreaturesInGame[player].Add(creature.CreateCreature());
        }

        public Game(GameSettings settings, List<Card> cards)
        {
            GameSettings = settings;
            AllCards = cards;
        }

        public void CreatePlayers(string name1, string name2)
        {
            var defaultCards = AllCards.Take(GameSettings.CardsPlayerCount).ToArray();
            FirstPlayer = new Player(name1, defaultCards);
            SecondPlayer = new Player(name2, defaultCards);
        }


        public void Start()
        {
        }
    }
}
