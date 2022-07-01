using UnityEngine;
using UnityEngine.EventSystems;

public class OnScreenControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerTankControl _ptc;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Left")
        {
            _ptc._dirY = -1;
            _ptc._dirZ = 0;
        }
        else if (gameObject.name == "Right")
        {
            _ptc._dirY = 1;
            _ptc._dirZ = 0;
        }
        else if (gameObject.name == "Up")
        {
            _ptc._dirY = 0;
            _ptc._dirZ = 1;
        }
        else if (gameObject.name == "Down")
        {
            _ptc._dirY = 0;
            _ptc._dirZ = -1;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _ptc._dirY = 0;
        _ptc._dirZ = 0;
    }
}
