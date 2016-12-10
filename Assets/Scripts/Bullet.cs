using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void Start () {
        GetComponent<TrailRenderer>().sortingOrder = 2;
	}
}
