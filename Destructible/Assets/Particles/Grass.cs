using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Particle
{
    public int growth;
    private int maxGrowth = 3;
    private bool bladeGrown;
    protected override void Gravity() {
        for (int randVal = Random.Range(0, 3); (randVal <= 2) && (growth <= maxGrowth) && (bladeGrown == false); randVal += 1) {
            Vector3 newPosition = Vector3.zero;
            if (randVal == 0) {
                newPosition = transform.position + new Vector3(0, 1, 0);
            }
            else if (randVal == 1) {
                newPosition = transform.position + new Vector3(-1, 1, 0);
            }
            else if (randVal == 2) {
                newPosition = transform.position + new Vector3(1, 1, 0);
            }

            if (!CheckSpace(newPosition)) {
                GameObject newBlade = Instantiate(gameObject, newPosition, Quaternion.identity);
                Grass newBladeScript = newBlade.GetComponent<Grass>();

                if (newBladeScript != null) {
                    newBladeScript.growth = growth + 1;
                }
                bladeGrown = true;
            }
        }
    }
}
