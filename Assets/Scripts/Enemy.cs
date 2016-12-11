using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public delegate void EnemyKilled();
    public static event EnemyKilled onEnemyKilled;

    public delegate void PlayerHit();
    public static event PlayerHit onPlayerHit;

    public SpriteRenderer s;
    public Animator a;

    public float scaleTime;
    public AnimationCurve scaleCurve;
    public AudioClip deathSfx;
    public AudioClip targetSfx;

    Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

        void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().enabled = false;
            a.SetTrigger("death");
            StartCoroutine(Scale());
            Destroy(gameObject, 1f);

            GameObject bullet = collision.gameObject;
            bullet.GetComponent<Collider>().enabled = false;
            bullet.GetComponent<Rigidbody>().isKinematic = true;
            bullet.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().enabled = false;
            bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            Destroy(bullet, 5f);

            AudioManager.instance.playSfx(deathSfx);
            if (onEnemyKilled != null)
                onEnemyKilled();
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            AudioManager.instance.playSfx(targetSfx);
            Destroy(gameObject);
            if (onPlayerHit != null)
                onPlayerHit();
        }
    }

    IEnumerator Scale()
    {
        float elapsed = 0f;
        while (elapsed < scaleTime)
        {
            transform.localScale = Vector3.LerpUnclamped(Vector3.zero, initialScale, scaleCurve.Evaluate(elapsed / scaleTime));
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.localScale = initialScale;
    }
}
