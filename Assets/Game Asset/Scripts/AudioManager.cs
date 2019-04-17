using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    private GameManagerScript gameManager;
    private AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManagerScript>();
        }
        
        audioSources = GetComponents<AudioSource>();
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
}
