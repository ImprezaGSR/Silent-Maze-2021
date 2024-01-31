using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeAnimationEvent : MonoBehaviour
{
    public Transform resultSelection;
    public void AfterFading(){
        resultSelection.gameObject.SetActive(true);
        resultSelection.Find("GameOver").gameObject.SetActive(true);
        resultSelection.Find("LevelComplete").gameObject.SetActive(false);
    }
}
