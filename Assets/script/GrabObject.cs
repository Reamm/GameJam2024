using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabObject : MonoBehaviour
{

    public GameObject grabbed_obj;
    public static bool isGrabbedObj;
    public static string nama_grab_obj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).transform.childCount > 0)
        {
            isGrabbedObj =true;
            grabbed_obj = transform.GetChild(0).transform.GetChild(0).gameObject;
            nama_grab_obj = grabbed_obj.name;

        }else{
            isGrabbedObj =false;
            nama_grab_obj = "";

            grabbed_obj = null;
        }

        Debug.Log(isGrabbedObj);
    }


}
