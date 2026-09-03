using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    public Joystick joystick;

    public bool brakePressed;

    public float Horizontal
    {
        get
        {
            float keyboard = Input.GetAxis("Horizontal");

            if (Mathf.Abs(joystick.Horizontal) > 0.1f)
            {
                return joystick.Horizontal;
            }

            return keyboard;
        }
    }

    public float Vertical
    {
        get
        {
            float keyboard = Input.GetAxis("Vertical");

            if (Mathf.Abs(joystick.Vertical) > 0.1f)
            {
                return joystick.Vertical;
            }

            return keyboard;
        }
    }

    public bool Brake
    {
        get
        {
            return brakePressed || Input.GetKey(KeyCode.Space);
        }
    }

    public void SetBrake(bool value)
    {
        brakePressed = value;
    }
}