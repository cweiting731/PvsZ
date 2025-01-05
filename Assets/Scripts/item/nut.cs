using UnityEngine;
using UnityEngine.UI;

public class Nut : MonoBehaviour, Damageable
{
    public Transform nutWall;
    public CapsuleCollider capsuleCollider;
    public float maxHealth = 100f; // ���G�𪺪�l��q
    public int EnegyCost = 20;
    private float currentHealth; // ���e��q
    public Slider healthBar;
    public Vector3 offset;
    public Canvas healthBarCanvas;

    void Start()
    {
        healthBarCanvas.transform.localPosition = new Vector3(0.01f, 0.05f, 0);
        // ��l�Ʀ�q
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        // 自動指定主攝影機
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Canvas"))
            {
                Canvas canvas = child.GetComponent<Canvas>();
                RectTransform rt = child.GetComponent<RectTransform>();
                if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
                {
                    canvas.worldCamera = Camera.main;
                    rt.localPosition = new Vector3(0f, 0.25f, 0f);
                }
                break;
            }
        }

        // ��l�]�m�G�T�� Kinematic�A������۵M���a

    }
    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        if (transform.position.y >= 0.2)
        {
            transform.position += Vector3.down * Time.deltaTime * 9.8f; // �������O
        }
    }

    // �����G���������ɽեΦ���k
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        //Debug.Log("���G���������A�Ѿl��q�G" + currentHealth);

        // �p�G��q���� 0�A�P�����G��
        if (currentHealth <= 0)
        {
            DestroyWall();
        }
        Debug.Log("CurrentHealth:" + currentHealth);
    }

    public void DestroyWall()
    {
        Debug.Log("���G��Q�R���I");

        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }
        // �T�ΩҦ� Collider
        //Collider[] colliders = GetComponentsInChildren<Collider>();
        //foreach (var collider in colliders)
        //{
        //    collider.enabled = false;
        //}
        // ����P���A�����z������s
        Destroy(healthBar.gameObject, 0.1f);
        Destroy(nutWall.gameObject, 0.1f);
    }
}

