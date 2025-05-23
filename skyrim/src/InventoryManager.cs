using System;
using System.Collections.Generic;

namespace Inventories.Skyrim;

public class InventoryManager
{
    private List<Item> items;
    private int maxWeight;

    public InventoryManager(int maxWeight)
    {
        items = new List<Item>();

        if ( maxWeight <= 0 )
        {
            Console.WriteLine( "Can't have a negative or 0 max weight!" );
            return;
        }

        this.maxWeight = maxWeight;
    }


}