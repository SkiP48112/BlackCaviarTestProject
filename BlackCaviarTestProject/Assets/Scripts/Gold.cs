using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private GameManagerScript _gameManager;

    private void Start(){
       _gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }

    //Отслеживаем нажатие на золотой слиток
    private void OnMouseDown()
    {
        Destroy(gameObject);    //Уничтожаем ответ.
        GetComponentInParent<Cell>().SetGoldenIngotOnCell(false);   //Передаём, что на клетке нет золотого слитка.
        _gameManager.IncreaseScore();   //Увеличиваем счёт.
    }
}
