using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    //allows for rotation speed to be finagled in the inspector so that i can see the speed of the object as i set it 
    public float RotationSpeed = 50f;

    //allows for object rotation 
    private bool isRotating = true;

    void Update()
    {
        if (isRotating)
        {
            //once it recognizes rotation is true, rotates object on the Y axis with the applied rotation speed
            //scaled by time.deltatime so that there is no frame jittering using Update function 
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }

    }
}
