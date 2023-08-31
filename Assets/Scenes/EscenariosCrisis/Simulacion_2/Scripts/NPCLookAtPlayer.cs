using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        Vector3 targetDirection = player.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = targetRotation;
    }
}