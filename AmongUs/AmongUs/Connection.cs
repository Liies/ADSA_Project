using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUs
{
    /// <summary>
    /// Represents a connection between two rooms, in other words the corridors.
    /// </summary>
    class Connection
    {
        // Between each pair of rooms 
        private Room room1;
        private Room room2;
        private double time; // travel time between a pair of rooms

        /// <summary>
        ///  Constructor of the class.
        ///  With the properties of each attribute.
        /// </summary>
        public Connection(Room room1, Room room2, double time)
        {
            this.room1 = room1;
            this.room2 = room2;
            this.time = time;
        }

        public Room Room1
        {
            get { return room1; }
            set { room1 = value; }
        }
        public Room Room2
        {
            get { return room2; }
            set { room2 = value; }
        }

        public double Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// Clones this connection.
        /// </summary>
        /// <returns>object:Connection</returns>
        public Connection Clone() 
        {
            return new Connection(room1, Room2, time);
        }

        /// <summary>
        /// Describes this connection by giving the two rooms and the travel time between them.
        /// </summary>
        public override string ToString() //Function that describes a connection between two parts 
        {
            return "Room 1 : " + room1 + "\n" +
                   "Room 2 : " + room2 + "\n" +
                   "Time : " + time + "\n";
                  
        }
    }



}

