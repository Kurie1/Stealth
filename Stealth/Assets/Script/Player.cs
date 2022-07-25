using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 7;
    public float smoothMoveTime = .1f;
    public float turnSpeed = 8;

    private float angle;
    private float smoothInputMagnitude;
    private float smoothMoveVelocity;
    private Rigidbody rigidbody1;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody1 = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed*inputMagnitude);

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;

    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.up * angle));
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + velocity * Time.deltaTime);
    }
}
