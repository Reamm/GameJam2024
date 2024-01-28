using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_manager : MonoBehaviour
{

    public NPCFunction[] npc_state_done;
    public bool playVideo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < npc_state_done.Length; i++)
        {
            if(!npc_state_done[i].done){
                playVideo = false;
            }
        }

        if(playVideo){
            StartCoroutine(ShowVideo());
        }
    }

    IEnumerator ShowVideo()
    {

        yield return new WaitForSeconds(3f);
    }
}
