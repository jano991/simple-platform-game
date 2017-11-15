using UnityEngine;

public class Controlls : MonoBehaviour
{

    public float Speed = 0;
    public float GravityModifier;
    public float GroundedDistanceFlag;
    public float RotationSpeed;
    public float MaxForspeed;
    public float MaxBackspeed;
    public float Acceleration;
    public float Deceleration;
    public CharacterController controller;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private bool IsGrounded()
    {
        bool isGrounded = false;
        var transform = GetComponent<Transform>();
        Vector3 down = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, down, GroundedDistanceFlag))
            isGrounded = true;
        return isGrounded;
    }

    private void Gravity()
    {
        var pos = transform.position;
        float gravityForce = GravityModifier * Time.deltaTime;
        transform.position = new Vector3(pos.x, pos.y - gravityForce, pos.z);
    }

    private void Move()
    {
        var isGrounded = IsGrounded();
        var isMovingForward = GetMovementDirection();

        if (isGrounded == false)
            Gravity();
        if (isMovingForward == 1)
        {
            if (Input.GetAxis("Rotation") > 0)
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
            if (Input.GetAxis("Rotation") < 0)
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime * -1f);
        }
        else if (isMovingForward == -1)
        {
            if (Input.GetAxis("Rotation") < 0)
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
            if (Input.GetAxis("Rotation") > 0)
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime * -1f);


        }



    }
    private int GetMovementDirection()
    {
        int MovingForward = 0;
        if (Input.GetAxis("For-Back") > 0 && (Speed < MaxForspeed))
        {
            Speed = Speed + Acceleration * Time.deltaTime;
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            MovingForward = 1;
        }
        else if (Input.GetAxis("For-Back") < 0 && (Speed > -MaxBackspeed))
        {
            Speed = Speed - Deceleration * Time.deltaTime;
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            MovingForward = -1;
        }
        else
        {
            if (Speed < MaxForspeed + 1 && Speed > 0)
                Speed = Speed - Deceleration * Time.deltaTime;
            if (Speed < 0 && Speed > -MaxBackspeed - 1)
                Speed = Speed + Deceleration * Time.deltaTime;
            if (Speed > -0.3f && Speed < 0.3f)
                Speed = 0;
        }

        return MovingForward;
    }
    private void Accelerate()
    {

    }
}
