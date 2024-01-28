using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class ObjInteraction : MonoBehaviour
{
    public Vector3 pos_awal;
    public Sprite sprite_awal;
    public TextMeshProUGUI hover_obj;
    public bool canGrab;
    public bool canRelease;

    public GameObject obj_grab;
    AudioSource audioSource;
    public AudioClip sound;
    void Start()
    {
        sprite_awal = GetComponent<SpriteRenderer>().sprite;
        pos_awal = gameObject.transform.position;
        obj_grab = null;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && canGrab && obj_grab != null)
        {
            GrabObj();
            audioSource.PlayOneShot(sound, 0.7F);
            canRelease = true;
            canGrab = false;
        }

        if (Input.GetKey(KeyCode.F) && canRelease && obj_grab == null)
        {
            audioSource.PlayOneShot(sound, 0.7F);

            gameObject.transform.SetParent(null);
            canRelease = false;
        }

        if (!canRelease)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, pos_awal, Time.deltaTime * 5f);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Bartender")
        {
            obj_grab = col.gameObject;
            GetComponent<Animator>().enabled = true;
            hover_obj.text = "Pickup the " + gameObject.name;
            canGrab = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Bartender")
        {
            obj_grab = null;
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = sprite_awal;

            hover_obj.text = "";
            canGrab = false;

        }
    }

    void GrabObj()
    {
        transform.SetParent(obj_grab.transform.GetChild(0).transform);
        transform.localPosition = new Vector3(0, 0, 0);
    }


}
