﻿using UnityEngine;
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
    public float angleTime;
    public float timeBetweenShots;

    void Start()
    {
        state = States.Idle;
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
        cursorSprite.SetActive(false);
        while (state == States.Idle) {
            if (Input.anyKeyDown)
                ChangeState(States.Angle);
            yield return 0;
        }
    }

    IEnumerator Angle()
    {
        cursorSprite.SetActive(true);
        Transform shootPointContainer = shootPoint.parent;
        float timer = 0f;
        bool angleGoingUp = true;
        while (state == States.Angle)
        {
            while (timer < angleTime)
            {
                timer += Time.deltaTime;
                shootPointContainer.rotation = angleGoingUp ? Quaternion.Euler(0, 0, timer / angleTime * 90f) 
                                                            : Quaternion.Euler(0, 0, (angleTime - timer) / angleTime * 90f);
                if ((angleGoingUp && Mathf.Abs(shootPointContainer.rotation.eulerAngles.z - 90f) < 1f) ||
                   (!angleGoingUp && Mathf.Abs(shootPointContainer.rotation.eulerAngles.z) < 1f)) {
                    timer = 0;
                    angleGoingUp = !angleGoingUp;
                }
                if (Input.anyKeyDown)
                    break;
                yield return 0;
            }
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation) as GameObject;
            bullet.transform.parent = transform;
            yield return new WaitForSeconds(timeBetweenShots);
            ChangeState(States.Idle);
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
