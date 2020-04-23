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

        public HashSet<Card> Cards { get; }

        public Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;


        public Game(GameSettings settings, HashSet<Card> cards)
        {
            GameSettings = settings;
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public Game(GameSettings settings)
        {
            GameSettings = settings;
            Cards = new HashSet<Card>();
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public void PlaceCreatureOnField(Card card, Player player)
        {
            var creature = card.Creature.CreateCreature(player);
            creature.Position = player.CreaturesSpawnPoint;

            playerCreaturesInGame[player].Add(creature);
        }

        public void DeleteCreatureFromField(ICreature creature, Player player)
        {
            playerCreaturesInGame[player].Remove(creature);
        }

        public void CreatePlayers(string name1, string name2)
        {
            var defaultCards = Cards.Take(GameSettings.CardsPlayerCount).ToArray();

            FirstPlayer = new Player(name1, Direction.Right, defaultCards);
            SecondPlayer = new Player(name2, Direction.Left, defaultCards);

            playerCreaturesInGame.Add(FirstPlayer, new HashSet<ICreature>());
            playerCreaturesInGame.Add(SecondPlayer, new HashSet<ICreature>());
        }

        public void Start()
        {
        }
    }
}
