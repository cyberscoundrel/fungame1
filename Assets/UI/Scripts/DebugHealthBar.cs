using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHealthBar : MonoBehaviour
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

}
