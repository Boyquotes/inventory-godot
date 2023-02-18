using Godot;
using System;

public partial class Rarity : Resource
{
    [Export]
    public string Name;

    [Export]
    public Color Color = Colors.White;
}
