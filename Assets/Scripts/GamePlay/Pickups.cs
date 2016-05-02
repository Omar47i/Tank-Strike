using UnityEngine;
using System.Collections;

public class Pickups : MonoBehaviour {

    // Make it singleton
    public static Pickups instance;

    [SerializeField]
    private int m_TimeBetPickups = 10;
    
    [SerializeField]
    private Pickup m_Pickup;
}

[System.Serializable]
public class Pickup
{
    public enum Types { Projectile, ArmorHP, Invisibility, MoveSpeed, FireSpeed, Freeze };
    public Types m_Type = Types.Projectile;

    [HideInInspector]
    public int m_ProjectileCount = 2;      // when instantiating a projectile pickup, assign this amount of projectiles.
    [HideInInspector]
    public int m_ArmorHPCount = 10;
    [HideInInspector]
    public int m_SpeedCount = 1000;
    [HideInInspector]
    public int m_InvisibilityTime = 12;

    [SerializeField]
    private GameObject m_ProjectilePickupPrefab;
}