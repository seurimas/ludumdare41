using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlantResource
{
    HONEY,
    SAFRON,
    JUICE,
    RUBY,
    SAPPHIRE,
    GARNET,
    TREE,
};

public class Plant : WorldItem
{
    public bool harvested = false;
    public PlantResource resource;
    public override int GetFlags()
    {
        return WorldItem.HARVESTABLE;
    }
}

public class PlantComponent : MonoBehaviour {
    private Plant plant;
    private SpriteRenderer spriteRenderer;
    public Sprite honeySprite;
    public Sprite safronSprite;
    public Sprite juiceSprite;
    public Sprite emptySprite;
    public Sprite treeSprite;
    public Sprite emptyTreeSprite;
    public Sprite rubySprite;
    public Sprite sapphireSprite;
    public Sprite garnetSprite;
    public Sprite emptyForestSprite;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        plant = GetComponent<WorldItemComponent>().GetItem<Plant>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(plant.NumberLinePosition, 0, 0);
		if (plant.harvested)
        {
            switch (plant.resource)
            {
                case PlantResource.HONEY:
                case PlantResource.JUICE:
                case PlantResource.SAFRON:
                    spriteRenderer.sprite = emptySprite;
                    break;
                case PlantResource.RUBY:
                case PlantResource.SAPPHIRE:
                case PlantResource.GARNET:
                    spriteRenderer.sprite = emptyForestSprite;
                    break;
                case PlantResource.TREE:
                    spriteRenderer.sprite = emptyTreeSprite;
                    break;
            }
        } else
        {
            switch (plant.resource)
            {
                case PlantResource.HONEY:
                    spriteRenderer.sprite = honeySprite;
                    break;
                case PlantResource.JUICE:
                    spriteRenderer.sprite = juiceSprite;
                    break;
                case PlantResource.SAFRON:
                    spriteRenderer.sprite = safronSprite;
                    break;
                case PlantResource.RUBY:
                    spriteRenderer.sprite = rubySprite;
                    break;
                case PlantResource.SAPPHIRE:
                    spriteRenderer.sprite = sapphireSprite;
                    break;
                case PlantResource.GARNET:
                    spriteRenderer.sprite = garnetSprite;
                    break;
                case PlantResource.TREE:
                    spriteRenderer.sprite = treeSprite;
                    break;
            }
        }
	}
}
