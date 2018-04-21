using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRenderer : MonoBehaviour, IRhythmListener {
    public float spaceBetween = 128;
    public Sprite target;
    public GameObject beatPrefab;
    private int latestBeat = 0;
    public void OnBeatEarly()
    {
    }

    public void OnBeatRight()
    {
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
        Beat bestBeat = GetActiveBeat();
        if (bestBeat != null)
        {
            bestBeat.Fail();
        }
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        Beat bestBeat = GetActiveBeat();
        if (bestBeat != null)
        {
            bestBeat.Hit(note);
        }
        return false;
    }

    private Beat GetActiveBeat()
    {
        return GetBeat(RhythmManager.instance.currentBeat);
    }

    private Beat GetBeat(int beatIndex)
    {
        Beat[] beats = GetComponentsInChildren<Beat>();
        foreach (Beat beat in beats)
        {
            if (beat.beatIndex == beatIndex)
            {
                return beat;
            }
        }
        return null;
    }

    // Use this for initialization
    void Start () {
        RhythmManager.instance.AddListener(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = RhythmManager.instance.currentBeat - 10;i < RhythmManager.instance.currentBeat + 8; i++)
        {
            Beat beat = GetBeat(i);
            if (beat == null && i >= RhythmManager.instance.currentBeat)
            {
                GameObject beatObj = Instantiate(beatPrefab, transform);
                beat = beatObj.GetComponent<Beat>();
                beat.beatIndex = i;
            }
            if (beat)
            {
                beat.transform.transform.localPosition = new Vector3(RhythmManager.instance.GetTTB(i) * spaceBetween, 0);
            }
        }
	}
}
