using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource Background, PlayerSound, EnemySound, FXPrefab;
    private List<AudioSource> fxSounds = new List<AudioSource>();
    public SoundClass[] audioClips;   
    
    // Start is called before the first frame update
    void Start()
    {
        fxSounds.Add(Background);
        fxSounds.Add(PlayerSound);
        fxSounds.Add(EnemySound);
    }

    public void PlaySound(int soundSource, int audioIndex)
    {
        fxSounds[soundSource].clip = audioClips[audioIndex].GetClip();
        fxSounds[soundSource].Play();
    }

    public void PlaySound(string sourceName, AudioClip clip)
    {
        if (sourceName == "Player" || sourceName == "player") PlayerSound.clip = clip; PlayerSound.Play();
    }

    public void PlaySound(string sourceName, int audioIndex)
    {
        if (sourceName == "Player" || sourceName == "player") PlaySound(1, audioIndex);
        if (sourceName == "Background" || sourceName == "background") PlaySound(0, audioIndex);
    }

    public void PlaySound(string sourceName, string soundclass)
    {
        PlaySound(sourceName, GetSoundClass(soundclass));
    }

    int GetSoundClass(string name)
    {
        int index = 0;
        foreach(SoundClass soundclass in audioClips)
        {
            if (soundclass.Name == name) return index;
            index += 1;
        }
        Debug.Log("soundclass not found");
        return -1;
    }
}

[System.Serializable]
public class SoundClass
{
    public string Name;
    public AudioClip[] clips;

    public AudioClip GetClip()
    {
        int clip = Random.Range(0, clips.Length );
        return GetClip(clip);
    }
    public AudioClip GetClip(int index)
    {
        return clips[index];
    }
}
