using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private Transform m_CanonPosition;        // reference to the position of firing the projectile

    [SerializeField]
    private GameObject m_ProjectilePrefab;    // Prefab of the tank's projectile

    [SerializeField]
    private int m_Projectiles = 16;           // number of prjectiles player can fire 

    [SerializeField]
    private Text m_ProjectilesHUD;            // reference to the projectiles count HUD

    public static PlayerAttack instance;      // Make it singleton!

    void Awake()
    {
        if (instance != this)
            instance = this;
    }

    void Update()
    {
        GetInput();
    }

    // Get user input to process attacks
    private void GetInput()
    {
        // get attack input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Fire a projectiles if available ammo
            if (m_Projectiles > 0)
            {
                m_Projectiles--;
                FireProjectile();
            }
            else
            {
                // TODO: Play not enough ammo sound
            }
        }
    }

    private void FireProjectile()
    {
        // Instantiate a projectile at the canon's position
        GameObject projectile = Instantiate(m_ProjectilePrefab, m_CanonPosition.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Projectile>().m_Direction = m_CanonPosition.right;
        projectile.GetComponent<Projectile>().OnInstantiate(m_CanonPosition.right);

        // Update projectiles HUD
        UpdateHUDs();
    }

    public void AddProjectiles(int amount)
    {
        m_Projectiles += amount;
        UpdateHUDs();
    }

    public void UpdateHUDs()
    {
        // Update projectiles HUD
        m_ProjectilesHUD.text = "Projectiles: " + m_Projectiles.ToString();
    }
}
