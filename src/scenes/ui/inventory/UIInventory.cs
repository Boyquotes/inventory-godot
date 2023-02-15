using Godot;

public partial class UIInventory : Control
{
    public Inventory Inventory
    {
        get { return _inventory; }
        set { setInventory(value); }
    }

    private GridContainer _itemsGridContainer;
    private Inventory _inventory;
    private PackedScene _uiItem = ResourceLoader.Load<PackedScene>("res://src/scenes/ui/inventory/UIItem.tscn");

    public override void _Ready()
    {
        _itemsGridContainer = GetNode<GridContainer>("Items");
    }

    private void setInventory(Inventory inventory)
    {
        _inventory = inventory;

        // remove all ui items
        foreach (Node child in _itemsGridContainer.GetChildren())
        {
            child.QueueFree();
        }

        // add ui items using inventory items
        foreach (Item item in Inventory.Items)
        {
            UIItem uiItem = _uiItem.Instantiate<UIItem>();

            if (item != null)
            {
                uiItem.Item = item;
            }

            _itemsGridContainer.AddChild(uiItem);
        }
    }
}
