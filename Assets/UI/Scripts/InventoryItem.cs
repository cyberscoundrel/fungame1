using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{

    public Sprite itemIcon;
    public bool stackable;


    public InventoryItem(Sprite icon, bool stack)
    {
        itemIcon = icon;
        stackable = stack;
    }

    public InventoryItem(Sprite icon) 
    {
        itemIcon = icon;
        stackable = false;
    }

    public Sprite getIcon() {
        return itemIcon;
    }

    public bool isStackable() {
        return stackable;
    }
}
