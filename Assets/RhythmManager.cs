using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Notes
{
    Bard,
    Cleric,
    Rogue,
    Fighter,
};

public interface IRhythmListener
{
    bool OnNote(Notes note, List<Notes> fullRhythm);
    void OnBeatRight(int beatNumber);
    void OnFailure(List<Notes> failedRhythm, bool tooEarly);
}

public class RhythmManager : MonoBehaviour {
    public static RhythmManager instance = null;
    public float nextBeat = 0;
    public int currentBeat = 0;
    private int nextPlayedBeat = 0;
    public float beatTime = 0.7f;
    public float beatLeadLeeway = 0.3f;
    public float beatLagLeeway = 0.3f;
    private List<Notes> notes = new List<Notes>();
    public Dictionary<KeyCode, Notes> keyMapping = new Dictionary<KeyCode, Notes>();
    private List<IRhythmListener> listeners = new List<IRhythmListener>();

    public float GetTTB()
    {
        return GetTTB(currentBeat);
    }

    public float GetTTB(int beatIndex)
    {
        int beatsBetween = beatIndex - currentBeat;
        return nextBeat - Time.time + (beatsBetween * beatTime);
    }

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        keyMapping.Add(KeyCode.Q, Notes.Bard);
        keyMapping.Add(KeyCode.W, Notes.Cleric);
        keyMapping.Add(KeyCode.E, Notes.Rogue);
        keyMapping.Add(KeyCode.R, Notes.Fighter);
        nextBeat = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetTTB() < -beatLagLeeway)
        {
            FailNote(false);
        }
        if (GetTTB(nextPlayedBeat) <= 0)
        {
            nextPlayedBeat++;
            HitBeat();
        }
		foreach (KeyValuePair<KeyCode, Notes> entry in keyMapping)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                if (GetTTB() > beatLeadLeeway)
                {
                    FailNote(true);
                } else
                {
                    // Hit a note!
                    ProcessNote(entry.Value);
                }
            }
        }
	}

    void HitBeat()
    {
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnBeatRight(currentBeat);
        }
    }

    void ProcessNote(Notes note)
    {
        EatBeat();
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
        EatBeat();
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnFailure(notes, tooEarly);
        }
        notes.Clear();
    }

    void EatBeat()
    {
        if (GetTTB() < beatTime + beatLeadLeeway)
        {
            nextBeat = nextBeat + beatTime;
            currentBeat++;
        }
    }

    public void AddListener(IRhythmListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(IRhythmListener listener)
    {
        listeners.Remove(listener);
    }
}
