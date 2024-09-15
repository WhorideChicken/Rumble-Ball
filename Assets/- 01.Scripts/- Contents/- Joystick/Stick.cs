using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine;
using UnityEngine.UI;

public class Stick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public MainPlayer mainChar;
    public Image direction;
    private Vector2 JoyVec;
    float Radius = 40.0f;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);

        float distance = Vector2.Distance(position, m_PointerDownPos);
        JoyVec = (position - m_PointerDownPos).normalized;
        if (distance < Radius)
        {
            this.transform.localPosition = position;
        }
        else
        {
            this.transform.localPosition = Vector2.zero + JoyVec * Radius;
        }

        var delta = position - m_PointerDownPos;

        Vector2 gap = delta;
        direction.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(gap.y, gap.x) * Mathf.Rad2Deg - 90.0f);
        var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);

        mainChar.SetVector(newPos);
        //SendValueToControl(newPos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction.transform.localPosition = Vector2.zero;
        direction.transform.localRotation = Quaternion.identity;
        this.transform.localPosition = Vector2.zero;
        mainChar.SetVector(Vector2.zero);
        this.transform.parent.gameObject.SetActive(false);
       //SendValueToControl(Vector2.zero);
    }


    [FormerlySerializedAs("movementRange")]
    [SerializeField]
    private float m_MovementRange = 0;

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    public float movementRange
    {
        get => m_MovementRange;
        set => m_MovementRange = value;
    }


    private Vector3 m_StartPos;
    private Vector2 m_PointerDownPos;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;

    }
}
