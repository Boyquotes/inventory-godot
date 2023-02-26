using Godot;
using System;

public partial class ItemDropArea : Control
{
	private Node2D _character;
	private Node2D _world;
	private PackedScene _worldItem = ResourceLoader.Load<PackedScene>("res://src/scenes/world/inventory/WorldItem.tscn");

	public override void _Ready()
	{
		_world = GetNode<Node2D>("/root/Main/World");
		_character = _world.GetNode<Node2D>("Character");
	}

	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		// allow anything to drop
		return true;
	}

	public override void _DropData(Vector2 atPosition, Variant data)
	{
		UIItem uiItem = (UIItem)data;

		// prevent dropping ui item with null item
		if (uiItem.Item == null)
		{
			return;
		}

		WorldItem worldItem = _worldItem.Instantiate<WorldItem>();
		worldItem.Item = uiItem.Item;
		worldItem.Position = _character.Position;
		_world.AddChild(worldItem);

		uiItem.EmitSignal("Dropped", uiItem);
	}
}
