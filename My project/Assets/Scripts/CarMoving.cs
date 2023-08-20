using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMoving: MonoBehaviour
{
    private NavMeshAgent _agent;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit; 

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
            {
                _agent.destination = hit.point;
            }
        }
    }
}
