using Godot;
using System;

public partial class Character : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
        {
			velocity.Y += gravity * (float)delta;
        }

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
			velocity.Y = JumpVelocity;
        }

		float moveRight = Input.GetActionStrength("MoveRight");
        float moveLeft = Input.GetActionStrength("MoveLeft");
        Vector2 direction = new Vector2(moveRight - moveLeft, 0);

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
