using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;
    public float fireRate = 0.1f;

    private float nextFireTime = 0f;

    private void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.transform.forward, Color.blue);
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        // 可添加子弹特效
    }
}
