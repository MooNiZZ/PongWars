using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SquareColor
{
    Red,
    Blue,
    Yellow,
    Green
}

public class Square : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color _redColor;
    [SerializeField] private Color _blueColor;
    [SerializeField] private SquareColor _squareColor;
    public SquareColor SquareColor => _squareColor;
    
    public event Action<SquareColor, SquareColor> OnSquareColorChange; 
    
    private int _redLayer;
    private int _blueLayer;
    private int _layerType;
    

    // Start is called before the first frame update
    void Awake()
    {
        _redLayer = LayerMask.NameToLayer("RedSide");
        _blueLayer = LayerMask.NameToLayer("BlueSide");
        
        _layerType  = gameObject.layer;
        _squareColor = _layerType == _redLayer ? SquareColor.Red : SquareColor.Blue;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;
        
        ChangeLayer();
        ChangeColorToLayerColor();
        ChangeSquareColor();
    }

    private void ChangeLayer()
    {
        if (_layerType == _redLayer)
            _layerType = _blueLayer;
        else
            _layerType = _redLayer;
        
        gameObject.layer = _layerType;

    }

    private void ChangeColorToLayerColor()
    {
        _spriteRenderer.color = _layerType == _redLayer ? _redColor : _blueColor;
    }

    private void ChangeSquareColor()
    {
        var previousColor = _squareColor;
        _squareColor = _layerType == _redLayer ? SquareColor.Red : SquareColor.Blue;
        OnSquareColorChange?.Invoke(_squareColor, previousColor);

    }
}
