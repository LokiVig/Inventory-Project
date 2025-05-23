namespace Inventories.Skyrim;

public class Item
{
    public virtual string name { get; set; }
    public virtual int weight { get; set; }

    public int count = 0;

    public virtual void DoAction()
    {

    }

    public override string ToString()
    {
        return $"{name} x{count} ({weight}kg)";
    }
}