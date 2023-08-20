using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Transform[] points;
    public Building hangar;


    public void Start()
    {
        StartCoroutine(SpawnHangar());
    }

    IEnumerator SpawnHangar()
    {
        for (int i = 0; i < points.Length; i++)
        {
            yield return new WaitForSeconds(5.0f);
            Building spawn = Instantiate(hangar);
            spawn.transform.position = points[i].position;
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0,UnityEngine.Random.Range(0,360),0));
            spawn.GetComponent<CarSpawn>().IsEnemy = true;
        }
    }
}
