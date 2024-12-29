using UnityEngine;
using UnityEngine.UI;

public class Nut : MonoBehaviour
{
    public Transform nutWall;
    public float maxHealth = 100f; // 堅果牆的初始血量
    public int EnegyCost = 20;
    private float currentHealth; // 當前血量
    public Slider healthBar;
    public Vector3 offset;
    public Canvas healthBarCanvas;

    void Start()
    {
        healthBarCanvas.transform.localPosition = new Vector3(0.01f, 0.05f, 0);
        // 初始化血量
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        // 初始設置：禁用 Kinematic，讓物體自然落地

    }
    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        if (transform.position.y >= 0.2)
        {
            transform.position += Vector3.down * Time.deltaTime * 9.8f; // 模擬重力
        }
    }

    // 當堅果牆受到攻擊時調用此方法
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        //Debug.Log("堅果牆受到攻擊，剩餘血量：" + currentHealth);

        // 如果血量降到 0，銷毀堅果牆
        if (currentHealth <= 0)
        {
            DestroyWall();
        }
        Debug.Log("CurrentHealth:" + currentHealth);
    }

    public void DestroyWall()
    {
        Debug.Log("堅果牆被摧毀！");

        // 禁用所有 Collider
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        // 延遲銷毀，讓物理引擎更新
        Destroy(healthBar.gameObject, 0.1f);
        Destroy(nutWall.gameObject, 0.1f);
    }
}

