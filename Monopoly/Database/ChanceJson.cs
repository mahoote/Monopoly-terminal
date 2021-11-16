﻿using System;
using System.Collections;
using Monopoly.Factory.Classes;
using Monopoly.Factory.Interface;
using Newtonsoft.Json.Linq;

namespace Monopoly.Database
{
    public class ChanceJson
    {
        #region Private fields

        private JObject _jsonData;

        #endregion

        #region Constructors

        public ChanceJson(JObject jsonContent)
        {
            // Read JSON file.
            _jsonData = jsonContent;
        }

        #endregion

        #region Methods

        public ArrayList RetrieveAll()
        {
            // Get the specific card, e.g: Card 1.
            var jsonCard = _jsonData["Card"]["Chance"];

            var indexes = JArray.Parse(jsonCard["indexList"].ToString());
            var cards = JArray.Parse(jsonCard["chanceCards"].ToString());

            ArrayList squares = new ArrayList();
            ArrayList chanceCards = new ArrayList();
            
            foreach (var index in indexes)
            {
                int squareId = (int) index;

                // Only make the cards if they haven't already been made.
                if (chanceCards.Count == 0)
                {
                    int i = 0;
                    
                    foreach (var card in cards)
                    {
                        var jsonChance = jsonCard["chanceCards"];
                    
                        int cardId = (int) jsonChance[i]["id"];
                        string cardContent = jsonChance[i]["content"].ToString();
                        int cardValue = (int) jsonChance[i]["value"];
                        int cardMoveIndex = (int) jsonChance[i]["moveIndex"];

                        ChanceCard chanceCard = new ChanceCard(cardId, cardContent, cardValue, cardMoveIndex);
                        chanceCards.Add(chanceCard);

                        i++;

                    }
                }

                // The squares contains the same amount and info about the cards.
                CreateChance chance = new CreateChance(squareId, chanceCards);
                ISquare square = chance.BuildSquare();

                squares.Add(square);
            }

            // Return the list of all the squares.
            return squares;
        }

        #endregion
    }
}