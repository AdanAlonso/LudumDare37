using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public delegate void EnemyKilled();
    public static event EnemyKilled onEnemyKilled;

    public delegate void PlayerHit();
    public static event PlayerHit onPlayerHit;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            if (onEnemyKilled != null)
                onEnemyKilled();
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
            if (onPlayerHit != null)
                onPlayerHit();
        }
    }
}
