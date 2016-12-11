using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {
    public delegate void GameOver();
    public static event GameOver onGameOver;

    public int health;
    public int maxHealth;
    public Player player;
    public Image healthBar;
    public float scaleTime;
    public AnimationCurve scaleCurve;
    public float fillTime;
    public AnimationCurve fillCurve;

    Vector3 initialScale;

    void Start() {
        initialScale = healthBar.transform.localScale;
        health = maxHealth;
    }

    void OnEnable()
    {
        Enemy.onPlayerHit += takeDamage;
    }

    void OnDisable()
    {
        Enemy.onPlayerHit -= takeDamage;
    }

    public void takeDamage() {
        health = (health - 1) >= 0 ? health - 1 : 0;
        StartCoroutine(Scale());
        StartCoroutine(Resize());
        if (health == 0) {
            player.kill();
            if (onGameOver != null)
                onGameOver();
            Time.timeScale = 0;
        }
    }

    IEnumerator Resize()
    {
        StartCoroutine(Scale());
        float elapsed = 0f;
        float newFill = (float) health / maxHealth;
        float initialFill = healthBar.fillAmount;
        while (elapsed < fillTime)
        {
            healthBar.fillAmount = Mathf.Lerp(initialFill, newFill, fillCurve.Evaluate(elapsed / fillTime));
            elapsed += Time.unscaledDeltaTime;
            yield return 0;
        }
        healthBar.fillAmount = newFill;
    }

    IEnumerator Scale()
    {
        float elapsed = 0f;
        while (elapsed < scaleTime)
        {
            healthBar.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, initialScale, scaleCurve.Evaluate(elapsed / scaleTime));
            elapsed += Time.unscaledDeltaTime;
            yield return 0;
        }
        healthBar.transform.localScale = initialScale;
    }
}
