using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public int score;
    public Text scoreText;

	void Start() {
        score = 0;
	}

    void OnEnable()
    {
        Enemy.onEnemyKilled += scorePlusOne;
    }

    void OnDisable()
    {
        Enemy.onEnemyKilled -= scorePlusOne;
    }

    void scorePlusOne() {
        score = (score + 1) > 999 ? 999 : score + 1;
        scoreText.text = score.ToString("D3");
    }
	
}
