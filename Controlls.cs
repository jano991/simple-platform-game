using UnityEngine;

public class Controlls : MonoBehaviour
{

    public float Speed;
    public float GravityModifier;
    public float GroundedDistanceFlag;
    public float RotationSpeed;
    public CharacterController controller;

    void Start()
    {

    }

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

    public void Move()
    {
        var isGrounded = IsGrounded();

        if (isGrounded == false)
            Gravity();

        if (Input.GetAxis("For-Back") > 0)
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (Input.GetAxis("For-Back") < 0)
            transform.Translate(Vector3.forward * Speed * Time.deltaTime * -1f);
        if (Input.GetAxis("Rotation") > 0)
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        if (Input.GetAxis("Rotation") < 0)
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime * -1f);
    }
}
