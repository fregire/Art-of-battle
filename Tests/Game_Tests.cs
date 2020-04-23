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
        private Game game;

        [Test]
        public void GameSettings_Test()
        {
            var gameSettings = new GameSettings();
            game = new Game(gameSettings, cards);
            Assert.AreEqual(10, gameSettings.Volume);
            Assert.AreEqual(new Size(1280, 720), gameSettings.WindowSize);
            Assert.AreEqual(2, gameSettings.CardsPlayerCount);
        }

        [Test]
        public void Cards_Tests()
        {
            var orc = new MeleeCreature(
                CreatureType.Orc,
                400,
                100,
                50,
                new Size(100, 100),
                Direction.None);

            var knight = new MeleeCreature(
                CreatureType.Orc,
                600,
                90,
                80,
                new Size(100, 100),
                Direction.None);

            var cards = new HashSet<Card>() {
                new Card(orc, 100, 10),
                new Card(knight, 200, 10)
            };

            game.CreatePlayers("Daniil", "Roman");
        }
    }
}
