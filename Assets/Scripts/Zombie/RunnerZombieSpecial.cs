using UnityEngine;

public class RunnerZombieSpecial : MonoBehaviour
{
    public ZombieController controller;
    private bool isChange = false;
    void Start()
    {
        controller = GetComponent<ZombieController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChange && controller.isHalfHealth())
        {
            isChange = true;
            controller.RunnerChangeSpeed();
        }
    }
}
