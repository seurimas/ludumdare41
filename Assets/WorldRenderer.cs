using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    private Tilemap floorTilemap;
    private Tilemap backgroundTilemap;
    private Tilemap foregroundTilemap;
    public float leftWorldX;
    public float rightWorldX;
    public Sprite floorSprite;
    public Sprite skySprite;
    public Sprite[] foregroundSprites;
	// Use this for initialization
	void Start () {
        floorTilemap = transform.GetChild(0).GetChild(0).GetComponent<Tilemap>();
        backgroundTilemap = transform.GetChild(1).GetChild(0).GetComponent<Tilemap>();
        foregroundTilemap = transform.GetChild(2).GetChild(0).GetComponent<Tilemap>();
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
        for (int i = 0;i < 128; i++)
        {
            if (Random.value < 0.3f)
            {
                Tile foregroundTile = ScriptableObject.CreateInstance<Tile>();
                foregroundTile.sprite = foregroundSprites[Random.Range(0, foregroundSprites.Length)];
                foregroundTilemap.SetTile(new Vector3Int(i, 0, 0), foregroundTile);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
