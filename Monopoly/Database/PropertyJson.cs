﻿using System.Drawing;
using Monopoly.Factory.Classes;
using Monopoly.Factory.Interface;
using Newtonsoft.Json.Linq;

namespace Monopoly.Database
{
    public class PropertyJson
    {
        #region Private fields

        private JObject _jsonData;

        #endregion

        #region Constructors

        public PropertyJson(JObject jsonContent)
        {
            // Read JSON file.
            _jsonData = jsonContent;
        }

        #endregion

        #region Methods

        public ISquare Retrieve(int id)
        {
            // Get the specific card, e.g: Card 1.
            var jsonCard = _jsonData["Card"][id.ToString()];
                
            // Only create cards that exist in the json file.
            if(jsonCard != null) {
                    
                string name = jsonCard["name"].ToString();
                Color color = Color.FromName(jsonCard["color"].ToString());
                int purchase = (int) jsonCard["purchase"];
                int rent = (int) jsonCard["rent"];

                CreateProperty property = new CreateProperty(id, name, color, purchase, rent);
                    
                ISquare square = property.BuildSquare();
                
                return square;
            }

            return null;
        }

        #endregion
    }
}