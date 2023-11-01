using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Particle
{

    protected override void Gravity() {
        /*Particle particle = gm.GetParticle(transform.position + new Vector3(0, -1, 0));
        if (particle.matterState != solid && particle.mass < mass) {
            Vector3 temp = particle.transform.position;
            particle.transform.position = transform.positon;
            transform.position = temp;
        }*/

        if (transform.position.y <= 0) {
            return;
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
            if (particle.element == Element.Water) {
                gm.SpawnParticle(Element.WetSand, transform.position);
                Destroy(particle);
            }
        }
    }

    protected override void Init() {
        ParticleDB.LoadParticleData(this, Element.Sand);
    }
}
