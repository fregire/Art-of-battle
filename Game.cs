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
        public GameSettings GameSettings { get; set; }
        public List<Card> DefaultPlayerCards
            => Cards.Take(GameSettings.CardsCountInPlayerHand).ToList();

        private GameStage gameStage = GameStage.NotStarted;
        public GameStage Stage => gameStage;

        public readonly List<Card> Cards;

        private readonly Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;

        public Game(GameSettings settings, List<Card> cards)
        {
            GameSettings = settings;
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public Game(List<Card> cards)
        {
            GameSettings = new GameSettings();
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
        }

        public HashSet<ICreature> GetEnemiesOf(Player player)
        {
            return player.Equals(FirstPlayer)
                ? playerCreaturesInGame[SecondPlayer]
                : playerCreaturesInGame[FirstPlayer];
        }

        public void PlaceCardCreatureOnField(Card card, Player player)
        {
            PlaceCreatureOnField(card.Creature.CreateCreature(player));
        }

        public void PlaceCreatureOnField(ICreature creature)
        {
            creature.Position = creature.Player.CreaturesSpawnPoint;
            playerCreaturesInGame[creature.Player].Add(creature);

            CreaturePlacedOnField?.Invoke(creature);
        }


        //TODO: ACT for game

        public event Action<ICreature> CreaturePlacedOnField;

        public void DeleteCreatureFromField(ICreature creature)
        {
            playerCreaturesInGame[creature.Player].Remove(creature);

            CreatureDeletedFromField?.Invoke(creature);
        }

        public event Action<ICreature> CreatureDeletedFromField;

        public void ClearField()
        {
            playerCreaturesInGame[FirstPlayer].Clear();
            playerCreaturesInGame[SecondPlayer].Clear();
        }

        public void ChangeState(GameStage stage)
        {
            this.gameStage = stage;
            StateChanged?.Invoke(stage);
        }

        public event Action<GameStage> StateChanged;

        public HashSet<ICreature> GetPlayerCreaturesInGame(Player player)
        {
            return playerCreaturesInGame[player];
        }

        private ICreature CreateCastle(Player player)
        {
            return new Building(
                CreatureType.Castle, 
                1000, 
                new Size(100, 100),
                player);
        }

        public bool TryGetWinner(out Player winner)
        {
            winner = null;

            if (!FirstPlayer.Castle.IsAlive())
            {
                winner = SecondPlayer;
                ChangeState(GameStage.Finished);
            }

            if (!SecondPlayer.Castle.IsAlive())
            {
                winner = FirstPlayer;
                ChangeState(GameStage.Finished);
            }

            return winner != null;
        }


        public void Act()
        {
            foreach (var e in playerCreaturesInGame)
            {
                var player = e.Key;
                var enemies = GetEnemiesOf(player);

                foreach(var creature in e.Value)
                    creature.Act(enemies);
            }
        }

        public void Start(Player firstPlayer, Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;

            CreateCastlesForPlayers();
            ChangeState(GameStage.Started);
        }

        public void Start()
        {
            if (FirstPlayer is null || SecondPlayer is null)
                throw new Exception("Not every player inited!");

            Start(FirstPlayer, SecondPlayer);
        }

        private void CreateCastlesForPlayers()
        {
            var firstCastle = CreateCastle(FirstPlayer);
            var secondCastle = CreateCastle(SecondPlayer);

            FirstPlayer.Castle = firstCastle;
            SecondPlayer.Castle = secondCastle;

            playerCreaturesInGame.Add(FirstPlayer, new HashSet<ICreature>() { firstCastle });
            playerCreaturesInGame.Add(SecondPlayer, new HashSet<ICreature>() { secondCastle });
        }
    }
}
