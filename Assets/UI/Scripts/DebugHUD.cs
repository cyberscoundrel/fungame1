using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public void setZero() 
    {
        int max = GameObject.Find("HealthBar").GetComponent<HealthBarController>().GetMaxHealth();
        Debug.Log(max);
        GameObject.Find("HealthBar").GetComponent<HealthBarController>().UpdateHealth(0 * max);
    }

    public void setTwo()
    {
        int max = GameObject.Find("HealthBar").GetComponent<HealthBarController>().GetMaxHealth();
        Debug.Log(max);
        GameObject.Find("HealthBar").GetComponent<HealthBarController>().UpdateHealth((int)(0.2 * max));
    }

    public void setHalf()
    {
        int max = GameObject.Find("HealthBar").GetComponent<HealthBarController>().GetMaxHealth();
        Debug.Log(max);
        GameObject.Find("HealthBar").GetComponent<HealthBarController>().UpdateHealth((int)(0.5 * max));
    }

    public void setSeven()
    {
        int max = GameObject.Find("HealthBar").GetComponent<HealthBarController>().GetMaxHealth();
        Debug.Log(max);
        GameObject.Find("HealthBar").GetComponent<HealthBarController>().UpdateHealth((int)(0.7 * max));
    }

    public void setFull()
    {
        int max = GameObject.Find("HealthBar").GetComponent<HealthBarController>().GetMaxHealth();
        Debug.Log(max);
        GameObject.Find("HealthBar").GetComponent<HealthBarController>().UpdateHealth(1 * max);
    }

    public void setRand() {
        int max = GameObject.Find("HealthBar").GetComponent<HealthBarController>().GetMaxHealth();
        GameObject.Find("HealthBar").GetComponent<HealthBarController>().UpdateHealth((int)(Random.Range(0f, 1f) * max));
    }

    public void selectInvZero() {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().setSelected(0);
    }

    public void selectInvOne()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().setSelected(1);
    }

    public void selectInvTwo()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().setSelected(2);
    }

    public void selectInvThree()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().setSelected(3);
    }

    public void putInInv() {
        Sprite icon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/LeanTween/Examples/Material/Futoro_PersonSprites.jpg", typeof(Sprite));
        Debug.Log(GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().putIntoInventory(new InventoryItem(icon)));
    }

    public void removeInvZero()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().removeFromIndex(0);
    }

    public void removeInvOne()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().removeFromIndex(1);
    }

    public void removeInvTwo()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().removeFromIndex(2);
    }

    public void removeInvThree()
    {
        GameObject.Find("InventoryPanel").GetComponent<InventoryManager>().removeFromIndex(3);
    }
}
