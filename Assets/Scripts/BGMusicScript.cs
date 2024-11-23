using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicScript : MonoBehaviour
{
    public static BGMusicScript _bGMusic;

    [SerializeField]
    private AudioSource _bGAudioSource;
    [SerializeField]
    private AudioClip _loopMusic;

    private void Awake()
    {
        if(_bGMusic != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _bGMusic = this;
        }
        DontDestroyOnLoad(this.gameObject);
        Invoke("ChangeToLoop", _bGAudioSource.clip.length);
    }

    private void ChangeToLoop()
    {
        _bGAudioSource.Stop();
        _bGAudioSource.clip = _loopMusic;
        _bGAudioSource.loop = true;
        _bGAudioSource.Play();
    }
}
