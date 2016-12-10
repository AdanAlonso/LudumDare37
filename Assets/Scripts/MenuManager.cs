using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public GameObject GameOverScreen;
    public GameObject VictoryScreen;

    void Start()
    {
        if (instance == null)
            instance = this;
        if (this != instance)
            Destroy(gameObject);
    }

    public void loadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void enableObject(GameObject go)
    {
        go.SetActive(true);
    }

    public void enableGameOverScreen()
    {
        enableObject(GameOverScreen);
    }

}
