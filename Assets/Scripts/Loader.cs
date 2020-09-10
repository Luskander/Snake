using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
	public enum Scene // перечисление всех существующих сцен
	{
		GameScene,
		Loading,
		MainMenu,
	}

	private static Action loaderCallBackAction; // создаем функцию действия

	public static void Load(Scene scene)
	{
		// добавили отдельную сцену загрузки, игра будет грузить сначала ее, а после уже нашу основную сцену. 
		// Необходимо для исправления того, что если у нас будет огромное кол-во объектов, то загрузка будет непозволительно длинной

		loaderCallBackAction = () =>
		{
			SceneManager.LoadScene(scene.ToString()); // загружает нашу сцену после загрузки загрузочной сцены
		};

		SceneManager.LoadScene(Scene.Loading.ToString());
	}

	public static void LoaderCallBack() // которая в свою очередь проверяет, если же наша функция действия не равна null, то вызывает ее и приравнивает ее к null
	{
		if(loaderCallBackAction != null)
		{
			loaderCallBackAction();
			loaderCallBackAction = null;
		}
	}
}
