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
        Health.onGameOver += Health_onGameOver;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
        Health.onGameOver -= Health_onGameOver;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        bgmSrc.clip = bgm[SceneManager.GetActiveScene().buildIndex];
        bgmSrc.loop = true;
        bgmSrc.Play();
    }

    private void Health_onGameOver()
    {
        bgmSrc.clip = bgm[2];
        bgmSrc.loop = false;
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