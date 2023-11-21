using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public enum MatterState {Solid, Liquid, Gas}

    public int mass;
    public MatterState matterState;

    protected GameManager gm;
    bool waiting = false;

    protected virtual void Awake() {
        LoadParticleData();
    }

    protected virtual void Start() {
        gm = GameManager.inst;
    }

    protected virtual bool CheckSpace(Vector3 point) {
        return GameManager.inst.CheckSpace(point);
    }

    protected virtual void FixedUpdate() {
        if (!waiting) {
            waiting = true;
            StartCoroutine(SimulatePhysics());
        }
    }

    protected virtual IEnumerator SimulatePhysics(){
        float updateVariance = 0;
        if (this.matterState == MatterState.Liquid) {
            updateVariance = 0.1f;
        } else if (this.matterState == MatterState.Gas) {
            updateVariance = 0.3f;
        }

        yield return new WaitForSeconds(gm.physicsUpdateDelta + (gm.r.Next(0, 2) == 0 ? 1 : -1) * gm.physicsUpdateDelta * updateVariance);

        InteractWithNeighbors();

        Gravity();

        waiting = false;
    }

    protected virtual void Gravity(){}

    protected virtual Particle[] GetNeighbors() {
        return new Particle[4]{
            gm.GetParticle(new Vector3(transform.position.x + 1, transform.position.y, 0)),
            gm.GetParticle(new Vector3(transform.position.x - 1, transform.position.y, 0)),
            gm.GetParticle(new Vector3(transform.position.x, transform.position.y + 1, 0)),
            gm.GetParticle(new Vector3(transform.position.x, transform.position.y - 1, 0))
        };
    }

    protected virtual void InteractWithNeighbors() {
        foreach (Particle particle in GetNeighbors()) {
            Interact(particle);
        }
    }

    protected virtual void Interact(Particle particle){}

    public class ParticleData {
        public int mass;
        public Particle.MatterState matterState;

        public ParticleData (int newMass = 10, Particle.MatterState newMatterState = Particle.MatterState.Solid) {
            mass = newMass;
            matterState = newMatterState;
        }
    }

    static Dictionary<System.Type, ParticleData> particleData = new Dictionary<System.Type, ParticleData>() {
        {typeof(Sand), new ParticleData(
            newMass : 50,
            newMatterState : MatterState.Solid
        )},
        {typeof(Stone), new ParticleData(
            newMass : 200,
            newMatterState : MatterState.Solid
        )},
        {typeof(Water), new ParticleData(
            newMass : 100,
            newMatterState : MatterState.Liquid
        )},
        {typeof(WetSand), new ParticleData(
            newMass : 150,
            newMatterState : MatterState.Solid
        )},
        {typeof(Lava), new ParticleData(
            newMass : 100,
            newMatterState : MatterState.Liquid
        )},
        {typeof(Smoke), new ParticleData(
            newMass : 10,
            newMatterState : MatterState.Gas
        )},
        {typeof(Glass), new ParticleData(
            newMass : 50,
            newMatterState : MatterState.Solid
        )},
        {typeof(Virus), new ParticleData(
            newMass : 100,
            newMatterState : MatterState.Solid
        )},
        {typeof(Antidote), new ParticleData(
            newMass : 100,
            newMatterState : MatterState.Solid
        )}
        {typeof(Grass), new ParticleData(
            newMass : 0,
            newMatterState : MatterState.Solid
        )},
        {typeof(GrassSeed), new ParticleData(
            newMass : 0,
            newMatterState : MatterState.Solid
        )},
        {typeof(Ice), new ParticleData(
            newMass : 50,
            newMatterState : MatterState.Solid
        )},
        {typeof(Human), new ParticleData(
            newMass : 50,
            newMatterState : MatterState.Solid
        )},
    };

    public void LoadParticleData() {
        mass = particleData[this.GetType()].mass;
        matterState = particleData[this.GetType()].matterState;
    }
}
