using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public enum Element {Sand, Stone, Water, WetSand}
    public enum MatterState {Solid, Liquid, Gas}
    public Element element;

    public int mass;
    public MatterState matterState;

    protected GameManager gm;
    bool waiting = false;

    protected virtual void Awake() {
        gm = GameManager.inst;
        Init();
    }

    protected virtual bool CheckSpace(Vector3 point) {
        return GameManager.inst.CheckSpace(point);
    }

    protected virtual void Update() {
        if (!waiting) {
            waiting = true;
            Invoke("WaitForPhysics", gm.physicsUpdateDelta);
        }
    }

    protected virtual void WaitForPhysics() {
        waiting = false;
        SimulatePhysics();
    }

    protected virtual void SimulatePhysics(){
        InteractWithNeighbors();
        Gravity();
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

    protected virtual void Init(){}

    protected virtual void LoadParticleData(){}
}
