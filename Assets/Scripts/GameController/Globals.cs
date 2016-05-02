using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

    public GameObject m_GamoverPanel;
    public GameObject m_WinPanel;

    public static Globals instance;

    // Is the player dead?
    public static bool m_PlayerDead = false;

    void Awake()
    {
        if (!instance)
            instance = this;

        m_PlayerDead = false;
    }

    void Update()
    {
        if (m_PlayerDead)
        {
            m_GamoverPanel.SetActive(true);
        }
    }
}
