using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixer mixer;

    public AudioSource bgmSrc;
    public AudioSource sfxSrc;
    public AudioSource aimingSfxSrc;

    public AudioClip[] bgm;

    void Start()
    {
        if (instance == null)
            instance = this;
        if (this != instance)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        bgmSrc.clip = bgm[SceneManager.GetActiveScene().buildIndex];
        bgmSrc.Play();
    }

    public void playSfx(AudioClip sfx)
    {
        if (sfx == null || Time.timeScale == 0)
            return;
        sfxSrc.PlayOneShot(sfx);
    }

    public void playAimingSfx(AudioClip sfx)
    {
        if (sfx == null || Time.timeScale == 0)
            return;
        aimingSfxSrc.PlayOneShot(sfx);
    }
}