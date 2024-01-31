using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoardQuiz : MonoBehaviour
{
    [SerializeField]
    private Transform chestPrehab;
    public Transform chestSpawn;
    public chestSpawner cS;
    public GameObject chess;
    private GameManager gameManager;
    private string[] chessAlphabets = new string[8]{"A","B","C","D","E","F","G","H"};
    private string chessAlphabet;
    private int chessNum = 0;
    private int isBlack = 0;
    public string chessCoordinate;
    private bool isQuizSolved = false;

    void Start(){
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        RandomChessCoordinate();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isQuizSolved){
            if(cS.isChestRoom){
                gameManager.SetDialogueBox(chessCoordinate);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && !isQuizSolved){
            gameManager.SetDialogueBox("");
        }
    }
    public void RandomChessCoordinate(){
        int randNum = Random.Range(0,8);
        isBlack = Random.Range(0,2);
        chessAlphabet = chessAlphabets[randNum];
        if(isBlack == 0){
            chessNum = Random.Range(1,3);
        }
        if(isBlack == 1){
            chessNum = Random.Range(7,9);
        }
        chessCoordinate = chessAlphabet+chessNum;
        SetCorrect();
    }
    private void SetCorrect(){
        for(int i = 0; i < 6; i++){
            transform.Find("Chess").Find("White").GetChild(i).GetComponent<chessPawns>().isCorrect = false;
            transform.Find("Chess").Find("Black").GetChild(i).GetComponent<chessPawns>().isCorrect = false;
        }
        if(chessNum == 2){
            transform.Find("Chess").Find("White").Find("Pawn").GetComponent<chessPawns>().isCorrect = true;
        }
        if(chessNum == 7){
            transform.Find("Chess").Find("Black").Find("Pawn").GetComponent<chessPawns>().isCorrect = true;
        }
        if(chessNum == 1){
            if(chessAlphabet == "A" || chessAlphabet == "H"){
                transform.Find("Chess").Find("White").Find("Rook").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "B" || chessAlphabet == "G"){
                transform.Find("Chess").Find("White").Find("Knight").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "C" || chessAlphabet == "E"){
                transform.Find("Chess").Find("White").Find("Bishop").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "D"){
                transform.Find("Chess").Find("White").Find("Queen").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "E"){
                transform.Find("Chess").Find("White").Find("King").GetComponent<chessPawns>().isCorrect = true;
            }
        }
        if(chessNum == 2){
            if(chessAlphabet == "A" || chessAlphabet == "H"){
                transform.Find("Chess").Find("Black").Find("Rook").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "B" || chessAlphabet == "G"){
                transform.Find("Chess").Find("Black").Find("Knight").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "C" || chessAlphabet == "E"){
                transform.Find("Chess").Find("Black").Find("Bishop").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "D"){
                transform.Find("Chess").Find("Black").Find("Queen").GetComponent<chessPawns>().isCorrect = true;
            }
            if(chessAlphabet == "E"){
                transform.Find("Chess").Find("Black").Find("King").GetComponent<chessPawns>().isCorrect = true;
            }
        }
    }
    
    public void SpawnChest(Transform pawnTransform){
        isQuizSolved = true;
        Vector3 pos = new Vector3(pawnTransform.position.x, chestSpawn.position.y, pawnTransform.position.z);
        Quaternion rot = pawnTransform.rotation;
        rot = Quaternion.LookRotation(-transform.forward, Vector3.up);
        chestSpawn.position = pos;
        chestSpawn.rotation = rot;
        
        Transform chest = Instantiate(chestPrehab, chestSpawn);
        chest.GetComponent<OpenChest>().isEmpty = false;
        chess.transform.Find("Black").gameObject.SetActive(false);
        chess.transform.Find("White").gameObject.SetActive(false);
        Debug.Log("Chess Disabled");
    }
}
