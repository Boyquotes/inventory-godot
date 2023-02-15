using Godot;
using Godot.Collections;

public partial class Item : Resource
{
    [Export]
    public string Name;

    [Export]
    public Dictionary Properties = new Dictionary();

    [Export]
    public Texture2D Texture = ResourceLoader.Load<Texture2D>("res://assets/textures/items/sword.svg");
}
