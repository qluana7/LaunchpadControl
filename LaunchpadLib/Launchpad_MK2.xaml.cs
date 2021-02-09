using Launchpad.Structures;
using Launchpad.Structures.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Midi.Devices;
using Midi.Enums;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;

namespace Launchpad
{
    /// <summary>
    /// Launchpad MK2 Edition
    /// </summary>
    public partial class Launchpad_MK2 : UserControl
    {
        #region Properties
        /// <summary>
        /// Pad button list.
        /// </summary>
        public Dictionary<Location, Rectangle> KeyPadButtons { get; }

        /// <summary>
        /// InputDevice for launchpad
        /// </summary>
        public IInputDevice Input { get; private set; }

        /// <summary>
        /// OutputDevice for launchpad
        /// </summary>
        public IOutputDevice Output { get; private set; }

        /// <summary>
        /// Whether can "Drag and Press"
        /// </summary>
        public bool MultiSelect { get; private set; }

        private bool _IsPressed = false;
        private Rectangle _CurrentRect;
        #endregion

        #region Fixed Data
        /// <summary>
        /// Default value of <see cref="PressColor"/>
        /// </summary>
        public static Brush DefaultPressedBrush { get; } = Brushes.DarkGray;
        private static readonly Brush UnPressedBrush = GetBrushFromString("#3F3F3F");
        #endregion

        #region XAML Properties
#pragma warning disable CS1591
        public int ButtonSize
        {
            get { return (int)GetValue(ButtonSizeProperty); }
            set { SetValue(ButtonSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.Register("ButtonSize", typeof(int), typeof(Launchpad_MK2), new PropertyMetadata(48));

        public Brush BackBoard
        {
            get { return (Brush)GetValue(BackBoardProperty); }
            set { SetValue(BackBoardProperty, value); GridBoard.Background = value; }
        }

        // Using a DependencyProperty as the backing store for BackBoard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackBoardProperty =
            DependencyProperty.Register("BackBoard", typeof(Brush), typeof(Launchpad_MK2), new PropertyMetadata(Brushes.Black));


        public BtnStyle ButtonStyle
        {
            get { return (BtnStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(BtnStyle), typeof(Launchpad_MK2), new PropertyMetadata(BtnStyle.Black));

        public Brush PressColor
        {
            get { return (Brush)GetValue(PressColorProperty); }
            set { SetValue(PressColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PressColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressColorProperty =
            DependencyProperty.Register("PressColor", typeof(Brush), typeof(Launchpad_MK2), new PropertyMetadata(DefaultPressedBrush));


        public bool ShowPressed
        {
            get { return (bool)GetValue(ShowPressedProperty); }
            set { SetValue(ShowPressedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPressed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPressedProperty =
            DependencyProperty.Register("ShowPressed", typeof(bool), typeof(Launchpad_MK2), new PropertyMetadata(true));
#pragma warning restore CS1591
        #endregion

        #region XAML Events
        /// <summary>
        /// Occurs when Pad button pressed.
        /// </summary>
        public event RoutedEventHandler ButtonPressed
        {
            add { this.AddHandler(ButtonPressedEvent, value); }
            remove { this.RemoveHandler(ButtonPressedEvent, value); }
        }

        private static readonly RoutedEvent ButtonPressedEvent =
            EventManager.RegisterRoutedEvent("ButtonPressed", RoutingStrategy.Bubble, typeof(EventHandler<ButtonPressedEventArgs>), typeof(Launchpad_MK2));

        private void OnButtonPressed(object sender, MouseEventArgs e)
        {
            base.RaiseEvent(new ButtonPressedEventArgs(ButtonPressedEvent, sender, e));
        }

        /// <summary>
        /// Occurs when chain button pressed.
        /// </summary>
        public event RoutedEventHandler ChainChanged
        {
            add { this.AddHandler(ChainChangedEvent, value); }
            remove { this.RemoveHandler(ChainChangedEvent, value); }
        }

        private static readonly RoutedEvent ChainChangedEvent =
            EventManager.RegisterRoutedEvent("ChainChanged", RoutingStrategy.Bubble, typeof(ChainChangedEventArgs), typeof(Launchpad_MK2));

        private void OnChainChanged(object sender, MouseEventArgs e)
        {
            base.RaiseEvent(new ChainChangedEventArgs(ChainChangedEvent, sender, e));
        }

        /// <summary>
        /// Occurs when move button pressed.
        /// </summary>
        public event RoutedEventHandler MoveButtonPressed
        {
            add { this.AddHandler(MoveButtonPressedEvent, value); }
            remove { this.RemoveHandler(MoveButtonPressedEvent, value); }
        }

        private static readonly RoutedEvent MoveButtonPressedEvent =
            EventManager.RegisterRoutedEvent("MoveButtonPressed", RoutingStrategy.Bubble, typeof(MoveButtonPressedEventArgs), typeof(Launchpad_MK2));

        private void OnMoveButtonPressed(object sender, MouseEventArgs e)
        {
            base.RaiseEvent(new MoveButtonPressedEventArgs(MoveButtonPressedEvent, sender, e));
        }

        /// <summary>
        /// Occurs when mode button pressed.
        /// </summary>
        public event RoutedEventHandler ModeChanged
        {
            add { this.AddHandler(ModeChangedEvent, value); }
            remove { this.RemoveHandler(ModeChangedEvent, value); }
        }

        private static readonly RoutedEvent ModeChangedEvent =
            EventManager.RegisterRoutedEvent("ModeChanged", RoutingStrategy.Bubble, typeof(MK2ModeChangedEventArgs), typeof(Launchpad_MK2));

        private void OnModeChanged(object sender, MouseButtonEventArgs e)
        {
            base.RaiseEvent(new MK2ModeChangedEventArgs(ModeChangedEvent, sender, e));
        }
        #endregion

        #region Enums
        /// <summary>
        /// Style of Buttons. Not support now.
        /// </summary>
        public enum BtnStyle
        {
            /// <summary>
            /// Standard
            /// </summary>
            Standerd,
            /// <summary>
            /// Black
            /// </summary>
            Black
        }

        /// <summary>
        /// Move buttons
        /// </summary>
        public enum MoveButton
        {
            /// <summary>
            /// Up button (▲)
            /// </summary>
            Up,
            /// <summary>
            /// Down button (▼)
            /// </summary>
            Down,
            /// <summary>
            /// Left button (◀)
            /// </summary>
            Left,
            /// <summary>
            /// Right button (▶)
            /// </summary>
            Right
        }

        /// <summary>
        /// Mode buttons
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// Session
            /// </summary>
            Session,
            /// <summary>
            /// User1
            /// </summary>
            User1,
            /// <summary>
            /// User2
            /// </summary>
            User2,
            /// <summary>
            /// Mixer
            /// </summary>
            Mixer
        }

        /// <summary>
        /// Control enum
        /// </summary>
        public enum KeyControl
        {
            /// <summary>
            /// Up
            /// </summary>
            Up = 104,
            /// <summary>
            /// Down
            /// </summary>
            Down,
            /// <summary>
            /// Left
            /// </summary>
            Left,
            /// <summary>
            /// Right
            /// </summary>
            Right,
            /// <summary>
            /// Session
            /// </summary>
            Session,
            /// <summary>
            /// Note
            /// </summary>
            Note,
            /// <summary>
            /// Device
            /// </summary>
            Device,
            /// <summary>
            /// User
            /// </summary>
            User
        }
        #endregion

        #region Consturctor
        /// <summary>
        /// Initailize <see cref="Launchpad_MK2"/>
        /// </summary>
        public Launchpad_MK2()
        {
            InitializeComponent();

            KeyPadButtons = new();

            for (int i = 1; i < 9; i++)
                for (int j = 1; j < 9; j++)
                    KeyPadButtons.Add(new Location(i, j), (Rectangle)FindName($"b{i}{j}"));

            Hook.GlobalEvents().MouseUp += GlobalMouseUp;
        }
        #endregion

        #region Events
        private void GlobalMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MultiSelect)
                return;

            if (_CurrentRect == null)
                return;

            _CurrentRect.Stroke = UnPressedBrush;

            _CurrentRect = null;
        }

        private void PadKeyPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            OnButtonPressed(sender, e);

            _IsPressed = true;

            if (!this.ShowPressed)
                return;

            _CurrentRect = sender as Rectangle;

            var b = sender as Rectangle;
            b.Stroke = PressColor;
        }

        private void SideKeyPressed(object sender, MouseButtonEventArgs e)
        {
            var n = sender.GetName()[1..];

            if (new string[] { "u", "d", "l", "r" }.Contains(n))
                OnMoveButtonPressed(sender, e);
            else if (new string[] { "s", "u1", "u2", "m" }.Contains(n))
                OnModeChanged(sender, e);
            else if (int.Parse(n) < 10)
                OnChainChanged(sender, e);

            _CurrentRect = sender as Rectangle;

            if (!this.ShowPressed)
                return;

            var b = sender as Rectangle;
            b.Stroke = PressColor;
        }

        private void ButtonUp(object sender, MouseButtonEventArgs e)
        {
            _IsPressed = false;

            if (!this.ShowPressed)
                return;

            var b = sender as Rectangle;
            b.Stroke = UnPressedBrush;
        }

        private void ButtonEnter(object sender, MouseEventArgs e)
        {
            if (!MultiSelect)
                return;

            if (!_IsPressed)
                return;

            var n = sender.GetName()[1..];

            if (new string[] { "u", "d", "l", "r" }.Contains(n))
                return;
            else if (new string[] { "s", "u1", "u2", "m" }.Contains(n))
                return;
            else if (int.Parse(n) < 10)
                return;

            if (e.LeftButton == MouseButtonState.Released)
                return;

            OnButtonPressed(sender, e);

            if (!this.ShowPressed)
                return;

            var b = sender as Rectangle;
            b.Stroke = PressColor;
        }

        private void ButtonLeave(object sender, MouseEventArgs e)
        {
            if (!MultiSelect)
                return;

            if (!this.ShowPressed)
                return;

            var b = sender as Rectangle;
            b.Stroke = UnPressedBrush;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Convert <see cref="string"/> to <see cref="Brush"/>
        /// </summary>
        /// <param name="s">An string that represents converted text</param>
        /// <returns>An <see cref="Brush"/> that represents converted text</returns>
        /// <exception cref="NotSupportedException"/>
        public static Brush GetBrushFromString(string s)
            => new BrushConverter().ConvertFromString(s) as SolidColorBrush;

        /// <summary>
        /// Connect InputDevice to Launchpad
        /// </summary>
        /// <param name="input">InputDevice that want to connect</param>
        public void ConnectInput(IInputDevice input)
        {
            Input = input;
            Input.Open();
        }

        /// <summary>
        /// Connect OutputDevice to Launchpad
        /// </summary>
        /// <param name="output">OutputDevice that want to connect</param>
        public void ConnectOutput(IOutputDevice output)
        {
            Output = output;
            Output.Open();
        }

        /// <summary>
        /// Start receving from input. Use <see cref="ConnectInput(IInputDevice)"/> first.
        /// </summary>
        /// <param name="clock"></param>
        public void StartReceiving(Clock clock = null)
            => Input.StartReceiving(clock ?? new Clock(100));

        /// <summary>
        /// Stop receving from input.
        /// </summary>
        public void StopReceiving()
            => Input.StopReceiving();

        /// <summary>
        /// Turn on the note with velocity.
        /// </summary>
        /// <param name="location">Location of note</param>
        /// <param name="velocity">Number of launchpad color. See "<see href="https://github.com/Lukince/LaunchpadColor">Launchpad Color</see>"</param>
        /// <exception cref="NullReferenceException" />
        public void NoteOn(Location location, int velocity)
        {
            if (Output == null)
                throw new NullReferenceException("Set OutputDevice to use ConnectOutput()");

            Output.SendNoteOn(Channel.Channel1, location.GetPitch(), velocity);
        }

        /// <summary>
        /// Turn off the note.
        /// </summary>
        /// <param name="location">Location of note</param>
        /// <exception cref="NullReferenceException" />
        public void NoteOff(Location location)
        {
            if (Output == null)
                throw new NullReferenceException("Set OutputDevice to use ConnectOutput()");

            Output.SendNoteOff(Channel.Channel1, location.GetPitch(), 0);
        }

        /// <summary>
        /// Run <see cref="NoteOn(Location, int)"/> and <see cref="NoteOff(Location)"/> with delay
        /// </summary>
        /// <param name="location">Location of note</param>
        /// <param name="delay">Holding time</param>
        /// <param name = "velocity" > Number of launchpad color. See "<see href="https://github.com/Lukince/LaunchpadColor">Launchpad Color</see>"</param>
        public async void NotePress(Location location, TimeSpan delay, int velocity)
        {
            NoteOn(location, velocity);
            var b = Task.Delay(delay);
            await b;
            NoteOff(location);
        }

        /// <summary>
        /// Turn on the control
        /// </summary>
        /// <param name="key">Key of control</param>
        /// <param name="velocity">Number of launchpad color. See "<see href="https://github.com/Lukince/LaunchpadColor">Launchpad Color</see>"</param>
        /// <exception cref="NullReferenceException" />
        public void ControlOn(KeyControl key, int velocity)
        {
            if (Output == null)
                throw new NullReferenceException("Set OutputDevice to use ConnectOutput()");

            Output.SendControlChange(Channel.Channel1, (Midi.Enums.Control)(int)key, velocity);
        }

        /// <summary>
        /// Turn off the control
        /// </summary>
        /// <param name="key">Key of control</param>
        /// <exception cref="NullReferenceException" />
        public void ControlOff(KeyControl key)
        {
            if (Output == null)
                throw new NullReferenceException("Set OutputDevice to use ConnectOutput()");

            Output.SendControlChange(Channel.Channel1, (Midi.Enums.Control)(int)key, 0);
        }

        /// <summary>
        /// Run <see cref="ControlOn(KeyControl, int)"/> and <see cref="ControlOff(KeyControl)"/> with delay
        /// </summary>
        /// <param name="key">Key of control</param>
        /// <param name="delay">Holding time</param>
        /// <param name = "velocity" > Number of launchpad color. See "<see href="https://github.com/Lukince/LaunchpadColor">Launchpad Color</see>"</param>
        public async void ControlPress(KeyControl key, TimeSpan delay, int velocity)
        {
            ControlOn(key, velocity);
            var b = Task.Delay(delay);
            await b;
            b.Wait();
            ControlOff(key);
        }

        /// <summary>
        /// Set whether "Drag and Press"
        /// </summary>
        public void SetMultiSelect(bool b)
        {
            if (b == MultiSelect)
                return;

            MultiSelect = b;
        }
        #endregion
    }
}
