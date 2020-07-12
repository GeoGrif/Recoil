using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{


    [SerializeField] private float ExplosiveRange = 5f;
    [SerializeField] private float ExplosivePower = 10f;
    [SerializeField] public float Damage = 10.0f;

    [SerializeField] private AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.gameObject.tag == "Projectile" && collision.otherCollider.gameObject.tag == "Projectile")
        //{
            Vector3 explosionPos = transform.position;

            CameraScript.TriggerShake();
            AudioManager.instance.PlaySFX(explosionSound);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, ExplosiveRange);

            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();


                if (rb != null)
                {
                    Vector3 hitPosition = rb.position;

                    var explosionDir = hitPosition - explosionPos;
                    var explosionDistance = explosionDir.magnitude;

                    explosionDistance /= ExplosiveRange;

                    explosionDir.Normalize();

                    float force = Mathf.Lerp(0, ExplosivePower, (1 - explosionDistance));

                    Vector3 forceDirection = force * explosionDir;

                    Debug.Log("explosionDistance " + explosionDistance);

                    rb.AddForce(forceDirection, ForceMode2D.Impulse);
                }

            }
       // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
