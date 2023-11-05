using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : Particle
{
    protected override void Gravity()
    {
        int randVal = gm.r.Next(0, 100);
        if (!CheckSpace(transform.position + new Vector3((randVal < 20 ? 1 : -1), 1, 0))) {
            transform.position += new Vector3((randVal == 0 ? 1 : -1), 1, 0);
        } else if (!CheckSpace(transform.position + new Vector3((randVal < 40 ? 1 : -1), 1, 0))){
            transform.position += new Vector3((randVal == 1 ? 1 : -1), 1, 0);
        } else if (!CheckSpace(transform.position + new Vector3(0, 1, 0))){
            transform.position += new Vector3(0, 1, 0);
        } 
    }

    protected override void Interact(Particle particle) {
        if (particle != null) {
            if (particle.GetType() == typeof(Water)) {
                gm.SpawnParticle(typeof(Stone), transform.position);
                if (!CheckSpace(transform.position + new Vector3(0, 1, 0))) {
                    gm.SpawnParticle(typeof(Smoke), transform.position + new Vector3(0, 1, 0));
                }
                Destroy(particle);
            }
        }
    }
}
