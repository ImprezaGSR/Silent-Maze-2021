using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class locker : MonoBehaviour
{
    public Animator animator;
    private GameManager gameManager;
    private PlayerMovement player;
    public bool isOpen = false;
    private bool isGettingIn = false;
    private bool allowInteraction;
    public GameObject MonsterIdle;
    public Transform ground;
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
        animator.SetBool("isOpen", isOpen);
        if(player.isHiding){
            isGettingIn = false;
            gameManager.SetDialogueBox("Press 'E' to get out from the locker");
        }
        if(isGettingIn){
            gameManager.SetDialogueBox("Now hiding...");
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
                    gameManager.SetDialogueBox("Press 'E' to hide inside the locker");
                }
                if(allowInteraction){
                    if(!player.isHiding){
                        isGettingIn = true;
                        if(!isOpen){
                            isOpen = true;
                            StartCoroutine(DelayClose(0.8f));
                        }else{
                            StartCoroutine(DelayClose(0f));
                        }
                        player.DesetAutoTarget();
                        player.SetAutoTarget(ground, "Hide");
                        MonsterIdle.SetActive(true);
                    }
                    if(player.isHiding){
                        isOpen = true;
                        player.DesetAutoTarget();
                        player.isHiding = false;
                        MonsterIdle.SetActive(false);
                    }
                    
                }
            }
            allowInteraction = false;
        }
    }
    void OnTriggerExit(Collider collision){
        if(collision.CompareTag("Player")){
            gameManager.SetDialogueBox("");
            isOpen = false;
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
        isOpen = false;
        yield return new WaitForSeconds(0.4f);
        player.isHiding = true;
    }
}
