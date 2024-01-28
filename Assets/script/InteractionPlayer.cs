using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionPlayer : MonoBehaviour
{

    [SerializeField] GameObject tangan;
    [SerializeField] Transform grab_pointer;
    [SerializeField] Transform ray_pointer;
    [SerializeField] float ray_distance;

    [SerializeField] GameObject grab_obj;
    int layerObj;

    // Start is called before the first frame update
    void Start()
    {
        layerObj = LayerMask.NameToLayer("Grab Obj");
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hit_info = Physics2D.Raycast(ray_pointer.position, Vector2.right * transform.localScale, ray_distance);
        RaycastHit2D hit_info = Physics2D.CircleCast(transform.position, 2f, Vector2.zero, 2f);

        if (hit_info.collider != null && hit_info.collider.gameObject.layer == layerObj)
        {
            Debug.Log(hit_info.collider.gameObject.name);
            if (grab_obj == null)
            {
                grab_obj = hit_info.collider.gameObject;
                grab_obj.transform.SetParent(tangan.transform);
                
            }

            else if (Input.GetKey(KeyCode.L))
            {
                grab_obj.transform.SetParent(null);
                grab_obj.transform.position = hit_info.collider.gameObject.transform.position;
                grab_obj = null;

            }
        }



    }


}
