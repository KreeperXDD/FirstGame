using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(150, 150);
    public Building[,] Grid;
    private Building _flyingBuilding;
    private Camera _mainCamera;

    private void Awake()
    {
        Grid = new Building[gridSize.x, gridSize.y];
        _mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }

        _flyingBuilding = Instantiate(buildingPrefab);
    }
    
    private void Update()
    {
        if (_flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray,out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;
                    
                if (x < 0 || x > gridSize.x - _flyingBuilding.size.x) available = false;
                if (y < 0 || y > gridSize.y - _flyingBuilding.size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;
                
                _flyingBuilding.transform.position = new Vector3(x,0,y);
                _flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x,y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.size.y; y++)
            {
                if (Grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }
    
    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.size.y; y++)
            {
                Grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }
        
        
        _flyingBuilding.SetNormal();
        _flyingBuilding = null; 
    }
}
