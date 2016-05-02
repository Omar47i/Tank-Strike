using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(TankMotor))]
public class UserControls : MonoBehaviour {

    // cash some variables for effecient access
    private float m_VerAxis;
    private float m_HorAxis;

    private TankMotor m_TankMotor;

    void Awake()
    {
        m_TankMotor = GetComponent<TankMotor>();
    }

    void FixedUpdate()
    {
        // Get the movement and turn axis values
        m_VerAxis = CrossPlatformInputManager.GetAxisRaw(Tags.ver);
        m_HorAxis = CrossPlatformInputManager.GetAxisRaw(Tags.hor);

        m_TankMotor.Move(m_VerAxis, m_HorAxis);
    }
}
