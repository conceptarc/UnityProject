using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
    public GameObject GameObject;
    public Rigidbody RigidBody;
    public float TotalMass; // kg
    public float EngineThrustForce; // N
    public float BaseAcceleration;
    public float BaseDrag;
    public float MaxVelocity;
    public float TurnRate;
    public float MaxTurnRate;
    //public bool IsInitialized = false;

    /*private void InitCarProperties(GameObject GameObject, float TotalMass, float EngineThrustForce,
        float BaseDrag, float MaxVelocity, float TurnRate, float MaxTurnRate)
    {
        this.GameObject = GameObject;
        this.TotalMass = TotalMass;
        this.EngineThrustForce = EngineThrustForce;
        this.BaseDrag = BaseDrag;
        this.MaxVelocity = MaxVelocity;
        this.TurnRate = TurnRate;
        this.MaxTurnRate = MaxTurnRate;


        IsInitialized = true;
    }*/

    void Start()
    {
        #region arbitrary test values

        #warning put this in a config file later

        GameObject = GameObject.Find("fillerCar");
        TotalMass = 10; // unknown units
        EngineThrustForce = 1000; // newtons?
        BaseDrag = 0.5f;
        MaxVelocity = 50; // m/s
        TurnRate = 1000;
        MaxTurnRate = 5; // deg/s ??
        RigidBody = transform.gameObject.GetComponent<Rigidbody>();
        RigidBody.mass = TotalMass;
        RigidBody.drag = BaseDrag;
        RigidBody.angularDrag = BaseDrag*20;

        #endregion
    }

    // Update is called once per frame
    void Update () {
        // the transform oriention is rotated when importing
        if (Input.GetKey(KeyCode.W)) // forward
        {
            RigidBody.AddForce(-transform.up * EngineThrustForce);
        }
        if (Input.GetKey(KeyCode.S)) // back
        {
            RigidBody.AddForce(transform.up * EngineThrustForce);
        }
        if (Input.GetKey(KeyCode.A)) // turn left
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 torque = -transform.up * TurnRate;
                RigidBody.AddTorque(torque);
            }
            else
            {
                Vector3 torque = -transform.forward * TurnRate;
                RigidBody.AddTorque(torque);
            }
        }
        if (Input.GetKey(KeyCode.D)) // turn right
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 torque = transform.up * TurnRate;
                RigidBody.AddTorque(torque);
            }
            else
            {
                Vector3 torque = transform.forward * TurnRate;
                RigidBody.AddTorque(torque);
            }
        }

    }
}
