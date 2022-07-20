using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool swiping;

    private void Awake()
    {
        swiping = false;
        
        cam = Camera.main;
        
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        
        trail.enabled = false;
        col.enabled = false;
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }
    
    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();
        if(target)
        {
            //Destroy the target
            target.DestroyTarget();
        }
    }
    
}
