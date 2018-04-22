using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmRenderer : MonoBehaviour, IRhythmListener {
    Beat[] beats;
    // Use this for initialization
    void Start () {
        beats = new Beat[4];
        foreach (Beat beat in GetComponentsInChildren<Beat>())
        {
            beats[beat.beatIndex] = beat;
        }
        RhythmManager.instance.AddListener(this);
	}
	
	// Update is called once per frame
	void Update () {

    }
    public void OnBeatEarly()
    {
    }

    public void OnBeatRight(int beatNumber)
    {
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
        for (int i = 0; i < 4; i++)
        {
            if (failedRhythm.Count > i)
            {
                beats[i].Hit(failedRhythm[i]);
            }
            else if (i == failedRhythm.Count)
            {
                beats[i].Fail();
            } else
            {
                beats[i].Clear();
            }
        }
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        for (int i = 0; i < 4; i++)
        {
            if (fullRhythm.Count > i)
            {
                beats[i].Hit(fullRhythm[i]);
            } else
            {
                beats[i].Clear();
            }
        }
        return false;
    }
}
