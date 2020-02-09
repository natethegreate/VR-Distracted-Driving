using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class driveController : MonoBehaviour {

    // Use this for initialization
    public float steerAngle;
    public GameObject steeringWheel;

    public WheelCollider frontRight, frontLeft;
    public WheelCollider rearRight, rearLeft;   //wheels
    public Transform fR, fL, rR, rL;    //used for transform modifications
    public Rigidbody car;

    public float maxSteer;
    public float force; //Reallly should be named Torque
    public float steer; //user input between -1 and 1
    public float throttle;
    public float brakes;    //force of each individual brake

    public float tightness = 160f;  //steering tightness. 180 is lowest.
	void Start () {
        brakes = 140;
        force = 550;    //based off Nates car ish in  N/m. 
        maxSteer = 30;  //adjust these values for driving dynamics
        car = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //steer = Input.GetAxis("Horizontal");    //These two will take jostick and keyboard values.
        getSteer();
        throttle = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        steering();
        accel();
        updateWheels();
		
	}
    //Take user input, get some angle and apply it to front wheels
    public void steering()
    {
        steerAngle = maxSteer * steer; //max * user input
        frontRight.steerAngle = steerAngle;
        frontLeft.steerAngle = steerAngle;
    }
    //Emulate RWD vehicle, throttle applies torque to rear wheels
    public void accel()
    {
        /*//smart breaking, but issues if we hit something?? Allows for koyboard and joystick throttling and braking
        if ((car.velocity.z > 0 && throttle < 0) || (car.velocity.z < 0 && throttle > 0))
        {
            //try four wheel brakes
            rearLeft.motorTorque = 0;
            rearRight.motorTorque = 0;
            rearLeft.brakeTorque = Math.Abs(throttle * brakes);
            rearRight.brakeTorque = Math.Abs(throttle * brakes);
            //frontLeft.brakeTorque = Math.Abs(throttle * brakes);
            //frontRight.brakeTorque = Math.Abs(throttle * brakes);
           
        }
        else if (throttle != 0)
        {
            //accel, 2 wheel (rear wheel) drive
            rearLeft.brakeTorque = 0;
            rearRight.brakeTorque = 0;
            frontLeft.brakeTorque = 0;
            frontRight.brakeTorque = 0;
            rearLeft.motorTorque = throttle * force;
            rearRight.motorTorque = throttle * force;
            //frontLeft.motorTorque = throttle * force;     //Enable these to enable AWD. Will be twice as fast unless you adjest force value.
            //frontRight.motorTorque = throttle * force;

            //Could help with backing out of a stuck area
            //if(car.velocity.z < 0 && throttle < 0)
            //{
            //    //reversing is kinda broken so do this
            //    rearLeft.motorTorque = throttle * force;
            //    rearRight.motorTorque = throttle * force;
            //    frontLeft.motorTorque = throttle * force;     //Enable these to enable AWD. Will be twice as fast unless you adjest force value.
            //    frontRight.motorTorque = throttle * force;
            //}
        }
        //else
        //{
        //    //i dont know
        //    rearLeft.motorTorque = throttle * force;
        //    rearRight.motorTorque = throttle * force;
        //    frontLeft.motorTorque = throttle * force;     //Enable these to enable AWD. Will be twice as fast unless you adjest force value.
        //    frontRight.motorTorque = throttle * force;
        //} */

        //VR throttle and brake controlls
        rearLeft.motorTorque = throttle * force;
        rearRight.motorTorque = throttle * force;
        //
        if(car.velocity.z < -1 && throttle >.1f)
        {
            rearLeft.brakeTorque = brakes;
            rearRight.brakeTorque = brakes;
            //reduce power of front brakes to prevent flipping
            frontLeft.brakeTorque = .9f * brakes;
            frontRight.brakeTorque = .9f * brakes;
        }
        if (throttle != 0)
        {
            //relieve brakes
            rearLeft.brakeTorque = 0;
            rearRight.brakeTorque = 0;
            frontLeft.brakeTorque = 0;
            frontRight.brakeTorque = 0;
        }
        //braking is button x for now
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            if (car.velocity.z > 1)
            {
                rearLeft.brakeTorque = brakes;
                rearRight.brakeTorque = brakes;
                //reduce power of front brakes to prevent flipping
                frontLeft.brakeTorque = .8f * brakes;
                frontRight.brakeTorque = .8f * brakes;
            }
            //else, holding x will reverse
            else
            {
                rearLeft.motorTorque = -force;
                rearRight.motorTorque = -force;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            rearLeft.brakeTorque = 0;
            rearRight.brakeTorque = 0;
            frontLeft.brakeTorque = 0;
            frontRight.brakeTorque = 0;
        }
    }
    //function updated wheel transforms, which makes them appear to move
    public void updateWheels()
    {
        updateWheelPose(frontLeft, fL);
        updateWheelPose(frontRight, fR);
        updateWheelPose(rearLeft, rL);
        updateWheelPose(rearRight, rR);
    }
    //This function gets information from collider, applies it to transform
    public void updateWheelPose(WheelCollider collide, Transform trans)
    {
        Vector3 position = trans.position;
        Quaternion q = trans.rotation;
        //got this from tutorial but 'out' is essentially pass by reference
        collide.GetWorldPose(out position, out q);
        trans.position = position;
        trans.rotation = q;
    }
    //function to get steering wheel rotation, as well as contrain the steering wheel
    public void getSteer()
    {
        //define 40 as max steering for now.

        float angle = steeringWheel.transform.localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;    //code to make other rotations negative, 100% stolen code
        if (angle <= 150 || angle >= -150)
            steer = -angle/tightness;    //change the divisor for tighter steering ratio
    }
}
