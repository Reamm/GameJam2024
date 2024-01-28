using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tungku_api : MonoBehaviour
{
    public AudioClip audio;
    AudioSource audio_sc;
    public NPCFunction script_npc;
    public bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        audio_sc = GetComponent<AudioSource>();
        audio_sc.volume = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && canInteract)
        {

            if (script_npc.getItem)
            {
                script_npc.item_npc_for_player.SetActive(true);
                if (script_npc.item_from_player != null)
                {
                    Destroy(script_npc.item_from_player);

                }
                GetComponent<Animator>().enabled = true;
                script_npc.showEmote = true;
                audio_sc.volume = 1;
                script_npc.done = true;
            }
            if (script_npc.item_from_player != null)
            {
                script_npc.state_dialog_quest = 1;
            }


        }
    }

    void OnCollisionEnter2D(Collision2D player_col)
    {
        if (player_col.gameObject.name == "Bartender")
        {

            script_npc.dialog.text = "Press E to interact with " + gameObject.name;


            if (GrabObject.isGrabbedObj && player_col.gameObject.GetComponent<GrabObject>().grabbed_obj.tag == script_npc.tag_obj)
            {

                script_npc.getItem = true;

                script_npc.item_from_player = player_col.gameObject.GetComponent<GrabObject>().grabbed_obj;

            }
            canInteract = true;

        }
    }

    void OnCollisionExit2D(Collision2D player_col)
    {
        if (player_col.gameObject.name == "Bartender")
        {
            if (GrabObject.isGrabbedObj && player_col.gameObject.GetComponent<GrabObject>().grabbed_obj.tag == script_npc.tag_obj)
            {

                script_npc.getItem = false;
                script_npc.item_from_player = null;
            }
            canInteract = false;

        }
    }

}
