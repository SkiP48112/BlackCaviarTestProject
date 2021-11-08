using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private int _shovels;  //Количество лопаток
    [SerializeField] private int _gameGoal; //Цель игры
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _shovelsText;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;


    private int _currentScore;  //Нынешний счёт
    private bool _isPaused; //Остановлена ли игра
    private void Start()
    {
        _currentScore = 0;
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
        _isPaused = false;
    }

    private void Update(){
        _scoreText.text = $"Золото: {_currentScore.ToString()}/{_gameGoal.ToString()}";
        _shovelsText.text = $"Лопатки: {_shovels.ToString()}";
        if(_shovels <= 0 && !_isPaused) 
            Lose();
        if(_currentScore == _gameGoal && !_isPaused)
            Win();
    }

    private void Win(){
        _winPanel.SetActive(true);
        _isPaused = true;
    }

    private void Lose(){
        _losePanel.SetActive(true);
        _isPaused = true;
    }
    public void IncreaseScore(){
        _currentScore++;
        Debug.Log(_currentScore);
    }

    public bool GetIsPaused(){
        return _isPaused;
    }

    public void DecreaseNumberOfShovels(){
        _shovels--;
        Debug.Log(_shovels);
    }
}
