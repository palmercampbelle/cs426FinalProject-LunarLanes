using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -15.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public float distance = 5.0f;

    private Transform camTransform;
    private float currentX = 180.0f;
    private float currentY = 30.0f;
    [SerializeField] private float sensitivityX = 1.0f;
    [SerializeField] private float sensitivityY = 1.0f;
    [SerializeField] private bool invertedY = false;

    private void Start()
    {
        camTransform = transform;
        if ( invertedY )
        {
            sensitivityY = -sensitivityY;
        }
    }

    private void Update()
    {
        currentX += Input.GetAxis( "Mouse X" ) * sensitivityX;
        currentY -= Input.GetAxis( "Mouse Y" ) * sensitivityY;

        currentY = Mathf.Clamp( currentY, Y_ANGLE_MIN, Y_ANGLE_MAX );
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt( lookAt.position );
    }
}
