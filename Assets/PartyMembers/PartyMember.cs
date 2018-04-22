using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public PartyMemberAnimation animation;
    int offset;

	// Use this for initialization
	void Start () {
        offset = (int)transform.localPosition.x;
    }

    public void Attack()
    {
        animation.Attack();

    }

    public void Forward()
    {
        offset += 1;
        animation.MoveToX((int)transform.parent.position.x + offset);
    }

    public void Backward()
    {
        offset -= 1;
        animation.MoveToX((int)transform.parent.position.x + offset);
    }

}
