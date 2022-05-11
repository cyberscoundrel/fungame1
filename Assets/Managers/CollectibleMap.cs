using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMap
{
    public static Dictionary<int, Func<int,uint,Collectible>> funcMap = new Dictionary<int, Func<int,uint,Collectible>>
    {
    	{0x01, Item.generateCollectible},
    	{0x02, Weapon.generateCollectible}
    };

    public static int[] keys = 
    {
    	0x01,
    	0x02
    };

    public static Dictionary<String, float> RarityValues = new Dictionary<String, float>
    {
        {"common", 1f},
        {"uncommon", 0.5f},
        {"cool", 0.33f},
        {"nice", 0.25f},
        {"wow", 0.125f},
        {"funny", 0.0625f},
        {"silly", 0.03125f} 
    };

}
