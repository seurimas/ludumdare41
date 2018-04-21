using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRhythmListener : MonoBehaviour, IRhythmListener
{
    public Dictionary<Notes[], IPartyAction> actions;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeatEarly()
    {
    }

    public void OnBeatRight()
    {
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        if (fullRhythm.Count == 4)
        {
            
        }
        return false;
    }
}