using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInput : MonoBehaviour
{
    [SerializeField] MyInputInit _InputControl;

    private Vector3 Movement;
    private Vector3 Rotation;
    private float Jump;
    [Range(0, 1000)][SerializeField] private float Speed;
    [Range(0, 1000)][SerializeField] private float RotateRatio;
    [SerializeField] ConstantForce _ConstantForce;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _ConstantForce = GetComponent<ConstantForce>();
        _InputControl.MoveEvent += HandleMoveByForce;
        _InputControl.JumpEvent += HandleJump;
    }

    private void HandleMoveByVelosity(Vector2 MoveValue)
    {
        //Movement = new Vector3(0, 0, MoveValue.y * Speed);
        //Rotation = new Vector3(0, MoveValue.x * RotateRatio, 0);
        Movement = -1 * MoveValue.y * Speed * transform.forward;
        Rotation = MoveValue.x * RotateRatio * transform.up;
    }
    private void HandleMoveByForce(Vector2 MoveValue)
    {
        //Movement = MoveValue;
        Movement = -1 * Speed * new Vector3(0, 0, MoveValue.y);
        Rotation = new Vector3(0, MoveValue.x * RotateRatio, 0);
    }
    private void HandleJump(float JumpValue)
    {
        Jump = JumpValue;
        _ConstantForce.relativeForce = new Vector3(0, JumpValue * Speed, 0);
    }
    private void Start()
    {
        Speed = 120;
        RotateRatio = 100;
    }
    private void FixedUpdate()
    {
        MoveBy_ConstantForce();
    }
    private void MoveBy_Velocity()
    {
        rb.velocity = Movement;
        _ConstantForce.relativeTorque = Rotation;
        //transform.Rotate(Rotation);
    }
    private void MoveBy_RiggidBodyForce()
    {

    }
    private void MoveBy_ConstantForce()
    {
        _ConstantForce.relativeForce = Movement;
        //_ConstantForce.relativeTorque = Rotation / 10;
        transform.Rotate(Rotation * Time.deltaTime);
    }
    private void MoveByRigidbody3D()
    {
        transform.Rotate(RotateRatio * Movement.x * Speed * Time.deltaTime * Vector3.up);
        Debug.Log($"Here: ");
    }
    private void MoveByRigidbody()
    {
        //_ConstantForce.force = Movement;
        //_ConstantForce.torque= Movement;
        //rb.AddForce(-1 * Speed * new Vector3(Movement.x, 0, Movement.y));
        //rb.velocity = new Vector3(Movement.x, 0, Movement.y) * -1 * Speed;
        //rb.AddForce(Movement * Speed);
        //rb.AddTorque(Movement * RotateRatio);
        //rb.MovePosition(-1 * Speed * Time.deltaTime * new Vector3(Movement.x, 0, Movement.y));
        //rb.MovePosition((Vector2)transform.position + (Movement * Speed * Time.deltaTime));
        rb.MovePosition(new Vector3(0, 0, Movement.y));
    }

    private void ControlByInputSysytem()
    {
        transform.Translate(Movement.y * Speed * Time.deltaTime * Vector3.back);
        transform.Rotate(RotateRatio * Movement.x * Speed * Time.deltaTime * Vector3.up);
    }
}