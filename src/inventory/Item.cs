using Godot;
using Godot.Collections;

public partial class Item : Resource
{
    [Export]
    public string Name;

    [Export]
    public Dictionary Properties = new Dictionary();
}
