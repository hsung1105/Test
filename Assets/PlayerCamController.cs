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
    private Vector3 refSpeedVec = Vector3.zero;
    

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

    public static void SetFollwoTarget(Transform followTartget)
    {
        playerTr = followTartget;
    }

    void Positioning()
    {
        movePos = playerTr.position + Vector3.up * 0.8f;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref refSpeedVec, 1f * Time.smoothDeltaTime);
    }

    void Rotate()
    {
        playerCamCart.m_Speed = -Input.GetAxis("Mouse Y") * mouseSensitiveY;
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitiveX, 0);
    }
    
    
}
