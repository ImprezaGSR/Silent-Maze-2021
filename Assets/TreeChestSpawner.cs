using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChestSpawner : MonoBehaviour
{
    private chestSpawner cS;
    private GameManager gameManager;
    private bool chestSpawned = false;
    private bool allowInteraction = false;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        cS = transform.parent.parent.parent.GetComponent<chestSpawner>();
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
    void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Interactive")){
            if(!chestSpawned){
                if(cS.isChestRoom){
                    gameManager.SetDialogueBox("Press 'E' to dig under the tree");
                    if(!chestSpawned && allowInteraction){
                        transform.parent.Find("TreeChest").gameObject.SetActive(true);
                        transform.parent.Find("TreeChest").GetChild(0).GetComponent<OpenChest>().isEmpty = false;
                        chestSpawned = true;
                        allowInteraction = false;
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Interactive")){
            gameManager.SetDialogueBox("");
        }
    }
    
}
