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
        public IEnumerable<Card> Cards { get; }
        public IEnumerable<Card> DefaultPlayerCards
            => Cards.Take(GameSettings.CardsCountInPlayerHand);

        public GameStage GameStage = GameStage.NotStarted;

        private Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;

        public Game(GameSettings settings, IEnumerable<Card> cards)
        {
            GameSettings = settings;
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public Game(IEnumerable<Card> cards)
        {
            GameSettings = new GameSettings();
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public Game()
        {
            GameSettings = new GameSettings();
            Cards = new HashSet<Card>();
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public void PlaceCreatureOnField(ICreature creature, Player player)
        {
            creature.Position = player.CreaturesSpawnPoint;
            playerCreaturesInGame[player].Add(creature);
        }

        public void DeleteCreatureFromField(ICreature creature, Player player)
        {
            playerCreaturesInGame[player].Remove(creature);
        }

        public void ClearField()
        {
            playerCreaturesInGame[FirstPlayer].Clear();
            playerCreaturesInGame[SecondPlayer].Clear();
        }

        public void ChangeState(GameStage stage)
        {
            this.GameStage = stage;
            StateChanged?.Invoke(stage);
        }

        public event Action<GameStage> StateChanged;

        private void PlaceCastlesOnField()
        {
            var firstCastle = new Building(
                CreatureType.Castle, 
                1000, 
                new Size(50, 100), 
                FirstPlayer.CreaturesDirection);

            var secondCastle = new Building(
                CreatureType.Castle,
                1000,
                new Size(50, 100),
                SecondPlayer.CreaturesDirection);

            firstCastle.Position = FirstPlayer.CreaturesSpawnPoint;
            secondCastle.Position = SecondPlayer.CreaturesSpawnPoint;

            PlaceCreatureOnField(firstCastle, FirstPlayer);
            PlaceCreatureOnField(secondCastle, SecondPlayer);
        }

        public void Start(Player firstPlayer, Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;

            playerCreaturesInGame.Add(FirstPlayer, new HashSet<ICreature>());
            playerCreaturesInGame.Add(SecondPlayer, new HashSet<ICreature>());

            PlaceCastlesOnField();
            ChangeState(GameStage.Started);
        }
    }
}
