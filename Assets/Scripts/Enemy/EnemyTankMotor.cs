using UnityEngine;

public class EnemyTankMotor : MonoBehaviour {

    [SerializeField]
    private float m_MoveSpeed = 5000f;      // the force applied to the tank to move

    private Vector3 m_MovementDirection;    // this vector will store the movement direction of the tank's player

    [HideInInspector]
    public bool m_IsMovingBack = false;     // is the player is moving back?

    public float m_TurnSpeed = 2f;          // the turn speed of the player's tank     

    // cash variables
    private Transform m_Trans;
    private Rigidbody2D m_RigidBody;

    void Awake()
    {
        m_Trans = transform;
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveX = 1)
    {
        if (moveX < 0f)
            m_IsMovingBack = true;
        else
            m_IsMovingBack = false;

        // get the movement direction of the tank
        m_MovementDirection = m_Trans.TransformDirection(moveX * m_MoveSpeed, 0f, 0f);

        m_RigidBody.AddForce(m_MovementDirection);
    }

    public void Rotate(float turn)
    {
        m_Trans.Rotate(0f, 0f, -turn * m_TurnSpeed * Time.deltaTime);
    }
}
