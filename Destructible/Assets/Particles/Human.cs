using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Particle
{
    private bool isHead = false;
    private bool hasHead = false;
    protected override void Gravity() {
        //growhead
        if ((hasHead == false) && (isHead == false)) {
            Vector3 temp = transform.position + new Vector3(0, 1, 0);
            if (!CheckSpace(temp)) {
                GameObject head = Instantiate(gameObject, temp, Quaternion.identity);
                head.transform.parent = transform; //make head a child of body
                Human HeadScript = head.GetComponent<Human>();
                if (HeadScript != null) {
                    HeadScript.isHead = true;
                }
                hasHead = true;
            }
        }

        if(isHead == false && hasHead == true) { //needs a head to move!!
            Particle particle = gm.GetParticle(transform.position + new Vector3(0, -1, 0));

            //if not falling, move around
            if (particle != null && particle.matterState == MatterState.Solid) { //grounded
                Debug.Log("grounded");
                Vector3 newPosition = Vector3.zero;
                int randVal = Random.Range(0, 2);
                if (randVal == 0) {
                    newPosition = new Vector3(-1, 0, 0);
                }
                else {
                    newPosition = new Vector3(1, 0, 0);
                }
                if (!CheckSpace(transform.position + newPosition)) {
                    transform.position += newPosition;
                }
            }

            else if (particle != null && particle.matterState != MatterState.Solid) {
                Debug.Log("through liquid/gas");
                Vector3 temp = particle.transform.position;
                particle.transform.position = transform.position;
                transform.position = temp;
            } 

            //if falling
            else { //not grounded
                Debug.Log("falling");
                transform.position += new Vector3(0, -1, 0);
            }
            
        }
    }
}
