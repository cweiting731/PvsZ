using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1f;
    public float rotationSpeed = 500f;
    public Transform followTarget;
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // view rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        followTarget.rotation *= Quaternion.AngleAxis(mouseX, Vector3.up);
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        followTarget.rotation *= Quaternion.AngleAxis(mouseY, Vector3.right);
        var angles = followTarget.localEulerAngles;
        angles.z = 0; // Prevent roll
        var angle = followTarget.localEulerAngles.x;
        if (angle > 180 && angle < 330)
        {
            angles.x = 330;
        }
        else if (angle < 180 && angle > 50)
        {
            angles.x = 50;
        }
        followTarget.localEulerAngles = angles;


        // Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = (followTarget.forward * moveVertical + followTarget.right * moveHorizontal).normalized * moveSpeed *
                           Time.deltaTime;
        movement.y = 0;
        Vector3 newPosition = rb.position + movement;
        rb.MovePosition(newPosition);

        // rotate head
        transform.rotation = Quaternion.Euler(0, followTarget.rotation.eulerAngles.y, 0);
        followTarget.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
}