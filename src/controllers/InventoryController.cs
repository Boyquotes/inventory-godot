using Godot;
using System;

public partial class InventoryController : Controller
{
    public Inventory Inventory = ResourceLoader.Load<Inventory>("data/player_inventory.tres");

    public override void Run()
    {
        Inventory.Init();
        UIInventory uiInventory = GetNode<UIInventory>("/root/Main/UI/UIInventory");
        uiInventory.Inventory = Inventory;
    }

    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest)
        {
            ResourceSaver.Save(Inventory, "data/player_inventory.tres");
        }
    }
}
