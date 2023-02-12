using Godot;
using System;

public partial class Main : Node
{
    public override void _Ready()
    {
        GD.Print("main scene ready");

        Inventory inventory = ResourceLoader.Load<Inventory>("data/player_inventory.tres");
        inventory.Connect("ItemAdded", new Callable(this, "onInventoryItemAdded"));
        inventory.Connect("ItemRemoved", new Callable(this, "onInventoryItemRemoved"));

        Item healthPotion = new Item();
        healthPotion.Name = "Health Potion";

        Item woodenSword = new Item();
        woodenSword.Name = "Wooden Sword";

        // test add
        inventory.AddItem(healthPotion);
        inventory.AddItem(woodenSword);

        // test remove
        inventory.RemoveItem(healthPotion);

        // test save
        ResourceSaver.Save(inventory, "data/player_inventory.tres");
    }

    private void onInventoryItemAdded(Item item) {
        GD.Print("item added " + item.Name);
    }

    private void onInventoryItemRemoved(Item item) {
        GD.Print("item removed " + item.Name);
    }
}
