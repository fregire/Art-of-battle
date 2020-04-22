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
        public void GameInit_Tests()
        {
            var gameSettings = new GameSettings();
            Assert.AreEqual(10, gameSettings.Volume);
            Assert.AreEqual(new Size(1280, 720), gameSettings.WindowSize);
            Assert.AreEqual(2, gameSettings.CardsPlayerCount);

            var orc = new MeleeCreature(
                CreatureType.Orc, 
                400, 
                100, 
                50, 
                new Size(100, 100));

            var knight = new MeleeCreature(
                CreatureType.Orc,
                600,
                90,
                80,
                new Size(100, 100));

            var cards = new HashSet<Card>() {
                new Card(orc, 100, 10), 
                new Card(knight, 200, 10)
            };

            var game = new Game(gameSettings, cards);
            game.CreatePlayers("Daniil", "Roman");
        }
    }
}
