using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] private float shieldLength = 1.0f;

    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    bool enableShield = false;
    bool shieldIsActive = false;

    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
