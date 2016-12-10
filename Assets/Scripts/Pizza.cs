using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour {

    public float turningSpeed;
    
    void Start()
    {
        StartCoroutine(turn());
    }

	IEnumerator turn() {
        while (true)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 10f * turningSpeed);
            yield return 0;
        }
    }
}
