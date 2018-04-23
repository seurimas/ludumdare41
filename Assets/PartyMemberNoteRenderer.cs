using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberNoteRenderer : MonoBehaviour {

    public PartyComponent party;
    public Camera camera;
    public Sprite[] symbols;
    public GameObject[] noteRenderers;
    public PartyMember[] pms;
    RectTransform rt;

    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
        Initialize();
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("RT: " + rt.transform.position);
        //Debug.Log("Number component: " + party.party.NumberLinePosition);
        //transform.position = new Vector3(transform.position.x + party.party.NumberLinePosition, transform.position.y, transform.position.z);
        
	}

    void Initialize()
    {
        foreach (PartyMember pm in pms)
        {
            pm.NoteProcessed += OnPartyMemberNoteProcessed;
        }

        MakeTransparent(noteRenderers);
    }

    void OnPartyMemberNoteProcessed(object sender, PartyMemberEventArgs e)
    {
        Image image;
        Debug.Log("Hello");
        Debug.Log(e.role);

        MakeTransparent(noteRenderers);

        switch (e.role)
        {


            case Notes.Bard:
                image = noteRenderers[0].GetComponent<Image>();
                image.color = new Color(255, 255, 255, 1);
                image.sprite = symbols[0];
                break;
            case Notes.Cleric:
                image = noteRenderers[1].GetComponent<Image>();
                image.color = new Color(255, 255, 255, 1);
                image.sprite = symbols[1];
                break;
            case Notes.Rogue:
                image = noteRenderers[2].GetComponent<Image>();
                image.color = new Color(255, 255, 255, 1);
                image.sprite = symbols[2];
                break;
            case Notes.Fighter:
                image = noteRenderers[3].GetComponent<Image>();
                image.color = new Color(255, 255, 255, 1);
                image.sprite = symbols[3];
                break;
        }
    }

    void MakeTransparent(GameObject[] noteRenderers)
    {
        Image image;
        foreach (GameObject go in noteRenderers)
        {
            image = go.GetComponent<Image>();
            image.color = new Color(255, 255, 255, 0);
        }
    }
}
