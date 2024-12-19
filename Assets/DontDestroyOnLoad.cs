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
        // 确保场景中只有一个 BGM 实例
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // 设置音频源
        musicSource = gameObject.AddComponent<AudioSource>();
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // 循环播放
            musicSource.playOnAwake = true;
            musicSource.volume = 0.5f; // 设置适当的音量
            musicSource.Play();
        }
    }

    // 提供公共方法来控制音乐
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