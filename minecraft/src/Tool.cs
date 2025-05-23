using System;

namespace Inventories.Minecraft;

public class Tool : Item
{
    public Tool() : base() { }

    public override int MaxCount => 1;

    protected virtual float durabilityLoss { get; set; } = 10f;

    public float durability = 100f;

    public override void DoAction()
    {
        base.DoAction();

        // Swing this tool! This'll lower its durability and such
        durability -= durabilityLoss;
        Console.WriteLine( $"Swoosh! You used the tool and it lost {durabilityLoss}% of its durability, it's now at {durability}% durability!" );
    }

    public override string ToString()
    {
        return $"\"{Name}\" - {durability}% durability";
    }
}