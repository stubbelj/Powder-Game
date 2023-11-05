using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Particle
{
    protected override void Gravity() {
        Particle particle = gm.GetParticle(transform.position + new Vector3(0, -1, 0));
        if (particle != null && particle.matterState != MatterState.Solid) {
            Vector3 temp = particle.transform.position;
            particle.transform.position = transform.position;
            transform.position = temp;
        } else if (!CheckSpace(transform.position + new Vector3(0, -1, 0))) {
            transform.position += new Vector3(0, -1, 0);
        } else if (!CheckSpace(transform.position + new Vector3(-1, -1, 0))) {
            transform.position += new Vector3(-1, -1, 0);
        } else if (!CheckSpace(transform.position + new Vector3(1, -1, 0))) {
            transform.position += new Vector3(1, -1, 0);
        }
    }

    protected override void Interact(Particle particle) {
        if (particle != null) {
            if (particle.GetType() == typeof(Water)) {
                gm.SpawnParticle(typeof(WetSand), transform.position);
                Destroy(particle);
            }
        }
    }
}
