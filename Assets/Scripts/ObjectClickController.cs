using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickController : MonoBehaviour
{
    private RaycastHit2D _hit;


    void Start()
    {
        
    }


    void Update()
    {
        ClickToDestroy();
    }

    private void ClickToDestroy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      
            if (_hit.collider != null)
            {
                Destroy(_hit.collider.gameObject);
            }  
        }
        
    }
}
