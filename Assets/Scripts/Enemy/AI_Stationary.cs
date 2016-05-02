using UnityEngine;

public class AI_Stationary : MonoBehaviour
{

    private Transform m_Transform;      // cash transform component

    [SerializeField]
    private GameObject m_Player;        // reference to the player 

    [SerializeField]
    private float m_Fov = 160f;         // 60 degree to the left and 60 degree to the right

    [SerializeField]                    // at this distance in unity units, player will be detected
    private float m_PlayerDetectionDistance = 2f;

    private Vector2 m_PlayerDirection;  // direction to the player, used to decide if this enemy saw the player

    [SerializeField]
    private float m_NextFireTime = 1f;  // time between fires

    private bool m_CanFire = true;      // a flag to indicate if this enemy can fire or not

    private bool m_CanRotate = true;    // a flag to indicate if this enemy can turn 

    void Awake()
    {   // cash transform..
        m_Transform = transform;
    }

    void Update()
    {
        if (Globals.m_PlayerDead)       // is player dead?
            return;
        float targetAngle = 0f;         // angle required to face the player

        // is the player detected? if is then rotate and fire
        bool detected = DetectPlayer(out targetAngle); 

        if (detected)        
        {
            if (m_CanRotate)
                Rotate(targetAngle);

            if (m_CanFire)
            {
                Fire();
            }
        }

        // calculate timig intervals between each fire
        ProjectileTiming();
    }

    public bool DetectPlayer(out float targetAngle)
    {
        // Get direction from this enemy to the player
        m_PlayerDirection = m_Player.transform.position - m_Transform.position;

        // Set the forward direction of the enemy (in this case the right direction)
        Vector2 forward = m_Transform.right;

        // Get the angle between this enemy and the player
        float angle = Vector2.Angle(forward, m_PlayerDirection);

        // Get the target angle that this enemy must rotate to successfully face the player
        targetAngle = Mathf.Atan2(m_PlayerDirection.y, m_PlayerDirection.x) * Mathf.Rad2Deg;

        // if the player is within this enemy tank distance
        if (Vector3.Distance(m_Player.transform.position, m_Transform.position) <= m_PlayerDetectionDistance)
        {
            // if the player is within this enemy FOV
            if (angle <= Mathf.Abs(m_Fov * .5f))
            {
                RaycastHit2D hit = Physics2D.Linecast(m_Transform.GetChild(1).position, m_Player.transform.position);
                if (hit.collider.gameObject.tag == Tags.player)
                    return true;
            }
        }

        return false;
    }

    public void ProjectileTiming()
    {
        if (m_CanFire == false)
        {
            m_NextFireTime -= Time.deltaTime;

            if (m_NextFireTime <= 0)
            {
                m_NextFireTime = 1f;
                m_CanFire = true;
            }
        }
    }

    public void Fire()
    {
        m_CanFire = false;

        GetComponent<EnemyAttack>().FireProjectile();
    }

    public void Rotate(float targetAngle)
    {
        Quaternion lookRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, lookRotation,
            GetComponent<EnemyTankMotor>().m_TurnSpeed * Time.deltaTime);
    }

    public static float Angle(Vector3 from, Vector3 to)
    {
        return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
    }
}

public interface Enemy_AI
{
    void ProjectileTiming();

    void Fire();

    void Rotate(float angle2);

    bool DetectPlayer(out float angle);
}
