using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;

    public delegate void OnBlockClicked(int index);
    private OnBlockClicked _onBlockClicked;

    public enum MarkerType { None, O, X }
    
    // Block Index
    private int _blockIndex;
    
    private SpriteRenderer _spriteRenderer;
    private Color _defaultBlockColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultBlockColor = _spriteRenderer.color;
    }

    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        SetBlockColor(_defaultBlockColor);
        _onBlockClicked = onBlockClicked;
    }
    
    // 2. 마커 ?�정
    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.None:
                markerSpriteRenderer.sprite = null;
                break;
            case MarkerType.O:
                markerSpriteRenderer.sprite = oSprite;
                break;
            case MarkerType.X:
                markerSpriteRenderer.sprite = xSprite;
                break;
        }
    }
    
    public void SetBlockColor(Color color)
    {
        _spriteRenderer.color = color;
    }
    
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        Debug.Log("Selected Block: " + _blockIndex);
        
        _onBlockClicked?.Invoke(_blockIndex);
    }
}
