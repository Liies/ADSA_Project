using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUs
{
    /// <summary>
    ///  Creates the graph for the relation "have seen" between two players
    /// </summary>
    class Graph
    {
        private List<Player> players = new List<Player>(); // list of the players that consitute the graph

        /// <summary>
        ///  Constructor of the class.
        /// </summary>
        /// <param name=matrix>Matrix : 1 if the players located in indexes i and j have seen each other, 0 otherwise.
        /// <param name=players>List of the players of the game.</param>
        public Graph(int[,] matrix, List<Player> players)
        {
            this.players = players;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1) // if the value of the matrix is one
                    {
                        this.players[i].Seen_Players.Add(this.players[j]); // then we add the player j in the list of seen players of player i
                    }
                }
            }
        }

        /// <summary>
        ///  Property
        /// </summary>
        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        /// <summary>
        /// After a player died, gives the probable imposters.
        /// </summary>
        /// <param name=dead>Player which is dead.</param>
        /// <returns>List<List<Player>></returns>
        public List<List<Player>> Probable_Imposters(Player dead)
        {
            List<List<Player>> results = new List<List<Player>>(); // list of second probable imposters for each first propable imposter
            foreach (Player p in dead.Seen_Players) // for each player who have seen the dead
            {
                List<Player> tmp = new List<Player>();
                tmp.Add(p);
                foreach (Player q in this.players)
                {
                    if (!p.Seen_Players.Contains(q) && !p.Equals(q)) tmp.Add(q); // we add the players he hasn't seen
                }
                results.Add(tmp);
            }
            return results;
        }

        /// <summary>
        /// Displays the list of probable imposters.
        /// </summary>
        /// <param name=probable_imposters>List of probable imposters.</param>
        public void Diplay_Impostors(List<List<Player>> probable_imposters)
        {
            foreach (List<Player> probability in probable_imposters)
            {
                Console.WriteLine("\nIf the first Impostor is " + probability[0].Pseudo); // the first player of each list is the probable fisrt imposter
                Console.WriteLine("\nThe second one might be :\n");
                for (int i = 1; i < probability.Count; i++)
                {
                    Console.WriteLine(probability[i].Pseudo);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Colors the graph according to the relation "have seen".
        /// </summary>
        /// <param name=k>Node we want to color.</param>
        /*public void Coloring_Graph(int k)
        {
            for (int c = 1; c <= 3; c++)
            {
                if (isSafe(k, c)) // if the color is safe
                {
                    this.players[k].Color = c; // color the node k
                    if (k + 1 < this.players.Count) Coloring_Graph(k + 1); // color the next node if there is yet
                    else this.Diplay(); // dispplay the tree otherwise
                    return;
                }
            }
        }*/

        /// <summary>
        /// Gives true if the color is safe, which means we can use it for the given node.
        /// False otherwise.
        /// </summary>
        /// <param name=k>Index of the node we want to color.
        /// <param name=c>Color we want to use.</param>
        /// <returns>bool</returns>
        /*public Boolean isSafe(int k, int c)
        {
            for (int i = 0; i <this.players.Count; i++)
            {
                if(this.players[k].Seen_Players.Contains(this.players[i]) && c == this.players[i].Color) // if the color is already used by a neighbor 
                {
                    return false; // we can't take it
                }
            }
            return true;
        }*/

        /// <summary>
        /// Displays the color of each player.
        /// </summary>
        /*public void Diplay()
        {
            int i = 0;
            foreach (Player p in this.players)
            {
                Console.WriteLine("Player " + i + " : " + p.Color);
                i += 1;
            }
        }*/
    }
}
