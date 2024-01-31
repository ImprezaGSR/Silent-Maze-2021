using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image fill;
    public TMP_Text text;
    public TextMeshProUGUI dialogueBox;
    public Transform resultSelection;
    public GameObject eye;
    public GameObject background;
    public bool UIOn;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetDialogueBox(string sentence){
        if(UIOn){
            dialogueBox.text = sentence;
        }
    }
    public void Pause(){
        Time.timeScale = 0;
    }
    public void Resume(){
        Time.timeScale = 1;
    }

    public void LoadLevel(int sceneIndex){
        FindObjectOfType<MenuLoader>().GetComponent<MenuLoader>().loadIndex = 5;
        FindObjectOfType<MenuLoader>().GetComponent<MenuLoader>().isLoaded = false;
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

    public void GameOver(){
        StartCoroutine(WaitForGameOver());
    }
    public void LevelComplete(){
        StartCoroutine(WaitForLevelComplete());
    }

    IEnumerator WaitForLevelComplete(){
        yield return new WaitForSeconds(1f);
        background.SetActive(true);
        resultSelection.gameObject.SetActive(true);
        resultSelection.Find("GameOver").gameObject.SetActive(false);
        resultSelection.Find("LevelComplete").gameObject.SetActive(true);
    }
    IEnumerator WaitForGameOver(){
        yield return new WaitForSeconds(1f);
        eye.SetActive(true);
    }
}
