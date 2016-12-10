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

    void Start() {
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
        healthBar.fillAmount = ((float) health) / maxHealth;
        if (health == 0) {
            player.kill();
            if (onGameOver != null)
                onGameOver();
            Time.timeScale = 0;
        }
    }
}
