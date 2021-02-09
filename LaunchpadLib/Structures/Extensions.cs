using Midi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launchpad.Structures
{
    internal static class Extensions
    {
        public static Pitch GetPitch(this Location location)
        {
            int x = location.X;
            int y = location.Y;

            if (9 <= x || x <= 0 || 9 <= y || y <= 0)
                throw new ArgumentOutOfRangeException("x and y must be in range. [1~8]");

            foreach (var btn in new ButtonClass().Buttons)
                if (btn.Value.X == x - 1 && btn.Value.Y == y - 1)
                    return btn.Key;
            throw new Exception("UnkownException");
        }

        public static string GetName(this object obj)
        {
            // First see if it is a FrameworkElement
            var element = obj as FrameworkElement;
            if (element != null)
                return element.Name;
            // If not, try reflection to get the value of a Name property.
            try { return (string)obj.GetType().GetProperty("Name").GetValue(obj, null); }
            catch
            {
                // Last of all, try reflection to get the value of a Name field.
                try { return (string)obj.GetType().GetField("Name").GetValue(obj); }
                catch { return null; }
            }
        }
    }
}
