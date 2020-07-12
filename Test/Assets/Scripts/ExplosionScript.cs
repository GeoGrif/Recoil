using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    PlayerScript player;
    Enemy enemy;

    [SerializeField] private float ExplosiveRange = 3.0f;
    [SerializeField] private float ExplosivePower = 10f;
    [SerializeField] public int damage = 40;

    [SerializeField] private AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 explosionPos = transform.position;

        CameraScript.TriggerShake();
        AudioManager.instance.PlaySFX(explosionSound);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, ExplosiveRange);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            Vector3 hitPosition = hit.transform.position;

            var explosionDir = hitPosition - explosionPos;
            var explosionDistance = explosionDir.magnitude;

            explosionDistance /= ExplosiveRange;

            if (hit.gameObject == player.gameObject)
            {
                player.takeDamage((int)Mathf.Round((1 / explosionDistance) * damage));
            }
            else if (hit.gameObject.tag == "Enemy")
            {
                hit.gameObject.GetComponent<Enemy>().takeDamage((int)Mathf.Round((1 / explosionDistance) * damage));
            }
            else if (hit.gameObject.tag == "Brakeable")
            {
                hit.gameObject.GetComponent<BrakeableObject>().RemoveHealth((1 / explosionDistance) * damage);
            }

            explosionDir.Normalize();

            float force = Mathf.Lerp(0, ExplosivePower, (1 - explosionDistance));

            Vector3 forceDirection = force * explosionDir;

            Debug.Log("Explosive force " + force);
            Debug.Log("explosionDistance " + explosionDistance);
            Debug.Log("Explosive direction vector " + forceDirection);
            Debug.Log("exploding on object: " + hit.gameObject.name);

            if (rb != null)
            {
                rb.AddForce(forceDirection, ForceMode2D.Impulse);
            }

        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
