using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;	

public class MainMenuWindow : MonoBehaviour
{
	private enum Sub // добавляем перечисление, содержащее в себе два окна с главным меню и с меню, как играть
	{
		Main,
		HowToPlayWindow,
	}

	private void Awake()
	{
		transform.Find("howToPlaySub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // устанавливаем координаты равными нулю для обоих окон
		transform.Find("mainSub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

		transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => // при нажатии на кнопку играть запускается сцена GameScene (которая является основной, в которой содержится все наша игра)
		{
			Loader.Load(Loader.Scene.GameScene);
		};
		transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().AddButtonSounds();

		transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => Application.Quit(); // при нажатии на кнопку quit, просто закрываем приложение
		transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().AddButtonSounds();

		transform.Find("mainSub").Find("howToPlayBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.HowToPlayWindow); // нажимая на How to play активируем окно HowToPlayWindow
		transform.Find("mainSub").Find("howToPlayBtn").GetComponent<Button_UI>().AddButtonSounds();

		transform.Find("howToPlaySub").Find("BackBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.Main); // кнопка для возвращения в главное меню из How to play
		transform.Find("howToPlaySub").Find("BackBtn").GetComponent<Button_UI>().AddButtonSounds();

		ShowSub(Sub.Main); // показываем окно главного меню
	}

	private void ShowSub(Sub sub)
	{
		transform.Find("mainSub").gameObject.SetActive(false); // изначально устанавливаем значения активности на false, чтобы окна не показывались
		transform.Find("howToPlaySub").gameObject.SetActive(false);

		switch (sub) {
		case Sub.Main:
			transform.Find("mainSub").gameObject.SetActive(true); // в случае окна Main присваиваем значение true и показываем его
			break;
		case Sub.HowToPlayWindow:
			transform.Find("howToPlaySub").gameObject.SetActive(true); // аналогично и в случае с HowToPlay
			break;
		}
	}
}
