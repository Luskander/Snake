using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class LevelGrid
{
	private Vector2Int foodGridPosition; // место расположения еды
	private GameObject foodGameObject; 
	private int width; // ширина и высота для спавна еды
	private int height;
	private Snake snake;


	public LevelGrid(int width, int height) // функция для задания размера области для спавна яблок 
	{
		this.width = width;
		this.height = height;
	}

	public void Setup(Snake snake) // добавляем две простые функции, что бы синхронизировать их в разных скриптах
	{
		this.snake = snake;
		SpawnFood(); // вызываем нашу функцию спавна еды
	}

	private void SpawnFood()
	{
		do // как только яблоко должно заспавнится там же, где и змейка, мы исключаем это событие
		{
			foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height)); // создаем расположение еды
		} while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1); // продолжаем создавать еду до тех пор, пока она не пересечется с нашим списком, который содержит в себе расположение нашей змейки. 
		// Простыми словами, еда спавнится везде в радиусе карты, где нет змейки
		

		foodGameObject = new GameObject("Food", typeof(SpriteRenderer)); // создали объект еда со спрайтом (нашим яблоком)
		foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite; // добавляет картинку нашего яблока
		foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y); // установили расположение
	}

	public bool SnakeMoved(Vector2Int snakeGridPosition) // функция передвижения и поедания еды змейкой
	{
		if (snakeGridPosition == foodGridPosition) // если змейка попадает на яблоко, то оно уничтожается, добавляет + к общему счету, спавнит еще одно, и возвращает истину, которая в последствии увеличивает тело нашей змейки
		{
			Object.Destroy(foodGameObject);
			Score.AddScore();
			SpawnFood();
			return true;
		}
		else
			return false;
	}

	public Vector2Int ValidateGridPosition(Vector2Int gridPosition) // позиции поворота относительно местоположения
	{
		if (gridPosition.x < 0)
		{
			gridPosition.x = width;
		}
		if (gridPosition.x > width)
		{
			gridPosition.x = 0;
		}

		if (gridPosition.y < 0)
		{
			gridPosition.y = height;
		}
		if (gridPosition.y > height)
		{
			gridPosition.y = 0;
		}
		return gridPosition;
	}
}
