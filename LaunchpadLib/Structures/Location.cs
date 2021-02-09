using System;
using System.Collections.Generic;
using System.Text;
using Midi.Enums;

namespace Launchpad.Structures
{
    /// <summary>
    /// Structure of launchpad key's location
    /// </summary>
    public struct Location
    {
        /// <summary>
        /// Create new instant with x, y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Location(int x, int y)
        {
            X = x; Y = y;
        }

        /// <summary>
        /// Create new instant with location
        /// </summary>
        /// <param name="location"></param>
        public Location(Location location)
        {
            X = location.X; Y = location.Y;
        }

        /// <summary>
        /// Create new instant from button. Don't use manually
        /// </summary>
        /// <param name="btn">Name of button</param>
        /// <returns></returns>
        public static Location FromButton(string btn)
            => new(int.Parse($"{btn[1]}"), int.Parse($"{btn[2]}"));

        /// <summary>
        /// X-coordinate of location
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-coordinate of location
        /// </summary>
        public int Y { get; set; }
    }
}
