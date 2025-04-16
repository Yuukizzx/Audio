using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // 单例模式，方便全局调用

    public AudioSource sfxSource; // 音效播放器
    public AudioSource bgmSource; // 背景音乐播放器

    public AudioClip fireSound;
    public AudioClip bulletHitSound;
    public AudioClip switchWeaponSound;
    public AudioClip noAmmoSound;
    public AudioClip enemySpawnSound;
    
    public AudioClip playerDashSound;
    public AudioClip enemyFireSound;
    public AudioClip enemyFireSound2;
    public AudioClip enemyFireSound3;
    public AudioClip enemyDeathSound;
    public AudioClip playerHurtSound;
    public AudioClip pickupSound1;
    public AudioClip pickupSound2;
    public AudioClip pickupSound3;
    public AudioClip pickupSound4;
    public AudioClip UI;
    public AudioClip RocketFx;
    public AudioClip Attack;
    public AudioClip win;
    public AudioClip lose;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    // 播放音效
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // 播放背景音乐
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }
}
