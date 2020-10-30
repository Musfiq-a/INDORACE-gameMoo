using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobil : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1500f;
    public float maxSteer = 20f;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Rigidbody _rigidbody;
    private Roda[] roda2;

    void Start()
    {
        roda2 = GetComponentsInChildren<Roda>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }
    
    void Update()
    {
        foreach (var roda in roda2)
        {
            roda.SteerAngle = Steer * maxSteer;
            roda.Torque = Throttle * motorTorque;
        }
    }

}
