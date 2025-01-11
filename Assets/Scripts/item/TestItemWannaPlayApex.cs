using UnityEngine;
using UnityEngine.UI;

public class TestItemWannaPlayApex : MonoBehaviour, Damageable
{
    public float maxHealth = 100;
    public Slider healthBar;
    public int EnergyCost = 1;
    public Vector3 offset;
    public Canvas healthBarCanvas;
    private bool isDead = false;
    private float currentHealth;

    void Start()
    {
        healthBarCanvas.transform.localPosition = new Vector3(0.01f, 0.05f, 0);
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
    }

    void Update()
    {
        healthBarCanvas.transform.rotation = Quaternion.identity;

        healthBar.gameObject.SetActive(currentHealth < maxHealth);
    }

    public void TakeDamage(float damage)
    {
        // Debug.Log("ApexHurt");
        // if (isDead) return;
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        if(bc != null)
        {
            bc.enabled = false;
        }
        isDead = true;
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(healthBar.gameObject, 0.1f);
        Destroy(gameObject, 0.1f);
    }

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}
