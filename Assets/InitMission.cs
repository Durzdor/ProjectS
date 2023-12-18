using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMission : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerModel>();
        
        if (player != null)
            Destroy(gameObject);
        
    }
}
