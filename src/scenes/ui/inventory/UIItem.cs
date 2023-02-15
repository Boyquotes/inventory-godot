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
        _iconTextureRect.Texture = null;

        _selectTextureRect = GetNode<TextureRect>("Select");
        _selectTextureRect.Visible = false;

        if (Item != null)
        {
            refreshItem();
        }
    }

    private void setItem(Item item)
    {
        _item = item;

        if (IsInsideTree())
        {
            refreshItem();
        }
    }

    private void refreshItem()
    {
        _iconTextureRect.Texture = Item.Texture;
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
