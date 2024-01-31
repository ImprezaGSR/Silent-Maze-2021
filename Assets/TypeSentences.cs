using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeSentences : MonoBehaviour
{
    public string sentence;
    public float delay = 0.01f;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Continuous(sentence));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Continuous(string sentence){
        GetComponent<TextMeshProUGUI>().text = "";
        foreach(char letter in sentence.ToCharArray()){
            GetComponent<TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds(delay);
            yield return null;
        }
    }
    IEnumerator Discontinuous(){
        string prevSentence = GetComponent<TextMeshProUGUI>().text;
        while(GetComponent<TextMeshProUGUI>().text != ""){
            if(GetComponent<TextMeshProUGUI>().text != ""){
                // GetComponent<TextMeshProUGUI>().text.Remove(GetComponent<TextMeshProUGUI>().text.Length-1);
                prevSentence = prevSentence.Remove(prevSentence.ToCharArray().Length-1,1);
                GetComponent<TextMeshProUGUI>().text = prevSentence;
                yield return new WaitForSeconds(delay);
                yield return null;
                Debug.Log("Deleted");
            }
        }
    }
    public void StartContinuous(string sentence){
        StartCoroutine(Continuous(sentence));
    }
    public void StartDiscontinuous(){
        StartCoroutine(Discontinuous());
    }
}
