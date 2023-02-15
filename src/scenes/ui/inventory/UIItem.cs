using Godot;

public partial class UIItem : Control
{
    public Item Item
    {
        get { return _item; }
        set { setItem(value); }
    }

    private Item _item;
    private TextureRect _textureRect;

    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("TextureRect");

        if (Item != null)
        {
			_textureRect.Texture = Item.Texture;
        }
    }

    private void setItem(Item item)
    {
        _item = item;
    }
}
