using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hay : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMovement player;
    private bool isGettingIn = false;
    private bool isGettingOut = false;
    private bool allowInteraction;
    public GameObject Outside;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        gameManager.SetDialogueBox("");
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isHiding){
            isGettingIn = false;
            gameManager.SetDialogueBox("Press 'E' to get out from the hay");
        }
        if(isGettingIn){
            gameManager.SetDialogueBox("Now hiding...");
        }
        if(isGettingOut){
            gameManager.SetDialogueBox("Getting out...");
        }
        if(Input.GetKeyDown(KeyCode.E)){
            allowInteraction = true;
        }
        if(Input.GetKeyUp(KeyCode.E)){
            allowInteraction = false;
        }
    }
    // void OnTriggerEnter(Collider collision)
    // {

    //     if(collision.CompareTag("Interactive")||collision.CompareTag("Player")){
    //         if(!player.isHiding){
    //             dialogueBox.text = "Press 'E' to hide inside the locker";
    //         }
    //     }
    // }
    void OnTriggerStay(Collider collision){
        if(collision.CompareTag("Interactive")||collision.CompareTag("Player")){
            if(!isGettingIn){
                if(!player.isHiding){
                    gameManager.SetDialogueBox("Press 'E' to hide inside the hay");
                }
                if(allowInteraction){
                    if(!player.isHiding){
                        isGettingIn = true;
                        StartCoroutine(DelayClose(0.8f));
                        player.DesetAutoTarget();
                        player.SetAutoTarget(transform.parent.Find("Inside"), "Hide");
                    }
                    if(player.isHiding){
                        player.DesetAutoTarget();
                        player.SetAutoTarget(transform.parent.Find("Outside"), "");
                        // StartCoroutine(WaitForMovable());
                        player.isHiding = false;
                        Outside.SetActive(false);
                    }
                    
                }
            }
            allowInteraction = false;
        }
    }
    void OnTriggerExit(Collider collision){
        if(collision.CompareTag("Player")){
            gameManager.SetDialogueBox("");
            allowInteraction = false;
            // dialogueBox.text = "";
            // isOpen = false;
            // allowInteraction = false;
        }
        if(collision.CompareTag("Interactive")){
            gameManager.SetDialogueBox("");
        }
    }
    
    IEnumerator DelayClose(float time){
        yield return new WaitForSeconds(time);
        yield return new WaitForSeconds(0.4f);
        player.isHiding = true;
        Outside.SetActive(true);
    }

    // IEnumerator WaitForMovable(){
    //     yield return new WaitForSeconds(1.25f);
    //     player.DesetAutoTarget();
    // }
}
