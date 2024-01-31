using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeDescSelect : MonoBehaviour
{
    private bool isSelected = false;
    private bool isTyping = true;
    private int highlightNum = 0;
    private int num = 0;
    public LevelLoader levelLoader;
    void OnEnable()
    {
        // if(num == 0){
        //     transform.GetChild(0).GetComponent<TypeSentences>().StartContinuous("MAZE: CASTLE");
        //     transform.GetChild(1).GetComponent<TypeSentences>().StartContinuous("DIFFICULTY: LOW");
        // }
        // if(num == 1){
        //     transform.GetChild(0).GetComponent<TypeSentences>().StartContinuous("MAZE: HEDGE");
        //     transform.GetChild(1).GetComponent<TypeSentences>().StartContinuous("DIFFICULTY: MODERATE");
        // }
        // if(num == 2){
        //     transform.GetChild(0).GetComponent<TypeSentences>().StartContinuous("MAZE: ?????");
        //     transform.GetChild(1).GetComponent<TypeSentences>().StartContinuous("DIFFICULTY: UNKNOWN");
        // }
        isTyping = true;
        StartCoroutine(WaitForDescription());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTyping){
            if(Input.GetKeyDown(KeyCode.RightArrow)&&!isSelected){
                highlightNum++;
                if(highlightNum > (transform.GetChild(3).childCount - 1)){
                    highlightNum = (transform.GetChild(3).childCount - 1);
                }
                Vector3 newTransform = new Vector3(transform.GetChild(3).GetChild(highlightNum).position.x, transform.GetChild(4).position.y, transform.GetChild(4).position.z);
                transform.GetChild(4).position = newTransform;
                for(int i = 0; i<transform.GetChild(3).childCount; i++){
                    if(i == highlightNum){
                        transform.GetChild(3).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(4).GetChild(0).GetComponent<Image>().color;
                    }else{
                        transform.GetChild(3).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(5).GetComponent<TMP_Text>().color;
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)&&!isSelected){
                highlightNum--;
                if(highlightNum < 0){
                    highlightNum = 0;
                }
                Vector3 newTransform = new Vector3(transform.GetChild(3).GetChild(highlightNum).position.x, transform.GetChild(4).position.y, transform.GetChild(4).position.z);
                transform.GetChild(4).position = newTransform;
                for(int i = 0; i<transform.GetChild(3).childCount; i++){
                    if(i == highlightNum){
                        transform.GetChild(3).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(4).GetChild(0).GetComponent<Image>().color;
                    }else{
                        transform.GetChild(3).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(5).GetComponent<TMP_Text>().color;
                    }
                }
            }
            if(Input.GetButtonDown("Submit")&&!isSelected&&!isTyping){
                isSelected = true;
                transform.GetChild(0).GetComponent<TypeSentences>().StartDiscontinuous();
                transform.GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
                transform.GetChild(2).GetComponent<TypeSentences>().StartDiscontinuous();
                for(int i = 0; i<transform.GetChild(3).childCount; i++){
                    transform.GetChild(3).GetChild(i).GetComponent<TypeSentences>().StartDiscontinuous();
                }
                transform.GetChild(4).gameObject.SetActive(false);
                if(highlightNum == 0){
                    for(int i = 0; i<transform.GetChild(3).childCount; i++){
                        transform.parent.GetChild(0).GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("isShrinking",true);
                    }
                    transform.parent.GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
                    StartCoroutine(WaitForLoading());
                }else{
                    StartCoroutine(WaitForDeactivation());
                }
                // transform.GetChild(2).gameObject.SetActive(false);
                // StartCoroutine(WaitForDeactivate());
                // StartCoroutine(WaitThenQuit(1));
                // animationUI.Deactivate();
            }
            if(Input.GetButtonDown("Cancel")&&!isSelected&&!isTyping){
                isSelected = true;
                transform.GetChild(0).GetComponent<TypeSentences>().StartDiscontinuous();
                transform.GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
                transform.GetChild(2).GetComponent<TypeSentences>().StartDiscontinuous();
                for(int i = 0; i<transform.GetChild(3).childCount; i++){
                    transform.GetChild(3).GetChild(i).GetComponent<TypeSentences>().StartDiscontinuous();
                }
                transform.GetChild(4).gameObject.SetActive(false);
                StartCoroutine(WaitForDeactivation());
            }
        }
    }
    public void SetNum(int newNum){
        num = newNum;
    }

    IEnumerator WaitForDescription(){
        yield return new WaitForSeconds(1f);
        isTyping = false;
        transform.GetChild(3).GetChild(highlightNum).GetComponent<TMP_Text>().color = transform.GetChild(4).GetChild(0).GetComponent<Image>().color;
        transform.GetChild(4).gameObject.SetActive(true);
    }
    IEnumerator WaitForDeactivation(){
        yield return new WaitForSeconds(0.75f);
        isSelected = false;
        transform.parent.GetComponent<SelectEvents>().BackToSelect();
        gameObject.SetActive(false);
    }
    IEnumerator WaitForLoading(){
        yield return new WaitForSeconds(1f);
        levelLoader.LoadLevel(num+1);
    }
}
