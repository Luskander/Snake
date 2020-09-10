using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PauseWindow : MonoBehaviour
{
	public static PauseWindow instance;

    private void Awake()
	{
		instance = this;


		transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // установили позицию для окна паузы на ноль
		transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

		transform.Find("resumeBtn").GetComponent<Button_UI>().ClickFunc = () => GameHandler.ResumeGame(); // вызывает функцию возврата к игре
		transform.Find("resumeBtn").GetComponent<Button_UI>().AddButtonSounds();

		transform.Find("changeBtn").GetComponent<Button_UI>().ClickFunc = () =>
		{
			Snake.instance.Change();
			SnakeExample.i.ChangeExample();
		};
		transform.Find("changeBtn").GetComponent<Button_UI>().AddButtonSounds();

		transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () =>
		{
			Loader.Load(Loader.Scene.MainMenu); // загружает окно главного меню
			Time.timeScale = 1f; // исправляет баг, заключающийся в том, что когда нажимаешь на главное меню из паузы и потом возвращаешься обратно в игру, змейка останавливалась
		};
		transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();

		Hide();
	}

	// используем функции из GameOverWindow для показа и скрытия определенных окон
	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}

	public static void ShowStatic()
	{
		instance.Show();
	}

	public static void HideStatic()
	{
		instance.Hide();
	}
}
