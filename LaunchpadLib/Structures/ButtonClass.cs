using Midi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Structures
{
    internal class ButtonClass
    {
        public Dictionary<Pitch, Location> Buttons { get; }
        public Dictionary<Pitch, int> Pages { get; }
        public Dictionary<Control, string> ToolButtons { get; }

        private readonly string[] ToolButtonString = { "Up", "Down", "Left", "Right", "Session", "User1", "User2", "Mixer" };

        public ButtonClass()
        {
            Dictionary<Pitch, Location> Buttons = new();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Buttons.Add((Pitch)((7 - i) * 10 + j + 11), new(i, j));
            this.Buttons = Buttons;

            Dictionary<Pitch, int> Pages = new();
            for (int i = 0; i < 8; i++)
                Pages.Add((Pitch)(10 * i + 19), i + 1);
            this.Pages = Pages;

            Dictionary<Control, string> ToolButtons = new Dictionary<Control, string>();
            for (int i = 0; i < this.ToolButtonString.Length; i++)
                ToolButtons.Add((Control)(i + 104), this.ToolButtonString[i]);
            this.ToolButtons = ToolButtons;
        }
    }
}
