using UnityEngine;
using System.Collections;

public class ChiliBomb : MonoBehaviour, Damageable
{
    public float explosionDelay = 1f;  // �z������ɶ�
    public float explosionRadius = 10f; // �z���d��
    public ParticleSystem explosionEffect; // �z���ĪG
    public GameObject explosionLightPrefab;
    public int EnergyCost = 50;
    public float rowStartZ = -100f;
    public float rowEndZ = 100f;
    public float step = 1f;

    void Start()
    {
        // �]�m�˭p�ɤ��z
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
                StartCoroutine(FadeAndDestroyLight(pointLight)); // ���ܺ���
            }
            Destroy(lightObject, 1f); // ���]���ī��� 2 ����R��
        }

        TriggerRowExplosion();

        // ������u�Ҧb�� Y �b
        float bombX = transform.position.x;

        // �M���Ҧ��L��
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject zombie in zombies)
        {
            // �p�G�L�ͦb�P�@�ơA�R����
            if (Mathf.Abs(zombie.transform.position.x - bombX) <= 1.5f) // ���]�ƪ����פ��\�p�~�t
            {
                Destroy(zombie);
            }
        }
        GameObject[] items = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject item in items)
        {
            // �p�G�L�ͦb�P�@�ơA�R����
            if (Mathf.Abs(item.transform.position.x - bombX) <= 1.5f) // ���]�ƪ����פ��\�p�~�t
            {
                Transform parent = item.transform.parent;
                Destroy(item);
                if (parent != null)
                {
                    Destroy(parent.gameObject);
                }
            }
        }
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
            //Destroy(gameObject);
        }
        // �R�����Ԭ��u
    }

    IEnumerator FadeAndDestroyLight(Light light)
    {
        float fadeDuration = 1f; // ���ī���ɶ�
        float initialIntensity = light.intensity;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            light.intensity = Mathf.Lerp(initialIntensity, 0, t / fadeDuration);
            yield return null;
        }

        light.intensity = 0;
        Destroy(light.gameObject); // �̫�P������
    }
    void TriggerRowExplosion()
    {
        // ������u�Ҧb�� Y �b
        float bombX = transform.position.x;
        float bombY = transform.position.y;

        // �M����ƽd��A�v�Ӽ���S��
        for (float z = rowStartZ; z <= rowEndZ; z += step)
        {
            TriggerExplosionEffect(new Vector3(bombX, bombY, z));
        }
    }

    void TriggerExplosionEffect(Vector3 position)
    {
        // ����ɤl�S��
        if (explosionEffect != null)
        {
            ParticleSystem instantiatedEffect = Instantiate(explosionEffect, position, Quaternion.identity);
            Destroy(instantiatedEffect.gameObject, instantiatedEffect.main.duration); // �ɤl�t�Ϋ���ɶ���R��
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

