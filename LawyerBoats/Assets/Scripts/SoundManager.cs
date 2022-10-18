using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<string> ClipNames = new List<string>();
    public List<AudioClip> ClipList = new List<AudioClip>();
    private Dictionary<string, AudioClip> Sound_Library = new Dictionary<string, AudioClip>();

    public GameObject Sound_Prefab;
    AudioSource Sound;
    public AudioSource stopSound;

    public static SoundManager GlobalSoundManager;

    void Start()
    {
        for (int i = 0; i < ClipNames.Count; i++)
        {
            Sound_Library.Add(ClipNames[i], ClipList[i]);
        }
    }

    private void Awake()
    {
        if (GlobalSoundManager == null)
        {
            GlobalSoundManager = this;
        }
        else if (GlobalSoundManager != this)
        {
            Destroy(gameObject);
        }

    }

    public void PlaySound(string ClipName)
    {
        if (Sound_Library.ContainsKey(ClipName))
        {
            Sound = Instantiate(Sound_Prefab).GetComponent<AudioSource>();
            Sound.PlayOneShot(Sound_Library[ClipName]);
            Destroy(Sound.gameObject, Sound_Library[ClipName].length);
        }
    }

    public void PauseSound(string ClipName)
    {
        if (Sound_Library.ContainsKey(ClipName))
        {
            Sound = Instantiate(Sound_Prefab).GetComponent<AudioSource>();
            Sound.Pause();
        }
    }
}
