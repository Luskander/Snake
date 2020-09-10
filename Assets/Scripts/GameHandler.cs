using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
	private static GameHandler instance; // создаем экземпляр класса для взаимодействия в других скриптах

	[SerializeField] private Snake snake; // переводит некие данные в формат, который можно передавать, загружать, что бы использовать в других скриптах

	private LevelGrid levelGrid; // по сути - расположение всего нашего уровня

	private void Awake()
	{
		instance = this;
		Score.IntialazeStatic(); // вызываем функцию из Score, которая устанавливает значения для текущего и наилучшего счета
	}

	private void Start()
	{
		Debug.Log("GameHandler.Start");

		levelGrid = new LevelGrid(20, 20); // задали значения спавна 20 по иксу и игреку, так как наш фон как раз такого размера

		snake.Setup(levelGrid); // вызываем наши функции, которые позволяют согласовывать наши функции в разных скриптах
		levelGrid.Setup(snake);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) // когда нажимаем эскейп проверяем, если игра уже стоит на паузе, возобновляет ее, если же нет, останавливает
			if(isGamePaused())
			{
				GameHandler.ResumeGame();
			} else
				GameHandler.PauseGame();
	}

	public static void SnakeDied() // показывает окно повторного запуска игры
	{
		bool isNewHighscore = Score.TrySetNewHighScore(); // передаем наш наилучший счет в булевскую переменную, которая в итоге на окне смерти будет или не будет выводить новый лучший счет
		GameOverWindow.ShowStatic(isNewHighscore);
		ScoreWindow.HideStatic();
	}

	public static void ResumeGame() // перезапускает игру
	{
		PauseWindow.HideStatic();
		Time.timeScale = 1f;
	}

	public static void PauseGame() // останавливает игру
	{
		PauseWindow.ShowStatic();
		Time.timeScale = 0f; // останавливает время в буквальном смысле, так как мы используем deltatime для движения нашей змейки
	}

	public static bool isGamePaused() // проверка, стоит ли игра на паузе
	{
		return Time.timeScale == 0f; 
	}
}
