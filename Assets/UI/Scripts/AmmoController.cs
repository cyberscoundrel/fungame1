using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoController : MonoBehaviour
{

    public void UpdateAmmoTexts(int currentAmmo, int AmmoInReserve)
    {

        string AllAmmo = $"{currentAmmo}/{AmmoInReserve}";
        this.gameObject.GetComponent<TextMeshProUGUI>().SetText(AllAmmo);

    }

}
