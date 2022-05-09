using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform followObject;
    public Vector3 offset;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        /*Transform floatParent = (Transform)GameObject.Find("FloatingCanvas").GetComponent<RectTransform>();
        transform.SetParent(floatParent);
        Vector3 pos = cam.WorldToScreenPoint(followObject.position + offset);
        uiElement.transform.position = pos;*/
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update Pos");
        Debug.Log(cam);
        Vector3 pos = cam.WorldToScreenPoint(followObject.position + offset);

        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
 