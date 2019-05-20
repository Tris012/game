using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public float health = 5;

    void Update()
    {
        
    }
    
    public void takeDamage(float damage) {
        health -= damage;
        Debug.Log("Damage taken");
        Debug.Log(health);
    }
}
