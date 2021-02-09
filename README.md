![Launchpad User Control for WPF](/LaunchpadControl.png)

## About

this is launchpad user control for wpf.

It contains follow functions

- ButtonPress Event
- Move, Mode, Chain, Mixer, Function ButtonPress Event
- MultiSelect (Drag and Press)
- Input and Output Methods
- Note, Control On, Off
- Button Size, Style (Currently fixing)
- Press Color and Board Color

Launchpad Size : MK2 = (340, 340) / Pro = (380, 380)

## Require

C# Version
- Net5.0 wpf (Netframwork version will add later)

Nugets
- [MouseKeyHook/5.6.0v](https://github.com/gmamaladze/globalmousekeyhook)
- [midi-dot-net2/2.0.1v](https://github.com/micdah/midi-dot-net)

## How to use

Add library to use control

```xml
xmlns:lp="clr-namespace:Launchpad;assembly=LaunchpadLib"

<lp:Launchpad_Pro x:Name="LpPro" ButtonPressed="LpPro_ButtonPressed"
                  ShowPressed="False"/>
```

```csharp
// Received as RoutedEventArgs and converted to ButtonPressedEventArgs
private void LpPro_ButtonPressed(object sender, RoutedEventArgs e)
{
    var er = e as ButtonPressEventArgs;
    
    // Require connect midi
    // See https://github.com/Lukince/LaunchpadColor for color value
    lp.NotePress(er.PressedLocation, TimeSpan.FromSeconds(2), 3);
}
```

## Reference

[Velocity Color Value for Launchpad](https://github.com/Lukince/LaunchpadColor)
