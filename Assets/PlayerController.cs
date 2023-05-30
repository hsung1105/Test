using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController charCon;
    private Animator anim;
    private float baseGravity = -9.8f;
    private bool isJump;
    private bool isGrounded => Physics.BoxCast(transform.position + Vector3.up * 0.4f, new Vector3(0.7f, 0.1f, 0.7f), -transform.up, Quaternion.identity, 0.4f, LayerMask.GetMask("Ground"));
    
    [Header("Player Movement Stats")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseJumpPower;
    [SerializeField] private float gravityScale;
    
    private float jumpPercentage = 100;
    private float speedPercentage = 100;
    
    private float speed => baseSpeed * speedPercentage * 0.01f;
    private float jumpPower => baseJumpPower * jumpPercentage * 0.01f;
    private float gravity => baseGravity * gravityScale;

    private Vector3 moveDir;
    private Vector3 moveVelo;
    

    public AnimationCurve jumpGravityCurve;
    private float gravityTime;


    [SerializeField] private Transform cameraTr;
    [SerializeField] private Transform angleCon;

    private void Awake()
    {
        charCon = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
    }
    

    void Update()
    {
        Move();
    }
    
    
    
    
    void Move()
    {
        SetMoveDir();
        if(isGrounded)
        {
            gravityTime = 0;
            isJump = false;
            moveVelo.y = 0;
            Jump();
        }
        else
        {
            JumpCancel();
            gravityTime += Time.deltaTime;
            moveVelo.y += gravity * Time.deltaTime * jumpGravityCurve.Evaluate(gravityTime);
        }
        charCon.Move(transform.TransformDirection(moveVelo * Time.deltaTime));
    }

    void SetMoveDir()
    {
        angleCon.position = transform.position;
        moveDir = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")).normalized;
        angleCon.LookAt(angleCon.position + cameraTr.TransformDirection(moveDir));
        transform.rotation = Quaternion.Slerp(transform.rotation, angleCon.rotation, .15f);
        moveVelo.z = (Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.z)) *speed;
        Debug.Log(moveDir);
    }
    
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && isGrounded)
        {
            moveVelo.y = jumpPower;
            isJump = true;
            anim.SetTrigger("Jump");
        }
    }

    void JumpCancel()
    {
        if (Input.GetButtonUp("Jump") && moveVelo.y > 0)
        {
            moveVelo.y *= 0.3f;
        }
    }
    
}
