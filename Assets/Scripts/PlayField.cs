using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayField : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private Square[] _squares;
    private int _redCount;
    private int _blueCount;
    private void Awake()
    {
        var count = transform.childCount;
        _squares = new Square[count];
        for (int i = 0; i < count; i++)
        {
            _squares[i] = transform.GetChild(i).GetComponentInChildren<Square>();    
            _squares[i].OnSquareColorChange += OnSquareColorChange;
            if (_squares[i].SquareColor == SquareColor.Red)
                _redCount++;
            else if (_squares[i].SquareColor == SquareColor.Blue)
                _blueCount++;

        }
    }

    private void OnSquareColorChange(SquareColor newColor, SquareColor previousColor)
    {
        if (previousColor == SquareColor.Blue)
        {
            _blueCount--;
        }
        else if (previousColor == SquareColor.Red)
        {
            _redCount--;
        }
        
        if (newColor == SquareColor.Blue)
        {
            _blueCount++;
        }
        else if (newColor == SquareColor.Red)
        {
            _redCount++;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"<color=#ff0000> Red: {_redCount}</color> - <color=#0000ff>Blue: {_blueCount}</color>";
    }
}
