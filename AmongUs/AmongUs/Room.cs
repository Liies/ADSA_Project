using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AmongUs
{
    /// <summary>
    /// Represents a room of the ADSA Map
    /// </summary>
    class Room
    {
        private string name; // identified by its name
        private List<Connection> connections = new List<Connection>(); // list of the connections with the other rooms
        private List<Room> neighbors = new List<Room>(); // list of its neighbors

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Room(string name)
        {
            this.name = name;  
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Room> Neighbors
        {
            get { return neighbors; }
            set { neighbors = value; }
        }


        public List<Connection> Connections
        {
            get { return connections; }
            set { connections = value; }
        }

        /// <summary>
        /// Load the connections for this room.
        /// </summary>
        /// <param name=connections>List of all the connections in the ADSA Map.</param>
        public void Load_Connections(List<Connection> connections)
        {
            foreach (Connection c in connections) // for each connection oh the list
            {
                if (c.Room1.Name==this.name) // if there is the name of the room 
                {
                    this.connections.Add(c); // add this connection to its list
                }

            }
        }

        /// <summary>
        /// Gives true if all the neighbors of this room are already in the circuit given in parameter.
        /// False otherwise.
        /// </summary>
        /// <param name=c>Circuit in which we want to test the containing.</param>
        /// <returns>bool</returns>
        public bool All_Neighbors_In_Circuit(Circuit c) {
            return !this.neighbors.Except(c.Rooms).Any();
        }

        /// <summary>
        /// Describes the room by giving its name.
        /// </summary>
        public override string ToString()
        {
            return this.name;
        }


    }
}
