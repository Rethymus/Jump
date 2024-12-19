using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance = null;
    private AudioSource musicSource;
    public AudioClip backgroundMusic;

    public static BackgroundMusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        // ȷ��������ֻ��һ�� BGM ʵ��
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // ������ƵԴ
        musicSource = gameObject.AddComponent<AudioSource>();
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // ѭ������
            musicSource.playOnAwake = true;
            musicSource.volume = 0.5f; // �����ʵ�������
            musicSource.Play();
        }
    }

    // �ṩ������������������
    public void PlayMusic()
    {
        if (musicSource && !musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource)
            musicSource.Stop();
    }

    public void SetVolume(float volume)
    {
        if (musicSource)
            musicSource.volume = volume;
    }
}