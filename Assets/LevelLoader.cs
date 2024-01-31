using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image fill;
    public TMP_Text text;
    public void LoadLevel(int sceneIndex){
        FindObjectOfType<MenuLoader>().GetComponent<MenuLoader>().loadIndex = 5;
        // AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress/.9f);
            fill.fillAmount = progress;
            text.text = "GENERATING... "+Mathf.RoundToInt(progress * 100f)+ "%";
            yield return null;
        }
    }
}
