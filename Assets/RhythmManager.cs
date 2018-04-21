﻿using System.Collections;
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
    void OnBeatEarly();
    bool OnNote(Notes note, List<Notes> fullRhythm);
    void OnBeatRight();
    void OnFailure(List<Notes> failedRhythm, bool tooEarly);
}

public class RhythmManager : MonoBehaviour {
    public static RhythmManager instance = null;
    public float nextBeat = 0;
    public int currentBeat = 0;
    public float beatTime = 0.7f;
    public float beatLeadLeeway = 0.3f;
    public float beatLagLeeway = 0.3f;
    private bool beatStarted = false;
    private bool beatPerfected = false;
    private List<Notes> notes = new List<Notes>();
    public Dictionary<KeyCode, Notes> keyMapping = new Dictionary<KeyCode, Notes>();
    private List<IRhythmListener> listeners = new List<IRhythmListener>();
    public AudioClip beat;
    public AudioSource source;

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
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start ()
    {
        keyMapping.Add(KeyCode.Q, Notes.Bard);
        keyMapping.Add(KeyCode.W, Notes.Cleric);
        keyMapping.Add(KeyCode.E, Notes.Rogue);
        keyMapping.Add(KeyCode.R, Notes.Fighter);
        source.clip = beat;
        nextBeat = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(string.Format("{0} {1} {2} {3} {4}", currentBeat, nextBeat, beatStarted, beatPerfected, Time.time));
        if (Time.time > nextBeat - beatLagLeeway && !beatStarted)
        {
            StartBeat();
        }
        if (Time.time > nextBeat && !beatPerfected)
        {
            HitBeat();
        }
        if (Time.time > nextBeat + beatLagLeeway)
        {
            FailNote(false);
            source.Stop();
        }
		foreach (KeyValuePair<KeyCode, Notes> entry in keyMapping)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                source.Stop();
                // Hit a note!
                if (!beatStarted)
                {
                    FailNote(true);
                } else { 
                    ProcessNote(entry.Value);
                }
            }
        }
	}

    void StartBeat()
    {
        beatStarted = true;
        source.Play();
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnBeatEarly();
        }
    }

    void HitBeat()
    {
        beatPerfected = true;
        foreach (IRhythmListener listener in listeners)
        {
            listener.OnBeatRight();
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
        if (beatStarted)
        {
            nextBeat = nextBeat + beatTime;
            currentBeat++;
        }
        beatStarted = false;
        beatPerfected = false;
    }

    public void AddListener(IRhythmListener listener)
    {
        listeners.Add(listener);
    }
}
