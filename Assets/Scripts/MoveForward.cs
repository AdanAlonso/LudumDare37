using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

    public float speed;

	void Update () {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * speed);
	}
}
