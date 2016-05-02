using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private int m_CurrentArmorHP;          // The current health of the player.

    [SerializeField]
    private int m_StartingArmorHP = 100;   // The amount of health the player starts the game with.

    [SerializeField]
    private Text m_UArmor;                 // reference to the armor UI

    void Start()
    {
        m_CurrentArmorHP = m_StartingArmorHP;

        // update Player Armor HUD
        UpdateHUDs();
    }

    public void UpdateHUDs()
    {
        m_UArmor.text = "Armor: " + m_CurrentArmorHP.ToString();
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

        UpdateHUDs();
    }

    public void AddArmor(int amount)
    {
        m_CurrentArmorHP += amount;

        UpdateHUDs();
    }

    void Die()
    {
        Globals.m_PlayerDead = true;

        Destroy(gameObject);
    }
}
