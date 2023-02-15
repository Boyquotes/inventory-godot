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

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        // allow anything to drop
        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        // swap dragged ui item with this ui item
        UIItem uiItem = (UIItem)data;
        Item uiItemItem = uiItem.Item;
        uiItem.Item = Item;
        Item = uiItemItem;
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        // return this as data
        return this;
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
        if (Item == null)
        {
            _iconTextureRect.Texture = null;
            return;
        }

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
