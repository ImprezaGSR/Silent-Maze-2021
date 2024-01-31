using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyHole : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject keyPrehab;
    private KeyLeft keyLeft;
    private bool keyHasInserted = false;
    private bool allowInteraction;
    
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        keyLeft = FindObjectOfType<KeyLeft>().GetComponent<KeyLeft>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            allowInteraction = true;
        }
        if(Input.GetKeyUp(KeyCode.E)){
            allowInteraction = false;
        }
    }

    void OnTriggerStay(Collider collision){
        if(collision.CompareTag("Interactive")){
            if(keyLeft.keyNum > (keyLeft.keyRemain + keyLeft.keyInserted)){
                gameManager.SetDialogueBox("Press 'E' to insert key");
                if(allowInteraction && !keyHasInserted){
                    keyPrehab.SetActive(true);
                    keyLeft.keyInserted++;
                    keyHasInserted = true;
                    StartCoroutine(WaitForDecend());
                }
            }else{
                gameManager.SetDialogueBox("You have no key to insert");
            }
            if(keyHasInserted){
                if(keyLeft.keyNum > keyLeft.keyInserted){
                    gameManager.SetDialogueBox("Key inserted! "+(keyLeft.keyNum - keyLeft.keyInserted)+" left to go");
                }else{
                    gameManager.SetDialogueBox("Key inserted! Get back to the gate!");
                }
                
            }
        }
    }

    void OnTriggerExit(Collider collision){
        if(collision.CompareTag("Interactive")){
            gameManager.SetDialogueBox("");
        }
    }

    IEnumerator WaitForDecend(){
        yield return new WaitForSeconds(1.75f);
        animator.SetBool("isDescending", true);
    }

    public void DecreaseKeyToInsert(){
        // keyLeft.keyInserted++;
    }
}
