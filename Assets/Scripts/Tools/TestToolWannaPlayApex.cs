using UnityEngine;

public class TestToolWannaPlayApex : MonoBehaviour
{
    public float health = 100;
    private bool isDead = false;
    private float timer = 0f;

    void Start()
    {
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // reserved for zombie.attack()

            // timer += Time.deltaTime;
            // Debug.Log($"health: {health}");
            // if(timer > 1)
            // {
            //     // TakeDamage(collision.gameObject.GetComponent<ZombieController>().originalAttack);
            //     timer = 0;
            // }
        }
    }
}
