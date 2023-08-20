using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;
    public Renderer mainRenderer;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            mainRenderer.material.color = Color.green;
        }
        else
        {
            mainRenderer.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        mainRenderer.material.color = Color.white;
    }
    
    
    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Gizmos.color = new Color(1.0f,0.0f,0.88f,0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x,0,y) , new Vector3(1,.1f,1));
            }
        }
    }
    
}
