using Godot;
using Godot.Collections;

public partial class UIInventory : Control
{
    public Inventory Inventory
    {
        get { return _inventory; }
        set { setInventory(value); }
    }

    private GridContainer _itemsGridContainer;
    private Inventory _inventory;
    private ItemInfo _itemInfo;
    private UIItem _selectedUIItem;
    private PackedScene _uiItem = ResourceLoader.Load<PackedScene>("res://src/scenes/ui/inventory/UIItem.tscn");

    public override void _Ready()
    {
        _itemsGridContainer = GetNode<GridContainer>("Items");
        _itemInfo = GetNode<ItemInfo>("ItemInfo");
        _itemInfo.Visible = false;
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
            uiItem.Moved += onUIItemMoved;
            uiItem.Selected += onUIItemSelected;

            if (item != null)
            {
                uiItem.Item = item;
            }

            _itemsGridContainer.AddChild(uiItem);
        }
    }

    private void onUIItemMoved(UIItem uiItem)
    {
        // swap moved item with moved onto item in inventory
        int itemMovedIndex = Inventory.Items.IndexOf(uiItem.Item);
        int itemMovedOntoIndex = _itemsGridContainer.GetChildren().IndexOf(uiItem);

        Item itemMoved = Inventory.Items[itemMovedIndex];
        Item itemMovedOnto = Inventory.Items[itemMovedOntoIndex];

        Inventory.Items[itemMovedOntoIndex] = itemMoved;
        Inventory.Items[itemMovedIndex] = itemMovedOnto;
    }

    private void onUIItemSelected(UIItem uiItem)
    {
        if (_selectedUIItem != null)
        {
            // selecting currently selected ui item, return
            if (_selectedUIItem == uiItem)
            {
                return;
            }

            // unselect currently selected ui item
            _selectedUIItem.IsSelected = false;
        }

        _selectedUIItem = uiItem;

        // empty ui item, clear all
        if (_selectedUIItem.Item == null)
        {
            _itemInfo.Visible = false;
            _selectedUIItem = null;
            return;
        }

        _selectedUIItem = uiItem;
        _selectedUIItem.IsSelected = true;
        _itemInfo.Item = _selectedUIItem.Item;
        _itemInfo.Visible = true;
    }
}
