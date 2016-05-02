using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

    [SerializeField]
    private float m_MovementSpeed = 6000f;   // force of momentum when firing the projectiles

    private int m_Damage = 20;               // amount of inflicting damage

    [HideInInspector]
    public Vector2 m_Direction;              // direction of the shooting projectile

    public void OnInstantiate(Vector2 tankForward, Collider2D col)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);

        // Apply force to this projectile when fired
        GetComponent<Rigidbody2D>().AddForce(tankForward * m_MovementSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.player)     // hit the player tank?
        {
            Destroy(gameObject);        // Destroy first the projectile

            // TODO: Create an explosion on impact

            // Add force in the direction of the shooting projectile
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(m_Direction * (m_MovementSpeed * .4f), ForceMode2D.Impulse);

            // Inflict damage to the enemy tank
            col.gameObject.GetComponent<PlayerHealth>().InflictDamage(m_Damage);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
