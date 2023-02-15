using Godot;
using System;

public partial class InventoryController : Controller
{
    public override void Run()
    {
        Inventory inventory = ResourceLoader.Load<Inventory>("data/player_inventory.tres");
        UIInventory uiInventory = GetNode<UIInventory>("/root/Main/UI/UIInventory");
        uiInventory.Inventory = inventory;
    }
}
