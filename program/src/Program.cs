using System;
using System.Linq;

#if DEBUG___MINECRAFT || RELEASE___MINECRAFT
using Inventories.Minecraft;
using Inventories.Minecraft.Items;
#elif DEBUG__SKYRIM || RELEASE___SKYRIM
using Inventories.Skyrim;
using Inventories.Skyrim.Items;
#endif // DEBUG___MINECRAFT || RELEASE___MINECRAFT

namespace Inventories.Program;

public class Program
{
    private static InventoryManager inventory;
    private static Item selectedItem;

    private static MenuState menuState = MenuState.SelectItem;

    private static bool running;

    private static int selectedItemIndex;
    private static int selectedOptionIndex;

    public static void Main()
    {
#if DEBUG___MINECRAFT || RELEASE___MINECRAFT
        inventory = new InventoryManager();
        inventory.AddItem<Sword>(1);
        inventory.AddItem<WoodenPlanks>(75);
        inventory.AddItem<InvisibilityPotion>(16);
#elif DEBUG__SKYRIM || RELEASE___SKYRIM
        inventory = new InventoryManager(250);
        inventory.AddItem();
#endif // DEBUG__MINECRAFT || RELEASE__MINECRAFT

        running = true;

        while (running)
        {
            // Display the inventory
            Display();

            // Take the user's input
            TakeInput(Console.ReadKey());
        }
    }

    private static void Display()
    {
        // Clear the console
        Console.Clear();

        // Display every item
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Item item = inventory.items[i];
            Console.WriteLine($"[{(selectedItemIndex == i ? "*" : " ")}] {item}");
        }

        // Depending on the state we're in...
        switch (menuState)
        {
            // If we're selecting an item...
            case MenuState.SelectItem:
                // Display the currently selected item
                Console.WriteLine($"\nCurrently selected item: {(selectedItem != default ? selectedItem : "N/A")}");
                break;

            // If we've selected an item...
            case MenuState.ItemManagement:
                // Display the fact that we're now managing the selected item
                Console.WriteLine($"\nManaging item {selectedItem}...");

                // Display the options for the item
                Console.WriteLine("\nOptions:");

                // Display every option
                for (int i = 0; i < selectedItem.Options.Count; i++)
                {
                    Console.WriteLine($"[{(selectedOptionIndex == i ? "*" : " ")}] {selectedItem.Options.Keys.ElementAt(i)}");
                }
                break;
        }
    }

    private static void TakeInput(ConsoleKeyInfo keyInfo)
    {
        // The amount of items
        int itemCount = inventory.items.Count;

        // The amount of options
        int optionsCount = selectedItem?.Options.Count ?? 0;

        if (keyInfo.Key == ConsoleKey.Q)
        {
            // Quit the application
            Environment.Exit(0);
        }

        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow: // Go down the count of items / options
                switch (menuState)
                {
                    case MenuState.SelectItem:
                        // Wrap-around
                        if (selectedItemIndex == itemCount - 1)
                        {
                            selectedItemIndex = 0; // Go to the first item
                            break;
                        }

                        selectedItemIndex++;
                        break;

                    case MenuState.ItemManagement:
                        // Wrap-around
                        if (selectedOptionIndex == optionsCount - 1)
                        {
                            selectedOptionIndex = 0; // Go to the first option
                            break;
                        }

                        selectedOptionIndex++;
                        break;
                }
                break;

            case ConsoleKey.UpArrow: // Go up the count of items / options
                switch (menuState)
                {
                    case MenuState.SelectItem:
                        // Wrap-around
                        if (selectedItemIndex < 1)
                        {
                            selectedItemIndex = itemCount - 1; // Go the last item
                            break;
                        }
                        selectedItemIndex--;
                        break;

                    case MenuState.ItemManagement:
                        // Wrap-around
                        if (selectedOptionIndex < 1)
                        {
                            selectedOptionIndex = optionsCount - 1; // Go the last option
                            break;
                        }

                        selectedOptionIndex--;
                        break;
                }
                break;

            case ConsoleKey.Enter: // Select the active item / invoke the selected option
                switch (menuState)
                {
                    case MenuState.SelectItem:
                        if (inventory.items.Count == 0)
                        {
                            break;
                        }

                        // If we're beyond the list of items...
                        if (selectedItemIndex > inventory.items.Count - 1)
                        {
                            // Clamp the index to the last item
                            selectedItemIndex = inventory.items.Count - 1; 
                        }

                        selectedItem = inventory.items[selectedItemIndex];
                        menuState = MenuState.ItemManagement;
                        break;

                    case MenuState.ItemManagement:
                        // Make sure we can actually do things with this item
                        if (selectedItem.Count <= 0 || (selectedItem as Tool)?.durability <= 0)
                        {
                            inventory.RemoveItem(selectedItem); // Remove the item
                            menuState = MenuState.SelectItem; // Return to the select item state
                            selectedItem = null; // Deselect the item
                            break;
                        }

                        selectedItem.Options.Values.ElementAt(selectedOptionIndex).Invoke();
                        break;
                }
                break;

            case ConsoleKey.Backspace: // Return back to item selection
                if (menuState == MenuState.ItemManagement)
                {
                    menuState = MenuState.SelectItem;
                }
                break;
        }
    }

    /// <summary>
    /// The different states for the menu to be in.
    /// </summary>
    private enum MenuState
    {
        SelectItem, // Selecting an item
        ItemManagement // Item has been selected, we should change stuff about it!
    }
}