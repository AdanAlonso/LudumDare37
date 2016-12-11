using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;
    public GameObject menuGO;
    public GameObject creditsGO;

    public Fade fade;

    void Start()
    {
        if (instance == null)
            instance = this;
        if (this != instance)
            Destroy(gameObject);
    }

    public void loadScene(int sceneNumber)
    {
        StartCoroutine(loadSceneCoroutine(sceneNumber));
    }

    public void resetScene()
    {
        StartCoroutine(loadSceneCoroutine(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator loadSceneCoroutine(int sceneNumber)
    {
        fade.FadeOut();
        yield return new WaitForSecondsRealtime(fade.fadeTime);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

    public void quitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void credits() {
        menuGO.SetActive(!menuGO.activeInHierarchy);
        creditsGO.SetActive(!creditsGO.activeInHierarchy);
    }
}
