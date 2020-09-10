using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public static class SoundManager
{
	public enum Sound // перечисление звуков
	{
		SnakeMove,
		SnakeEat,
		SnakeDie,
		ButtonHower,
		ButtonClick,
	}

    public static void PlaySound(Sound sound) // функция для добавления звуков в игру
	{
		GameObject soundGameObject = new GameObject("Sound");
		AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
		audioSource.PlayOneShot(GetAudioClip(sound));
	}

	private static AudioClip GetAudioClip(Sound sound) // функция для вызова звуков
	{
		foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
		{
			if(soundAudioClip.sound == sound)
			{
				return soundAudioClip.audioClip;
			}
		}
		Debug.LogError("Sound " + sound + " not found!");
		return null;
	}

	public static void AddButtonSounds(this Button_UI button_UI) // функция для добавления звука всем кнопками
	{
		button_UI.MouseOverOnceFunc += () => SoundManager.PlaySound(Sound.ButtonHower); // звук для наведения на кнопку
		button_UI.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonClick); // звук для нажатия на кнопку
	}
}
