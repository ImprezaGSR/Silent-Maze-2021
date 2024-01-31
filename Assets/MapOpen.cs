using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOpen : MonoBehaviour
{
    public bool open = true;
    public Vector3 openPos;
    public Vector3 closePos;
    public float duration = 1f;
    public float start = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(open){
            GetComponent<RectTransform>().anchoredPosition = openPos;
            start = duration;
        }
        if(!open){
            GetComponent<RectTransform>().anchoredPosition = closePos;
            start = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)&&!open){
            open = true;
            Debug.Log(open);
            return;
        }
        if(Input.GetKeyDown(KeyCode.M)&&open){
            open = false;
            Debug.Log(open);
            return;
        }
        if(open){
            start += Time.deltaTime;
        }
        if(!open){
            start -= Time.deltaTime;
        }
        if (start > duration){
            start = duration;
        }
        if (start < 0){
            start = 0f;
        }
        float t = start/duration;
        GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(closePos, openPos, t);
    }
    void FixedUpdate()
    {
        
    }
}
