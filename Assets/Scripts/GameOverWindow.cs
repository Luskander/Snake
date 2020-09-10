using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOverWindow : MonoBehaviour
{
	private static GameOverWindow instance;

    private void Awake()
	{
		instance = this;

		transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () =>
		{
			Loader.Load(Loader.Scene.GameScene); // добавляет кнопку рестарта
		};
		transform.Find("retryBtn").GetComponent<Button_UI>().AddButtonSounds();

		transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () =>
		{
			Loader.Load(Loader.Scene.MainMenu); // загружает окно главного меню
		};
		transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();

		Hide();
	}

	private void Show(bool isNewHighScore) // передает значение true, когда мы умерли
	{
		gameObject.SetActive(true);

		transform.Find("newHighScoreText").gameObject.SetActive(isNewHighScore);
		transform.Find("highScoreText").gameObject.SetActive(isNewHighScore);

		transform.Find("currentScoreText").GetComponent<Text>().text = Score.GetCurrentScore().ToString();
		transform.Find("highScoreText").GetComponent<Text>().text = Score.GetHighScore().ToString();
	}

	private void Hide() // false, когда живы
	{
		gameObject.SetActive(false);
	}

	public static void ShowStatic(bool isNewHighScore) // функция для показа кнопки
	{
		instance.Show(isNewHighScore);
	}


}
