using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Art_of_battle.Model;
using Art_of_battle.Model.Creatures;
using NUnit.Framework;

namespace Art_of_battle.Tests
{
    [TestFixture]
    class Game_Tests
    {
        [Test]
        public void GameDefaultSettings_Test()
        {
            var game = new Game();

            Assert.AreEqual(2, game.GameSettings.CardsCountInPlayerHand);
            Assert.AreEqual(5, game.GameSettings.GoldPerTick);
            Assert.AreEqual(100, game.GameSettings.MaxGoldAmount);
        }

        private List<Card> GenerateCards()
        {
            var orcCard = new Card(
                new MeleeCreature(
                    CreatureType.Orc,
                    100,
                    20,
                    50,
                    new Size(50, 50),
                    Direction.None),
                15,
                20
            );

            var knightCard = new Card(
                new MeleeCreature(
                    CreatureType.Knight,
                    100,
                    50,
                    10,
                    new Size(200, 300),
                    Direction.None
                ),
                25,
                15
            );

            return new List<Card>() {orcCard, knightCard};
        }

        private Game GetInitedAndStartedGame()
        {
            var cards = GenerateCards();
            var game = new Game(cards);

            var firstPlayer = new Player(
                "Daniil",
                Direction.Right,
                game.DefaultPlayerCards);

            var secondPlayer = new Player(
                "Roman",
                Direction.Left,
                game.DefaultPlayerCards);

            game.Start(firstPlayer, secondPlayer);

            return game;
        }
        [Test]
        public void StartingGame_Test()
        {
            var game = GetInitedAndStartedGame();

            Assert.AreEqual(game.GameSettings.CardsCountInPlayerHand, game.FirstPlayer.Cards.Count());
            Assert.AreEqual(game.GameSettings.CardsCountInPlayerHand, game.SecondPlayer.Cards.Count());
            Assert.AreEqual(GameStage.Started, game.GameStage);
        }

        [Test]
        public void TwoCreaturesAct_Test()
        {
            var game = GetInitedAndStartedGame();
            var testUnitType = new MeleeCreature(
                CreatureType.Knight,
                10,
                10,
                1,
                new Size(1, 1),
                Direction.None);

            var firstCreature = testUnitType.CreateCreature(game.FirstPlayer);
            var enemyCreature = testUnitType.CreateCreature(game.SecondPlayer);

            game.PlaceCreatureOnField(firstCreature, game.FirstPlayer);
            game.PlaceCreatureOnField(enemyCreature, game.SecondPlayer);

            firstCreature.Position = new Point(0, 0);
            enemyCreature.Position = new Point(1, 1);

            firstCreature.Act(game.GetEnemiesOf(game.FirstPlayer));

            Assert.AreEqual(10, enemyCreature.Health);
            Assert.AreEqual(0, enemyCreature.CurrHealth);

        }
    }
}
