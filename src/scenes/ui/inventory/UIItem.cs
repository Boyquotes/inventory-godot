using Godot;

public partial class UIItem : Control
{
    public Item Item
    {
        get { return _item; }
        set { setItem(value); }
    }

    private Item _item;
    private TextureRect _iconTextureRect;
    private TextureRect _selectTextureRect;

    public override void _Ready()
    {
        // signals
        MouseEntered += onMouseEntered;
        MouseExited += onMouseExited;

        _iconTextureRect = GetNode<TextureRect>("Icon");
        _selectTextureRect = GetNode<TextureRect>("Select");
        _selectTextureRect.Visible = false;

        if (Item != null)
        {
            _iconTextureRect.Texture = Item.Texture;
        }
    }

    private void setItem(Item item)
    {
        _item = item;
    }

    private void onMouseEntered()
    {
        _selectTextureRect.Visible = true;
    }

    private void onMouseExited()
    {
        _selectTextureRect.Visible = false;
    }
}
