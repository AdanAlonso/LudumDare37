using UnityEngine;
using System.Collections;

public class Bounds : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
