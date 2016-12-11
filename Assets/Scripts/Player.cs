using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    [System.Serializable]
    public enum States
    {
        Idle,
        Angle,
        Dead
    }
    public States state;

    public GameObject bulletPrefab;
    public Transform shootPoint;
    public GameObject cursorSprite;
    public GameObject arm;
    public float angleTime;
    public float timeBetweenShots;

    public AudioClip aimingSfx;
    public AudioClip throwKnifeSfx;

    public Animator a;
    Animator armA;

    void Start()
    {
        state = States.Idle;
        armA = arm.GetComponent<Animator>();
        StartCoroutine(FSM());
    }


    IEnumerator FSM()
    {
        ChangeState(state);
        while (true)
        {
            yield return StartCoroutine(state.ToString());
        }
    }

    void ChangeState(States newState)
    {
        state = newState;
    }

    IEnumerator Idle()
    {
        a.SetBool("idle", true);
        cursorSprite.SetActive(false);
        arm.SetActive(false);
        while (state == States.Idle) {
            if (Input.anyKeyDown)
                ChangeState(States.Angle);
            yield return 0;
        }
    }

    IEnumerator Angle()
    {
        a.SetBool("idle", false);
        cursorSprite.SetActive(true);
        arm.SetActive(true);
        armA.SetBool("shoot", false);
        Transform shootPointContainer = shootPoint.parent;
        float timer = 0f;
        bool angleGoingUp = true;
        while (state == States.Angle)
        {
            if (!AudioManager.instance.aimingSfxSrc.isPlaying)
                AudioManager.instance.playAimingSfx(aimingSfx);
            timer += Time.unscaledDeltaTime;
            shootPointContainer.rotation = angleGoingUp ? Quaternion.Euler(0, 0, timer / angleTime * 90f) 
                                                        : Quaternion.Euler(0, 0, (angleTime - timer) / angleTime * 90f);
            if ((angleGoingUp && Mathf.Abs(shootPointContainer.rotation.eulerAngles.z - 90f) < 1f) ||
                (!angleGoingUp && Mathf.Abs(shootPointContainer.rotation.eulerAngles.z) < 1f)) {
                timer = 0;
                angleGoingUp = !angleGoingUp;
            }
            if (Input.anyKeyDown) {
                armA.SetBool("shoot", true);
                AudioManager.instance.playSfx(throwKnifeSfx);
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation) as GameObject;
                bullet.transform.parent = transform;
                yield return new WaitForSeconds(timeBetweenShots);
                ChangeState(States.Idle);
            }
            yield return 0;
        }
    }

    public void kill() {
        ChangeState(States.Dead);
    }

    IEnumerator Dead()
    {
        while(state == States.Dead)
        {
            yield return 0;
        }
    }

}
