using UnityEngine;

public class SceneManager : MonoBehaviour {

    private int m_EnemyCount;
    [HideInInspector]
    public int m_CurrEnemyCount;

    public static SceneManager instance;

    void Awake()
    {
        if (instance != this)
            instance = this;
    }

    void Start()
    {
        m_EnemyCount = GameObject.FindGameObjectsWithTag(Tags.enemy).Length;

        m_CurrEnemyCount = m_EnemyCount;
    }

	public void OnRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void CheckForWin()
    {
        m_CurrEnemyCount--;

        if (m_CurrEnemyCount == 0)
        {
            Globals.instance.m_WinPanel.SetActive(true);
        }
    }
}
