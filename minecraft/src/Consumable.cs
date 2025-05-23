namespace Inventories.Minecraft;

public class Consumable : Item
{
    public Consumable() : base() { }

    public override int MaxCount => 16;

    public override void DoAction()
    {
        base.DoAction();

        Count--;
    }
}