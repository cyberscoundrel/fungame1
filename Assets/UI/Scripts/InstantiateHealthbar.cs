using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHealthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthbarUI;
    public Transform followTransform;
    public GameObject entity;
    public GameObject healthbarInstance;

    void Start()
    {
        healthbarInstance = Instantiate(healthbarUI);
        Transform floatParent = (Transform)GameObject.Find("FloatingCanvas").GetComponent<RectTransform>();
        healthbarInstance.transform.SetParent(floatParent);
        healthbarInstance.GetComponent<FollowObject>().followObject = followTransform;
        Debug.Log("ENEMY HEALTH: " + entity.GetComponent<EnemyController>().entityObject.getHealth());
        healthbarUI.GetComponent<HealthBarController>().SetMaxHealth(entity.GetComponent<EnemyController>().entityObject.getHealth());
        healthbarUI.GetComponent<HealthBarController>().SetCurrentHealth(entity.GetComponent<EnemyController>().entityObject.getHealth());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
