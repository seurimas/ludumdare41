using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRhythmListener : MonoBehaviour, IRhythmListener
{
    public Dictionary<Notes[], Action<List<Notes>>> rhythms = new Dictionary<Notes[], Action<List<Notes>>>();
    public WorldPartyActions worldActions;
    // Use this for initialization
    void Start()
    {
        Notes R = Notes.Fighter;
        Notes E = Notes.Rogue;
        Notes W = Notes.Cleric;
        Notes Q = Notes.Bard;
        rhythms.Add(new Notes[] { R, R, R, Q },
            (rhythm) => worldActions.AdvanceParty()
        );
        rhythms.Add(new Notes[] { Q, R, Q, R },
            (rhythm) => worldActions.RetreatParty()
        );
        rhythms.Add(new Notes[] { E, E, R, E },
            (rhythm) => worldActions.PartyAttack() // Attack
        );
        rhythms.Add(new Notes[] { W, W, R, Q },
            (rhythm) => worldActions.PartyRest() // Defend
        );
        rhythms.Add(new Notes[] { W, W, E, E },
            (rhythm) => worldActions.PartyHarvest() // Harvest
        );
        rhythms.Add(new Notes[] { Q, Q, E, E },
            (rhythm) => worldActions.PartyCraft() // Special 2
        );
        GetComponent<RhythmManager>().AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeatRight(int beatNumber)
    {
    }

    public void OnFailure(List<Notes> failedRhythm, bool tooEarly)
    {
    }

    public bool OnNote(Notes note, List<Notes> fullRhythm)
    {
        foreach (KeyValuePair<Notes[], Action<List<Notes>>> rhythm in rhythms)
        {
            if (fullRhythm.Count != rhythm.Key.Length)
            {
                continue;
            }
            bool isAction = true;
            for (int i = 0;i < fullRhythm.Count;i++)
            {
                if (rhythm.Key[i] != fullRhythm[i])
                {
                    isAction = false;
                }
            }
            if (isAction)
            {
                rhythm.Value(fullRhythm);
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