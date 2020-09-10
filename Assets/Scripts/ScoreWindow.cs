using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
	private Text totalScoreText; // объявляем переменную totalScoreText типа текст отвечающую за глобальный счет
	private Text currentScoreText; // объявляем переменную currentScoreText типа текст отвечающий за текущий счет

	private static ScoreWindow instance;

	private void Awake()
	{
		instance = this;
		totalScoreText = transform.Find("totalScoreText").GetComponent<Text>(); // при запуске игры ищем текст на экране
		currentScoreText = transform.Find("currentScoreText").GetComponent<Text>(); // при запуске игры ищем текст на экране

		Score.OnHighScoreChanged += Score_OnHighScoreChanged;
		UpdateHighScore(); // вызываем функцию обновления
	}

	private void Score_OnHighScoreChanged(object sender, System.EventArgs e) // потом разберусь, не совсем понимаю, что делает
	{
		UpdateHighScore();
	}

	private void UpdateHighScore() // обновляет наилучший счет на экране игры
	{
		int highScore = Score.GetHighScore();
		transform.Find("highScoreText").GetComponent<Text>().text = highScore.ToString();
	}

	private void Update()
	{
		totalScoreText.text = Score.GetTotalScore().ToString(); // каждый кадр обновляем значение для текста из функции GetScore, при этом переведя ее в строку
		currentScoreText.text = Score.GetCurrentScore().ToString();
	}

	public static void HideStatic() // изначально прячем окно со счетом
	{
		instance.gameObject.SetActive(false);
	}
}
