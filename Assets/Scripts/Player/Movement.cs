using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(0, 100)] public float movement_speed = 10.0f;
    [Range(0, 100)] public float horizontal_speed = 100.0f;
    [Range(0, 100)] public float vertical_speed = 100.0f;

    private CharacterController controller_;
    private float yaw_ = 0f;
    private float pitch_ = 0f;
    private Vector3 move_direction_ = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        controller_ = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        yaw_    +=  Input.GetAxisRaw("Mouse X") * horizontal_speed * Time.deltaTime;
        pitch_  += -Input.GetAxisRaw("Mouse Y") * vertical_speed   * Time.deltaTime;
        pitch_   = Mathf.Clamp(pitch_, -80f, 80f);
        transform.localEulerAngles = new Vector3(pitch_, yaw_, 0f);

        move_direction_ = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            move_direction_ += transform.forward * movement_speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            move_direction_ -= transform.right   * movement_speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            move_direction_ -= transform.forward * movement_speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            move_direction_ += transform.right   * movement_speed * Time.deltaTime;

        controller_.Move(move_direction_);
    }
}
