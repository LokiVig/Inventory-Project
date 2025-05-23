using System;
using System.Collections.Generic;

namespace Inventories.Minecraft;

/// <summary>
/// A holder of items. A container of usables. Inventory of resources. Table of interactables.
/// </summary>
public class InventoryManager
{
    /// <summary>
    /// The list of items in this inventory.
    /// </summary>
    public List<Item> items = new List<Item>();

    /// <summary>
    /// Adds a specific item, defined from the type.
    /// </summary>
    /// <typeparam name="T">The type of the item we should add.</typeparam>
    /// <param name="item">The specific item we should add.</param>
    /// <param name="count">The amount of this item we should add to.</param>
    public void AddItem<T>(int count) where T : Item, new()
    {
        // Create a new instance of the item we want to add
        T item = new T();

        // Check every item in our list...
        for (int i = 0; i < items.Count; i++)
        {
            // If we've found one corresponding to the item we want to add to...
            if (items[i] == item)
            {
                // Increase its count
                items[i].Count += count;

                // Clamp the count if it reached the max
                if (items[i].Count > items[i].MaxCount)
                {
                    items[i].Count = items[i].MaxCount;
                    Console.WriteLine($"AddItem<T>(int): INFO; Item (\"{items[i].Name}\") hit max count ({items[i].MaxCount})! It's been clamped.");
                    return; // We don't want to progress to the part where we add the item if it doesn't already exist
                }
            }
        }

        // Add the item we want to add, because it doesn't already exist in our list
        item.Count = count;

        // Clamp the count
        if (item.Count > item.MaxCount)
        {
            item.Count = item.MaxCount;
            Console.WriteLine($"AddItem<T>(int): INFO; Item (\"{item.Name}\") hit max count ({item.MaxCount})! It's been clamped.");
        }

        // Add the item to our list
        items.Add(item);
    }

    /// <summary>
    /// Removes a specific item, defined from the type.
    /// </summary>
    /// <typeparam name="T">The type of the weapon we should remove.</typeparam>
    /// <param name="item">The specified item we should remove from.</param>
    /// <param name="count">The amount of this item we should remove from.</param>
    public void RemoveItem<T>(int count) where T : Item, new()
    {
        foreach (Item item in items)
        {
            if (item is T)
            {
                // Remove count amount of this item
                item.Count -= count;

                Console.WriteLine($"RemoveItem<T>(int): INFO; Successfully removed {count} counts of {typeof(T)}!");

                // If there's no more of this item...
                if (item.Count <= 0)
                {
                    // Remove it from our list
                    items.Remove(item);
                }

                // Return out of the function before we hit the failcase
                return;
            }
        }

        // Failcase, couldn't find specific item!
        Console.WriteLine($"RemoveItem(): ERROR; Item of type \"{typeof(T)}\" not found in inventory!");
    }

    /// <summary>
    /// Removes a specific item, defined from the argument.
    /// </summary>
    /// <param name="item">The item we wish to remove.</param>
    /// <param name="count">The optional count of this item we wish to remove of.</param>
    public void RemoveItem(Item item, int count = 0)
    {
        // If we want to remove a count of this item...
        if (count > 0)
        {
            // Remove the count
            item.Count -= count;

            // If there's less or equal to zero of this item now...
            if (item.Count <= 0)
            {
                // Remove it from the list
                items.Remove(item);
            }
        }
        else // Otherwise, we want to remove the item as a whole...
        {
            // Remove it directly from the list
            items.Remove(item);
        }
    }
}