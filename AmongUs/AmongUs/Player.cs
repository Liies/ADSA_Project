using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace AmongUs
{
    /// <summary>
    ///  Represents a Player in the game.
    /// </summary>
    class Player
    {
        private string pseudo; // identified by its pseudo
        private List<double> scores = new List<double>(); // list of its scores for each game
        private double score = 0; // final score

        // Step 1 : in our avl tree, each node is a player. So it has a left child and a right child. 
        private Player left;
        private Player right;

        private List<Player> seen_players = new List<Player>(); // list of the players he has seen (step 2)
        //private int color = 0;

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Player(string pseudo)
        {
            this.pseudo = pseudo;
        }

        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }

        public List<double> Scores
        {
            get { return scores; }
            set { scores = value; }
        }
        public double Score
        {
            get { return score; }
            set { score = value; }
        }

        public Player Left
        {
            get { return left; }
            set { left = value; }
        }
        public Player Right
        {
            get { return right; }
            set { right = value; }
        }

        public List<Player> Seen_Players
        {
            get { return seen_players; }
            set { seen_players = value; }
        }

        /*public int Color
        {
            get { return color; }
            set { color = value; }
        }*/

        /// <summary>
        /// Updates the score of the player. 
        /// </summary>
        public void Update_Score()
        {
            this.score = this.scores.Average(); // mean of all its scores.
        }

        /// <summary>
        /// Describes each player with his scores obtained in each game and his final score.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            String results = "Player : " + this.pseudo + " ; Scores : ";
            foreach (int s in this.Scores)
            {
                results += s + " | ";
            }
            results += " ; Average : " + this.score;
            return results;
        }

        /// <summary>
        /// Compares two players.
        /// Gives true if the players are the same, false otherwise.
        /// </summary>
        /// <param name=obj>Player which this is compared to</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   pseudo == player.pseudo;
        }


    }
}
