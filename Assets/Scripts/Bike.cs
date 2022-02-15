using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : MonoBehaviour
{
    public Transform handle;
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    void start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.5f, -0.2f);
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }
        Transform visualWheel = collider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxMotorTorque * Input.GetAxis("Horizontal");
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.Wheel.steerAngle = steering;
                handle.localEulerAngles = new Vector3(0, steering, 0);
            }
            if (axleInfo.motor)
            {
                axleInfo.Wheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.Wheel);
        }
    }   
    }
[System.Serializable]
public class AxleInfo
{
    public WheelCollider Wheel;
    public bool motor;
    public bool steering;
}
