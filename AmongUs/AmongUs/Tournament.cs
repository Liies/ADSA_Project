using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;



namespace AmongUs
{

    /// <summary>
    ///  Organizes the tournament.
    ///  Generates also an AVL tree based on ranking oh the players
    /// </summary>
    class Tournament
    {
        private List<Player> all_players = new List<Player>(); // list of all the players
        private Player root; // root of the AVL tree

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Tournament(Player root, List<Player> all_players)
        {
            this.all_players = all_players;
            this.root = root;
        }

        public Player Root
        {
            get { return root; }
            set { root = value; }
        }

        /// <summary>
        /// Starts the tournament.
        /// </summary>
        public void Start()
        {
            // Interaction with the user
            /*Console.WriteLine("\n1.View players by round....................................... ");
            Console.WriteLine("\n2.View players by round....................................... ");
            Console.WriteLine("\n3.View of the last Five last games............................ ");
            Console.WriteLine("\n4.Podium...................................................... ");
            string str = Convert.ToString(Console.ReadLine());*/

            // The first three rounds are random
            int i;
            Round random_rounds;
            for (i = 1; i <= 3; i++)
            {
                random_rounds = new Round(10, this.all_players);
                Console.WriteLine("\n\n--------------------- Round n°" + i + " ---------------------\n");
                random_rounds.Start_Random(); // new scores for each player
                random_rounds.Display_Score();
                Console.WriteLine("\nPress any key to see the next round.");
                Console.ReadKey();
                Console.Clear();

            }

            Console.WriteLine();
            Console.WriteLine("\n First Ranking before Round 4\n");
            // Round 4 starts with ranking and selection
            // Ranking
            this.Remove_All(this.all_players.Count);
            Console.WriteLine("\n");
            this.Create_Tree(this.all_players);

            // Selection
            List<Player> previous_players = this.all_players;
            Console.WriteLine("\nPlayers still in the game : \n");
            List<Player> remaining_players = this.Remove_Last10();

            Console.WriteLine("\n\nPress any key to continue\n");
            Console.ReadKey();
            Console.Clear();

            int nb_games = 9;
            i = 4;
            Round classified_rounds;
            while (remaining_players.Count > 10)
            {
                this.Remove_All(previous_players.Count); // tree deleted in order to recreate another one completely

                classified_rounds = new Round(nb_games, remaining_players);
                Console.WriteLine("\n\n--------------------- Round n°" + i + " ---------------------\n");
                classified_rounds.Start_Classified(); // new scores for each player
                Console.WriteLine("\n");
                classified_rounds.Display_Score();
                Console.WriteLine("\n");
                // New ranking
                this.Create_Tree(remaining_players);
                Console.WriteLine("\n");

                // New selection
                previous_players = remaining_players;
                Console.WriteLine("\nPlayers still in the game : \n");
                remaining_players = this.Remove_Last10();


                nb_games -= 1;  // a game in less after each round
                i += 1;

                Console.WriteLine("\n\nPress any key to see the next round.");
                Console.ReadKey();
                Console.Clear();


            }

            Console.WriteLine("\n                                     TOP 10                                  \n");
            Console.WriteLine("                        Five last games with the top 10 players                \n");
            // Five last games with the top 10 players
            List<Player> top10 = remaining_players;

            foreach (Player p in top10) // reinitialized ranking
            {
                p.Score = 0;
                p.Scores = new List<Double>();
            }

            for (int j = 0; j < 5; j++)
            {
                Game last_five_games = new Game(top10);
                Console.WriteLine("");
                Console.WriteLine("\n--------------------- Game n°" + (j + 1) + " ---------------------");
                this.Remove_All(previous_players.Count); // tree deleted in order to recreate another one completely
                last_five_games.New_Score(); // new scores for each player
                this.Create_Tree(top10); // last ranking

            }
            Console.WriteLine("");
            Console.WriteLine("\nPress any key to see the podium.");
            Console.ReadKey();
            Console.Clear();


            // End of the game - Podium
            Console.WriteLine("\n\nEnd of the game !!!");
            Console.WriteLine("\n\n..................................Podium...................................\n");

            for (int j = 0; j < 7; j++) // delete the last 7 players from the tree
            {
                Player last_player = this.Min_Score_Player(this.Root);
                this.Delete(last_player);
            }

            Console.WriteLine("\n1st Place : " + this.Root.Right.ToString());
            Console.WriteLine("\n2nd Place : " + this.Root.ToString());
            Console.WriteLine("\n3rd Place : " + this.Root.Left.ToString());

            Console.WriteLine("\nPress any key to go back to the main menu\n");
        }


        /// <summary>
        /// Creates a tree and displays it.
        /// </summary>
        /// <param name=players>List from which will be created the tree.</param>
        public void Create_Tree(List<Player> players)
        {
            foreach (Player p in players)
            {
                this.Add(p); // all the players are added to the tree
            }
            Console.WriteLine("\n\nAVL Tree of players.\n\n");
            this.Display_Tree(this.Root);
        }

        /// <summary>
        /// Displays the tree.
        /// In-Order.
        /// </summary>
        /// <param name=current_node>Node from which we start to display.</param>
        public void Display_Tree(Player current_node)
        {
            if (current_node != null)
            {
                Display_Tree(current_node.Left);
                Console.Write(current_node.Pseudo + " : ({0}) | ", current_node.Score);
                Display_Tree(current_node.Right);
            }
        }

        /// <summary>
        /// Adds a node to the tree.
        /// </summary>
        public void Add(Player p)
        {
            if (this.root == null) // if the root of the tree is empty, then the new node become the root
            {
                this.root = p;
            }
            else // otherwise if there is already a root
            {
                root = Insert_Player(root, p); // recursivity on the root
            }
        }

        /// <summary>
        /// Deletes a given node from the tree.
        /// </summary>
        /// <param name=p>Node to be deleted.</param>
        public void Delete(Player p)
        {
            this.root = Delete(this.root, p);
        }

        /// <summary>
        /// Deletes the last ten nodes from the tree.
        /// Gives a list of the remaining nodes in the tree.
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> Remove_Last10()
        {
            for (int i = 0; i < 10; i++)
            {
                Player last_player = this.Min_Score_Player(this.Root); // node with lowest value
                this.Delete(last_player);                              // deleted
            } // done 10 times
            Console.WriteLine("\nSelection (Delete the 10 last players)\n");
            this.Display_Tree(this.Root); // display the new tree
            List<Player> remaining_players = new List<Player>();
            this.Remaining_Players(this.Root, remaining_players); // new list of the remaining players
            return remaining_players;
        }

        /// <summary>
        /// Deletes all the nodes from the tree.
        /// </summary>
        /// <param name=nb_players>Number of nodes to be removed.</param>
        public void Remove_All(int nb_players)
        {
            for (int i = 0; i < nb_players; i++)
            {
                Player last_player = this.Min_Score_Player(this.Root); // node with lowest value
                this.Delete(last_player);                              // deleted
            } // done the number times of nodes in the tree
        }

        /// <summary>
        /// Gives the list of the nodes contained in the tree.
        /// </summary>
        /// <param name=current_player>Node from which we start to store.
        /// <param name=results>New list to be filled in.</param>
        public void Remaining_Players(Player current_player, List<Player> results)
        {
            if (current_player != null)
            {
                // In-Order filling in
                Remaining_Players(current_player.Left, results);
                results.Add(current_player);
                Remaining_Players(current_player.Right, results);
            }
        }

        /// <summary>
        /// Inserts a node into the tree below a given node.
        /// Gives the last node returned by rotations which will take the place of the node <paramref name="current_player" /> given in parameter.
        /// </summary>
        /// <param name=current_player>Node under which will be inserted the new one.
        /// <param name=new_player>Node we want to insert.</param>
        /// <returns>object:Player</returns>
        public Player Insert_Player(Player current_player, Player new_player)
        {
            if (current_player == null) // end of the recursive, the new node is inserted
            {
                current_player = new_player;
                return current_player;
            }
            else if (new_player.Score <= current_player.Score) // if the score of the new player is lower than the current_player's one, the node will be inserted in the left subtree
            {
                current_player.Left = Insert_Player(current_player.Left, new_player); // recursivity on the left subtree
                current_player = Balancing_Tree(current_player); // after inserting, rebalancing the tree
            }
            else if (new_player.Score > current_player.Score) // otherwise, if the score of the new player is greater than the current_player's one, the node will be inserted in the right subtree
            {
                current_player.Right = Insert_Player(current_player.Right, new_player); // recursivity on the right subtree
                current_player = Balancing_Tree(current_player); // after inserting, rebalancing the tree
            }
            return current_player;
        }

        /// <summary>
        /// Deletes a given node.
        /// Gives the node which will take the place of the deleted one.
        /// </summary>
        /// <param name=current_player>Parent of the node we want to delete.
        /// <param name=p>Node from which we start comparing the scores.</param>
        /// <returns>object:Player</returns>
        public Player Delete(Player current_player, Player p)
        {
            // Step 1 : Find the node to be deleted (Standard Binary Search Tree)
            if (current_player == null) // end of the recursive
                return current_player;

            if (p.Score < current_player.Score) // if the node to be deleted has a smaller score than the current_player's one
                current_player.Left = Delete(current_player.Left, p);

            else if (p.Score > current_player.Score) // otherwise if the node to be deleted has a greater score than the current_player's one
                current_player.Right = Delete(current_player.Right, p); // then it's on the right subtree

            else if (p.Score == current_player.Score && p.Pseudo != current_player.Pseudo) // otherwise if the node to be deleted has a the same score as the current_player's one
                                                                                           // but not the same pseudo, then it could be on the right or on the left subtree
            {
                current_player.Left = Delete(current_player.Left, p);
                current_player.Right = Delete(current_player.Right, p);
            }

            else // otherwise if the node to be deleted has a the same score as the current_player's one and the same pseudo, then we found the node we want to delete
            {
                // Step 2 : deal with the children of the node to be deleted
                if ((current_player.Left == null) || (current_player.Right == null)) // only one or no child cases
                {
                    Player tmp = null;
                    if (current_player.Left == null)
                        tmp = current_player.Right;
                    else
                        tmp = current_player.Left;

                    if (tmp == null) // node with no child case
                    {
                        //tmp = current_player;
                        current_player.Right = null; // delete the right subtree
                        current_player.Left = null; // delete the left subtree
                        current_player = null; // delete the wanted node
                    }
                    else // node with only child case 
                    {
                        current_player.Right = null; // delete the right subtree
                        current_player.Left = null; // delete the left subtree
                        current_player = tmp; // the deleted node become the only child he had
                    }

                }
                else // otherwise two children cases
                {
                    Player tmp = Min_Score_Player(current_player.Right); // the inorder successor of the wanted node

                    // Copy the inorder successor's data to this Player  
                    current_player.Score = tmp.Score;
                    current_player.Pseudo = tmp.Pseudo;
                    current_player.Scores = tmp.Scores;

                    current_player.Right = Delete(current_player.Right, tmp); // delete the inorder successor
                }
            }

            // If the tree had only one Player then return  
            if (current_player == null)
                return current_player;

            // Step 3 : re-balance the tree
            current_player = Balancing_Tree(current_player);

            return current_player;
        }

        /// <summary>
        /// Gives the node with the lowest score.
        /// </summary>
        /// <param name=root>Node from which we start comparing the scores.</param>
        /// <returns>object:Player</returns>
        public Player Min_Score_Player(Player root)
        {
            if (root == null || root.Left == null)
            {
                return root;
            }
            return Min_Score_Player(root.Left);
        }

        /// <summary>
        /// Balances the subtree of the given root.
        /// Gives the last node returned by rotations.
        /// </summary>
        /// <param name=current_player>Node on which balancing will be controlled.</param>
        /// <returns>object:Player</returns>
        public Player Balancing_Tree(Player current_player)
        {
            int balancing_factor = Balancing_Factor(current_player); // balancing factor of the given node
            if (Math.Abs(balancing_factor) > 1) // if the absolute value is greater than one, the tree is unbalanced
            {
                if (balancing_factor > 1) // if the balancing factor is greater than one, the left subtree is too long
                {
                    if (Balancing_Factor(current_player.Left) >= 0) // if the balancing factor of the below left node is greater than zero, the left subtree of the left subtree is too long
                    {
                        current_player = Right_Rotation(current_player); // Left-Left case : then we perform a right rotation
                    }
                    else // otherwise, if it's lower than zero, the right subtree of the left subtree is too long
                    {
                        current_player = Left_Right_Rotation(current_player); // Left-Right case : then we perform a left rotation followed by a right one
                    }
                }
                else if (balancing_factor < -1) // otherwise if the balancing_factor is lower than minus one, the right subtree is too long
                {
                    if (Balancing_Factor(current_player.Right) > 0) // if the balancing factor of the below right node is greater than zero, the left subtree of the right subtree is too long
                    {
                        current_player = Right_Left_Rotation(current_player); // Right-Left case : then we perform a right rotation followed by a left one
                    }
                    else // otherwise, if it's lower than zero, the right subtree of the right subtree is too long
                    {
                        current_player = Left_Rotation(current_player); // Right-Right case : then we perform a left rotation
                    }
                }
            }
            return current_player;
        }

        /// <summary>
        /// Left-Right Case.
        /// Performs a left rotation on the given node, then a right rotation on the node returned.
        /// Gives the node which will take the place of the given one.
        /// </summary>
        /// <param name=current_player>Node on which the first rotation will be made.</param>
        /// <returns>object:Player</returns>
        public Player Left_Right_Rotation(Player current_player)
        {
            Player tmp = current_player.Left;
            current_player.Left = Left_Rotation(tmp);
            return Right_Rotation(current_player);
        }

        /// <summary>
        /// Right-Left Case.
        /// Performs a right rotation on the given node, then a left rotation on the node returned.
        /// Gives the node which will take the place of the given one.
        /// </summary>
        /// <param name=current_player>Node on which the first rotation will be made.</param>
        /// <returns>object:Player</returns>
        public Player Right_Left_Rotation(Player current_player)
        {
            Player tmp = current_player.Right;
            current_player.Right = Right_Rotation(tmp);
            return Left_Rotation(current_player);
        }

        /// <summary>
        /// Left-Left Case.
        /// Perform a right rotation on the given node.
        /// Gives the node which will take the place of the given one.
        /// </summary>
        /// <param name=current_player>Node on which the rotation will be made.</param>
        /// <returns>object:Player</returns>
        public Player Right_Rotation(Player current_player)
        {
            /*
             * Same principle as permuting two values :
             * tmp = a;
             * a = b;
             * b = a;
             */
            Player tmp = current_player.Left;
            current_player.Left = tmp.Right;
            tmp.Right = current_player;
            return tmp;

        }

        /// <summary>
        /// Right-Right Case.
        /// Performs a left rotation on the given node.
        /// Gives the node which will take the place of the given one.
        /// </summary>
        /// <param name=current_player>Node on which the rotation will be made.</param>
        /// <returns>object:Player</returns>
        public Player Left_Rotation(Player current_player)
        {
            /*
             * Same principle as permuting two values :
             * tmp = a;
             * a = b;
             * b = a;
             */
            Player tmp = current_player.Right;
            current_player.Right = tmp.Left;
            tmp.Left = current_player;
            return tmp;
        }

        /// <summary>
        /// Calculates the balancing factor of a given node.
        /// </summary>
        /// <param name=current_player>Node which factor is calculated.</param>
        /// <returns>int</returns>
        public int Balancing_Factor(Player current_player)
        {
            int l = Height(current_player.Left); // height of the left subtree
            int r = Height(current_player.Right); // height of the right subtree
            int diff = l - r; // difference between the two subtrees
            return diff;
        }

        /// <summary>
        /// Calculates the hight of the tree.
        /// </summary>
        /// <param name=current_player>Root of the subtree which height is calculated.</param>
        /// <returns>int</returns>
        public int Height(Player current_player)
        {
            int height = 0;
            if (current_player != null)
            {
                int l = Height(current_player.Left); // height of the left subtree
                int r = Height(current_player.Right); // height of the right subtree
                int m = Math.Max(l, r); // maximum height between the two subtrees
                height = m + 1; // +1 for the root
            }
            return height;
        }

    }
}
