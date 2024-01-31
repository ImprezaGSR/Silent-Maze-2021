using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectEvents : MonoBehaviour
{
    private bool isAnimating = true;
    private bool isSelected = false;
    private int highlightNum = 0;
    public GameObject animationUI;
    void OnEnable()
    {
        isAnimating = true;
        for(int i = 0; i<transform.GetChild(0).childCount; i++){
            transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = transform.GetChild(3).GetComponent<TMP_Text>().color;
        }
        StartCoroutine(WaitForAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAnimating){
            if(Input.GetKeyDown(KeyCode.RightArrow)&&!isSelected){
                highlightNum++;
                if(highlightNum > (transform.GetChild(0).childCount - 1)){
                    highlightNum = (transform.GetChild(0).childCount - 1);
                }
                Vector3 newTransform = new Vector3(transform.GetChild(0).GetChild(highlightNum).position.x, transform.GetChild(2).position.y, transform.GetChild(2).position.z);
                transform.GetChild(2).position = newTransform;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    if(i == highlightNum){
                        transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = transform.GetChild(2).GetChild(0).GetComponent<Image>().color;
                        transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<CircleIcon>().SetIsSelected(true);
                    }else{
                        transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = transform.GetChild(3).GetComponent<TMP_Text>().color;
                        transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<CircleIcon>().SetIsSelected(false);
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)&&!isSelected){
                highlightNum--;
                if(highlightNum < 0){
                    highlightNum = 0;
                }
                Vector3 newTransform = new Vector3(transform.GetChild(0).GetChild(highlightNum).position.x, transform.GetChild(2).position.y, transform.GetChild(2).position.z);
                transform.GetChild(2).position = newTransform;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    if(i == highlightNum){
                        transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = transform.GetChild(2).GetChild(0).GetComponent<Image>().color;
                        transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<CircleIcon>().SetIsSelected(true);
                    }else{
                        transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = transform.GetChild(3).GetComponent<TMP_Text>().color;
                        transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<CircleIcon>().SetIsSelected(false);
                    }
                }
            }
            if(Input.GetButtonDown("Submit")&&!isSelected&&!isAnimating){
                if(highlightNum > 1){
                    return;
                }
                isSelected = true;
                isAnimating = true;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    if(i != highlightNum){
                        transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<CircleIcon>().Deactivate();
                    }
                    transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
                }
                transform.GetChild(2).gameObject.SetActive(false);
                StartCoroutine(WaitForDeactivate());
                // StartCoroutine(WaitThenQuit(1));
                // animationUI.Deactivate();
            }
            if(Input.GetButtonDown("Cancel")&&!isSelected&&!isAnimating){
                isSelected = true;
                isAnimating = true;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<CircleIcon>().Deactivate();
                    transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
                }
                transform.GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
                transform.GetChild(2).gameObject.SetActive(false);
                StartCoroutine(WaitForQuit());
                // StartCoroutine(WaitThenQuit(1));
                // animationUI.Deactivate();
            }
        }
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(2);
        transform.GetChild(0).GetChild(highlightNum).GetChild(1).GetComponent<TMP_Text>().color = transform.GetChild(2).GetChild(0).GetComponent<Image>().color;
        transform.GetChild(0).GetChild(highlightNum).GetChild(0).GetComponent<CircleIcon>().SetIsSelected(true);
        transform.GetChild(2).gameObject.SetActive(true);
        isSelected = false;
        isAnimating = false;
    }
    IEnumerator WaitForDeactivate(){
        yield return new WaitForSeconds(0.25f);
        Debug.Log(transform.GetChild(0).Find("Castle").GetComponent<RectTransform>().anchoredPosition3D);
        transform.GetChild(0).GetChild(highlightNum).GetChild(0).GetComponent<CircleIcon>().SetDestination(transform.GetChild(0).Find("Castle").GetComponent<RectTransform>().anchoredPosition3D);
        transform.GetChild(0).GetChild(highlightNum).GetChild(0).GetComponent<CircleIcon>().SetIsMoving(true);
        yield return new WaitForSeconds(0.25f);
        for(int i = 0; i<transform.GetChild(0).childCount; i++){
            // if(i != highlightNum){
            //     transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(false);
            // }
            transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
        Debug.Log(highlightNum);
        if(highlightNum == 0){
            transform.GetChild(4).GetChild(0).GetComponent<TypeSentences>().sentence = "MAZE: CASTLE";
            transform.GetChild(4).GetChild(1).GetComponent<TypeSentences>().sentence = "DIFFICULTY: LOW";
        }
        if(highlightNum == 1){
            transform.GetChild(4).GetChild(0).GetComponent<TypeSentences>().sentence = "MAZE: HEDGE";
            transform.GetChild(4).GetChild(1).GetComponent<TypeSentences>().sentence = "DIFFICULTY: MODERATE";
        }
        if(highlightNum == 2){
            transform.GetChild(4).GetChild(0).GetComponent<TypeSentences>().sentence = "MAZE: ?????";
            transform.GetChild(4).GetChild(1).GetComponent<TypeSentences>().sentence = "DIFFICULTY: UNKNOWN";
        }
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(4).GetComponent<MazeDescSelect>().SetNum(highlightNum);
    }

    public void BackToSelect(){
        // StartCoroutine(WaitForBack());
        for(int i = 0; i<transform.GetChild(0).childCount; i++){
            if(i != highlightNum){
                transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            }
        }
        transform.GetChild(0).GetChild(highlightNum).GetChild(0).GetComponent<CircleIcon>().SetIsMoving(false);
        StartCoroutine(WaitForBack());
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForBack(){
        yield return new WaitForSeconds(1.5f); 
        for(int i = 0; i < transform.GetChild(0).childCount; i++){
            transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(true);
        }
    }
    IEnumerator WaitForQuit(){
        yield return new WaitForSeconds(1f);
        animationUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
