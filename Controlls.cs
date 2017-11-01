using UnityEngine;

public class Controlls : MonoBehaviour
{

    public float Speed;
    public float GravityModifier;
    public float GroundedDistanceFlag;
    public float RotationSpeed;
    public CharacterController controller;
    public Vector3 prevpos;
    public Vector3 fwd;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void LateUpdate()
    {
        prevpos = transform.position;
        fwd = transform.forward;
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
        Debug.Log("jedzie");
        var isGrounded = IsGrounded();
        var isMovingForward = GetMovementDirection();

        if (isGrounded == false)
            Gravity();
        if (isMovingForward == false)
        {
            if (Input.GetAxis("Rotation") < 0)
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
            if (Input.GetAxis("Rotation") > 0)
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime * -1f);
        } 
        
        if (Input.GetAxis("For-Back") > 0)
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (Input.GetAxis("For-Back") < 0)
            transform.Translate(Vector3.forward * Speed * Time.deltaTime * -1f);
        if (Input.GetAxis("Rotation") > 0)
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        if (Input.GetAxis("Rotation") < 0)
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime * -1f);
    }
    private bool GetMovementDirection()
    {
        Vector3 movement;
        Vector3 newpos;
        bool MovingForward = true;

        newpos = transform.position;
        movement = (newpos - prevpos);

        if (Vector3.Dot(fwd, movement) < 0)
        {
            MovingForward = false;
            return MovingForward;
        }

        return MovingForward;
    }
}
