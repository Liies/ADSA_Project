using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUs
{
    /// <summary>
    ///  Represents a Game between 10 players.
    /// </summary>
    class Game
    {
        private List<Player> players = new List<Player>(10); // 10 players per game.

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Game(List<Player> players)
        {
            this.players = players;
        }

        /// <summary>
        /// Adds a new score to each player of the game, calculated randomly.
        /// Updates the general score.
        /// </summary>
        public void New_Score()
        {
            Random random = new Random();
            foreach (Player p in this.players)
            {
                p.Scores.Add(random.Next(13)); // random score between 0 and 13 exclusive.
                p.Update_Score(); // update the player's general score

            }
        }

        /// <summary>
        /// Displays the players in the game
        /// </summary>
        public void Display()
        {
            Console.WriteLine("Players in the game :");
            foreach(Player p in this.players)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine("\n\n");
        }
    }
}
