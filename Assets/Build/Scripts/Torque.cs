using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque : MonoBehaviour
{

    [SerializeField] public Vector3 torque;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(torque * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        enabled = false;
    }
}
