using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAttack : MonoBehaviour
{
    [NonSerialized] public int Health = 100;
    
    public float radius = 70.0f;
    public GameObject bullet;
    private Coroutine _coroutine;

    private void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
       Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

       if (hitColliders.Length == 0 && _coroutine != null)
       {
           StopCoroutine(_coroutine);
           _coroutine = null;
           
           if(gameObject.CompareTag("Enemy"))
           {
               GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
           }
       }
       
       foreach (var element in hitColliders)
       {
           if ((gameObject.CompareTag("Player") && element.gameObject.CompareTag("Enemy")) ||
               (gameObject.CompareTag("Enemy") && element.gameObject.CompareTag("Player")))
           {
               if (gameObject.CompareTag("Enemy"))
               {
                   GetComponent<NavMeshAgent>().SetDestination(element.transform.position);
               }
               
               if(_coroutine == null)
                   _coroutine = StartCoroutine(StartAttack(element));
           }
       }
    }

    IEnumerator StartAttack(Collider enemyPosition)
    {
        GameObject element = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
        element.GetComponent<BulletControl>().Position = enemyPosition.transform.position;
        yield return new WaitForSeconds(1.0f);
        _coroutine = null;
    }
}
