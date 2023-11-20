using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Particle
{
    protected override void Gravity()
    {
        int randVal = gm.r.Next(0, 2);
        if (!CheckSpace(transform.position + new Vector3(0, -1, 0))) {
            transform.position += new Vector3(0, -1, 0);
        } else if (!CheckSpace(transform.position + new Vector3((randVal == 0 ? 1 : -1), 0, 0))) {
            transform.position += new Vector3((randVal == 0 ? 1 : -1), 0, 0);
        } else if (!CheckSpace(transform.position + new Vector3((randVal == 1 ? 1 : -1), 0, 0))){
            transform.position += new Vector3((randVal == 1 ? 1 : -1), 0, 0);
        } 
    }

    protected override void Interact(Particle particle) {
        if (particle != null) {
            if (particle.GetType() == typeof(Water)) {
                gm.SpawnParticle(typeof(Stone), particle.transform.position);
                Destroy(particle);
            }
            if (particle.GetType() == typeof(Sand)) {
                gm.SpawnParticle(typeof(Glass), transform.position);
                Destroy(particle);
            }
            if (particle.GetType() == typeof(GrassSeed) || particle.GetType() == typeof(Grass)) {
                Destroy(particle.gameObject);
            }
            if (particle.GetType() == typeof(Ice)) {
                gm.SpawnParticle(typeof(Water), particle.transform.position);
                Destroy(particle);
            }
        }
    }
}
