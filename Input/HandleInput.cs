using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInput : MonoBehaviour
{
    [SerializeField] MyInputInit _InputControl;

    private float MoveStart;
    private float MoveCanceled;
    private float H_Input;
    private float V_Input;
    [Range(0, 10)]
    [SerializeField] private float Speed;
    private void HandleMove(Vector2 getInput)
    {
        MoveStart = getInput.y;
    }
    private void Start()
    {
        Speed = 2;
    }

    private void FixedUpdate()
    {
        ControlByUnityInputSystem();
    }
    private void handleMove(Vector2 MoveInput)
    {

    }

    private void ControlByUnityInputSystem()
    {
        H_Input = Input.GetAxis("Horizontal");
        V_Input = Input.GetAxis("Vertical");

        transform.Translate(Vector3.back * Time.deltaTime * V_Input * Speed);
        //transform.Translate(Vector3.left * Time.deltaTime * H_Input * Speed);
        transform.Rotate(Vector3.up * Time.deltaTime * H_Input * Speed * 50);
    }
}