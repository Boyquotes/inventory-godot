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
        // add null items until inventory limit is reached
        int inventoryBuffer = Limit - Items.Count;

        for (int i = 0; i < inventoryBuffer; i++)
        {
            AddItem(null);
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        EmitSignal("ItemAdded", item);
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        EmitSignal("ItemRemoved", item);
    }
}
