using UnityEngine;

public class TNT : MonoBehaviour
{
    public float explosionForce = 100f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 1f;
    public GameObject triggerObject;

    void Start()
    {
        if (triggerObject == null)
        {
            Debug.LogError("Trigger object not assigned in TNT script!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObject) return;

        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
