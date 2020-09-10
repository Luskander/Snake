using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Snake : MonoBehaviour
{
	public static Snake instance;
	private SpriteRenderer snakeHead;
	private Sprite snakeHeadSprite, snakeBodySprite, snakeVioletSprite, snakeBlueSprite, snakeKingSprite, snakeBodyViolet, snakeBodyBlue, snakeBodyKing;
	private enum Direction // перечисление направлений
	{
		Left,
		Right,
		Up,
		Down
	}

	private enum State // змейка либо жива, либо мертва
	{
		Alive,
		Dead
	}

	private State state;
	private Vector2Int gridPosition; // выставляем позицию по иксу и игреку
	private Direction gridMoveDirection; // хранит в себе направление
	private float gridMoveTimer; // содержит в себе время до последнего перемещения
	private float gridMoveTimerMax; // время между передвижениями
	private LevelGrid levelGrid; // объявляем переменную нашего уровня
	private int snakeBodySize; // создаем переменную , которая будет отвечать за размер нашей змейки
	private List<SnakeMovePosition> snakeMovePositionList; // список, чтобы добавлять к нашей голове туловище (по сути содержит в себе список из количества тел)
	private List<SnakeBodyPart> snakeBodyPartList; // список содержащий в себе трансформацию (перемещение) тела нашей змейки

	public void Setup(LevelGrid levelGrid) // добавляем две простые функции, что бы синхронизировать их в разных скриптах
	{
		this.levelGrid = levelGrid;
	}

	private void Awake()
	{
		instance = this;
		gridPosition = new Vector2Int(10, 10); // ставим при запуске координаты 10, 10
		gridMoveTimerMax = 0.1f; // змейка будет двигаться каждые 0.3 секунды
		gridMoveTimer = gridMoveTimerMax; // приравниваем значения, что бы в дальнейшем использовать кнопки для перемещения
		gridMoveDirection = Direction.Right; // по умолчанию змейка двигается вправо

		snakeHeadSprite = Resources.Load<Sprite>("SnakeHead");
		snakeVioletSprite = Resources.Load<Sprite>("SnakeHeadViolet");
		snakeBlueSprite = Resources.Load<Sprite>("SnakeHeadBlue");
		snakeKingSprite = Resources.Load<Sprite>("SnakeHeadCrown");
		snakeBodySprite = Resources.Load<Sprite>("SnakeBody");
		snakeBodyViolet = Resources.Load<Sprite>("SnakeBodyViolet");
		snakeBodyBlue = Resources.Load<Sprite>("SnakeBodyBlue");
		snakeBodyKing = Resources.Load<Sprite>("SnakeBodyCrown");

		snakeHead = GetComponent<SpriteRenderer>();
		snakeHead.sprite = snakeHeadSprite;

		snakeBodySize = 0; // изначальный размер туловища змейки
		snakeMovePositionList = new List<SnakeMovePosition>(); // инициализируем список содержащий позицию змейки

		snakeBodyPartList = new List<SnakeBodyPart>(); // инициализируем список
		state = State.Alive; // изначально устанавливаем, что змейка жива
	}

	public void Change()
	{
		if (snakeHead != null)
		{
			if (snakeHead.sprite == snakeHeadSprite)
			{
				snakeHead.sprite = snakeVioletSprite;
			}
			else if (snakeHead.sprite == snakeVioletSprite)
			{
				snakeHead.sprite = snakeBlueSprite;
			}
			else if (snakeHead.sprite == snakeBlueSprite)
			{
				snakeHead.sprite = snakeKingSprite;
			}
			else if (snakeHead.sprite == snakeKingSprite)
			{
				snakeHead.sprite = snakeHeadSprite;
			}
		}
		else
		{
			snakeHead.sprite = snakeHeadSprite;
		}
	}

	private void Update() // тут активируем наши функции
	{
		switch(state) // свитч для состояния змейки, если жива, срабатывают функции, если мертва = игра заканчивается
		{
			case State.Alive:
				HandleInput();
				HandleGridMovement();
				break;
			case State.Dead:
				break;
		}
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.W)) // управление (каждый кадр проверяется нажатие WASD, если же кнопка была нажата, положение змейки меняется)
		{
			if (gridMoveDirection != Direction.Down) // позволяет исключить возможность поворачивать в обратную сторону от движения
			{
				gridMoveDirection = Direction.Up;
			}
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			if (gridMoveDirection != Direction.Up)
			{
				gridMoveDirection = Direction.Down;
			}
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			if (gridMoveDirection != Direction.Left)
			{
				gridMoveDirection = Direction.Right;
			}
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			if (gridMoveDirection != Direction.Right)
			{
				gridMoveDirection = Direction.Left;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) // управление (каждый кадр проверяется нажатие WASD, если же кнопка была нажата, положение змейки меняется)
		{
			if (gridMoveDirection != Direction.Down) // позволяет исключить возможность поворачивать в обратную сторону от движения
			{
				gridMoveDirection = Direction.Up;
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (gridMoveDirection != Direction.Up)
			{
				gridMoveDirection = Direction.Down;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (gridMoveDirection != Direction.Left)
			{
				gridMoveDirection = Direction.Right;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (gridMoveDirection != Direction.Right)
			{
				gridMoveDirection = Direction.Left;
			}
		}
	}
	private void HandleGridMovement()
	{
		gridMoveTimer += Time.deltaTime; // Time.deltaTime - время от последнего обновления 
		if (gridMoveTimer >= gridMoveTimerMax) // проверяем, если MoveTimer > MoveTimerMax, значит прошло больше 0.5 секунды с момента последнего нажатия
		{
			gridMoveTimer -= gridMoveTimerMax; // сбрасываем значения в нашем таймере

			SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

			SnakeMovePosition previousSnakeMovePosition = null; // задаем предыдущую позицию равной null
			if(snakeMovePositionList.Count > 0)
			{
				previousSnakeMovePosition = snakeMovePositionList[0];
			}
			SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
			snakeMovePositionList.Insert(0, snakeMovePosition); // прежде чем змейка переместится, вставляем в наш список ее позицию, и после этого змейка перемещается

			Vector2Int gridMoveDirectionVector;
			switch (gridMoveDirection) // варианты поворота туловища змейки
			{
				default:
				case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
				case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
				case Direction.Left: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
				case Direction.Right: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
			}

			gridPosition += gridMoveDirectionVector; // приравниваем позицию к направлению

			gridPosition = levelGrid.ValidateGridPosition(gridPosition);

			bool snakeAteFood = levelGrid.SnakeMoved(gridPosition); // после движения вызывается функция, которая отвечает за поедание яблока
			if (snakeAteFood) // если функция поедания еды вернула истину, то увеличиваем размер нашей змейки на 1
			{
				SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
				snakeBodySize++;
				CreateSnakeBody(); // вызваем функцию создания туловища
			}

			if (snakeMovePositionList.Count >= snakeBodySize + 1) // если количество элементов в списке больше, чем размер змейки + 1, то количество нашего списка уменьшается на 1
			{
				snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
			}
			foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList) // добавляем цикл проверки, если же мы пересеклись головой с туловищем, игра заканчивается
			{
				Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
				if (gridPosition == snakeBodyPartGridPosition)
				{
					// CMDebug.TextPopup("You are dead. Good game!", transform.position);
					SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
					state = State.Dead;
					GameHandler.SnakeDied();
				}
			}

			transform.position = new Vector3(gridPosition.x, gridPosition.y); // когда обновляется кадр, позиция меняется
			transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90); // изначально устанавливаем, что бы змейка смотрела в правильном направлении

			UpdateSnakeBodyParts();
		}
	}

	private void CreateSnakeBody()
	{
		snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count)); // добавляем в список еще одну часть туловища змейки
	}

	private void UpdateSnakeBodyParts()
	{
		for (int i = 0; i < snakeBodyPartList.Count; i++)
		{
			snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]); // выставляем позицию i элемента списка к списку перемещения головы
		}
	}

	private float GetAngleFromVector(Vector2Int dir) // функция поворота, взял из инета
	{
		float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		if (n < 0) n += 360;
		return n;
	}

	public Vector2Int GetGridPosition() // возвращаем текущую позицию змейки, используем ее для того, что бы исключить спавн яблока на змейке
	{
		return gridPosition;
	}

	public List<Vector2Int> GetFullSnakeGridPositionList() // возвращает полностью размер всей змейки (голова и туловище)
	{
		List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition }; // создаем список, который будет содержать в себе позицию головы
		foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
		{
			gridPositionList.Add(snakeMovePosition.GetGridPosition());
		}
		return gridPositionList; // возвращает список из туловища + головы
	}

	private class SnakeBodyPart // класс, содержащий в себе код относящийся к телу змейки
	{
		private SnakeMovePosition snakeMovePosition;
		private Transform transform;
		private static SnakeBodyPart instance;
		GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));

		public SnakeBodyPart(int bodyIndex)
		{
			snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite; // добавляем ему корректный спрайт, предварительно создав его в скрипте GameAssets 
			ChangeBody();
			snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex; // как я понял, это нужно для того, чтобы избежать различных глитчей в случае добавления большого кол-ва объектов туловища
			transform = snakeBodyGameObject.transform;
		}


		public void ChangeBody()
		{
			if (snakeBodyGameObject != null)
			{
				if (Snake.instance.snakeHead.sprite == Snake.instance.snakeHeadSprite)
				{
					snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = Snake.instance.snakeBodySprite;
				}
				else if (Snake.instance.snakeHead.sprite == Snake.instance.snakeVioletSprite)
				{
					snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = Snake.instance.snakeBodyViolet;
				}
				else if (Snake.instance.snakeHead.sprite == Snake.instance.snakeBlueSprite)
				{
					snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = Snake.instance.snakeBodyBlue;
				}
				else if (Snake.instance.snakeHead.sprite == Snake.instance.snakeKingSprite)
				{
					snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = Snake.instance.snakeBodyKing;
				}
			}
			else
			{
				snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite; // добавляем ему корректный спрайт, предварительно создав его в скрипте GameAssets 
			}
		}


		public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) // устанавливает позицию
		{
			this.snakeMovePosition = snakeMovePosition;
			transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

			float angle; // переменная, которая будет равна нашему углу
			switch (snakeMovePosition.GetDirection()) // поворот туловища змейки при перемещении
			{
				default:
				case Direction.Up:
					switch (snakeMovePosition.GetPreviousDirection())
					{
						default:
							angle = 180; break;
						case Direction.Left:
							angle = 180 - 45; break;
						case Direction.Right:
							angle = 180 + 45; break;
					}
					break;
				case Direction.Down:
					switch (snakeMovePosition.GetPreviousDirection())
					{
						default:
							angle = 0; break;
						case Direction.Left:
							angle = 0 + 45; break;
						case Direction.Right:
							angle = 0 - 45; break;
					}
					break;
				case Direction.Left:
					switch (snakeMovePosition.GetPreviousDirection())
					{
						default:
							angle = 90; break;
						case Direction.Down:
							angle = 45; break;
						case Direction.Up:
							angle = -45; break;
					}
					break;
				case Direction.Right:
					switch (snakeMovePosition.GetPreviousDirection())
					{
						default:
							angle = -90; break;
						case Direction.Down:
							angle = -45; break;
						case Direction.Up:
							angle = 45; break;
					}
					break;
			}
			transform.eulerAngles = new Vector3(0, 0, angle);
		}

		public Vector2Int GetGridPosition() // возвращаем направление змейки
		{
			if(snakeMovePosition == null) // исправляет баг, который при нуль значении крашил всю игру, проверяем, если наша позиция равна null, то возвращаем нулевую позицию
			{
				return new Vector2Int(0, 0);
			}
			else // в любом другом случае, возвращаем текущую позицию
				return snakeMovePosition.GetGridPosition();
		}
	}

	private class SnakeMovePosition // класс содержащий в себе одно направление змейки
	{
		private SnakeMovePosition previousSnakeMovePosition; // предыдущее положение тела змейки
		private Vector2Int gridPosition; // храним как и местонахождение змейки
		private Direction direction; // так и ее направление
		public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
		{
			this.previousSnakeMovePosition = previousSnakeMovePosition;
			this.gridPosition = gridPosition;
			this.direction = direction;
		}
		public Vector2Int GetGridPosition()
		{
			return gridPosition;
		}
		public Direction GetDirection()
		{
			return direction;
		}
		public Direction GetPreviousDirection() // тут мы проверяем, если же предыдущее положение тела не изменилось, возвращаем обычное правое, если изменилось, возвращаем предыдущее
		{
			if (previousSnakeMovePosition == null)
			{
				return Direction.Right;
			}
			else
			{
				return previousSnakeMovePosition.direction;
			}
		}
	}
}
