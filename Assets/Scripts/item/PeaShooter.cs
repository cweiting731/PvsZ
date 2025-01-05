using Unity.VisualScripting;
using UnityEngine;

public class PeaShooter : MonoBehaviour, Damageable
{
    public GameObject bulletPrefab;
    public BoxCollider thisCollider;
    public float attackInterval;
    public float maxHealth;
    public int energyCost = 20;
    public Transform muzzle;
    //
    private float currentHealth;

    private float attackTimer = 0f;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (currentHealth <= 0)
        {
            DestroyPeaShooter();    
        }
    }

    private void DestroyPeaShooter()
    {
        Debug.Log("�ܨ��g��Q�R��");
        thisCollider.enabled = false;
        Destroy(gameObject, 0.1f);
    }
}
