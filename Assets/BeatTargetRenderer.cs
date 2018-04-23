using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTargetRenderer : MonoBehaviour, IRhythmListener {
    public float maxScale = 1.4f;
    private GameObject stable;
    private GameObject pulse;
    // Use this for initialization
    void Start () {
        stable = transform.Find("Stable").gameObject;
        pulse = transform.Find("Pulse").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        float progress = RhythmManager.instance.GetTTB() / RhythmManager.instance.beatTime;
        float scale = Mathf.LerpUnclamped(1, maxScale, progress);
        pulse.transform.localScale = new Vector3(scale, scale, 1);
    }
    public void OnBeatRight(int beatNumber)
    {
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        return false;
    }
}
