using Godot;
using System;

public partial class Main : Node
{
    public override void _Ready()
    {
        GD.Print("Main Ready");

        foreach (Controller controller in GetNode("Controllers").GetChildren())
        {
            GD.Print($"{controller.Name} Run");
            controller.Run();
        }
    }
}
