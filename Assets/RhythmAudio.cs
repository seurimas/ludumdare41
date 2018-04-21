using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmAudio : MonoBehaviour, IRhythmListener
{
    public AudioClip beat;
    public AudioClip fail;
    public AudioClip fighter;
    public AudioClip bard;
    public AudioClip cleric;
    public AudioClip rogue;
    public AudioSource source;

    public void OnBeatEarly()
    {
    }

    public void OnBeatRight()
    {
        source.PlayOneShot(beat, 0.5f);
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
        if (tooEarly)
        {
            source.PlayOneShot(fail);
        }
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        switch (note)
        {
            case Notes.Fighter:
                source.PlayOneShot(fighter);
                break;
            case Notes.Bard:
                source.PlayOneShot(bard);
                break;
            case Notes.Cleric:
                source.PlayOneShot(cleric);
                break;
            case Notes.Rogue:
                source.PlayOneShot(rogue);
                break;
        }
        return false;
    }

    // Use this for initialization
    void Start () {
        RhythmManager.instance.AddListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
