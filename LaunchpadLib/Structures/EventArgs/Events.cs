using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using static Launchpad.Launchpad_Pro;

namespace Launchpad.Structures.EventArgs
{
    /// <summary>
    /// Provide data for ButtonPressedEvent. Contains pressed location and mouse event
    /// </summary>
    public class ButtonPressedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public ButtonPressedEventArgs(RoutedEvent revent, object source) : base(revent, source)
        {
            var b = (Rectangle)source;
            var s = b.GetName();

            PressedLocation = new(int.Parse($"{s[1]}"), int.Parse($"{s[2]}"));
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public ButtonPressedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Button location of the event occurred
        /// </summary>
        public Location PressedLocation { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; } = null;
    }

    /// <summary>
    /// Provide data for ChainChangedEvent. Contains chain and mouse event
    /// </summary>
    public class ChainChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public ChainChangedEventArgs(RoutedEvent revent, object source) :base(revent, source)
        {
            var i = int.Parse($"{source.GetName()[1]}");
            Chain = i;
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public ChainChangedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Chain of the event occurred
        /// </summary>
        public int Chain { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; }
    }

    /// <summary>
    /// Provide data for MoveButtonPressedEvent. Contains move button and mouse event
    /// </summary>
    public class MoveButtonPressedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public MoveButtonPressedEventArgs(RoutedEvent revent, object source) : base(revent, source)
        {
            var en = source.GetName() switch
            {
                "bu" => MoveButton.Up,
                "bd" => MoveButton.Down,
                "bl" => MoveButton.Left,
                "br" => MoveButton.Right,
                _ => throw new InvalidOperationException(source.GetName())
            };

            MoveButton = en;
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public MoveButtonPressedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Move button of the event occurred
        /// </summary>
        public MoveButton MoveButton { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; }
    }

    /// <summary>
    /// Provide data for ModeChangedEvent of <see cref="Launchpad_MK2"/>. Contains mode button and mouse event
    /// </summary>
    public class MK2ModeChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public MK2ModeChangedEventArgs(RoutedEvent revent, object source) : base(revent, source)
        {
            var en = source.GetName() switch
            {
                "bs" => Launchpad_MK2.Mode.Session,
                "bu1" => Launchpad_MK2.Mode.User1,
                "bu2" => Launchpad_MK2.Mode.User2,
                "bm" => Launchpad_MK2.Mode.Mixer,
                _ => throw new InvalidOperationException(source.GetName())
            };

            Mode = en;
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public MK2ModeChangedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Mode button of the event occurred
        /// </summary>
        public Launchpad_MK2.Mode Mode { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; }
    }

    /// <summary>
    /// Provide data for ModeChangedEvent of <see cref="Launchpad_Pro"/>. Contains mode button and mouse event
    /// </summary>
    public class ProModeChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public ProModeChangedEventArgs(RoutedEvent revent, object source) : base(revent, source)
        {
            var en = source.GetName() switch
            {
                "bs" => Mode.Session,
                "bn" => Mode.Note,
                "bde" => Mode.Device,
                "bus" => Mode.User,
                _ => throw new InvalidOperationException(source.GetName())
            };

            Mode = en;
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public ProModeChangedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Mode button for the event occurred
        /// </summary>
        public Mode Mode { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; }
    }

    /// <summary>
    /// Provide data for MixerChangedEvent. Contains mixer button and mouse event
    /// </summary>
    public class MixerButtonPressedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public MixerButtonPressedEventArgs(RoutedEvent revent, object source) : base(revent, source)
        {
            var en = source.GetName() switch
            {
                "bdra" => Mixer.Record_Arm,
                "bdts" => Mixer.Track_Select,
                "bdm" => Mixer.Mute,
                "bds" => Mixer.Solo,
                "bdv" => Mixer.Volumn,
                "bdp" => Mixer.Pan,
                "bdss" => Mixer.Sends,
                "bdsc" => Mixer.Stop_Clip,
                _ => throw new InvalidOperationException(source.GetName())
            };

            Mixer = en;
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public MixerButtonPressedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Mixer button of the event occurred
        /// </summary>
        public Mixer Mixer { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; }
    }

    /// <summary>
    /// Provide data for FunctionChangedEvent. Contains function button and mouse event
    /// </summary>
    public class FunctionButtonPressedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        public FunctionButtonPressedEventArgs(RoutedEvent revent, object source) : base(revent, source)
        {
            var en = source.GetName() switch
            {
                "bls" => Function.Shift,
                "blc" => Function.Click,
                "blu" => Function.Undo,
                "bld" => Function.Delete,
                "blq" => Function.Quantise,
                "bldu" => Function.Dupilcate,
                "bldo" => Function.Double,
                "blr" => Function.Record,
                _ => throw new InvalidOperationException(source.GetName())
            };

            Function = en;
        }

        /// <summary>
        /// Create new instant
        /// </summary>
        /// <param name="revent">Routed event</param>
        /// <param name="source">Rectangle source</param>
        /// <param name="e">Mouse event</param>
        public FunctionButtonPressedEventArgs(RoutedEvent revent, object source, MouseEventArgs e) : this(revent, source)
        {
            MouseEvent = e;
        }

        /// <summary>
        /// Function button of the event occurred
        /// </summary>
        public Function Function { get; }

        /// <summary>
        /// Mouse event of the event occurred
        /// </summary>
        public MouseEventArgs MouseEvent { get; }
    }
}
