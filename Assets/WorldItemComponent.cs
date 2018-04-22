using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemComponent : MonoBehaviour {
    public WorldItem worldItem;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public TItem GetItem<TItem>() where TItem : WorldItem
    {
        return (TItem) worldItem;
    }

    public void Damage(int amount)
    {
        Debug.Log(amount);
        if (GetComponent<AnimalComponent>() != null)
        {
            GetComponent<AnimalComponent>().Damage(amount);
        }
    }
}
