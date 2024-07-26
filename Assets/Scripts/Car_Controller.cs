using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;
using Cinemachine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;
    private bool canMove = false;

    // Settings
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    public PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view != null)
        {
            if (view.IsMine)
            {
                CinemachineVirtualCamera vcam = GameObject.Find("vcam").GetComponent<CinemachineVirtualCamera>();
                Transform followpoint = transform.Find("Followpoint");
                vcam.m_Follow = followpoint;
                vcam.m_LookAt = followpoint;
            }
        }

        frontLeftWheelCollider = transform.Find("Wheels/wheel colliders/flc").GetComponent<WheelCollider>();
        frontRightWheelCollider = transform.Find("Wheels/wheel colliders/frc").GetComponent<WheelCollider>();
        rearLeftWheelCollider = transform.Find("Wheels/wheel colliders/rlc").GetComponent<WheelCollider>();
        rearRightWheelCollider = transform.Find("Wheels/wheel colliders/rrc").GetComponent<WheelCollider>();

        frontLeftWheelTransform = transform.Find("Wheels/Meshes/flw").transform;
        frontRightWheelTransform = transform.Find("Wheels/Meshes/frw").transform;
        rearLeftWheelTransform = transform.Find("Wheels/Meshes/rlw").transform;
        rearRightWheelTransform = transform.Find("Wheels/Meshes/rrw").transform;

        // Start the countdown coroutine
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        // Display countdown (this can be modified to use UI elements for better visuals)
        yield return new WaitForSeconds(1f);
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        Debug.Log("GO!");

        // Enable car movement after countdown
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (view.IsMine && canMove)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
