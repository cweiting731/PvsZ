using UnityEngine;
using UnityEngine.UI;

public class Nut : MonoBehaviour
{
    public Transform nutWall;
    public float maxHealth = 100f; // ��G�𪺪�l��q
    public int EnegyCost = 20;
    private float currentHealth; // ��e��q
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

    // ���G���������ɽեΦ���k
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        //Debug.Log("��G���������A�Ѿl��q�G" + currentHealth);

        // �p�G��q���� 0�A�P����G��
        if (currentHealth <= 0)
        {
            DestroyWall();
        }
        Debug.Log("CurrentHealth:" + currentHealth);
    }

    public void DestroyWall()
    {
        Debug.Log("��G��Q�R���I");

        // �T�ΩҦ� Collider
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        // ����P���A�����z������s
        Destroy(healthBar.gameObject, 0.1f);
        Destroy(nutWall.gameObject, 0.1f);
    }
}

