using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Rigidbody rb;
    private Vector2 currentDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => InputManager.OnMove += MovePlayer;
    private void OnDisable() => InputManager.OnMove -= MovePlayer;
    private void MovePlayer(Vector2 direction)
    {
       currentDirection = direction;
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(currentDirection.x, 0f, currentDirection.y) * speed; //Направление движения
        rb.linearVelocity = movement;
    }
}
