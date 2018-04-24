using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryRenderer : MonoBehaviour {
    public PartyComponent party;
    public GameObject lootIcon;
    public Sprite garnetSprite;
    public Sprite sapphireSprite;
    public Sprite rubySprite;
    public Sprite juiceSprite;
    public Sprite safronSprite;
    public Sprite honeySprite;
    public Sprite garnetWeaponSprite;
    public Sprite sapphireWeaponSprite;
    public Sprite rubyWeaponSprite;
    public Sprite juiceWeaponSprite;
    public Sprite safronWeaponSprite;
    public Sprite honeyWeaponSprite;
    public Sprite woodSprite;
    public Sprite goldSprite;
	// Use this for initialization
	void Start ()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                GameObject icon = Instantiate(lootIcon, transform);
                icon.GetComponent<RectTransform>().anchoredPosition = new Vector3(x * -40 - 8, y * -40 - 8);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        List<Loot> loot = party.party.loot;
        for (int i = 0;i < loot.Count; i++)
        {
            if (i >= transform.childCount)
            {
                break;
            }
            GameObject icon = transform.GetChild(i).gameObject;
            Image image = icon.GetComponent<Image>();
            image.sprite = GetImage(loot[i]);
            image.color = Color.white;
        }
        for (int i = loot.Count;i < transform.childCount;i++)
        {
            GameObject icon = transform.GetChild(i).gameObject;
            Image image = icon.GetComponent<Image>();
            image.color = Color.clear;
        }
	}

    Sprite GetImage(Loot loot)
    {
        switch (loot)
        {
            case Loot.GARNET:
                return garnetSprite;
            case Loot.GOLD:
                return goldSprite;
            case Loot.SAPPHIRE:
                return sapphireSprite;
            case Loot.RUBY:
                return rubySprite;
            case Loot.HONEY:
                return honeySprite;
            case Loot.SAFRON:
                return safronSprite;
            case Loot.JUICE:
                return juiceSprite;
            case Loot.GARNET_WEAPON:
                return garnetWeaponSprite;
            case Loot.SAPPHIRE_WEAPON:
                return sapphireWeaponSprite;
            case Loot.RUBY_WEAPON:
                return rubyWeaponSprite;
            case Loot.HONEY_WEAPON:
                return honeyWeaponSprite;
            case Loot.SAFRON_WEAPON:
                return safronWeaponSprite;
            case Loot.JUICE_WEAPON:
                return juiceWeaponSprite;
            case Loot.WOOD:
                return woodSprite;
        }
        return null;
    }
}
