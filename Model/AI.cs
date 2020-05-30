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

        public void Act()
        {
            var cardToPlace = game.SecondPlayer.Cards.First();
            if (cardToPlace.Cost < game.SecondPlayer.BattleGoldAmount)
                game.PlaceCardCreatureOnField(cardToPlace, game.SecondPlayer);
        }
    }
}
