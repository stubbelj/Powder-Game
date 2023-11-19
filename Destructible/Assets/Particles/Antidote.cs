using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antidote : Particle
{
    protected override void Interact(Particle particle)
    {
        if (particle != null && particle.GetType() == typeof(Virus))
        {
            Destroy(particle.gameObject);
            Destroy(gameObject);
        }
    }
}
