using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    private SpiralWorldManager world;
    private Tilemap floorTilemap;
    private Tilemap backgroundTilemap;
    public float leftWorldX;
    public float rightWorldX;
    public Sprite floorSprite;
    public Sprite skySprite;
	// Use this for initialization
	void Start () {
        world = GetComponent<SpiralWorldManager>();
        floorTilemap = transform.GetChild(0).GetChild(0).GetComponent<Tilemap>();
        backgroundTilemap = transform.GetChild(1).GetChild(0).GetComponent<Tilemap>();
        generateBeach();
    }

    void generateBeach()
    {
        for (int i = 0; i < 64; i++)
        {
            Tile floorTile = ScriptableObject.CreateInstance<Tile>();
            floorTile.sprite = floorSprite;
            floorTilemap.SetTile(new Vector3Int(i, 0, 0), floorTile);
            Tile skyTile = ScriptableObject.CreateInstance<Tile>();
            skyTile.sprite = skySprite;
            backgroundTilemap.SetTile(new Vector3Int(i, 0, 0), skyTile);
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
