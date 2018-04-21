using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour {
    private SpiralWorldManager world;
    public float leftWorldX;
    public float rightWorldX;
	// Use this for initialization
	void Start () {
        world = GetComponent<SpiralWorldManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
