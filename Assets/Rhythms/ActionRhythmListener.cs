﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRhythmListener : MonoBehaviour, IRhythmListener
{
    public Dictionary<Notes[], Action<List<Notes>>> actions = new Dictionary<Notes[], Action<List<Notes>>>();
    public SpiralWorldManager world;
    // Use this for initialization
    void Start()
    {
        Notes R = Notes.Fighter;
        Notes E = Notes.Rogue;
        Notes W = Notes.Cleric;
        Notes Q = Notes.Bard;
        actions.Add(new Notes[] { R, R, R, Q },
            (rhythm) => world.AdvanceParty()
        );
        actions.Add(new Notes[] { Q, R, Q, R },
            (rhythm) => world.RetreatParty()
        );
        actions.Add(new Notes[] { E, E, R, E },
            (rhythm) => world.PartyAttack() // Attack
        );
        actions.Add(new Notes[] { W, W, R, Q },
            (rhythm) => world.PartyAttack() // Defend
        );
        actions.Add(new Notes[] { W, W, E, E },
            (rhythm) => world.PartyHarvest() // Harvest
        );
        actions.Add(new Notes[] { Q, Q, E, E },
            (rhythm) => world.PartyAttack() // Special 2
        );
        actions.Add(new Notes[] { R, E, W, Q },
            (rhythm) => world.PartyAttack() // Special 3
        );
        actions.Add(new Notes[] { Q, W, E, R },
            (rhythm) => world.PartyAttack() // Special 4
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