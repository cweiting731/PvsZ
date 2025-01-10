using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public float damage = 20f;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        // reserve for Damage() or Effect()
        if (target.CompareTag("Enemy"))
        {
            ZombieController zbController = target.GetComponent<ZombieController>();
            MinerZombie minerZombie = target.GetComponent<MinerZombie>();
            if(zbController == null && minerZombie  != null)
            {
                minerZombie.TakeDamage(damage);
            }
            else if (zbController != null && minerZombie == null)
            {
                zbController.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("bullet target controller is null");
                return;
            }
        }
        Destroy(gameObject);
    }
}
