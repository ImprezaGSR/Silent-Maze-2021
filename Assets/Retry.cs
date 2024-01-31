using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Retry : MonoBehaviour
{
    public GameManager gM;
    private int highlightNum = 0;
    private bool isTyping = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        isTyping = true;
        StartCoroutine(WaitForSelection());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTyping){
            Vector3 newTransform = Vector3.zero;
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                highlightNum++;
                if(highlightNum > (transform.GetChild(0).childCount - 1)){
                    highlightNum = (transform.GetChild(0).childCount - 1);
                }else{
                    newTransform = new Vector3(transform.GetChild(0).GetChild(highlightNum).position.x, transform.GetChild(1).position.y, transform.GetChild(1).position.z);
                    transform.GetChild(1).position = newTransform;
                    Selected(highlightNum);
                }
                    
                
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                highlightNum--;
                if(highlightNum < 0){
                    highlightNum = 0;
                }else{
                    newTransform = new Vector3(transform.GetChild(0).GetChild(highlightNum).position.x, transform.GetChild(1).position.y, transform.GetChild(1).position.z);
                    transform.GetChild(1).position = newTransform;
                    Selected(highlightNum);
                }
            }
        }
        if(Input.GetButtonDown("Submit")&&!isTyping){
            isTyping = true;
            transform.GetChild(1).gameObject.SetActive(false);
            if(transform.Find("GameOver").gameObject.activeSelf){
                transform.Find("GameOver").GetComponent<TypeSentences>().StartDiscontinuous();
            }
            if(transform.Find("LevelComplete").gameObject.activeSelf){
                transform.Find("LevelComplete").GetComponent<TypeSentences>().StartDiscontinuous();
            }
            transform.GetChild(0).GetChild(0).GetComponent<TypeSentences>().StartDiscontinuous();
            transform.GetChild(0).GetChild(1).GetComponent<TypeSentences>().StartDiscontinuous();
            StartCoroutine(WaitForLoading(2-(highlightNum * 2)));
        }
    }

    IEnumerator WaitForSelection(){
        yield return new WaitForSeconds(1f);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        isTyping = false;
        Selected(highlightNum);
        transform.GetChild(0).GetChild(highlightNum).GetComponent<TMP_Text>().color = transform.GetChild(2).GetComponent<TMP_Text>().color;
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void Selected(int num){
        for(int i = 0; i<transform.GetChild(0).childCount; i++){
            if(i == num){
                transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(2).GetComponent<TMP_Text>().color;
            }else{
                transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(3).GetComponent<TMP_Text>().color;
            }
        }
        
    }
    IEnumerator WaitForLoading(int num){
        yield return new WaitForSeconds(1f);
        gM.LoadLevel(num);
    }
}
