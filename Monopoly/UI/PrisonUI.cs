using System;
using Monopoly.Logics.CardFactory.Interface;
using Monopoly.Logics.SquareLogics;

namespace Monopoly.UI
{
    public class PrisonUI : AbstractLogics
    {

        private PrisonLogics _prisonLogics = new();
        public override void Handle(ISquare square, int playerId)
        {
            ConsoleOutput.Print(square.ToString(), ConsoleColor.White);
            _prisonLogics.HandlePrisonLogic(playerId);
        }
    }
}