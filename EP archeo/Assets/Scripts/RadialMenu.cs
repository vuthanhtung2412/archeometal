using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    [Header("Scene")]
    public Transform selectionTransform = null;
    public Transform cursorTransform = null;

    [Header("Events")]
    public RadialSection top = null;
    public RadialSection right = null;
    public RadialSection bottom = null;
    public RadialSection left = null;

    private Vector2 touchPosition = Vector2.zero;
    private List<RadialSection> radialSections = null;
    private RadialSection highlightedSection = null;

    private readonly float degreeIncrement = 90.0f;

    private void Awake()
    {
        CreateAndSetupSections();
    }

    private void CreateAndSetupSections()
    {
        radialSections = new List<RadialSection>()
        {
            top,
            right,
            bottom,
            left
        };

        foreach (RadialSection section in radialSections)
        {
            section.iconRenderer.sprite = section.icon;
        }
    }
    private void Start()
    {
        Show(false);
    }
    private void Update()
    {
        Vector2 dir = Vector2.zero + touchPosition;
        float rotation = GetDegree(dir);

        SetCursorPosition();
        SetSelectionRotation(rotation);
        SetSelectedEvent(rotation);
    }
    private float GetDegree(Vector2 dir)
    {
        float value = Mathf.Atan2(dir.x, dir.y);
        value *= Mathf.Rad2Deg;
        if(value < 0)
        {
            value += 360.0f;
        }
        return value;
    }
    private void SetCursorPosition()
    {
        cursorTransform.localPosition = touchPosition;
    }
    private void SetSelectionRotation(float r)
    {
        float snappedRo = SnapRotation(r);
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRo);
    }
    private float SnapRotation(float r)
    {
        return GetNearestIncrement(r) * degreeIncrement;
    }
    private int GetNearestIncrement(float r)
    {
        return Mathf.RoundToInt(r / degreeIncrement);
    }
    private void SetSelectedEvent(float ro)
    {
        int index = GetNearestIncrement(ro);
        if (index == 4) 
            index = 0;

        highlightedSection = radialSections[index];
    }
    public void SetTouchPosition(Vector2 v)
    {
        touchPosition = v;
    }
    public void Show(bool v)
    {
        gameObject.SetActive(v);
    }
    public void HightlightSection()
    {
        highlightedSection.onClick.Invoke();
    }
}
