using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public AudioClip clashSfx;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            AudioManager.instance.playSfx(clashSfx);
            GameObject bullet = collision.gameObject;
            bullet.transform.rotation = Quaternion.Euler(0,0,180);
            bullet.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().enabled = false;
            bullet.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
