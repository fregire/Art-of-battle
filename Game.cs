using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;

namespace Art_of_battle
{
    public class Game
    {
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }
        public Size BattleFieldSize { get; set; }
        public GameSettings GameSettings { get; set; }

        public HashSet<Card> AllCards { get; }

        public Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;

        public Game(GameSettings settings, HashSet<Card> cards)
        {
            GameSettings = settings;
            AllCards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public void PlaceCreatureCardOnField(Card card, Player player)
        {
            playerCreaturesInGame[player].Add(card.Creature.CreateCreature());
        }

        public void DeleteCreatureFromField(ICreature creature, Player player)
        {
            playerCreaturesInGame[player].Remove(creature);
        }

        public void CreatePlayers(string name1, string name2)
        {
            var defaultCards = AllCards.Take(GameSettings.CardsPlayerCount).ToArray();
            FirstPlayer = new Player(name1, Direction.Right, defaultCards);
            SecondPlayer = new Player(name2, Direction.Left, defaultCards);
        }

        public void MakeCreaturesAct()
        {
            
        }

        public void Start()
        {
            var castle = new Building(CreatureType.Castle, 1000, new Size(200, 200));

            playerCreaturesInGame[FirstPlayer].Add(castle);
            playerCreaturesInGame[SecondPlayer].Add(castle.CreateCreature());
        }
    }
}
