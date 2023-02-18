using Godot;
using System;

public partial class ItemInfo : Control
{
    public Item Item
    {
        get { return _item; }
        set { setItem(value); }
    }

    private Item _item;
    private Label _nameLabel;
    private TextureRect _iconTextureRect;

    public override void _Ready()
    {
        _nameLabel = GetNode<Label>("Name");
        _iconTextureRect = GetNode<TextureRect>("Icon");
    }

    private void setItem(Item item)
    {
        _item = item;

        _nameLabel.Text = Item.Name;
        _nameLabel.Modulate = Item.Rarity.Color;

        _iconTextureRect.Modulate = Item.Rarity.Color;
        _iconTextureRect.Texture = Item.Texture;
    }
}
