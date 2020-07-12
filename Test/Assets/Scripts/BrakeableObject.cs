using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeableObject : MonoBehaviour
{
    /**
      * Sprite to alpha in as the halth of this object gets lower.  
      */
    [SerializeField] private SpriteRenderer Cracks;

    /**
    * The amount of hit points this object can take before destruction.  
    */
    [SerializeField] private float CurrentHealth;

    [SerializeField] private float TotalHealth;

    float projectileDamage;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Projectile")
        {
            projectileDamage = collision.gameObject.GetComponent<Projectile>().Damage;
            RemoveHealth(projectileDamage);
        }
    }

    public void RemoveHealth(float damage)
    {
        CurrentHealth -= damage;

        Color tempColour = Cracks.color;

        tempColour.a = CurrentHealth / TotalHealth;

        Cracks.color = tempColour;

        if (tempColour.a <= 0) Destroy(this.gameObject);
    }
}
