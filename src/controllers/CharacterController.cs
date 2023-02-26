using Godot;
using Godot.Collections;

public partial class CharacterController : Controller
{
    private CharacterBody2D Character
    {
        get { return _character; }
        set { setCharacter(value); }

    }

    private CharacterBody2D _character;
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private InventoryController _inventoryController;
    private Array<WorldItem> _worldItems = new Array<WorldItem>();
    private Stat _jumpVelocity;
    private Stat _moveSpeed;

    public override void Run()
    {
        Character = GetNode<CharacterBody2D>("/root/Main/World/Character");
        _inventoryController = GetNode<InventoryController>("/root/Main/Controllers/InventoryController");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Interact") && _worldItems.Count > 0)
        {
            // interact with most recent world item, add item to inventory then remove
            WorldItem worldItem = _worldItems[_worldItems.Count - 1];
            _inventoryController.Inventory.AddItem(worldItem.Item);
            worldItem.QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_character == null)
        {
            return;
        }

        if (_character.Position.Y > 200)
        {
            GD.Print("Character teleported back to Vector2.Zero");
            _character.Position = Vector2.Zero;
        }

        Vector2 velocity = _character.Velocity;

        // Add the gravity.
        if (!_character.IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && _character.IsOnFloor())
        {
            velocity.Y = _jumpVelocity.Value;
        }

        float moveRight = Input.GetActionStrength("MoveRight");
        float moveLeft = Input.GetActionStrength("MoveLeft");
        Vector2 direction = new Vector2(moveRight - moveLeft, 0);

        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * _moveSpeed.Value;
        }
        else
        {
            velocity.X = Mathf.MoveToward(_character.Velocity.X, 0, _moveSpeed.Value);
        }

        _character.Velocity = velocity;
        _character.MoveAndSlide();
    }

    private void setCharacter(CharacterBody2D character)
    {
        _character = character;
        _jumpVelocity = Character.GetNode<Stat>("Stats/JumpVelocity");
        _moveSpeed = Character.GetNode<Stat>("Stats/MoveSpeed");

        Area2D interactor = Character.GetNode<Area2D>("Interactor");
        interactor.BodyEntered += onBodyEntered;
        interactor.BodyExited += onBodyExited;
    }

    private void onBodyEntered(Node2D body)
    {
        WorldItem worldItem = (WorldItem)body;
        _worldItems.Add(worldItem);
    }

    private void onBodyExited(Node2D body)
    {
        WorldItem worldItem = (WorldItem)body;
        _worldItems.Remove(worldItem);
    }
}
