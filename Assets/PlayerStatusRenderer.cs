using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusRenderer : MonoBehaviour {
    public PartyComponent party;
    public Sprite bardSymbol;
    public Sprite clericSymbol;
    public Sprite rogueSymbol;
    public Sprite fighterSymbol;
    public Color high;
    public Color mid;
    public Color low;
    public Color none;
    public GameObject indicatorPrefab;
    private Dictionary<Notes, GameObject> indicators = new Dictionary<Notes, GameObject>();
	// Use this for initialization
	void Start ()
    {
        indicators.Add(Notes.Bard, Instantiate(indicatorPrefab, transform));
        indicators[Notes.Bard].GetComponent<RectTransform>().anchoredPosition = new Vector3(16, 0);
        indicators[Notes.Bard].transform.Find("Symbol").GetComponent<Image>().sprite = bardSymbol;

        indicators.Add(Notes.Cleric, Instantiate(indicatorPrefab, transform));
        indicators[Notes.Cleric].GetComponent<RectTransform>().anchoredPosition = new Vector3(48, 0);
        indicators[Notes.Cleric].transform.Find("Symbol").GetComponent<Image>().sprite = clericSymbol;

        indicators.Add(Notes.Rogue, Instantiate(indicatorPrefab, transform));
        indicators[Notes.Rogue].GetComponent<RectTransform>().anchoredPosition = new Vector3(80, 0);
        indicators[Notes.Rogue].transform.Find("Symbol").GetComponent<Image>().sprite = rogueSymbol;

        indicators.Add(Notes.Fighter, Instantiate(indicatorPrefab, transform));
        indicators[Notes.Fighter].GetComponent<RectTransform>().anchoredPosition = new Vector3(112, 0);
        indicators[Notes.Fighter].transform.Find("Symbol").GetComponent<Image>().sprite = fighterSymbol;

    }


	
	// Update is called once per frame
	void Update () {
        foreach (KeyValuePair<Notes, GameObject> indicatorPair in indicators)
        {
            Notes role = indicatorPair.Key;
            GameObject indicator = indicatorPair.Value;
            PartyMemberStatus status = party.party.GetStatus(role);
            float healthState = ((float)status.health) / status.maxHealth;
            float hungerState = ((float)status.hunger) / status.maxHunger;
            indicator.transform.Find("HealthIndicator").GetComponent<Image>().color = GetColor(healthState);
            indicator.transform.Find("FoodIndicator").GetComponent<Image>().color = GetColor(healthState > 0 ? hungerState : 0);
        }
	}

    Color GetColor(float state)
    {
        if (state > 0.5f)
        {
            return Color.Lerp(mid, high, (state - 0.5f) / 0.5f);
        } else if (state > 0)
        {
            return Color.Lerp(low, mid, (state - 0.5f) / 0.5f);
        } else
        {
            return none;
        }
    }
}
