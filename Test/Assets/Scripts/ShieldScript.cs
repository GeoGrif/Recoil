using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    //can be used to change the shield length
    public float shieldLengthModifier = 2.0f;
    public float reflectForce = 250.0f;
    public int shieldHealth = 100;
    public float shieldRechargeTime = 3.0f;
    public float shieldDownTime = 0.5f;
    public float timeBeforeRecharge = 1.0f;

    private float tempShieldDownTime = 0.5f;
    private float tempShieldRechargeTime = 3.0f;
    private float tempTimeBeforeRecharge = 1.0f;

    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    bool enableShield = false;
    bool shieldDown = false;
    public static bool shieldIsActive = false;
    Vector3 originalShieldSize;

    //bool to use to apply shield length change
    public static bool changeShieldLength = false;
    public static bool revertShieldLength = false;
    public static bool shieldRecharging = false;

    private int startingShieldHealth = 100;
    private bool rechargeShield = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider = this.GetComponent<BoxCollider2D>();

        originalShieldSize = transform.localScale;

        tempShieldRechargeTime = shieldRechargeTime;
        startingShieldHealth = shieldHealth;
        tempShieldDownTime = shieldDownTime;
        tempTimeBeforeRecharge = timeBeforeRecharge;
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

        if (Input.GetMouseButtonUp(1) || shieldRecharging)
        {
            enableShield = false;
            shieldDown = true;
        }

        if (enableShield && !shieldIsActive && !shieldRecharging && !shieldDown)
        {
            ActivateShield();
            shieldIsActive = true;
            tempTimeBeforeRecharge = timeBeforeRecharge;
        }

        if (!enableShield && shieldIsActive)
        {
            DeactivateShield();
            shieldIsActive = false;
        }

        //for when we want the natural shield recharge if its not broken
        if(!shieldIsActive && !shieldRecharging && shieldHealth < startingShieldHealth)
        {
            tempTimeBeforeRecharge -= Time.deltaTime;
            if(tempTimeBeforeRecharge <= 0)
            {
                rechargeShield = true;
            }
        }
        else
        {
            rechargeShield = false;
        }

        if(shieldHealth <= 0)
        {
            shieldRecharging = true;
        }

        if(shieldRecharging)
        {
            tempShieldRechargeTime -= Time.deltaTime;
            shieldHealth = startingShieldHealth - (int)Mathf.Round((100 * (tempShieldRechargeTime / shieldRechargeTime)));
            Debug.Log("Shield health is " + shieldHealth);

                if (tempShieldRechargeTime <= 0)
                {
                    shieldHealth = startingShieldHealth;
                    shieldRecharging = false;
                    tempShieldRechargeTime = shieldRechargeTime;
                }
        }

        if(shieldDown)
        {
            tempShieldDownTime -= Time.deltaTime;

            if (tempShieldDownTime <= 0)
            {
                shieldDown = false;
                tempShieldDownTime = shieldDownTime;
            }
        }
    }

    void FixedUpdate()
    {
        if(rechargeShield)
        {
            shieldHealth += 1;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj;
        proj = other.gameObject.GetComponent<Projectile>();

        if (proj != null)
        {
            ReflectProjectile(proj, other, reflectForce);
            shieldHealth -= 10;
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
