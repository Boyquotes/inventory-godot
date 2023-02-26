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

    [Export]
    public int Limit;

    public void Init()
    {
        Items.Resize(Limit);
    }

    public void AddItem(Item item)
    {
        int index = Items.IndexOf(null);

        if (index == -1)
        {
            GD.Print("Inventory is full");
            return;
        }

        Items[index] = item;
        EmitSignal("ItemAdded", item);
    }

    public void RemoveItem(Item item)
    {
        int index = Items.IndexOf(item);
        Items[index] = null;

        EmitSignal("ItemRemoved", item);
    }
}
