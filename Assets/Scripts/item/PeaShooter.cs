using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PeaShooter : MonoBehaviour, Damageable
{
    public GameObject bulletPrefab;
    public BoxCollider thisCollider;
    public float attackInterval;
    public float maxHealth;
    public int EnergyCost = 20;
    public Transform muzzle;
    public Slider healthBar;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public Canvas healthBarCanvas;
    //
    private float currentHealth;

    private float attackTimer = 0f;
    void Start()
    {
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
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        if (attackTimer >= attackInterval)
        {
            Debug.Log("Shooooooooooooooooooooooooooooooot");
            attackTimer = 0f;
            Shoot();
        }
        attackTimer += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            DestroyPeaShooter();    
        }
        Debug.Log("PeaShooter Health" + currentHealth);
    }

    private void DestroyPeaShooter()
    {
        Debug.Log("�ܨ��g��Q�R��");
        thisCollider.enabled = false;
        Destroy(gameObject, 0.1f);
    }

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}
