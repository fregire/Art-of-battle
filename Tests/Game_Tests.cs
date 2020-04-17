using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Art_of_battle.Model;
using NUnit.Framework;

namespace Art_of_battle.Tests
{
    [TestFixture]
    class Game_Tests
    {
        [Test]
        public void GameInit_Tests()
        {
            var gameSettings = new Settings();
            Assert.AreEqual(gameSettings.Volume, 10);
            Assert.AreEqual(gameSettings.WindowSize, new Size(1280, 720));
            Assert.AreEqual(gameSettings.Slots.Length, 4);
        }
    }
}
