using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUs
{
    /// <summary>
    /// Represents a series of connection from a room to another one without going through the same twice.
    /// </summary>
    class Circuit
    {
        private List<Connection> steps = new List<Connection>(); // list of steps of this circuit
        private List<Room> rooms = new List<Room>(); // list of the rooms it went through

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Circuit(List<Connection> steps)
        {
            this.steps = steps;
        }

        public Circuit(List<Connection> steps, List<Room> rooms)
        {
            this.steps = new List<Connection>(steps);
            this.rooms = new List<Room>(rooms);
        }

        public Circuit() {}

        public List<Connection> Steps
        {
            get { return steps; }
            set { steps = value; }

        }

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; } 
        }

        /// <summary>
        /// Adds a conection to this circuit.
        /// </summary>
        /// <param name=c>Connection we want to add.</param>
        public void Add_Connection(Connection c)
        {
            this.steps.Add(c); // added to the list of the connections
            this.rooms.Add(c.Room2); // new room added to the list of the rooms
        }

        /// <summary>
        /// Calculates the time it takes to cross this circuit.
        /// </summary>
        /// <returns>double</returns>
        public double Get_Time()
        {
            double res = 0;
            foreach (Connection c in this.steps) { res += c.Time; } // add the time it takes to cross each corridor
            return res;
        }

        /// <summary>
        /// Describes this circuit by giving all the rooms it contains in order.
        /// </summary>
        public override string ToString()
        {
            string s = steps[0].Room1.Name + " -> ";

            foreach (Connection c in steps)
            {

                if (steps.IndexOf(c) == steps.Count - 1)
                {
                    s += c.Room2.Name+ "\n";
                }
                else
                {
                    s += c.Room2.Name + " -> ";
                }
            }
            s += "\nTime : " + String.Format("{0:0.00}",this.Get_Time()) + " seconds.";
            return s;
        }
    }
}
