using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    /*private CharacterController characterController;

    Vector3 movementVector;
    [SerializeField] private float speed = 10f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementVector = new Vector3(0f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        characterController.Move(movementVector * speed * Time.fixedDeltaTime);
    }

    public void OnMovementChanged()
    {
        *//*movementVector.x = dir.x;
        movementVector.z = dir.y;
        movement = new Vector3(dir.x, 0f, dir.y);*//*
    }*/
}