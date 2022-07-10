using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private SliderJoint2D slider;
    private JointMotor2D motor;

    void Start()
    {
        motor = slider.motor;
        motor.motorSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "CloudTrigger")
        {
            //Debug.Log("Triggered");
            motor.motorSpeed = -motor.motorSpeed;
            slider.motor = motor;
        }
    }

}
