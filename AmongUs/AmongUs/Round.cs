using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUs
{
    /// <summary>
    ///  Represents a Round of the tournament.
    ///  Each round is composed of several games that are playes simultaneously.
    /// </summary>
    class Round
    { 
        private int nb_games; // generates <paramref name="nb_games" games
        private List<Game> games = new List<Game>(); // list of the games of this round
        private List<Player> players = new List<Player>(); // list of all the players in this round

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Round(int nb_games, List<Player> players)
        {
            this.nb_games = nb_games;
            this.players = players;
        }

        /// <summary>
        /// Games based on the database
        /// </summary>
        public void Start_Random()
        {
            List<Player> tmp = new List<Player>(this.players); // copy of the list of all players
            Random random = new Random();

            for (int i = 0; i < this.nb_games; i++) // <paramref name="nb_games" created in this round
            {
                List<Player> game_players = new List<Player>(10); 
                if (tmp.Count > 0)
                {
                    for (int j = 0; j < 10; j++) // 10 players placed randomly into the actual game
                    {
                        int index = random.Next(0, tmp.Count);
                        game_players.Add(tmp[index]);
                        tmp.RemoveAt(index); // player removed to not pick him twice

                    }
                    Game g = new Game(game_players);
                    Console.WriteLine("Game " + (i+1));
                    g.Display();
                    this.games.Add(g); // actual game added to the list of the games of this round
                }
            }

            foreach (Game g in this.games) // updtate score for each player
            {
                g.New_Score();
            }
        }

        /// <summary>
        /// Games based on ranking
        /// </summary>
        public void Start_Classified()
        {

            List<Player> tmp = new List<Player>(this.players); // copy of the sorted list of all players by their general (or final) score

            for (int i = 0; i < this.nb_games; i++) // <paramref name="nb_games" created in this round
            {
                List<Player> game_players = new List<Player>(10); // 10 players placed in order into the actual game
                for (int j = 0; j < 10; j++)
                {
                    game_players.Add(tmp[(i * 10) + j]);
                }
                Game g = new Game(game_players);
                Console.WriteLine("Game " + (i + 1));
                g.Display();
                this.games.Add(g); // actual game added to the list of the games of this round
            }

            foreach (Game g in this.games) // update score for each player
            {
                g.New_Score();
            }
        }

        /// <summary>
        /// Displays each player of this round
        /// </summary>
        public void Display_Score()
        {
            // for each player of the round we display his score 
            foreach (Player p in this.players)
            {
                Console.WriteLine(p.ToString());
            }
        }


    }



}
