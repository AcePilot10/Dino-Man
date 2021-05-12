using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour, IPowerup
{
    public virtual void Activate() 
    {
        Destroy(gameObject);
    }
}