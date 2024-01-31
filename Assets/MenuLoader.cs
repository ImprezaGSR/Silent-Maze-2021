using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public bool isLoaded;
    // public bool isMainMenuNow;
    [Range(1,5)]
    public int loadIndex = 1;
    public static MenuLoader instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Main Title"){
            if(!isLoaded){
                isLoaded = true;
                for(int i = 1; i < FindObjectOfType<Canvas>().transform.childCount; i++){
                    FindObjectOfType<Canvas>().transform.GetChild(i).gameObject.SetActive(false);
                }
                if(loadIndex == 1){
                    FindObjectOfType<Canvas>().transform.Find("WarningUI").gameObject.SetActive(true);
                }
                if(loadIndex == 2){
                    FindObjectOfType<Canvas>().transform.Find("AnimationUI").gameObject.SetActive(true);
                }
                if(loadIndex == 3){
                    FindObjectOfType<Canvas>().transform.Find("TitleUI").gameObject.SetActive(true);
                }
                if(loadIndex == 4){
                    FindObjectOfType<Canvas>().transform.Find("SettingUI").gameObject.SetActive(true);
                }
                if(loadIndex == 5){
                    FindObjectOfType<Canvas>().transform.Find("SelectUI").gameObject.SetActive(true);
                }
            }
        }
    }
}
