using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public int currentHealth;
    public LeanTweenType easing;
    public float animationSpeed;
    public LeanTweenType flashEasing;
    public float flashSpeed;
    public Color damageFlashColor;
    public Color healFlashColor;

    public void UpdateHealth(int health) 
    {
        Debug.Log("Current: " + currentHealth + "New: " + health);
        float currentHealthDec = (float)currentHealth/(float)maxHealth;
        float newHealthDec = (float)health/(float)maxHealth;
        if (newHealthDec < currentHealthDec)
        {
            FlashDamage();
        }
        if (newHealthDec > currentHealthDec) {
            FlashHeal();
        }
        LeanTween.cancel(gameObject);
        Debug.Log("CurrentDec: " + currentHealthDec + "NewDec: " + newHealthDec);
        LeanTween.value(gameObject, currentHealthDec, newHealthDec, animationSpeed).setEase(easing).setOnUpdate((float val) =>
        {
            gameObject.GetComponent<Slider>().value = val;
        });
        currentHealth = health;
    }

    public void SetMaxHealth(int health) 
    {
        maxHealth = health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void FlashDamage() 
    {
        GameObject barfill = GameObject.Find("BarFill");
        //barfill.GetComponent<Image>().color = flashColor;
        LeanTween.value(barfill, damageFlashColor, Color.white, flashSpeed).setEase(flashEasing).setOnUpdate( (Color val) => {
            barfill.GetComponent<Image>().color = val;
        });
    }

    public void FlashHeal() {
        GameObject barfill = GameObject.Find("BarFill");
        //barfill.GetComponent<Image>().color = flashColor;
        LeanTween.value(barfill, healFlashColor, Color.white, flashSpeed).setEase(flashEasing).setOnUpdate((Color val) => {
            barfill.GetComponent<Image>().color = val;
        });
    }
}
