using Godot;
using System;

public partial class Main : Node
{
    public override void _Ready()
    {
        GD.Print("Main Ready");

        foreach (Controller controller in GetNode("Controllers").GetChildren())
        {
            GD.Print(String.Format("{0} Run", controller.Name));
            controller.Run();
        }
    }
}
