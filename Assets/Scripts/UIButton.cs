using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Color highlightColor = Color.cyan;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;

    [SerializeField] private Vector3 buttonHoverScaleVector = new Vector3(0.3f,0.3f,1.0f);
    [SerializeField] private Vector3 buttonReleaseScaleVector = new Vector3(1.0f,1.0f,1.0f);

    private void Awake()
    {
        //SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
            _spriteRenderer.color = highlightColor;
    }
    private void OnMouseExit()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
            _spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        transform.localScale = buttonHoverScaleVector;
    }
    private void OnMouseUp()
    {
        transform.localScale = buttonReleaseScaleVector;
        if (targetObject != null)
            targetObject.SendMessage(targetMessage);
    }
}
