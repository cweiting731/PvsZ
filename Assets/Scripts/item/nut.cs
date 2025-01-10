using UnityEngine;
using UnityEngine.UI;

public class Nut : MonoBehaviour, Damageable
{
    public Transform nutWall;
    public CapsuleCollider capsuleCollider;
    public float maxHealth = 100f; // ���G�𪺪�l��q
    public int EnergyCost = 20;
    private float currentHealth; // ���e��q
    public Slider healthBar;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public Canvas healthBarCanvas;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // ��l�Ʀ�q
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + offset; // 初始化位置
            healthBarCanvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward); // 朝向攝影機
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
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
    }
    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        // if (transform.position.y >= 0.2)
        // {
        //     transform.position += Vector3.down * Time.deltaTime * 9.8f; // �������O
        // }
    }

    // �����G���������ɽեΦ���k
    public void TakeDamage(float damage)
    {
        Debug.Log("NutHurt");
        currentHealth -= damage;
        if (animator != null)
        {
            animator.SetTrigger("hurtTrigger");
        }
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

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}

