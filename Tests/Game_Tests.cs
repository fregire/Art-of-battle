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
        private List<Card> GenerateCards()
        {
            var orcCard = new Card(
                new MeleeCreature(
                    CreatureType.Orc,
                    100,
                    20,
                    50,
                    new Size(50, 50)),
                15,
                20
            );

            var knightCard = new Card(
                new MeleeCreature(
                    CreatureType.Knight,
                    100,
                    50,
                    10,
                    new Size(200, 300)
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

            Assert.IsNotEmpty(game.Cards);
            Assert.AreEqual(game.GameSettings.CardsCountInPlayerHand, game.FirstPlayer.Cards.Count());
            Assert.AreEqual(game.GameSettings.CardsCountInPlayerHand, game.SecondPlayer.Cards.Count());
            Assert.AreEqual(GameStage.Started, game.Stage);
        }


        private ICreature GetTestCreature(int hp, int damage, int attackRange, Size size, Player player)
        {
            return new MeleeCreature(
                CreatureType.Knight,
                hp,
                damage,
                attackRange,
                size,
                player);
        }

        private void GenerateTestCreaturesForPlayer(int creaturesCount, Game game, Player player)
        {
            var testCreature = GetTestCreature(10, 10, 10, new Size(20, 20), player);

            for(var i = 0; i < creaturesCount; i++)
                game.PlaceCreatureOnField(testCreature.CreateCreature(player));
        }

        [Test]
        public void RightDirectionAttack_Test()
        {
            var game = GetInitedAndStartedGame();
            var testUnitType = new MeleeCreature(
                CreatureType.Knight,
                10,
                10,
                1,
                new Size(1, 1));

            var firstCreature = testUnitType.CreateCreature(game.FirstPlayer);
            var enemyCreature = testUnitType.CreateCreature(game.SecondPlayer);

            game.PlaceCreatureOnField(firstCreature);
            game.PlaceCreatureOnField(enemyCreature);

            firstCreature.Position = new Point(0, 0);
            enemyCreature.Position = new Point(1, 1);

            firstCreature.Act(game.GetEnemiesOf(game.FirstPlayer));

            Assert.AreEqual(10, enemyCreature.MaxHealth);
            Assert.AreEqual(0, enemyCreature.CurrHealth);
        }

        [Test]
        public void LeftDirectionAttack_Test()
        {
            var game = GetInitedAndStartedGame();
            var creature = GetTestCreature(10, 10, 10, new Size(10, 10), null);

            var creature1 = creature.CreateCreature(game.FirstPlayer);
            var enemyCreature = creature.CreateCreature(game.SecondPlayer);

            game.PlaceCreatureOnField(creature1);
            game.PlaceCreatureOnField(enemyCreature);

            creature1.Position = new Point(150, 0);
            enemyCreature.Position = new Point(165, 0);

            enemyCreature.Act(game.GetEnemiesOf(game.SecondPlayer));

            Assert.AreEqual(10, creature1.MaxHealth);
            Assert.AreEqual(0, creature1.CurrHealth);
        }


        [Test]
        public void ChooseClosestEnemyAndDontTouchDiedEnemy_Test()
        {
            var game = GetInitedAndStartedGame();
            var creature = GetTestCreature(10, 10, 10, new Size(10, 10), null);

            var creature1 = creature.CreateCreature(game.FirstPlayer);
            var creature2 = creature.CreateCreature(game.FirstPlayer);
            var enemyCreature = creature.CreateCreature(game.SecondPlayer);

            game.PlaceCreatureOnField(creature1);
            game.PlaceCreatureOnField(creature2);
            game.PlaceCreatureOnField(enemyCreature);

            creature1.Position = new Point(150, 0);
            creature2.Position = new Point(151, 0);
            enemyCreature.Position = new Point(165, 0);

            enemyCreature.Act(game.GetEnemiesOf(game.SecondPlayer));

            Assert.AreEqual(0, creature2.CurrHealth);
            Assert.AreEqual(10, creature1.CurrHealth);

            enemyCreature.Act(game.GetEnemiesOf(game.SecondPlayer));

            Assert.AreEqual(0, creature2.CurrHealth);
            Assert.AreEqual(0, creature1.CurrHealth);
        }

        [Test]

        public void CreatureIsRemoved()
        {
            var game = GetInitedAndStartedGame();
            var creature = GetTestCreature(10, 10, 10, new Size(10, 10), null);
            var creature1 = creature.CreateCreature(game.FirstPlayer);
            var creature2 = creature.CreateCreature(game.FirstPlayer);
            game.PlaceCreatureOnField(creature1);
            game.PlaceCreatureOnField(creature2);
            Assert.AreEqual(3, game.GetPlayerCreaturesInGame(game.FirstPlayer).Count);
            game.DeleteCreatureFromField(creature2);
            Assert.AreEqual(2, game.GetPlayerCreaturesInGame(game.FirstPlayer).Count);
            game.DeleteCreatureFromField(creature1);
            Assert.AreEqual(1, game.GetPlayerCreaturesInGame(game.FirstPlayer).Count);
        }

        [Test]
        public void GetWinner_Test()
        {
            var game = GetInitedAndStartedGame();
        }


    }
}
