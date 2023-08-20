using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    public GameObject cube;
    private Camera _camera;
    public LayerMask layer;
    private GameObject _cubeSelection;
    private RaycastHit _hit;
    public List<GameObject> units;
    public LayerMask unitsLayer;

    private void Awake()
    {
        _camera = GetComponent<Camera>(); 
    }

    private void Update()
    {
        
        
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var element in units)
            {
                if (element != null)
                {
                    element.transform.GetChild(0).gameObject.SetActive(false);
                    element.GetComponent<CarMoving>().enabled = false;
                }
            }

            units.Clear();
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out _hit, 1000f, layer))
               _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);

            
        }

        if (_cubeSelection)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = hitDrag.point.x - _hit.point.x;
                float zScale = _hit.point.z - hitDrag.point.z;
                if (xScale < 0.0f && zScale < 0)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else if(xScale<0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(0, 0, 180);
                }
                else if (zScale<0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(180, 0, 0);
                }
                else
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                _cubeSelection.transform.localScale =
                    new Vector3(Math.Abs(xScale), 1,Math.Abs(zScale) );
            }
        }

        if (Input.GetMouseButtonUp(0) && _cubeSelection)
        {
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
                Vector3.up, 
                Quaternion.identity, 
                0.0f, 
                unitsLayer);

            foreach (var element in hits)
            {
                if(element.collider.CompareTag("Enemy")) continue;
                units.Add(element.transform.gameObject);
                element.transform.GetChild(0).gameObject.SetActive(true);
            }
            
            foreach (var element in units)
            {
                element.GetComponent<CarMoving>().enabled = true;
            }
            
            Destroy(_cubeSelection);
        }
    }
}
