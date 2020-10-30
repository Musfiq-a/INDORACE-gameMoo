using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roda : MonoBehaviour
{
    public bool steer;
    public bool invertSteer;
    public bool power;

    public float SteerAngle { get; set; }
    public float Torque { get; set; }

    private WheelCollider rodaCollider;
    private Transform rodaTransform;

    // Start is called before the first frame update
    void Start()
    {
        rodaCollider = GetComponentInChildren <WheelCollider>();
        rodaTransform = GetComponentInChildren <MeshRenderer>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rodaCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        rodaTransform.position = pos;
        rodaTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            rodaCollider.steerAngle = SteerAngle * (invertSteer ? -1 : 1);
        }

        if (power)
        {
            rodaCollider.motorTorque = Torque;
        }
    }
}
