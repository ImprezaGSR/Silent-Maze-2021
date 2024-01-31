using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessPawns : MonoBehaviour
{
    private GameManager gameManager;
    private bool allowInteraction = false;
    public bool isCorrect = false;
    private chessBoardQuiz quiz;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        quiz = transform.parent.parent.parent.GetComponent<chessBoardQuiz>();
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
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Interactive")){
            gameManager.SetDialogueBox("Press 'E' to check this pawn");
        }
    }
    void OnTriggerStay(Collider collision){
        if(collision.CompareTag("Interactive")){
            if(allowInteraction){
                if(isCorrect){
                    gameManager.SetDialogueBox("Correct!");
                    quiz.SpawnChest(transform);
                    allowInteraction = false;
                    return;
                }
                if(!isCorrect){
                    quiz.RandomChessCoordinate();
                    gameManager.SetDialogueBox(quiz.chessCoordinate);
                    allowInteraction = false;
                    return;
                }
            }
        }
    }
    void OnTriggerExit(Collider collision){
        if(collision.CompareTag("Interactive")){
            gameManager.SetDialogueBox("");
        }
    }
}
