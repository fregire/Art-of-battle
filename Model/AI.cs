using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class AI
    {
        private Game game;
        public AI(Game game)
        {
            this.game = game;
        }

        public void Act(int timeElapsedSinceStart)
        {
            var cardToPlace = game.CurrentLevel.LevelName == LevelName.Mountains 
                ? GetCard() : game.SecondPlayer.ChoosedCardsForGame.First();
            var isEnoughTime = cardToPlace.TimeElapsed == 0 ||
                               timeElapsedSinceStart - cardToPlace.TimeElapsed > cardToPlace.TimeReloadInMs;

            if (isEnoughTime)
            {
                var isEnoughGold = game.PlaceCardCreatureOnField(cardToPlace, game.SecondPlayer);

                if (isEnoughGold)
                    cardToPlace.TimeElapsed = timeElapsedSinceStart;
            }
        }

        private Card GetCard()
        {
            var rnd = new Random();
            var index = rnd.Next(0, game.SecondPlayer.ChoosedCardsForGame.Count);

            return game.SecondPlayer.ChoosedCardsForGame[index];
        }
    }
}
