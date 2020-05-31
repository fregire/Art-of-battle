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
            var cardToPlace = game.SecondPlayer.Cards.First();
            var isEnoughTime = cardToPlace.TimeElapsed == 0 ||
                               timeElapsedSinceStart - cardToPlace.TimeElapsed > cardToPlace.TimeReloadInMs;

            if (isEnoughTime)
            {
                var isEnoughGold = game.PlaceCardCreatureOnField(cardToPlace, game.SecondPlayer);

                if (isEnoughGold)
                    cardToPlace.TimeElapsed = timeElapsedSinceStart;
            }
        }
    }
}
