using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    private Transform m_CanonPosition;        // reference to the position of firing the projectile

    [SerializeField]
    private GameObject m_ProjectilePrefab;    // Prefab of the tank's projectile to be instantiated

    [SerializeField]
    private int m_ProjectileCount = 16;       // number of prjectiles player can fire 

    public void FireProjectile()
    {
        if (m_ProjectileCount > 0)
        {
            // Instantiate a projectile at the canon's position
            GameObject projectile = Instantiate(m_ProjectilePrefab, m_CanonPosition.position, Quaternion.identity) as GameObject;
            m_ProjectileCount--;
            projectile.GetComponent<EnemyProjectile>().m_Direction = m_CanonPosition.right;
            projectile.GetComponent<EnemyProjectile>().OnInstantiate(m_CanonPosition.right, GetComponent<Collider2D>());
        }
    }
}
