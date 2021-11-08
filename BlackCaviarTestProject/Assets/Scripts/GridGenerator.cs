using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //Скрипт, отвечающий за генерацию игрового поля
    [SerializeField] private Vector2Int _gridSize; //Размер создаваемого поля
    [SerializeField] private Cell _cell; // Клетки поля
    [SerializeField] private float _gap; //Расстояние между клетками поля
    [SerializeField] private Transform _parent; //Объект, в котором будут слкадироваться созданные клетки(Grid Cells)
    //Вызывается из инспектора, для того чтобы создать поле
    [ContextMenu("Generate New Grid")]
    private void GenerateGrid(){
        var cellsize = _cell.GetComponent<SpriteRenderer>().bounds.size;
        
        for(int x = 0; x < _gridSize.x; x++){
            for(int y = 0; y < _gridSize.y; y++){
                Vector2 position = new Vector2(x * (cellsize.x + _gap), y * (cellsize.y + _gap)); //Проверить
                Cell cell = Instantiate(_cell, position, Quaternion.identity, _parent);
                cell.name = $"X: {x} Y : {y}";
            }
        }
    }
}
