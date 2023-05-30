using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerCamController : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart playerCamCart;
    [SerializeField] private float mouseSensitiveY;
    [SerializeField] private float mouseSensitiveX;

    [SerializeField] private Transform playerTr;


    private Vector3 movePos;

    private float mouseInputY;
    private float mouseInputX;



    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void LateUpdate()
    {
        Positioning();
    }


    private void Update()
    {
        Rotate();
    }

    void Positioning()
    {
        movePos = playerTr.position + Vector3.up * 0.5f;
        if (Input.GetAxis("Horizontal") > 0)
        {
            movePos += transform.TransformDirection(new Vector3(1, 0, 0.7f));
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            movePos += transform.TransformDirection(new Vector3(-1, 0, 0.7f));
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            movePos += transform.TransformDirection(new Vector3(0, 0, -1.4f));
        }
        transform.position = Vector3.MoveTowards(transform.position, movePos, 27.5f * Time.smoothDeltaTime);
    }

    void Rotate()
    {
        playerCamCart.m_Speed = -Input.GetAxis("Mouse Y") * mouseSensitiveY;
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitiveX, 0);
    }
    
    
}
