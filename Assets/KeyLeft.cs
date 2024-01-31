using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyLeft : MonoBehaviour
{
    public TextMeshProUGUI KeyText;
    public int keyRemain = 0;
    public int keyNum = 0;
    public int keyInserted = 0;
    // Start is called before the first frame update
    void Start()
    {
        keyRemain = keyNum;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KeyText.text = "Key Left: "+keyRemain;
        if(keyRemain <= 0){
            KeyText.text = "Return to the Gate!";
        }
    }
}
