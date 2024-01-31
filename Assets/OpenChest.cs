using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenChest : MonoBehaviour
{
    public Animator animator;
    public Animator keyAnimator;
    public bool isEmpty = false;
    [SerializeField]
    private GameObject keyModel = null;
    private GameManager gameManager;
    private TextMeshProUGUI dialogueBox;
    private bool isCollected = false;
    private KeyLeft keyLeft;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        keyLeft = FindObjectOfType<KeyLeft>().GetComponent<KeyLeft>();
        gameManager.SetDialogueBox("");
        if(!isEmpty){
            keyModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Interactive")){
            animator.SetBool("isOpen", true);
            animator.SetBool("unlocked", true);
            if(!isEmpty){
                keyAnimator.SetBool("isOut",true);
                gameManager.SetDialogueBox("Press 'E' to collect key");
            }else{
                gameManager.SetDialogueBox("This chest is empty");
            }
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Interactive")){
            if(Input.GetKeyDown(KeyCode.E)&&!isEmpty&&!isCollected){
                keyLeft.keyRemain--;
                gameManager.SetDialogueBox("Key Collected!");
                isEmpty = true;
                Destroy(keyModel);
                if(keyLeft.keyRemain < keyLeft.keyNum-1){
                    FindObjectOfType<StalkerAI>().GetComponent<StalkerAI>().LevelUp();
                }
                if(Input.GetKeyUp(KeyCode.E)){
                    isCollected = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.E)&&isEmpty&&isCollected){
                gameManager.SetDialogueBox("This chest is empty");
            }
        }
    }
    void OnTriggerExit(Collider collision){
        if(collision.CompareTag("Interactive")){
            animator.SetBool("isOpen", false);
            gameManager.SetDialogueBox("");
            if(isEmpty){
                isCollected = true;
            }
        }
    }
}
