using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Particle
{
    protected override void Gravity()
    {
        if (transform.position.y <= 0) {
            return;
        } else if (!CheckSpace(transform.position + new Vector3(0, -1, 0))) {
            transform.position += new Vector3(0, -1, 0);
        } else if (!CheckSpace(transform.position + new Vector3(-1, 0, 0))) {
            transform.position += new Vector3(-1, 0, 0);
        } else if (!CheckSpace(transform.position + new Vector3(1, 0, 0))) {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    protected override void Init() {
        ParticleDB.LoadParticleData(this, Element.Water);
    }
}
