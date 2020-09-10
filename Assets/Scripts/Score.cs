using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
	public static event EventHandler OnHighScoreChanged;
	private static int CurrentScore; // переменная, отвечающая за текущий счет
	private static int TotalScore; // переменная, отвечающая за общий счет

	public static void IntialazeStatic()
	{
		TotalScore = PlayerPrefs.GetInt("total");
		OnHighScoreChanged = null;
		CurrentScore = 0;
	}

	public static void AddScore() // функция добавления счета
	{
		CurrentScore += 100;
		TotalScore += 100;
		PlayerPrefs.SetInt("total", TotalScore);
	}

	public static int GetTotalScore() // возвращаем тотальный счет для последующего вывода на экран
	{
		return TotalScore;
	}

	public static int GetCurrentScore() // возвращает текущий счет
	{
		return CurrentScore;
	}

	public static int GetHighScore()
	{
		return PlayerPrefs.GetInt("highscore", 0); // устанавливаем значение 0 для наивысшего счета
	}

	public static bool TrySetNewHighScore() // функция возвращающая наилучший счет (в случае проверки)
	{
		return TrySetNewHighScore(CurrentScore);
	}

		public static bool TrySetNewHighScore(int score) // функция возвращающая счет, при этом проверяющая, является ли текущий счет выше наилучшего
	{
		int highscore = GetHighScore();
		if (score > highscore)
		{
			PlayerPrefs.SetInt("highscore", score);
			PlayerPrefs.Save();
			if (OnHighScoreChanged != null)
				OnHighScoreChanged(null, EventArgs.Empty);
			return true;
		}
		else { 
				return false;
			 }
	}
}
