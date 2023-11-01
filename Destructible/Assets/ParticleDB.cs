using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDB
{
    public class ParticleData {
        public int mass;
        public Particle.MatterState matterState;

        public ParticleData (int newMass = 10, Particle.MatterState newMatterState = Particle.MatterState.Solid) {
            mass = newMass;
            matterState = newMatterState;
        }
    }

    static Dictionary<Particle.Element, ParticleData> particleData = new Dictionary<Particle.Element, ParticleData>() {
        {Particle.Element.Sand, new ParticleData(
            newMass : 50,
            newMatterState : Particle.MatterState.Solid
        )},
        {Particle.Element.Stone, new ParticleData(
            newMass : 200,
            newMatterState : Particle.MatterState.Solid
        )},
        {Particle.Element.Water, new ParticleData(
            newMass : 100,
            newMatterState : Particle.MatterState.Solid
        )},
        {Particle.Element.WetSand, new ParticleData(
            newMass : 150,
            newMatterState : Particle.MatterState.Solid
        )},
    };

    public static void LoadParticleData(Particle particle, Particle.Element element) {
        particle.element = element;
        particle.mass = particleData[element].mass;
        particle.matterState = particleData[element].matterState;
    }
}
