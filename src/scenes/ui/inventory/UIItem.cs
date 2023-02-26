using Godot;

public partial class UIItem : Control
{
    [Signal]
    public delegate void DroppedEventHandler(UIItem uiItem);

    [Signal]
    public delegate void MovedEventHandler(UIItem uiItem);

    [Signal]
    public delegate void SelectedEventHandler(UIItem uiItem);

    public Item Item
    {
        get { return _item; }
        set { setItem(value); }
    }

    public bool IsSelected
    {
        get { return _isSelected; }
        set { setIsSelected(value); }
    }

    private bool _isSelected;
    private Item _item;
    private TextureRect _iconTextureRect;
    private TextureRect _selectTextureRect;
    private TextureRect _selectedTextureRect;

    public override void _Ready()
    {
        // signals
        MouseEntered += onMouseEntered;
        MouseExited += onMouseExited;
        GuiInput += onGUIInput;

        _iconTextureRect = GetNode<TextureRect>("Icon");
        _iconTextureRect.Texture = null;

        _selectTextureRect = GetNode<TextureRect>("Select");
        _selectTextureRect.Visible = false;

        _selectedTextureRect = GetNode<TextureRect>("Selected");
        _selectedTextureRect.Visible = false;

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
        UIItem uiItem = (UIItem)data;
        Item uiItemItem = uiItem.Item;

        // prevent moving ui item with null item or onto same position
        if (uiItemItem == null || uiItemItem == Item)
        {
            return;
        }

        // swap dragged ui item with this ui item
        uiItem.Item = Item;
        Item = uiItemItem;

        // emit moved and selected signal after updating items
        EmitSignal("Selected", this);
        EmitSignal("Moved", this);
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

    private void setIsSelected(bool isSelected)
    {
        _isSelected = isSelected;
        _selectedTextureRect.Visible = IsSelected;
    }

    private void refreshItem()
    {
        if (Item == null)
        {
            MouseDefaultCursorShape = CursorShape.Arrow;
            _iconTextureRect.Texture = null;
            return;
        }

        MouseDefaultCursorShape = CursorShape.PointingHand;
        _iconTextureRect.Modulate = Item.Rarity.Color;
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

    private void onGUIInput(InputEvent @event)
    {
        if (@event.IsActionPressed("PrimaryAction"))
        {
            EmitSignal("Selected", this);
        }
    }
}
