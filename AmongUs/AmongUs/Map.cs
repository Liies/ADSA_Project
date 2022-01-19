using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUs
{
    /// <summary>
    ///  Represents the ADSA Map.
    /// </summary>
    class Map
    {
        public double infinity = 10000;
        private int nb_vertices;
        private double[,] matrix; // matrix of dimension <paramref name="nb_vertices">*<paramref name="nb_vertices">
                                  // i : ith vertice of the graph ; j : jth vertice of the graph
                                  // matrix[i][j] = distance from the ith vertex to the jth vertex
                                  // matrix[i][j] = infinity, if there is no path from ith vertex to jth vertex

        //STEP 4 : 
        private Room root; // starting room
        private List<Circuit> circuits = new List<Circuit>(); // list of the different circuits 

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Map() {}

        public List<Circuit> Circuits
        {
            get { return circuits; }
            set { circuits = value; }
        }

        /// <summary>
        /// Displays the shortest times travel between each pair of nodes.
        /// Calculated with Floyd-Warshall algorithm.
        /// </summary>
        /// <param name=mat_distance>Matrix of the times travel.
        /// <param name=nb_vertices>Number of vertices of the graph.</param>
        public void Display_Times(double[,] mat_distance, int nb_vertices)
        {
            for (int i = 0; i < nb_vertices; ++i) // interval of time for each pair of rooms
            {
                for (int j = i+1; j < nb_vertices; ++j)
                {
                    if (mat_distance[i, j] == infinity)
                    {
                        Console.Write("\nTime for the shortest path beetween room " + i + " and room " + j + " = Infinity" );
                    } 
                    else
                    {
                        Console.Write("\nTime for the shortest path beetween room "+i+" and room "+j+" = "+ mat_distance[i, j]+" s");
                    }   
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays the matrix with the fastest path between each par of nodes.
        /// Calculated with Floyd-Warshall algorithm.
        /// </summary>
        /// <param name=mat_distance>Matrix of the initial times travel.
        /// <param name=nb_vertices>Number of vertices of the graph.</param>
        public void Display_Matrix(double[,] mat_distance, int nb_vertices)
        {
            for (int i = 0; i < nb_vertices; ++i)
            {
                for (int j = 0; j < nb_vertices; ++j)
                {
                    if (mat_distance[i, j] == infinity)
                    {
                        Console.Write("Infinity".PadLeft(8)); //aligns the values 
                    }

                    else
                    {
                        Console.Write(mat_distance[i, j].ToString().PadLeft(8)); // distance from the ith vertex to the jth vertex
                    }

                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays the travel time between any pair of rooms chosen by the user.
        /// </summary>
        /// <param name=mat_distance>Matrix of the initial times travel.</param>
        public void Travel(double[,] mat_distance)
        {
            Console.WriteLine("\nChoose a pair of pieces for which you want the time of the run : ");
            Console.WriteLine("\n0 : Cafetaria \n1 : Weapons \n2 : Navigation \n3 : O2 \n4 : Shield \n5 : Communication \n6 : Storage \n7 :  Admin  \n8 : Electrical \n9 : Lower Engine \n10 : Reactor \n11 : Security \n12 : Medbay \n13 : Upper Engine ");
            string str = "\nChoose the first piece";
            int i = SaisieInt(str);
            string str2 = "\nChoose the second piece";
            int j = SaisieInt(str2);
            if (Math.Abs(i) > 14 || Math.Abs(j) > 14)
            {
                Console.WriteLine("\nError !!!");
                Travel(mat_distance);

            }
            else
            {
                Console.WriteLine("You need : " + mat_distance[i, j] + " second to travel this pair of room.");
            }
        }

        /// <summary>
        /// Function that allows to verify that the input of the interlocutor is of type int
        /// </summary>
        /// <param name="str">Entered by the user</param>
        static int SaisieInt(string str)
        {
            int num = 0;
            string maLigne;
            bool err;
            do
            {
                try
                {
                    err = false;
                    Console.WriteLine(str);
                    maLigne = Console.ReadLine();
                    num = int.Parse(maLigne);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("\nYour entry is incorrect !");
                    err = true;
                }
            } while (err);

            return num;
        }

        /// <summary>
        /// Implements Floyd-Warshall algorithm.
        /// </summary>
        /// <param name=matrix>Matrix of the initial times travel.
        /// <param name=nb_vertices>Number of vertices of the graph.</param>
        /// <returns>double[,]</returns>
        public double[,] Shortest_path(double[,] matrix, int nb_vertices) 
        {
            double[,] mat_distance = new double[nb_vertices, nb_vertices];

            for (int i = 0; i < nb_vertices; ++i)
            {
                for (int j = 0; j < nb_vertices; ++j)
                {
                    mat_distance[i, j] = matrix[i, j]; //we copy our graph in a distance matrix 
                }

            }

            for (int k = 0; k < nb_vertices; k++)
            {
                for (int i = 0; i < nb_vertices; i++)
                {
                    for (int j = 0; j < nb_vertices; j++)
                    {
                        if (mat_distance[i, k] + mat_distance[k, j] < mat_distance[i, j]) // if the depart node and the arrival node are connected
                                                                                          // and if there is a shortest path
                        {
                            mat_distance[i, j] = Math.Round(mat_distance[i, k] + mat_distance[k, j],2); // we replace the value of the distance by the shortest path found
                        }
                    }
                }
            }

            return (mat_distance);

        }

        /// <summary>
        /// Creates the ADSA Map.
        /// </summary>
        /// <param name=matrix>Matrix of the connections and the initial times travel.
        /// <param name=rooms>List of the rooms of the ADSA Map.</param>
        public void Create_Map(double[,] matrix, List<Room> rooms)
        {
            double infinity = 10000;
            for (int i = 0; i < matrix.GetLength(0); i++) // for each room
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != infinity && i != j) // we don't take diagonal and infinity values (no connections between rooms)
                    {
                        rooms[i].Neighbors.Add(rooms[j]); // add to its neighbor list the rooms for which there is a value in the matrix
                        rooms[i].Connections.Add(new Connection(rooms[i], rooms[j], matrix[i, j])); // create connections for each pair of rooms when it exists
                    }
                }
            }
        }

        /// <summary>
        /// Finds a hamiltonian circuit for one room as departure.
        /// </summary>
        /// <param name=current_room>Departure room.
        /// <param name=circuit>Circuit that is step by step incremented.</param>
        public void Circuits_One_Room(Room current_room, Circuit circuit)
        {
            if (current_room.All_Neighbors_In_Circuit(circuit) && circuit.Rooms.Count == 14) // end of the recursive
            {
                this.circuits.Add(circuit); // built circuit, added to the list 
                return;
            }
            if (current_room.All_Neighbors_In_Circuit(circuit)) // if there is no possibilty anymore, quit
            {
                return;
            }
            foreach (Connection c in current_room.Connections) // each connection of the <paramref name="current_room"> is a new possibility
            {
                Circuit circuit_prolonge = new Circuit(circuit.Steps, circuit.Rooms);
                if (!circuit.Rooms.Contains(c.Room2))
                {
                    circuit_prolonge.Add_Connection(c);
                    Circuits_One_Room(c.Room2, circuit_prolonge);
                }
            }
        }

        /// <summary>
        /// Finds all the hamiltonian circuits.
        /// </summary>
        /// <param name=rooms>List of all the rooms of the ADSA Map.</param>
        public void All_Circuits(List<Room> rooms)
        {
            foreach(Room r in rooms) // each room as a departure
            {
                Circuit c = new Circuit();
                c.Rooms.Add(r);
                Circuits_One_Room(r, c);
            }
        }

        /// <summary>
        /// Gives the fastest circuit.
        /// </summary>
        /// <returns>object:Circuit</returns>
        public Circuit Fastest_Circuit()
        {
            if (circuits.Count == 0) // Exception error
            {
                throw new InvalidOperationException("Empty list");
            }

            double min_time = 120; // fix a maximum time
            Circuit fastest = new Circuit();

            foreach (Circuit c in this.circuits)
            {
                if (c.Get_Time() < min_time) // if there is a faster path, it becomes the fastest one
                {
                    min_time = c.Get_Time();
                    fastest = c;
                }
            }
            return fastest;
        }

    }

}

