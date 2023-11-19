using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Particle
{
    protected override void Gravity()
    {
        int randVal = gm.r.Next(0, 2);
        if (!CheckSpace(transform.position + new Vector3(0, -1, 0)))
        {
            transform.position += new Vector3(0, -1, 0);
        }
        else if (!CheckSpace(transform.position + new Vector3((randVal == 0 ? 1 : -1), 0, 0)))
        {
            transform.position += new Vector3((randVal == 0 ? 1 : -1), 0, 0);
        }
        else if (!CheckSpace(transform.position + new Vector3((randVal == 1 ? 1 : -1), 0, 0)))
        {
            transform.position += new Vector3((randVal == 1 ? 1 : -1), 0, 0);
        }
    }

    protected override void Interact(Particle particle)
    {
        if (particle != null && particle.GetType() != typeof(Virus))
        {
            print("HIII");
            Destroy(particle.gameObject);
        }
    }
}
