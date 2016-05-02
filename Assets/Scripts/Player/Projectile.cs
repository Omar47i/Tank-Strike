using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float m_MovementSpeed = 6000f;   // force of momentum when firing the projectiles
     
    public int m_Damage = 20;                // amount of inflicting damage

    [HideInInspector]
    public Vector2 m_Direction;              // direction of the shooting projectile

	public void OnInstantiate(Vector2 tankForward)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), PlayerAttack.instance.gameObject.GetComponent<Collider2D>());

        // Apply momentum to htis projectile when fired
        GetComponent<Rigidbody2D>().AddForce(tankForward * m_MovementSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.enemy)      // hit ana enemy?
        {
            Destroy(gameObject);        // Destroy first the projectile

            // TODO: Create an explosion on impact

            // Add force in the direction of the shooting projectile
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(m_Direction * (m_MovementSpeed * .05f), ForceMode2D.Impulse);

            // Inflict damage to the enemy tank
            col.transform.GetChild(0).GetComponent<EnemyHealth>().InflictDamage(m_Damage);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
