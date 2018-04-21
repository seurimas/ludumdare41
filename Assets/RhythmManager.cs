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
    private float timeSinceBeat;
    public float beatTime = 0.5f;
    public float beatLeadLeeway = 0.1f;
    public float beatLagLeeway = 0.2f;
    private List<Notes> notes = new List<Notes>();
    public Dictionary<KeyCode, Notes> keyMapping = new Dictionary<KeyCode, Notes>();
    private List<IRhythmListener> listeners = new List<IRhythmListener>();
    // Use this for initialization
    void Start () {
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
        else if (timeSinceBeat < beatTime + beatLagLeeway && currentTime > beatTime + beatLagLeeway)
        {
            FailNote(false);
            timeSinceBeat = 0;
        }
        timeSinceBeat = currentTime;
		foreach (KeyValuePair<KeyCode, Notes> entry in keyMapping)
        {
            if (Input.GetKeyDown(entry.Key))
            {
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
