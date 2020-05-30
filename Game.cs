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
        public List<Level> Levels { get; set; }
        public Level CurrentLevel { get; set; }

        private GameStage gameStage = GameStage.NotStarted;
        public GameStage Stage => gameStage;

        private readonly Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;

        public Game(GameSettings settings, List<Level> lvls)
        {
            GameSettings = settings;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
            Levels = lvls;
        }

        public Game(List<Card> cards, List<Level> lvls)
        {
            GameSettings = new GameSettings();
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
            Levels = lvls;
        }

        public HashSet<ICreature> GetEnemiesOf(Player player)
        {
            return player.Equals(FirstPlayer)
                ? playerCreaturesInGame[SecondPlayer]
                : playerCreaturesInGame[FirstPlayer];
        }

        public bool PlaceCardCreatureOnField(Card card, Player player)
        {
            var isEnoughGold = player.BattleGoldAmount >= card.Cost;

            if (isEnoughGold)
            {
                player.BattleGoldAmount -= card.Cost;
                PlaceCreatureOnField(card.Creature.CreateCreature(player));

                return true;
            }
            else
                return false;

        }

        public void PlaceCreatureOnField(ICreature creature)
        {
            creature.Position = creature.Player.CreaturesSpawnPoint;
            playerCreaturesInGame[creature.Player].Add(creature);

            CreaturePlacedOnField?.Invoke(creature);
        }

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

        private Building CreateCastle(Player player)
        {
            return new Building(
                CreatureType.Castle, 
                1000, 
                new Size(500, 500),
                player);
        }

        public Player GetWinner()
        {
            Player winner = null;

            if (!FirstPlayer.Castle.IsAlive())
                winner = SecondPlayer;

            if (!SecondPlayer.Castle.IsAlive())
                winner = FirstPlayer;

            return winner;
        }

        public void Act()
        {
            var creaturesToDelete = new List<ICreature>();

            foreach (var e in playerCreaturesInGame)
            {
                var player = e.Key;
                var enemies = GetEnemiesOf(player);

                foreach (var creature in e.Value)
                {
                    if (creature.IsAlive())
                        creature.Act(enemies);
                    else
                        creaturesToDelete.Add(creature);
                }
            }

            if (IsFinished())
            {
                var winner = GetWinner();
                winner.GameGoldAmount += CurrentLevel.ReceivedGoldAmount;
                winner.PlayerLevelInfo.CurrentExperienceAmount += CurrentLevel.ReceivedExperienceAmount;

                ChangeState(GameStage.Finished);
            }

            foreach (var creature in creaturesToDelete)
                DeleteCreatureFromField(creature);

            Acted?.Invoke();
        }

        private bool IsFinished()
        {
            return !FirstPlayer.Castle.IsAlive() || !SecondPlayer.Castle.IsAlive();
        }

        public event Action Acted;

        public void Start(Player firstPlayer, Player secondPlayer, Level lvl)
        {
            AddPlayer(firstPlayer);
            AddPlayer(secondPlayer);
            ClearField();
            CreateCastlesForPlayers();
            CurrentLevel = lvl;
            FirstPlayer.BattleGoldAmount = GameSettings.GoldAmountPerPlayer;
            SecondPlayer.BattleGoldAmount = GameSettings.GoldAmountPerPlayer;
            ChangeState(GameStage.Started);
        }

        public void Start(Level lvl)
        {
            if (FirstPlayer is null || SecondPlayer is null)
                throw new Exception("Not every player inited!");

            Start(FirstPlayer, SecondPlayer, lvl);
        }

        public void AddPlayer(Player player)
        {
            if (FirstPlayer == null)
            {
                FirstPlayer = player;
                playerCreaturesInGame.Add(FirstPlayer, new HashSet<ICreature>());
            }
            else if (SecondPlayer == null)
            {
                SecondPlayer = player;
                playerCreaturesInGame.Add(SecondPlayer, new HashSet<ICreature>());
            }
        }

        private void CreateCastlesForPlayers()
        {
            var firstCastle = CreateCastle(FirstPlayer);
            var secondCastle = CreateCastle(SecondPlayer);

            FirstPlayer.Castle = firstCastle;
            SecondPlayer.Castle = secondCastle;

            PlaceCreatureOnField(firstCastle);
            PlaceCreatureOnField(secondCastle);
        }
    }
}
