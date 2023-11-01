using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetSand : Particle
{

    protected override void Gravity() {
        if (transform.position.y <= 0) {
            return;
        } else if (!CheckSpace(transform.position + new Vector3(0, -1, 0))) {
            transform.position += new Vector3(0, -1, 0);
        }
    }

    protected override void Interact(Particle particle) {
    }

    protected override void Init() {
        ParticleDB.LoadParticleData(this, Element.WetSand);
    }
}
