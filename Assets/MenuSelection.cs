using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSelection : MonoBehaviour
{
    private bool animationEnded = false;
    private int highlightNum = 0;
    private bool isDeactivating = false;
    public AnimationEvents animationUI;
    public GameObject MazeSelect;
    public GameObject SettingUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        animationEnded = false;
        isDeactivating = false;
        for(int i = 0; i<transform.GetChild(0).childCount; i++){
            transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(2).GetComponent<TMP_Text>().color;
        }
        StartCoroutine(WaitForAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if(animationEnded){
            if(Input.GetKeyDown(KeyCode.DownArrow)&&!isDeactivating){
                highlightNum++;
                if(highlightNum > (transform.GetChild(0).childCount - 1)){
                    highlightNum = (transform.GetChild(0).childCount - 1);
                }
                Vector3 newTransform = new Vector3(transform.GetChild(1).position.x, transform.GetChild(0).GetChild(highlightNum).position.y+15, transform.GetChild(1).position.z);
                transform.GetChild(1).position = newTransform;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    if(i == highlightNum){
                        transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(1).GetChild(0).GetComponent<Image>().color;
                    }else{
                        transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(2).GetComponent<TMP_Text>().color;
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)&&!isDeactivating){
                highlightNum--;
                if(highlightNum < 0){
                    highlightNum = 0;
                }
                Vector3 newTransform = new Vector3(transform.GetChild(1).position.x, transform.GetChild(0).GetChild(highlightNum).position.y+15, transform.GetChild(1).position.z);
                transform.GetChild(1).position = newTransform;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    if(i == highlightNum){
                        transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(1).GetChild(0).GetComponent<Image>().color;
                    }else{
                        transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>().color = transform.GetChild(2).GetComponent<TMP_Text>().color;
                    }
                }
            }
            if(Input.GetButtonDown("Submit")&&!isDeactivating){
                animationEnded = false;
                isDeactivating = true;
                for(int i = 0; i<transform.GetChild(0).childCount; i++){
                    transform.GetChild(0).GetChild(i).GetComponent<TypeSentences>().StartDiscontinuous();
                }
                transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(WaitThenQuit(1));
                animationUI.Deactivate();
            }
        }
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(1);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(highlightNum).GetComponent<TMP_Text>().color = transform.GetChild(1).GetChild(0).GetComponent<Image>().color;
        animationEnded = true;
    }
    IEnumerator WaitThenQuit(float sec){
        yield return new WaitForSeconds(sec);
        if(highlightNum == 0){
            MazeSelect.gameObject.SetActive(true);
        }
        if(highlightNum == 1){
            SettingUI.SetActive(true);
        }
        if(highlightNum == 2){
            yield return new WaitForSeconds(1f);
            Application.Quit();
        }
        gameObject.SetActive(false);
    }
}
