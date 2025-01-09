using UnityEngine;

public class Cleanup : MonoBehaviour
{
    public float cleanupDelay = 5f; // 延遲刪除的時間

    void Start()
    {
        // 在指定的延遲時間後刪除物件
        Destroy(gameObject, cleanupDelay);
    }
}

