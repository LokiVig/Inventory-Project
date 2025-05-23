namespace Inventories.Minecraft;

public class Block : Item
{
    public Block() : base() { }

    public override int MaxCount => 64;

    public override void DoAction()
    {
        base.DoAction();

        Count--;
    }
}