using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public Transform followTarget; // Assign the FollowTarget GameObject in the Inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Based Rotation

        // Rotate the Follow Target transform based on the input
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        followTarget.rotation *= Quaternion.AngleAxis(mouseX, Vector3.up);

        #endregion

        #region Vertical Rotation

        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        followTarget.rotation *= Quaternion.AngleAxis(mouseY, Vector3.right);

        // Clamp the Up/Down rotation
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

        #endregion

        #region Player Movement

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (followTarget.forward * moveVertical + followTarget.right * moveHorizontal).normalized * moveSpeed *
                           Time.deltaTime;
        Vector3 newPosition = rb.position + movement;
        rb.MovePosition(newPosition);

        #endregion
    }
}
