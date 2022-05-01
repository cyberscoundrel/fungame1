using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    public LeanTweenType easing;
    public float animationSpeed;
    //public GameObject obj;

    public void OnHoverScaleUp()
    {
        Debug.Log("Hover");
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), animationSpeed).setEase(easing);
    }

    public void OnUnhoverScaleDown()
    {
        Debug.Log("UnHover");
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), animationSpeed).setEase(easing);
    }

    public void OnHoverTransformX() {
        LeanTween.cancel(gameObject);
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().anchoredPosition.x - 40f, animationSpeed).setFrom(gameObject.GetComponent<RectTransform>().anchoredPosition.x).setEase(easing);
    }

    public void OnUnhoverTransformX()
    {
        LeanTween.cancel(gameObject);
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().anchoredPosition.x + 40f, animationSpeed).setFrom(gameObject.GetComponent<RectTransform>().anchoredPosition.x).setEase(easing);
    }
}
