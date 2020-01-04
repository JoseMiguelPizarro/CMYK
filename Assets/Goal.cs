using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : FloorElement
{
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();

        if (p)
        {
            GameManager.instance.GoalReached();
        }
    }
}
