using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1f;
    public float rotationSpeed = 500f;
    public float placeItemDistance = 5f;
    public Transform followTarget;
    public InventoryController inventoryController;
    public CenterController centerController;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        // some collision will cause player rotation, this may fix it but currently not useful
        rb.angularVelocity = Vector3.zero;

        // place tool
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1);
            if (Physics.Raycast(ray, out hit) && hit.distance < placeItemDistance)
            {
                // Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Ground")
                {
                    Vector3 placePosition = hit.point;
                    // Debug.Log($"prefab: {inventoryController.GetTool().prefab.name}");
                    if (inventoryController.GetItem() != null)
                    {
                        // placePosition.y = inventoryController.GetItem().prefab.transform.position.y;
                        // placePosition.y = inventoryController.GetItem().prefab.transform.position.y;
                        GameObject prefab = inventoryController.GetItem().prefab;
                        Damageable item = prefab.transform.GetChild(0).GetComponent<Damageable>();
                        if (item != null)
                        {
                            if (item.TakeEnergyCost() <= centerController.energy)
                            {
                                Instantiate(prefab, placePosition, Quaternion.identity);
                                centerController.energy -= item.TakeEnergyCost();
                            }
                        }
                        else Debug.Log("can't not find item");
                    }
                }
            }
        }
    }
}
