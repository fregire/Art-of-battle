﻿using System;
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

        public GameStage GameStage { get; }

        public readonly List<Card> Cards;

        private readonly Dictionary<Player, HashSet<ICreature>> playerCreaturesInGame;

        public Game(GameSettings settings, List<Card> cards)
        {
            GameSettings = settings;
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
            GameStage = GameStage.NotStarted;
        }

        public Game(List<Card> cards)
        {
            GameSettings = new GameSettings();
            Cards = cards;
            playerCreaturesInGame = new Dictionary<Player, HashSet<ICreature>>();
            GameStage = GameStage.NotStarted;
        }

        public HashSet<ICreature> GetEnemiesOf(Player player)
        {
            return player.Equals(FirstPlayer)
                ? playerCreaturesInGame[SecondPlayer]
                : playerCreaturesInGame[FirstPlayer];
        }

        public void PlaceCardCreatureOnField(Card card, Player player)
        {
            PlaceCreatureOnField(card.Creature.CreateCreature(player), player);
        }

        public void PlaceCreatureOnField(ICreature creature, Player player)
        {
            creature.Position = player.CreaturesSpawnPoint;
            playerCreaturesInGame[player].Add(creature);

            CreaturePlacedOnField?.Invoke(creature);
        }

        public event Action<ICreature> CreaturePlacedOnField;

        public void DeleteCreatureFromField(ICreature creature, Player player)
        {
            playerCreaturesInGame[player].Remove(creature);

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
            this.GameStage = stage;
            StateChanged?.Invoke(stage);
        }

        public event Action<GameStage> StateChanged;

        private ICreature CreateCastle(Direction direction)
        {
            return new Building(
                CreatureType.Castle, 
                1000, 
                new Size(100, 100),
                Direction.None);
        }

        public Player GetWinner()
        {
            if (!FirstPlayer.Castle.IsAlive())
                return SecondPlayer;

            if (!SecondPlayer.Castle.IsAlive())
                return FirstPlayer;

            return null;
        }

        public void Start(Player firstPlayer, Player secondPlayer)
        {
            var firstCastle = CreateCastle(firstPlayer.CreaturesDirection);
            var secondCastle = CreateCastle(secondPlayer.CreaturesDirection);

            FirstPlayer = firstPlayer;
            FirstPlayer.Castle = firstCastle;
            SecondPlayer = secondPlayer;
            SecondPlayer.Castle = secondCastle;

            playerCreaturesInGame.Add(FirstPlayer, new HashSet<ICreature>() { firstCastle });
            playerCreaturesInGame.Add(SecondPlayer, new HashSet<ICreature>() { secondCastle });

            ChangeState(GameStage.Started);
        }
    }
}
