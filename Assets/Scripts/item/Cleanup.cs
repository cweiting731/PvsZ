using UnityEngine;

public class Cleanup : MonoBehaviour
{
    public float cleanupDelay = 5f; // ����R�����ɶ�

    void Start()
    {
        // �b���w������ɶ���R������
        Destroy(gameObject, cleanupDelay);
    }
}

