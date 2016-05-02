using UnityEngine;

public class TankMotor : MonoBehaviour {

    [SerializeField]
    private float m_MoveSpeed = 5000f;      // the force applied to the tank to move
    [SerializeField]
    private float m_TurnSpeed = 120f;       // the turn speed of the player's tank     
    //[SerializeField]
    //private float m_Damping = 0.98f;      // apply damping to tank movement

    private Vector3 m_MovementDirection;    // this vector will store the movement direction of the tank's player
    [HideInInspector]
    public bool m_IsMovingBack = false;     // is the player is moving back?

    // cash variables
    private Transform m_Trans;
    private Rigidbody2D m_RigidBody;

    void Awake()
    {
        m_Trans = transform;
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(float move, float turn)
    {
        if (move < 0f)
            m_IsMovingBack = true;
        else
            m_IsMovingBack = false;

        // get the movement direction of the tank
        m_MovementDirection = m_Trans.TransformDirection(move * m_MoveSpeed, 0f, 0f);

        //m_RigidBody.MovePosition(m_Trans.position + m_MovementDirection);
        m_RigidBody.AddForce(m_MovementDirection);

        // turn the tank based on player's input
        if (!m_IsMovingBack)
            m_Trans.Rotate(0f, 0f, -turn * m_TurnSpeed * Time.deltaTime);
        else
            m_Trans.Rotate(0f, 0f, turn * m_TurnSpeed * Time.deltaTime);

        // check first if the new translation of the tank will not collide with anything
        //Move();
    }

    // Moves the player's tank with respect to collision
    //private void Move()
    //{
    //    // cast a ray from the center of the tank to the new predicted translated position.
    //    Vector3 newPos = m_Trans.position + m_MovementDirection;
    //    Vector3 frontPos = m_Trans.TransformDirection(newPos + new Vector3(GetComponent<BoxCollider2D>().bounds.extents.x, 0f, 0f));

    //    RaycastHit2D hit = Physics2D.Linecast(newPos, frontPos);

    //    Debug.DrawLine(newPos, frontPos, Color.red);
    //    if (hit == true)
    //    {
    //        print("Somthing will collide: " + hit.collider.name);
    //    }
    //    Debug.Log("New pos: " + newPos);
    //    Debug.Log("End point: " + newPos * GetComponent<BoxCollider2D>().bounds.extents.x);
    //    Debug.Log("Extents: " + GetComponent<BoxCollider2D>().bounds.extents);
    //    m_Trans.position = newPos;
    //}

}
