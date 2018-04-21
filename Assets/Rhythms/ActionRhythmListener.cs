using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRhythmListener : MonoBehaviour, IRhythmListener
{
    public Dictionary<Notes[], Action<string>> actions = new Dictionary<Notes[], Action<string>>();
    // Use this for initialization
    void Start()
    {
        actions.Add(new Notes[] { Notes.Fighter, Notes.Fighter, Notes.Fighter, Notes.Bard }, (str) => Debug.Log(str));
        GetComponent<RhythmManager>().AddListener(this);
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
        Debug.Log(note.ToString());
        foreach (KeyValuePair<Notes[], Action<string>> action in actions)
        {
            if (fullRhythm.Count != action.Key.Length)
            {
                continue;
            }
            bool isAction = true;
            for (int i = 0;i < fullRhythm.Count;i++)
            {
                if (action.Key[i] != fullRhythm[i])
                {
                    isAction = false;
                }
            }
            if (isAction)
            {
                action.Value("Onwards");
                return true;
            }
        }
        return false;
    }
}