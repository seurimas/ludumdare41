using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWorldManager : MonoBehaviour {
    public readonly SpiralWorld world = new SpiralWorld();
    public PartyComponent party;
	// Use this for initialization
	void Start () {
        world.AddItem(party.party);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
