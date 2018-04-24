using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    private Tilemap floorTilemap;
    private Tilemap backgroundTilemap;
    private Tilemap foregroundTilemap;
    public Sprite seaFloorSprite;
    public Sprite beachFloorSprite;
    public Sprite beachSkySprite;
    public Sprite[] beachForegroundSprites;
    public Sprite forestFloorSprite;
    public Sprite forestSkySprite;
    public Sprite[] forestForegroundSprites;
    public Sprite beachForestFloorSprite;
    public Sprite beachForestSkySprite;
    public const int beachSize = 128;
    public const int forestSize = 256;
    // Use this for initialization
    void Start () {
        floorTilemap = transform.Find("FloorGrid").GetChild(0).GetComponent<Tilemap>();
        backgroundTilemap = transform.Find("SkyGrid").GetChild(0).GetComponent<Tilemap>();
        foregroundTilemap = transform.Find("ForegroundGrid").GetChild(0).GetComponent<Tilemap>();
        generateBeach();
    }

    void generateBeach() { 
    
        for (int i = 1; i< 8; i++)
        {
            Tile floorTile = ScriptableObject.CreateInstance<Tile>();
            floorTile.sprite = seaFloorSprite;
            floorTilemap.SetTile(new Vector3Int(-i, 0, 0), floorTile);
            Tile skyTile = ScriptableObject.CreateInstance<Tile>();
            skyTile.sprite = beachSkySprite;
            backgroundTilemap.SetTile(new Vector3Int(-i, 0, 0), skyTile);
        }
        for (int i = 0; i < beachSize / 2; i++)
        {
            Tile floorTile = ScriptableObject.CreateInstance<Tile>();
            floorTile.sprite = beachFloorSprite;
            floorTilemap.SetTile(new Vector3Int(i, 0, 0), floorTile);
            Tile skyTile = ScriptableObject.CreateInstance<Tile>();
            skyTile.sprite = beachSkySprite;
            backgroundTilemap.SetTile(new Vector3Int(i, 0, 0), skyTile);
        }
        for (int i = 64; i < forestSize / 2; i++)
        {
            Tile floorTile = ScriptableObject.CreateInstance<Tile>();
            floorTile.sprite = forestFloorSprite;
            floorTilemap.SetTile(new Vector3Int(i, 0, 0), floorTile);
            Tile skyTile = ScriptableObject.CreateInstance<Tile>();
            skyTile.sprite = forestSkySprite;
            backgroundTilemap.SetTile(new Vector3Int(i, 0, 0), skyTile);
        }
        Tile transitionFloorTile = ScriptableObject.CreateInstance<Tile>();
        transitionFloorTile.sprite = beachForestFloorSprite;
        floorTilemap.SetTile(new Vector3Int(beachSize / 2, 0, 0), transitionFloorTile);
        Tile transitionSkyTile = ScriptableObject.CreateInstance<Tile>();
        transitionSkyTile.sprite = beachForestSkySprite;
        backgroundTilemap.SetTile(new Vector3Int(beachSize / 2, 0, 0), transitionSkyTile);
        for (int i = 0; i < beachSize; i++)
        {
            if (Random.value < 0.3f)
            {
                Tile foregroundTile = ScriptableObject.CreateInstance<Tile>();
                foregroundTile.sprite = beachForegroundSprites[Random.Range(0, beachForegroundSprites.Length)];
                foregroundTilemap.SetTile(new Vector3Int(i, 0, 0), foregroundTile);
            }
        }
        for (int i = 128; i < forestSize; i++)
        {
            if (Random.value < 0.3f)
            {
                Tile foregroundTile = ScriptableObject.CreateInstance<Tile>();
                foregroundTile.sprite = forestForegroundSprites[Random.Range(0, forestForegroundSprites.Length)];
                foregroundTilemap.SetTile(new Vector3Int(i, 0, 0), foregroundTile);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
