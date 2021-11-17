﻿using System;
using System.Collections.Generic;
using Monopoly.Logics.CardFactory.Classes;
using Monopoly.Logics.CardFactory.Interface;
using Monopoly.UI;

namespace Monopoly.Logics.SquareLogics
{
    public class ChanceLogics: AbstractLogics
    {
        private readonly GameManager _manager = GameManager.GetInstance();
        
        public override void Handle(ISquare square, int playerId)
        {
            Chance chance = (Chance)square;

            // Retreive all chanceCards.
            List<ChanceCard> cardsList = chance.GetChanceCards();
            // Pick one.
            ChanceCard chanceCard = cardsList[new Random().Next(cardsList.Count)];
            chance.SetChanceCard(chanceCard);
            
            ConsoleOutput.Print(square.ToString());

            Bank bank = new Bank();
            bank.ChanceHandler(playerId, chanceCard.Value);

            if (chanceCard.MoveIndex >= 0)
            {
                BoardMap map = _manager.Map;
                map.Players[playerId] = chanceCard.MoveIndex;
                
                _manager.SquareController(chanceCard.MoveIndex, playerId);
            }

            ConsoleOutput.PrintEnter();
            ConsoleInput.ReadString();
        }
    }
}