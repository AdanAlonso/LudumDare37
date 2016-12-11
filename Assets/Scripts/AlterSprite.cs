using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlterSprite : MonoBehaviour {

    public Sprite spriteNormal;
    public Sprite spriteAlter;

    bool alt;

    void Start()
    {
        alt = PlayerPrefs.GetInt("alt") == 1;
        GetComponent<Image>().sprite = (alt ? spriteAlter : spriteNormal);
    }
}
