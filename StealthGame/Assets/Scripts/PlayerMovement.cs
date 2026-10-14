using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;

    private Vector2 inputDirection;
    private Vector3 movement;
    private Quaternion rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void FixedUpdate()
    {
        //  入力情報から移動方向の単位ベクトルを算出
        movement.Set(inputDirection.x, 0, inputDirection.y);
        movement.Normalize();

        //  歩きアニメーション
        bool hasHorizontalInput = !Mathf.Approximately(inputDirection.x, 0f);
        bool hasVerticalInput = !Mathf.Approximately(inputDirection.y, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("IsWalking", isWalking);

        //  移動方向に合わせてキャラクターの向きを算出
        var desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        //  キャラクターの回転・移動
        rigidBody.MoveRotation(rotation);
        rigidBody.MovePosition(rigidBody.position + movement * walkSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //  入力がなくなったら、移動しない
        if (context.canceled)
        {
            inputDirection = Vector2.zero;
            return;
        }

        inputDirection = context.ReadValue<Vector2>();
    }
}
