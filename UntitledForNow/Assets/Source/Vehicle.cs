using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
    public GameObject GameObject;
    public float TotalMass; // kg
    public float EngineThrustForce; // N
    public float BaseAcceleration;
    public float BaseDrag;
    public float MaxVelocity;
    public float TurnRate;
    public float MaxTurnRate;
    public bool IsInitialized = false;

    private void InitCarProperties(GameObject GameObject, float TotalMass, float EngineThrustForce,
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
    }

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsInitialized) return;
	}
}
