using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Particle
{
    protected override void Gravity()
    {
        int randVal = gm.r.Next(0, 2);
        if (!CheckSpace(transform.position + new Vector3(0, -1, 0))) {
            transform.position += new Vector3(0, -1, 0);
        } else if (!CheckSpace(transform.position + new Vector3((randVal == 0 ? 1 : -1), 0, 0))) {
            transform.position += new Vector3((randVal == 0 ? 1 : -1), 0, 0);
        } else if (!CheckSpace(transform.position + new Vector3((randVal == 1 ? 1 : -1), 0, 0))){
            transform.position += new Vector3((randVal == 1 ? 1 : -1), 0, 0);
        } 
    }
}