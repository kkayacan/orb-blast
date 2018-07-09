using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed = 2.0f;
    private Vector3 target; //that's for moving 
    
    void Start()
    {
        target = transform.position; //gameobjects position before moving
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if user clicked somewhere on the screen
        {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition); //target (click) position
                target.x = (float)System.Math.Round(target.x);
                if(target.x > 2)
                {
                    target.x = 2;
                }
                else if(target.x < -2)
                {
                    target.x = -2;
                }
                target.y = transform.position.y;
                target.z = transform.position.z; //we dont want to move our gameobject in z axis since its a 2D game.
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //moving its easy dont need explanation

    }
}
