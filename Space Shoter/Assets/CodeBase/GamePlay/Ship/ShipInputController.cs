using UnityEngine;
using Common;

public class ShipInputController : MonoBehaviour
{
   public enum ControlMode
    {
        Keyboard,
        Mobile
    }

    

    [SerializeField] private ControlMode m_ControlMode;

    private VirtualGamePad m_VirtualGamepad;
    private SpaceShip m_TargetShip;

    public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

    public void Construct(VirtualGamePad virtualGamePad)
    {
        m_VirtualGamepad = virtualGamePad;
    }

    private void Start()
    {
        if(m_ControlMode == ControlMode.Keyboard)
        {
            m_VirtualGamepad.joystick.gameObject.SetActive(false);
            m_VirtualGamepad.mobileFirePrimary.gameObject.SetActive(false);
            m_VirtualGamepad.mobileFireSecondary.gameObject.SetActive(false);

        }
            
        else
        {
            m_VirtualGamepad.joystick.gameObject.SetActive(true);
            m_VirtualGamepad.mobileFirePrimary.gameObject.SetActive(true);
            m_VirtualGamepad.mobileFireSecondary.gameObject.SetActive(true);
        }
 
    }

    private void Update()
    {
        if(m_TargetShip == null) return;
        if(m_ControlMode == ControlMode.Keyboard) ControlKeyboard(); 
        if(m_ControlMode == ControlMode.Mobile) ControlMobile(); 
    }

    private void ControlMobile()
    {
        var dir = m_VirtualGamepad.joystick.Value;
        m_TargetShip.ThrustControl = dir.y;
        m_TargetShip.TorqueControl = -dir.x;

        if (m_VirtualGamepad.mobileFirePrimary.IsHold == true)
        {
            m_TargetShip.Fire(TurretMode.Primary);
        }

        if (m_VirtualGamepad.mobileFireSecondary.IsHold == true)
        {
            m_TargetShip.Fire(TurretMode.Secondary);
        }
    }

    private void ControlKeyboard()
    {
        float trust = 0;
        float torque = 0;

        if (Input.GetKey(KeyCode.UpArrow)) trust = 1.0f;
        if (Input.GetKey(KeyCode.DownArrow)) trust = -1.0f;
        if (Input.GetKey(KeyCode.LeftArrow)) torque = 1.0f;
        if (Input.GetKey(KeyCode.RightArrow)) torque = -1.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            m_TargetShip.Fire(TurretMode.Primary);
        }

        if (Input.GetKey(KeyCode.X))
        {
            m_TargetShip.Fire(TurretMode.Secondary);
        }

        m_TargetShip.ThrustControl = trust; 
        m_TargetShip.TorqueControl = torque; 
    }

}
