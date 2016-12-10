using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public int health;
    public int maxHealth;
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
            MenuManager.instance.enableGameOverScreen();
            Time.timeScale = 0;
        }
    }
}
