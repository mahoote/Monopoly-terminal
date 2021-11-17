﻿using System;
using Monopoly.Factory.Classes;
using Monopoly.Logics;
using Monopoly.Logics.PlayerFlyweight.Static;

namespace Monopoly.UI
{
    public class MenuUI
    {
        private PlayerGenerator _generator = PlayerGenerator.GetInstance();
        private readonly GameManager _manager = new();
        private int _currentPlayerId = 1;

        public void StartGame()
        {
            _manager.InitializeMap();
            
            ConsoleOutput.Print("--- Welcome to Monopoly! ---", ConsoleColor.Magenta);
            ConsoleOutput.Print("How many players are you ( 2-4 )", ConsoleColor.Magenta);

            // Set players:
            int playersCount = GetPlayerCount();
            _manager.CreatePlayers(playersCount);
            SetPlayers(playersCount);
            
            PrintMap();

            // Print Info (BoardMap, Players + wallet):
            while (playersCount > 1)
            {
                PrintState();
            }
        }

        private void PrintMap()
        {
            ConsoleOutput.Print("------- Board Map -------\n" + $"{_manager.Map}", ConsoleColor.Yellow);
        }

        private int GetPlayerCount()
        {
            int value = 0;
            bool playersSet = false;
            
            while (!playersSet)
            {
                value = ConsoleInput.ReadInt();

                if (value is >= 2 and <= 4)
                    playersSet = true;
                else
                    ConsoleOutput.Print("- You must be minimum 2 players and maximum 4!");
            }

            return value;
        }

        private void SetPlayers(int count)
        {
            foreach (var player in _generator.Players)
            {
                ConsoleOutput.Print($"Type in name for player {player.Key}");
                string name = ConsoleInput.ReadString();
                
                _manager.SetPlayersInfo(name, player.Key);
            }

            ConsoleOutput.Print("Players set", ConsoleColor.Red);
        }

        private void PrintState()
        {
            if (_currentPlayerId >= _manager.Map.Players.Count + 1)
                _currentPlayerId = 1;
            
            NextTurn(_currentPlayerId);
        }

        private void NextTurn(int playerId)
        {
            int playerIndex;

            // Player starts next turn.
            ConsoleInput.ReadString();
            _currentPlayerId++;
            Console.Clear();
            
            ConsoleOutput.Print($"Your turn: \n{_generator.Players[playerId]}");
            
            if (!PlayerGenerator.GetInstance().Get(playerId).InPrison)
            {
                Dice dice = new Dice();
                int diceThrow;
                
                ConsoleOutput.Print("Press enter to roll the dice", ConsoleColor.Cyan);
            
                ConsoleInput.ReadString();
                Console.Clear();

                diceThrow = dice.RollDice();
                ConsoleOutput.Print($"You rolled: {diceThrow}", ConsoleColor.Magenta);
            
                _manager.MovePlayer(playerId, diceThrow);
                playerIndex = _manager.Map.Players[playerId];

                _manager.SquareController(playerIndex, playerId);
                PrintMap();

                ConsoleOutput.Print($"{_generator.Players[playerId]}", ConsoleColor.White);
            
                ConsoleOutput.Print("Your turn has ended", ConsoleColor.Red);
            }
            else
            {
                _manager.SquareController(6, playerId);
            }
            
        }
    }
}