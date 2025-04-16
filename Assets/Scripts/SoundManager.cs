using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // ����ģʽ������ȫ�ֵ���

    public AudioSource sfxSource; // ��Ч������
    public AudioSource bgmSource; // �������ֲ�����

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

    // ������Ч
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // ���ű�������
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
