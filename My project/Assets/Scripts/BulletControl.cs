using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [NonSerialized] public Vector3 Position;
    public float speed = 30.0f;
    public int damage = 20;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Position,step);

        if (transform.position == Position)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            
           CarAttack attack = other.GetComponent<CarAttack>();
           attack.Health -= damage;
            
           Transform healthBar =  other.transform.GetChild(0).transform;
           healthBar.localScale = new Vector3(
               healthBar.localScale.x - 0.3f,
               healthBar.localScale.y,
               healthBar.localScale.z);
            
           
           if (attack.Health <= 0)
           {
               Destroy(other.gameObject);
           }
           
        }
    }
}
