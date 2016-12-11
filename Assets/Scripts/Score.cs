using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    public delegate void GetPoints();
    public static event GetPoints onGetPoints;

    public int score;
    public Text scoreText;
    public float scaleTime;
    public AnimationCurve scaleCurve;
    public AudioClip speedUpSfx;

    Vector3 initialScale;

    void Start() {
        initialScale = scoreText.transform.localScale;
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
        StartCoroutine(Scale());
        score = (score + 1) > 999 ? 999 : score + 1;
        scoreText.text = score.ToString("D3");
        if (onGetPoints != null && score % 5 == 0) {
            AudioManager.instance.playSfx(speedUpSfx);
            onGetPoints();
        }
    }

    IEnumerator Scale()
    {
        float elapsed = 0f;
        while (elapsed < scaleTime)
        {
            scoreText.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, initialScale, scaleCurve.Evaluate(elapsed / scaleTime));
            elapsed += Time.deltaTime;
            yield return 0;
        }
        scoreText.transform.localScale = initialScale;
    }

}
