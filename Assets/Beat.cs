using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beat : MonoBehaviour {
    private Image image;
    public int beatIndex;
    public Notes note;
    public bool hit = false;
    public bool failed = false;
    public Sprite noNote;
    public Sprite failedNote;
    public Sprite bardNote;
    public Sprite clericNote;
    public Sprite rogueNote;
    public Sprite fighterNote;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hit)
        {
            switch (note)
            {
                case Notes.Bard:
                    image.sprite = bardNote;
                    break;
                case Notes.Cleric:
                    image.sprite = clericNote;
                    break;
                case Notes.Fighter:
                    image.sprite = fighterNote;
                    break;
                case Notes.Rogue:
                    image.sprite = rogueNote;
                    break;
            }
        } else if (failed)
        {
            image.sprite = failedNote;
        } else
        {
            image.sprite = noNote;
        }
	}

    public void Clear()
    {
        hit = false;
        failed = false;
    }

    public void Hit(Notes note)
    {
        this.note = note;
        hit = true;
        failed = true;
    }

    public void Fail()
    {
        failed = true;
        hit = false;
    }
}
