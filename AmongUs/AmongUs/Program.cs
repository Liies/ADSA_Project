using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace AmongUs
{
    class Program
    {

        static void Menu()
        {
            Console.WriteLine("\n                                              AMONG US                       \n");
            Console.WriteLine("\n SUMMARY\n");
            Console.WriteLine("\n STEP 1...............................................................................................Page 1");
            Console.WriteLine("\n STEP 2...............................................................................................Page 2");
            Console.WriteLine("\n STEP 3...............................................................................................Page 3");
            Console.WriteLine("\n STEP 4...............................................................................................Page 4");

            Console.WriteLine("\n\n Please enter your choice (1, 2, 3 or 4).");
            string str = Convert.ToString(Console.ReadLine());

            Console.WriteLine("\n\n Press any key to continue");
            Console.ReadKey();
            Console.Clear();

            if (str == "1")
            {
                Step_1();
            }
            if (str == "2")
            {
                Step_2();
            }
            if (str == "3")
            {
                Step_3();
            }
            if (str == "4")
            {
                Step_4();
            }
            else
            {
                Console.ReadKey();
                Console.Clear();
                Menu();

            }

        }
        static void Step_1()
        {
            Console.WriteLine("\n......................Step 1: To organize the tournament.......................");
            List<Player> players = new List<Player>();
            for (int i = 0; i < 100; i++)
            {
                players.Add(new Player("pseudo" + (i + 1))); // We create a list of 100 players for the tournament
            }
            //We create the tournament and we start it
            Tournament tournament = new Tournament(players[0], players);
            tournament.Start();
            Console.ReadKey();
            Console.Clear();


        }
        static void Step_2()
        {
            Console.WriteLine("\n......................Step 2: Professor Layton < Guybrush Threepwood < You.......................");
            Console.WriteLine("\n\n1.View of the players which see each other’s...................................................");
            Console.WriteLine("\n\n2.View of the probable imposters...............................................................");
            Console.WriteLine("\n\n Press 0 to go back to the main menu.");
            string str = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\n\n Press any key to continue");
            Console.ReadKey();
            Console.Clear();

            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player("pseudo" + i));
            }
            int[,] matrix = { {0,1,0,0,1,1,0,0,0,0},
                              {1,0,1,0,0,0,1,0,0,0},
                              {0,1,0,1,0,0,0,1,0,0},
                              {0,0,1,0,1,0,0,0,1,0},
                              {1,0,0,1,0,0,0,0,0,1},
                              {1,0,0,0,0,0,0,1,1,0},
                              {0,1,0,0,0,0,0,0,1,1},
                              {0,0,1,0,0,1,0,0,0,1},
                              {0,0,0,1,0,1,1,0,0,0},
                              {0,0,0,0,1,0,1,1,0,0} };

            Graph test = new Graph(matrix, players);
            if (str == "1")
            {
                Console.WriteLine("\nView of the players which see each other’s :");
                foreach (Player p in players)
                {
                    Console.WriteLine("\n" + p.Pseudo + " saw :");
                    foreach (Player seen in p.Seen_Players)
                    {
                        Console.Write("\n" + seen.Pseudo);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n\nPress any key to go back to the step 2 menu.");
                Console.ReadKey();
                Console.Clear();
                Step_2();
            }

            Player dead = players[0];
            List<List<Player>> probable_imposters = test.Probable_Imposters(dead);
            if (str == "2")
            {
                Console.WriteLine("\nProbable imposters : \n");
                Console.WriteLine(dead.Pseudo + " is dead !\n");
                test.Diplay_Impostors(probable_imposters);
                Console.WriteLine("\nPress any key to go back to the step 2 menu.");
                Console.ReadKey();
                Console.Clear();
                Step_2();

            }
            if (str == "0")
            {
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
            else
            {
                Console.WriteLine("\nError !!!! ");
                Console.ReadKey();
                Console.Clear();
                Step_2();

            }


        }
        static void Step_3()
        {
            //For this step we use the Floyd-Warshall algorithm, which calculates the shortest path in a graph.
            //Floyd-Warshall computes the shortest distances between every pair of vertices in the input graph.
            //for that we must define the distances matrix


            double infinity = 10000;//We use a very large number for the infinites of our matrix 
            double[,] matrix = { { 0, 3.2, infinity, infinity, infinity, infinity, 4.5, 4, infinity, infinity, infinity, infinity, 4.1, 5.3 } ,
                               { 3.2, 0, 3.7, 2.2, 5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, 3.7, 0, 3.5, 4.5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, 2.2, 3.5, 0, 4.5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, 5, 4.5, 4.6, 0, 2.2, 3.5, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, 2.2, 0, 3.2, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { 4.5, infinity, infinity, infinity, 3.5, 3.2, 0, 3.5, 3.5, 4.5, infinity, infinity, infinity, infinity },
                               { 4, infinity, infinity, infinity, infinity, infinity, 3.5, 0, infinity, infinity,infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, infinity, infinity, 3.5, infinity, 0, 4.5, infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, infinity, infinity, 4.5, infinity, 4.5, 0, 3, 3, infinity, 3.6 },
                               { infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3, 0, 2.6, infinity, 3.5 },
                               { infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3, 2.6, 0, infinity, 3.3 },
                               { 4, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 0, 4},
                               { 5.3, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3.6, 3.5, 3.3, 4, 0 } }; // the crewmates' distance matrix

            double[,] matrix_vent = { { 0, 3.2, 1.5, 1.5, 1.6, infinity, 4.5, 0, infinity, infinity, infinity, infinity, 4.1, 5.3 } ,
                               { 3.2, 0, 0, 2.2, 5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { 1.5, 0, 0, 3.5, 0, infinity, infinity, 2.5, infinity, infinity, infinity, infinity, infinity, infinity },
                               { 1.5, 2.2, 3.5, 0, 4.6, infinity, infinity, 1.5, infinity, infinity, infinity, infinity, infinity, infinity },
                               { 1.5, 5, 0, 4.6, 0, 2.2, 3.5, 1.5, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, 2.2, 0, 3.2, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { 4.5, infinity, infinity, infinity, 3.5, 3.2, 0, 3.5, 3.5, 4.5, infinity, infinity, infinity, infinity },
                               { 0, 3, 2.5, 0, 1.7, infinity, 3.5, 0, infinity, infinity,infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, infinity, infinity, 3.5, infinity, 0, 4.5, infinity, 0, 0, infinity },
                               { infinity, infinity, infinity, infinity, infinity, infinity, 4.5, infinity, 4.5, 0, 0, 3, infinity, 3.6 },
                               { infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 0, 0, 2.6, infinity, 0 },
                               { infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 0, 3, 2.6, 0, 0, 3.3 },
                               { 4.1, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 0, infinity, infinity, 0, 0, 4},
                               { 5.3, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3.6, 0, 3.3, 4, 0 } };// the impostors' distance matrix


            //We create 2 differents graphs, one for the crewmates players and one for the impostors
            Map map_crewmates = new Map();
            Map map_impostors = new Map();

            Console.WriteLine("\n......................Step 3: I don't see him, but I can give proofs he vents !.......................");

            Console.WriteLine("\n 1. Display the travel time for any pair of rooms for crewmates.......................................");
            Console.WriteLine("\n 2. Display the travel time for any pair of rooms for impostors.......................................");
            Console.WriteLine("\n 3. Display the interval of time for each pair of room where the traveler is an impostor..............");

            Console.WriteLine("\n\n Please enter your choice (1, 2 or 3).");
            Console.WriteLine("\n\n Press 0 to go back to the main menu.");
            string str = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\n\n Press any key to continue");
            Console.ReadKey();
            Console.Clear();

            if (str == "1")
            {
                //We display the travel time for any pair of rooms for crewmates.
                Console.WriteLine("\nTravel Time for Crewmate : ");
                map_crewmates.Travel(map_crewmates.Shortest_path(matrix, 14));
                Console.WriteLine("\n\nPress any key to go back to the step 3 menu.");
                Console.ReadKey();
                Console.Clear();
                Step_3();
            }
            if (str == "2")
            {
                //We display the travel time for any pair of rooms for impostors.
                Console.WriteLine("\nTravel Time for Impostor : ");
                map_impostors.Travel(map_impostors.Shortest_path(matrix_vent, 14));
                Console.WriteLine("\n\nPress any key to go back to the step 3 menu.");
                Console.ReadKey();
                Console.Clear();
                Step_3();

            }
            if (str == "3")
            {
                //We display the interval of time for each pair of room where the traveler is an impostor
                Console.WriteLine("\nIMPOSTOR Time to travel for any pair of room :\n"); //Matrix with the Shortest path beetween each pair of pieces
                map_impostors.Display_Matrix(map_impostors.Shortest_path(matrix_vent, 14), 14);

                Console.WriteLine("\nShortest path beetween each pair of pieces");
                map_impostors.Display_Times(map_impostors.Shortest_path(matrix_vent, 14), 14);

                Console.WriteLine("\n\nPress any key to go back to the step 3 menu.");
                Console.ReadKey();
                Console.Clear();
                Step_3();
            }
            if (str == "0")
            {
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
            else
            {
                Console.WriteLine("\nError !!!! ");
                Console.ReadKey();
                Console.Clear();
                Step_3();

            }
        }
        static void Step_4()
        {
            Console.WriteLine("\n...........................Step 4: Secure the last tasks............................");
            double infinity = 10000;//We use a very large number for the infinites of our matrix 
            double[,] matrix = { { 0, 3.2, infinity, infinity, infinity, infinity, 4.5, 4, infinity, infinity, infinity, infinity, 4.1, 5.3 } ,
                               { 3.2, 0, 3.7, 2.2, 5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, 3.7, 0, 3.5, 4.5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, 2.2, 3.5, 0, 4.5, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, 5, 4.5, 4.6, 0, 2.2, 3.5, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, 2.2, 0, 3.2, infinity, infinity, infinity, infinity, infinity, infinity, infinity },
                               { 4.5, infinity, infinity, infinity, 3.5, 3.2, 0, 3.5, 3.5, 4.5, infinity, infinity, infinity, infinity },
                               { 4, infinity, infinity, infinity, infinity, infinity, 3.5, 0, infinity, infinity,infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, infinity, infinity, 3.5, infinity, 0, 4.5, infinity, infinity, infinity, infinity },
                               { infinity, infinity, infinity, infinity, infinity, infinity, 4.5, infinity, 4.5, 0, 3, 3, infinity, 3.6 },
                               { infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3, 0, 2.6, infinity, 3.5 },
                               { infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3, 2.6, 0, infinity, 3.3 },
                               { 4, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 0, 4},
                               { 5.3, infinity, infinity, infinity, infinity, infinity, infinity, infinity, infinity, 3.6, 3.5, 3.3, 4, 0 } }; //the crewmates' distance matrix
            List<Room> rooms = new List<Room>() {new Room("Cafeteria"),
                                                 new Room("Weapons"),
                                                 new Room("Navigation"),
                                                 new Room("O2"),
                                                 new Room("Shields"),
                                                 new Room("Communication"),
                                                 new Room("Storage"),
                                                 new Room("Admin"),
                                                 new Room("Electrical"),
                                                 new Room("Lower Engine"),
                                                 new Room("Reactor"),
                                                 new Room("Security"),
                                                 new Room("Medbay"),
                                                 new Room("Upper Engine")};
            Map map = new Map();
            map.Create_Map(matrix, rooms);
            map.All_Circuits(rooms);

            Console.WriteLine("\n\nNumber of found circuits : " + map.Circuits.Count);
            Circuit fastest_circuit = map.Fastest_Circuit();
            Console.WriteLine("\n\nFastest hamiltonian circuit : \n");
            Console.WriteLine(fastest_circuit.ToString());
            Console.WriteLine("\n\n\nPress 1 to view all the circuits found");
            Console.WriteLine("\n\nPress 0 to go back to the main menu.");
            string str = Convert.ToString(Console.ReadLine());
            if (str == "1")
            {
                int i = 1;
                foreach (Circuit c in map.Circuits)
                {
                    Console.WriteLine("Circuit n°" + i);
                    Console.WriteLine(c.ToString()+"\n\n");
                    i += 1;
                }
                Console.WriteLine("\n\nPress any key to go back to the step 4 menu.");
                Console.ReadKey();
                Console.Clear();
                Step_4();
            }
            if (str == "0")
            {
                Console.ReadKey();
                Console.Clear();
                Menu();

            }
            else
            {
                Console.WriteLine("\nError !!!! ");
                Console.ReadKey();
                Console.Clear();
                Step_4();

            }


        }

        static void Main(string[] args)
        {
            Menu();

            Console.ReadKey();
        }






    }
}
