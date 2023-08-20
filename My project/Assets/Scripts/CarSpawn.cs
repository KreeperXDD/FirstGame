using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    [NonSerialized] public bool IsEnemy = false;
    public GameObject unit;
    public float time = 7.0f;

    public void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(time);
            GameObject spawn =  Instantiate(unit, transform.GetChild(0).position, Quaternion.identity);

            if (IsEnemy)
            {
                spawn.tag = "Enemy";
            }
        }
    }
}
