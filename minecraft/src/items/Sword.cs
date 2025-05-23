namespace Inventories.Minecraft.Items;

public class Sword : Tool
{
    public Sword() : base() { }

    public override string Name => "Sword";

    protected override float durabilityLoss => 25f;
}