using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRhythmListener : MonoBehaviour, IRhythmListener
{
    public Dictionary<Notes[], Action<List<Notes>>> actions = new Dictionary<Notes[], Action<List<Notes>>>();
    public PartyComponent party;
    // Use this for initialization
    void Start()
    {
        actions.Add(new Notes[] { Notes.Fighter, Notes.Fighter, Notes.Fighter, Notes.Bard },
            (rhythm) => party.party.applyAction(new AdvanceAction())
        );
        GetComponent<RhythmManager>().AddListener(this);
    }

    // Update is called once per frame
    void Update()
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
        foreach (KeyValuePair<Notes[], Action<List<Notes>>> action in actions)
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
                action.Value(fullRhythm);
                return true;
            }
        }
        if (fullRhythm.Count == 4)
        {
            return true;
        }
        return false;
    }
}