using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEvent : MonoBehaviour
{
    public GameManager gameManager;
    public void ActivateBackground(){
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DeactivateBackground(){
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ActivateGameOverUI(){
        gameManager.GameOver();
    }
}
