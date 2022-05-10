using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] inv;
    [SerializeField]
    private int itemCount;
    [SerializeField]
    private int selectedIndex;

    public LeanTweenType easing;
    public float animationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        inv = new InventoryItem[4];
        setSelected(0);
        //For testing purposes
        /*Sprite icon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/UI/Icons/check-circled-outline-01.png", typeof(Sprite));
        Sprite icon2 = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/LeanTween/Examples/Material/Futoro_PersonSprites.jpg", typeof(Sprite));
        inv[0] = new InventoryItem(icon);
        inv[1] = new InventoryItem(icon2);
        //updateUI();
        itemCount++;
        putIntoInventory(new InventoryItem(icon));
        removeFromIndex(0);
        setSelected(1);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            Debug.Log("Mouse Scroll Up");
            setSelected(selectedIndex - 1);
            //wheel goes up
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            Debug.Log("Mouse Scroll Down");
            setSelected(selectedIndex + 1);
        }
    }

    public bool putIntoInventory(InventoryItem item) {
        for (int i = 0; i < inv.Length; i++)
        {
            Debug.Log(i + ": " + inv[i]);
            if (inv[i] == null) 
            {
                Debug.Log("NULL FOUND");
                inv[i] = item;
                itemCount++;
                this.transform.Find("Slot" + (i + 1)).transform.Find("Icon").GetComponent<Image>().sprite = inv[i].getIcon();
                this.transform.Find("Slot" + (i + 1)).transform.Find("Icon").GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                LeanTween.scale(this.transform.Find("Slot" + (i + 1)).transform.Find("Icon").GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f, 1.0f), animationSpeed).setFrom(new Vector3(0f, 0f, 0f)).setEase(easing);
                //updateUI();
                return true;
            }
        }
        return false;
    }

    public void removeFromIndex(int index) {
        if (inv[index] != null) {
            inv[index] = null;
            itemCount--;
            LeanTween.scale(this.transform.Find("Slot" + (index + 1)).transform.Find("Icon").GetComponent<RectTransform>(), new Vector3(0f, 0f, 0f), animationSpeed).setFrom(new Vector3(1f, 1f, 1f)).setEase(easing).setOnComplete(() => {
                this.transform.Find("Slot" + (index + 1)).transform.Find("Icon").GetComponent<Image>().sprite = null;
                this.transform.Find("Slot" + (index + 1)).transform.Find("Icon").GetComponent<Image>().color = new Color(0, 0, 0, 0f);
            });
        }

        //updateUI();
    }

    public void setSelected(int index) {

        if (index > inv.Length - 1)
        {
            selectedIndex = 0;
        }
        else if (index < 0)
        {
            selectedIndex = inv.Length - 1;
        }
        else {
            selectedIndex = index;
        }

        Debug.Log(selectedIndex);
        

        for (int i = 0; i < inv.Length; i++)
        {
            this.transform.Find("Slot" + (i + 1)).transform.Find("SlotBackground").GetComponent<Outline>().enabled = false;
            this.transform.Find("Slot" + (i + 1)).transform.Find("SlotBackground").GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        }
        this.transform.Find("Slot" + (selectedIndex + 1)).transform.Find("SlotBackground").GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        this.transform.Find("Slot" + (selectedIndex + 1)).transform.Find("SlotBackground").GetComponent<Outline>().enabled = true;
        //updateUI();
    }
}
