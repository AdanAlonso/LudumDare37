using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

    public GameObject GOScreen;

    void OnEnable()
    {
        Health.onGameOver += onGameOverScreen;
    }

    void OnDisable()
    {
        Health.onGameOver -= onGameOverScreen;
    }

    void onGameOverScreen()
    {
        GOScreen.SetActive(true);
    }
}
