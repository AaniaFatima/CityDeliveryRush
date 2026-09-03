using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MobileInputManager mobileInput;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mobileInput != null)
        {
            mobileInput.SetBrake(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (mobileInput != null)
        {
            mobileInput.SetBrake(false);
        }
    }
}
