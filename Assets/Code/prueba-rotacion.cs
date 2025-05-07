using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float angle = 45;
    private float x;
    private float z;
    private bool rotateX;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vibrate();
    }

    void Vibrate()
    {
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle * Time.deltaTime);
        transform.localRotation = rotation;
    }
    void Rotate()
    {
        transform.Rotate(0, 0, angle);
    }

    void MixedMovement()
    {
        if (rotateX == true)
        {
            x += Time.deltaTime*angle;
            if (x > 360.0f)
            {
                x = 0.0f;
                rotateX = false;
            }
            else
            {
                z += Time.deltaTime * angle;
                if (z > 360.0f)
                {
                    z = 0.0f;
                    rotateX = true;
                }
            }
            transform.localRotation = Quaternion.Euler(x, 0, z);
        }
    }

}