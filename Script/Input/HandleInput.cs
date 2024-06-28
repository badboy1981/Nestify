using Collectable;
using SaveAndLoad;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleInput : MonoBehaviour
{
    [SerializeField] MyInputInit _InputControl;
    [SerializeField] SaveScript _SavePlayerData;
    Collector _CollectedName;
    [SerializeField] GameObject _Player;
    private Vector3 Movement;
    private Vector3 Rotation;
    [Range(0, 1000)][SerializeField] private float Speed;
    [Range(0, 1000)][SerializeField] private float RotateRatio;
    [SerializeField] ConstantForce _ConstantForce;

    private Rigidbody rb;

    //private void OnEnable()    
    private void Start()
    {
        Speed = 120;
        RotateRatio = 200;
        rb = GetComponent<Rigidbody>();
        _ConstantForce = GetComponent<ConstantForce>();
        _CollectedName = _Player.GetComponent<Collector>();
        _InputControl.MoveEvent += HandleMoveByForce;
        _InputControl.JumpEvent += HandleJump;
        _InputControl.GoToMap += HandleGoToMap;
        _InputControl.GoToPlay += HandleGoToPlay;
    }
    
    private void HandleGoToMap(float GoMap)
    {
        Debug.Log($"Go To Map {GoMap}");
        SceneManager.LoadScene("Maze_Easy_6-8");
    }
    private void HandleGoToPlay(float GoPlay)
    {
        Debug.Log($"Go To Play {GoPlay}");
        SceneManager.LoadScene("Maze_Easy_6-8");
    }
    private void SaveData()
    {
        _SavePlayerData.SaveTest2(_Player.transform.position, _Player.transform.rotation, _CollectedName.KeyList, _CollectedName.CoinList);        
    }
    private void SaveData2()
    {
        //_SavePlayerData = new SavePlayerData()
        //{
        //    PlayerPosition = _Player.transform.position,
        //    TestFloat = Time.deltaTime
        //};
        //_SavePlayerData = ScriptableObject.CreateInstance<SavePlayerData>();
        //_SavePlayerData.PlayerPosition = _Player.transform.position;
        //Debug.Log($"Player Position: {_SavePlayerData.PlayerPosition}");
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
        _ConstantForce.relativeForce = new Vector3(0, JumpValue * Speed, 0);
    }

    private void FixedUpdate()
    {
        MoveBy_ConstantForce();
        //MoveByOldSystem();
    }
    private void MoveBy_ConstantForce()
    {
        _ConstantForce.relativeForce = Movement;
        //_ConstantForce.relativeTorque = Rotation / 10;
        transform.Rotate(Rotation * Time.deltaTime);
    }
    private void MoveByOldSystem()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float speed = 2;

        Vector3 movement = new(x, 0, z);
        transform.Translate(movement * speed * Time.deltaTime);
    }
    private void MoveBy_Velocity()
    {
        rb.velocity = Movement;
        _ConstantForce.relativeTorque = Rotation;
        //transform.Rotate(Rotation);
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