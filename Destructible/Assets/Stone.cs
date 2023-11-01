using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Particle
{
    protected override void Init() {
        ParticleDB.LoadParticleData(this, Element.Stone);
    }
}
