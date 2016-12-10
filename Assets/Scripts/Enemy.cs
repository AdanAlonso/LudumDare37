using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            MenuManager.instance.enableGameOverScreen();
            Time.timeScale = 0;
        }
    }
}
