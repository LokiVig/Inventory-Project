using System;
using System.Collections.Generic;

namespace Inventories.Minecraft;

public class Item
{
    public virtual int MaxCount { get; set; }
    public virtual string Name { get; set; }

    public Dictionary<string, Action> Options;

    public int Count = 0;

    public Item()
    {
        Options = new Dictionary<string, Action>()
        {
            { "Use", DoAction }
        };
    }

    public virtual void DoAction()
    {

    }

    public override string ToString()
    {
        return $"\"{Name}\" x{Count}";
    }
}