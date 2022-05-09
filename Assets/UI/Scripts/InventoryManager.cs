using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] inv;
    public int itemCount;

    // Start is called before the first frame update
    void Start()
    {
        inv = new InventoryItem[4];
        //For testing purposes
        Sprite icon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/UI/Icons/check-circled-outline-01.png", typeof(Sprite));
        Sprite icon2 = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/LeanTween/Examples/Material/Futoro_PersonSprites.jpg", typeof(Sprite));
        inv[0] = new InventoryItem(icon);
        inv[1] = new InventoryItem(icon2);
        updateUI();
        itemCount++;
        putIntoInventory(new InventoryItem(icon));
        removeFromIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void putIntoInventory(InventoryItem item) {
        for (int i = 0; i < inv.Length; i++)
        {
            Debug.Log(inv[i]);
            if (inv[i] == null) 
            {
                inv[i] = item;
                break;
            }
        }
        itemCount++;
        updateUI();
    }

    public void removeFromIndex(int index) {
        inv[index] = null;
        itemCount--;
        updateUI();
    }

    public void updateUI() {
        Debug.Log(this.transform.Find("Slot" + (0 + 1)).transform.Find("Icon"));
        for (int i = 0; i < inv.Length; i++) {
            if (inv[i] != null)
            {
                this.transform.Find("Slot" + (i + 1)).transform.Find("Icon").GetComponent<Image>().sprite = inv[i].getIcon();
            }
            else {
                this.transform.Find("Slot" + (i + 1)).transform.Find("Icon").GetComponent<Image>().sprite = null;
            }
        }
    }
}
