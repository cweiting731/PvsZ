using UnityEngine;
using System.Collections;

public class ChiliBomb : MonoBehaviour, Damageable
{
    public float explosionDelay = 1f;  // 爆炸延遲時間
    public float explosionRadius = 10f; // 爆炸範圍
    public ParticleSystem explosionEffect; // 爆炸效果
    public GameObject explosionLightPrefab;
    public int EnergyCost = 50;
    public float rowStartZ = -100f;
    public float rowEndZ = 100f;
    public float step = 1f;

    void Start()
    {
        // 設置倒計時引爆
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        if (explosionLightPrefab != null)
        {
            GameObject lightObject = Instantiate(explosionLightPrefab, transform.position, Quaternion.identity);
            Light pointLight = lightObject.GetComponent<Light>();
            if (pointLight != null)
            {
                StartCoroutine(FadeAndDestroyLight(pointLight)); // 漸變熄滅
            }
            Destroy(lightObject, 1f); // 假設光效持續 2 秒後刪除
        }

        TriggerRowExplosion();

        // 獲取炸彈所在的 Y 軸
        float bombX = transform.position.x;

        // 遍歷所有殭屍
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject zombie in zombies)
        {
            // 如果殭屍在同一排，摧毀它
            if (Mathf.Abs(zombie.transform.position.x - bombX) <= 0.5f) // 假設排的高度允許小誤差
            {
                Destroy(zombie);
            }
        }

        // 摧毀辣椒炸彈
        Destroy(gameObject);
    }

    IEnumerator FadeAndDestroyLight(Light light)
    {
        float fadeDuration = 1f; // 光效持續時間
        float initialIntensity = light.intensity;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            light.intensity = Mathf.Lerp(initialIntensity, 0, t / fadeDuration);
            yield return null;
        }

        light.intensity = 0;
        Destroy(light.gameObject); // 最後銷毀光效
    }
    void TriggerRowExplosion()
    {
        // 獲取炸彈所在的 Y 軸
        float bombX = transform.position.x;
        float bombY = transform.position.y;

        // 遍歷整排範圍，逐個播放特效
        for (float z = rowStartZ; z <= rowEndZ; z += step)
        {
            TriggerExplosionEffect(new Vector3(bombX, bombY, z));
        }
    }

    void TriggerExplosionEffect(Vector3 position)
    {
        // 播放粒子特效
        if (explosionEffect != null)
        {
            ParticleSystem instantiatedEffect = Instantiate(explosionEffect, position, Quaternion.identity);
            Destroy(instantiatedEffect.gameObject, instantiatedEffect.main.duration); // 粒子系統持續時間後刪除
        }
    }

    public void TakeDamage(float damage)
    {

    }

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}

