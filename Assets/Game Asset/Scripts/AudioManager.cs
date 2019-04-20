using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;

    private AudioSource[] audioSources;

    private void Awake()
    {
        //Singleton pattern
        if ( AM == null )
        {
            DontDestroyOnLoad( gameObject );
            AM = this;
        }
        else if ( AM != this )
        {
            Destroy( gameObject );
        }

        audioSources = GetComponents<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioClip(string clipName, Vector3 location)
    {
        foreach(AudioSource audio in audioSources)
        {
            if (audio.clip.name == clipName)
            {
                AudioSource.PlayClipAtPoint(audio.clip, location);
            }
        }
    }

    public void StopAudioClip(string clipName)
    {
        foreach (AudioSource audio in audioSources)
        {
            if (audio.clip.name == clipName)
            {
                audio.Stop();
            }
        }
    }
}
