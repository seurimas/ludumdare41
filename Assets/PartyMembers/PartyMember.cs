using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public PartyMemberAnimation animation;

	// Use this for initialization
	void Start () {
    }

    public void Attack()
    {
        animation.Attack();

    }

    public void Forward()
    {
        animation.Forward();
    }

    public void Backward()
    {
        animation.Backward();
    }

}
