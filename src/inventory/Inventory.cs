using Godot;
using Godot.Collections;

public partial class Inventory : Resource
{
    [Signal]
    public delegate void ItemAddedEventHandler(Item item);

    [Signal]
    public delegate void ItemRemovedEventHandler(Item item);

    [Export]
    public Array<Item> Items = new Array<Item>();

    public void AddItem(Item item) {
        Items.Add(item);
        EmitSignal("ItemAdded", item);
    }

    public void RemoveItem(Item item) {
        Items.Remove(item);
        EmitSignal("ItemRemoved", item);
    }
}
