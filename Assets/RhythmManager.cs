using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Notes
{
    Cleric,
    Bard,
    Rogue,
    Fighter,
};

public interface IRhythmListener
{
    void OnBeatEarly();
    bool OnNote(Notes note, List<Notes> fullRhythm);
    void OnBeatRight();
    void OnFailure(List<Notes> failedRhythm, bool tooEarly);
}

public class RhythmManager : MonoBehaviour {
    public float timeSinceBeat = 0;
    public float beatTime = 0.5f;
    public float beatLeadLeeway = 0.4f;
    public float beatLagLeeway = 0.3f;
    private List<Notes> notes = new List<Notes>();
    public Dictionary<KeyCode, Notes> keyMapping = new Dictionary<KeyCode, Notes>();
    private List<IRhythmListener> listeners = new List<IRhythmListener>();
    public AudioClip beat;
    public AudioSource source;
    // Use this for initialization
    void Start () {
        keyMapping.Add(KeyCode.Q, Notes.Cleric);
        keyMapping.Add(KeyCode.W, Notes.Bard);
        keyMapping.Add(KeyCode.E, Notes.Rogue);
        keyMapping.Add(KeyCode.R, Notes.Fighter);
        source.clip = beat;
    }
	
	// Update is called once per frame
	void Update () {
        float currentTime = timeSinceBeat + Time.deltaTime;
        if (timeSinceBeat < beatTime - beatLeadLeeway && currentTime >= beatTime - beatLeadLeeway)
        {
            StartBeat();
        }
        else if (timeSinceBeat < beatTime && currentTime >= beatTime)
        {
            HitBeat();
        }
        else if (timeSinceBeat < beatTime + beatLagLeeway && currentTime >= beatTime + beatLagLeeway)
        {
            FailNote(false);
            source.Stop();
            currentTime = 0;
        }
        timeSinceBeat = currentTime;
		foreach (KeyValuePair<KeyCode, Notes> entry in keyMapping)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                source.Stop();
                // Hit a note!
                if (timeSinceBeat < beatTime - beatLeadLeeway)
                {
                    FailNote(true);
                    timeSinceBeat = 0;
                } else { 
                    ProcessNote(entry.Value);
                    timeSinceBeat = 0;
                }
            }
        }
	}

    void StartBeat()
    {
        source.Play();
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnBeatEarly();
        }
    }

    void HitBeat()
    {
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnBeatRight();
        }
    }

    void ProcessNote(Notes note)
    {
        bool wantClear = false;
        notes.Add(note);
        foreach (IRhythmListener listener in listeners)
        {
            wantClear |= listener.OnNote(note, notes);
        }
        if (wantClear)
        {
            notes.Clear();
        }
    }

    void FailNote(bool tooEarly)
    {
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnFailure(notes, tooEarly);
        }
        notes.Clear();
    }

    public void AddListener(IRhythmListener listener)
    {
        listeners.Add(listener);
    }
}
