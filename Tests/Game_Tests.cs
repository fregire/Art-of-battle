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

            var orcCard = new Card(new Orc(300), 10, 20);
            var knightCard = new Card(new Knight(150), 25, 40);
            var cards = new List<Card>() {orcCard, knightCard};

            var game = new Game(gameSettings, cards);

            game.CreatePlayers("Daniil", "Bot");

        }
    }
}
