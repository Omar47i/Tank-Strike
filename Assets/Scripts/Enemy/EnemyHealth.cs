using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    private Transform m_Tank;               // reference to the tank parent
    private Quaternion m_Rotation;          // starting rotation of the HB, used to restrict rotation with the tank                   
    private Transform m_ArmorSlider;        // Reference to the UI's armor hp bar.
    private float m_HeightFromCenter;       // used to define height from the tank

    private int m_CurrentArmorHP;           // The current health the tank has.

    public int m_StartingArmorHP = 100;     // The amount of health the player starts the game with.

    void Awake()
    {
        // Initialize variables in Awake
        m_Rotation = transform.rotation;

        m_HeightFromCenter = transform.position.y - transform.parent.position.y;

        m_ArmorSlider = transform;

        m_Tank = transform.parent;

        m_CurrentArmorHP = m_StartingArmorHP;
    }

    public void InflictDamage(int dmg)
    {
        // Decrease the enemy tank current health
        m_CurrentArmorHP -= dmg;

        if (m_CurrentArmorHP <= 0)
        {
            Die();
            return;
        }

        // update the health bar slider length
        m_ArmorSlider.localScale = new Vector3((float)m_CurrentArmorHP / m_StartingArmorHP, m_ArmorSlider.localScale.y);
    } 

    private void Die()
    {
        // TODO: DO some special effects regarding enemy explosion

        Destroy(transform.parent.gameObject);
   
        // check for player win
        SceneManager.instance.CheckForWin();
    }

    // Restrict health bar rotation
    void LateUpdate()
    {
        //transform.rotation = m_Rotation;
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(m_Tank.position.x - .66f, m_Tank.position.y + m_HeightFromCenter);
    }
}
