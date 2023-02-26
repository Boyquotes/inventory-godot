using Godot;
using System;

public partial class WorldItem : RigidBody2D
{
    public Item Item
    {
        get { return _item; }
        set { setItem(value); }
    }

    private Item _item;
    private Sprite2D _iconSprite;

    public override void _Ready()
    {
        _iconSprite = GetNode<Sprite2D>("Icon");

        if (Item != null)
        {
            _iconSprite.Texture = Item.Texture;
        }
    }

    private void setItem(Item item)
    {
        _item = item;
    }
}
