using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
	public static GameAssets i; // добавляем ссылку на использоваение паблик спрайтов	

	private void Awake()
	{
		i = this;
	}
		
	public Sprite snakeHeadSprite; // добавляем спрайты
	public Sprite foodSprite;
	public Sprite snakeBodySprite;
	public Sprite monkaSprite;

	public AudioClip snakeMove; // звуки

	public SoundAudioClip[] soundAudioClipArray; // массив, содержащий в себе все элементы звуков для игры

	[Serializable]
	public class SoundAudioClip
	{
		public SoundManager.Sound sound;
		public AudioClip audioClip;
	}
}
