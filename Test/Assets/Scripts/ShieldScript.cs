using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    //can be used to change the shield length
    public float shieldLengthModifier = 2.0f;
    public float reflectForce = 250.0f;

    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    bool enableShield = false;
    public static bool shieldIsActive = false;
    Vector3 originalShieldSize;

    //bool to use to apply shield length change
    public static bool changeShieldLength = false;
    public static bool revertShieldLength = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider = this.GetComponent<BoxCollider2D>();
        originalShieldSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(changeShieldLength)
        {
            //for powerups etc
            ChangeShieldLength(shieldLengthModifier);            
            changeShieldLength = false;
        }

        if (revertShieldLength)
        {
            //for powerups etc
            RevertShieldLength();
            revertShieldLength = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            enableShield = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            enableShield = false;
        }

        if (enableShield && !shieldIsActive)
        {
            ActivateShield();
            shieldIsActive = true;
        }

        if (!enableShield && shieldIsActive)
        {
            DeactivateShield();
            shieldIsActive = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj;
        proj = other.gameObject.GetComponent<Projectile>();

        if (proj != null)
        {
            ReflectProjectile(proj, other, reflectForce);
        }
    }

    void ActivateShield()
    {
        spriteRenderer.enabled = true;
        collider.enabled = true;
    }

    void DeactivateShield()
    {
        spriteRenderer.enabled = false;
        collider.enabled = false;
    }

    void ChangeShieldLength(float modifier)
    {
        transform.localScale += new Vector3(modifier, 0f, 0f);
    }

    void RevertShieldLength()
    {
        transform.localScale = new Vector3(originalShieldSize.x, originalShieldSize.y, originalShieldSize.z);
    }

    void ReflectProjectile(Projectile projectile, Collision2D collision, float force)
    {
        Rigidbody2D rb;

        Vector2 dir = new Vector2(collision.contacts[0].point.x - transform.position.x, collision.contacts[0].point.y - transform.position.y);
        dir = dir.normalized;

        rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force);
    }
}
