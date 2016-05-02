using UnityEngine;

public class AI_Patrol : MonoBehaviour {
    
    [SerializeField]
    private Vector3 m_StartingPoint;    // patrol starting point
                                 
    [SerializeField]
    private Vector3 m_EndingPoint;      // patrol ending point

    [SerializeField]                    // at this distance in unity units, player will be detected
    private float m_PlayerDetectionDistance = 2f;

    [SerializeField]
    private float m_Fov = 160f;         // 80 degree to the left and 80 degree to the right

    [SerializeField]
    private GameObject m_Player;        // reference to the player 

    [SerializeField]
    private float m_NextFireTime = 1f;  // time between fires

    private Vector2 m_PlayerDirection;  // direction to the player, used to decide if this enemy saw the player

    private Transform m_Transform;      // cash transform component

    private bool m_CanFire = true;      // a flag to indicate if this enemy can fire or not
    private bool m_CanRotate = true;    // a flag to indicate if this enemy can turn 
    private bool m_MoveForward = true;  // this flag decides if the tank should move to the next point or return back
    private bool m_CanMove = true;

    void Awake()
    {   // cash transform..
        m_Transform = transform;
    }

    void Update()
    {
        if (Globals.m_PlayerDead)   // is player dead?
            return;
        float targetAngle = 0f;     // angle between this enemy and the player

        bool detected = DetectPlayer(out targetAngle);

        // If player is not detected? then patrol between two points
        if (!detected)
        {
            Patrol();
        }
        else
        {
            // Player detected; initiate the fire protocol {rotate towards the player and fire}
            if (m_CanRotate)
                Rotate(targetAngle);
            if (m_CanFire)
                Fire();
        }

        // calculate timig intervals between each fire
        ProjectileTiming();
    }

    /// <summary>
    /// Test against player sight, if detected then fire at him
    /// </summary>
    /// <param name="targetAngle">Target angle this enemy needs to face the player</param>
    /// <returns></returns>
    public bool DetectPlayer(out float targetAngle)
    {
        // Get direction from this enemy to the player
        m_PlayerDirection = m_Player.transform.position - m_Transform.position;

        // Set the forward direction of the enemy (in this case the right direction)
        Vector2 forward = m_Transform.right;

        // Get the angle between this enemy and the player
        float angle = AI_Stationary.Angle(forward, m_PlayerDirection);

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

    /// <summary>
    /// If a projectile is fired, then wait amount of time before firing again if player still in sight
    /// </summary>
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

    /// <summary>
    /// Fire at the player
    /// </summary>
    public void Fire()
    {
        m_CanFire = false;

        GetComponent<EnemyAttack>().FireProjectile();
    }

    /// <summary>
    /// Patrol from one point to another
    /// </summary>
    public void Patrol()
    {
        // Get the target dir to the ending point
        Vector3 targetDir = (m_MoveForward) ? (m_EndingPoint - m_Transform.position).normalized :
            (m_StartingPoint - m_Transform.position).normalized;

        if (m_CanMove)
        {
            GetComponent<EnemyTankMotor>().Move();
            if (targetDir != m_Transform.right)
            {
                // check if the tank is not deviated from their path, if so then rotate towards their path
                ReturnToPath(targetDir);
            }
        }

        // if this enemy reached the end of the second point, halt, rotate and head back
        if (Vector3.Distance(m_Transform.position, m_EndingPoint) <= 0.2f && m_MoveForward)
        {
            m_CanMove = false;           // restrict movement while rotating

            Rotate(m_StartingPoint - m_EndingPoint);
        }

        // if this enemy reached the end of the first point, halt, rotate and head back
        else if (Vector3.Distance(m_Transform.position, m_StartingPoint) <= 0.2f && !m_MoveForward)
        {
            m_CanMove = false;           // restrict movement while rotating

            Rotate(m_EndingPoint - m_StartingPoint);
        }

    }

    /// <summary>
    /// Rotate when reaching the end of a point
    /// </summary>
    /// <param name="TargetDir"></param>
    public void Rotate(Vector3 TargetDir)
    {
        // the angle that this tank must rotate to in order to return back
        float angle = Mathf.Atan2(TargetDir.y, TargetDir.x) * Mathf.Rad2Deg;

        // create the target rotation
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

        m_Transform.rotation = Quaternion.RotateTowards(m_Transform.rotation, targetRot, 
            GetComponent<EnemyTankMotor>().m_TurnSpeed);

        // Enemy tank completed rotation to the target
        if (Vector3.Normalize(TargetDir) == m_Transform.right)
        {
            m_CanMove = true;

            // if player is was heading forward, then set the flag to head backward and vice versa
            m_MoveForward = (m_MoveForward == true) ? false : true;
        }
    }

    // Rotate to face the player
    public void Rotate(float angle)
    {
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, lookRotation,
            GetComponent<EnemyTankMotor>().m_TurnSpeed * Time.deltaTime);
    }

    public void ReturnToPath(Vector3 TargetDir)
    {
        // the angle that this tank must rotate to in order to return back
        float angleToTarget = Mathf.Atan2(TargetDir.y, TargetDir.x) * Mathf.Rad2Deg;

        // create the target rotation
        Quaternion targetRot = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
        //print("Angle to target: " + angleToTarget);
        m_Transform.rotation = Quaternion.RotateTowards(m_Transform.rotation, targetRot, GetComponent<EnemyTankMotor>().m_TurnSpeed);
    }
}
