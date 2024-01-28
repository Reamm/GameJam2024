using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class NPCFunction : MonoBehaviour
{

    public String hover_state;
    public int state_dialog_quest;
    public string[] dialog_quest;
    public bool canInteract;
    public bool getItem;
    public bool showEmote;
    public GameObject item_from_player;
    public GameObject item_npc_for_player;
    public TextMeshProUGUI dialog;
    public string tag_obj;

    public GameObject emot;

    public Sprite sprite_awal;

    public bool done;
    public bool nextDialog;
    public bool videoPlaying;

    public VideoPlayer video;

    void Start()
    {
        showEmote = false;
        sprite_awal = GetComponent<SpriteRenderer>().sprite;
        video.loopPointReached += WhenVideoFinish;
        ClearRenderTexture();
        video.Pause();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && canInteract)
        {

            if (gameObject.name == "Traveler")
            {
                dialog.text = dialog_quest[state_dialog_quest];

            }
            else
            {
                dialog.text = dialog_quest[state_dialog_quest];

                if (getItem)
                {
                    Destroy(item_from_player);
                    item_npc_for_player.SetActive(true);
                    showEmote = true;
                }

                if (item_from_player != null)
                {
                    state_dialog_quest = 1;
                }
                if (gameObject.name == "Goblin")
                {
                    if (state_dialog_quest == 1 && getItem && item_from_player != null)
                    {
                        nextDialog = true;
                    }
                }

            }


        }
        if (showEmote)
        {
            StartCoroutine(ShowEmote());
        }
        if (nextDialog)
        {
            StartCoroutine(ShowNextDialog());

        }

        if (item_npc_for_player != null)
        {

            if (Input.GetKey(KeyCode.F) && GrabObject.nama_grab_obj == item_npc_for_player.name)
            {
                state_dialog_quest = 2;
                dialog.text = dialog_quest[state_dialog_quest];
                done = true;
            }

        }

    }
    IEnumerator ShowNextDialog()
    {
        yield return new WaitForSeconds(2f);
        state_dialog_quest = 2;
        dialog.text = dialog_quest[state_dialog_quest];
        nextDialog = false;

        yield return new WaitForSeconds(3f);
        video.Play();


        yield return new WaitForSeconds(2f);



    }
    void WhenVideoFinish(VideoPlayer video)
    {
        Debug.Log("YA");
        SceneManager.LoadScene("StartScene");
    }
    private void ClearRenderTexture()
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = video.targetTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }



    IEnumerator ShowEmote()
    {

        emot.SetActive(true);

        yield return new WaitForSeconds(3f);
        emot.SetActive(false);
        showEmote = false;
    }
    void OnCollisionEnter2D(Collision2D player_col)
    {
        if (player_col.gameObject.name == "Bartender")
        {
            dialog.text = "Press E to interact with " + gameObject.name;
            GetComponent<Animator>().enabled = true;
            if (gameObject.name == "Traveler")
            {
                canInteract = true;

            }
            else if (gameObject.name == "Goblin" && state_dialog_quest == 1)
            {
                item_npc_for_player.transform.SetParent(player_col.transform.GetChild(0).transform);
            }
            else
            {
                if (GrabObject.isGrabbedObj && player_col.gameObject.GetComponent<GrabObject>().grabbed_obj.tag == tag_obj)
                {

                    getItem = true;

                    item_from_player = player_col.gameObject.GetComponent<GrabObject>().grabbed_obj;

                }
                canInteract = true;
            }

        }
    }

    void OnCollisionExit2D(Collision2D player_col)
    {
        if (player_col.gameObject.name == "Bartender")
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = sprite_awal;

            dialog.text = "";
            canInteract = false;
            if (GrabObject.isGrabbedObj && player_col.gameObject.GetComponent<GrabObject>().grabbed_obj.tag == tag_obj)
            {

                getItem = false;
                item_from_player = null;
            }

        }
    }


}
