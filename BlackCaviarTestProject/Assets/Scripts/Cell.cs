using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //Скрипт каждой клетки поля, с его помощью отслеживается взаимодейсвтие игрока с игровым полем.
    [SerializeField] private SpriteRenderer _renderer; //Передаём Renderer, чтобы изменять цвет клетки.
    [SerializeField] private Gold _gold; //Золотые слитки
    [SerializeField] private int _depth; //Глубина клетки
    [SerializeField] private float _chanceOfFinding; //Шанс генерации золотого слитка на каждом уровне клетки
    private GameManagerScript _gameManager; //Скрипт, с помощью которого ведется подсчёт очков и лопаток.

    private Color _grassColor, _groundColor, _stoneColor, _cellColor, _colorDelta;  //Используемые цвета, цвет клетки, и Дельта цвета.
    private bool _isGrass = true, _goldenIngotOnCell = false; //Находимся ли мы на первом уровне глубины(на траве), находится ли на клетке золотой слиток.
    private void Start(){
        _grassColor = new Color(0.05f, 0.205f, 0.05f);
        _groundColor = new Color(0.212f, 0.123f, 0.074f);
        _stoneColor = new Color(0.130f, 0.136f, 0.134f);
        _cellColor = _grassColor;
        SetCellColor(_cellColor);
        SetColorDelta();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }
    private void SetCellColor(Color color){
        _renderer.color = color;
    }

    //Дельта цвета нужна для того, чтобы клетки разной глубины отличались друг от друга, независимо от того,
    //какая максимальная глубина задана в инспекторе.
    private void SetColorDelta(){
        Color deepGround = new Color(0.058f, 0.046f,0.036f);
        _colorDelta.g = (deepGround.g - _groundColor.g)/_depth;
        _colorDelta.r = (deepGround.r - _groundColor.r)/_depth; 
        _colorDelta.b = (deepGround.b - _groundColor.b)/_depth;  
        _colorDelta.a = 1.0f;
    }

    private Color AmountOfColor(Color a, Color b){
            a.r+=b.r;
            a.g+=b.g;
            a.b+=b.b;
        return a;
    }

    private void SpawnGoldenIngot(){
        Vector2 position = gameObject.transform.position;
        Gold gold = Instantiate(_gold, position, Quaternion.identity, gameObject.transform);
        _goldenIngotOnCell = true;
    }
    private void OnMouseEnter(){
        Color hoverColor = new Color(0.05f,0.05f,0.05f);
        if(_cellColor!=_stoneColor && !_gameManager.GetIsPaused())
            SetCellColor(AmountOfColor(_cellColor, hoverColor));
    }

    private void OnMouseExit(){
        SetCellColor(_cellColor);
    }

    //Скрипт отслеживающий тапы на клетки
    private void OnMouseDown(){
        if(!_gameManager.GetIsPaused())
            if(!_goldenIngotOnCell)
                if(_depth > 1){
                    if(!_isGrass){
                        _gameManager.DecreaseNumberOfShovels();
                        _cellColor = AmountOfColor(_cellColor, _colorDelta);
                        SetCellColor(_cellColor);
                        _depth--;
                        if(Random.Range(0.0f, 1.0f) < _chanceOfFinding)
                            SpawnGoldenIngot();
                    }else ChangeGrassToGround();
                } else if(_depth == 1){
                    ChangeGroundToStone();
                }
    }

    public void SetGoldenIngotOnCell(bool temp){
        _goldenIngotOnCell = temp;
    }

    //Переход с уровня травы на уровни земли
    private void ChangeGrassToGround(){
        _gameManager.DecreaseNumberOfShovels();
        _cellColor = _groundColor;
        SetCellColor(_cellColor);
        _isGrass = false;
        _depth--;
        if(Random.Range(0.0f, 1.0f) < _chanceOfFinding)
            SpawnGoldenIngot();
    }
    
    //Переход с уровня земли на уровень камня. (На уровне камня копать нельзя)
    private void ChangeGroundToStone(){
        _gameManager.DecreaseNumberOfShovels();
        _cellColor = _stoneColor;
        SetCellColor(_cellColor);
        _depth--;
        if(Random.Range(0.0f, 1.0f) < _chanceOfFinding)
            SpawnGoldenIngot();
    }

}
