using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ChangeSprite : MonoBehaviour
{
	public static ChangeSprite i;
	private SpriteRenderer snakeHead;
	private Sprite monkaSprite, coolSprite, snakeHeadSprite;

	public void Awake()
	{
		i = this;

		monkaSprite = Resources.Load<Sprite>("monkaSprite");
		coolSprite = Resources.Load<Sprite>("coolSprite");
		snakeHeadSprite = Resources.Load<Sprite>("SnakeHead");

		snakeHead = null;
		snakeHead = GetComponent<SpriteRenderer>();
		snakeHead.sprite = snakeHeadSprite;
	}

	public void Change()
	{
		if (snakeHead != null)
		{
			if (snakeHead.sprite == snakeHeadSprite)
			{
				snakeHead.sprite = monkaSprite;
			}
			else if (snakeHead.sprite == monkaSprite)
			{
				snakeHead.sprite = coolSprite;
			}
			else if (snakeHead.sprite == coolSprite)
			{
				snakeHead.sprite = snakeHeadSprite;
			}
		}
		else
		{
			snakeHead.sprite = snakeHeadSprite;
		}
	}
}

		// transform.Find("Button").GetComponent<Button_UI>().ClickFunc = () => snakeHead.sprite = GameAssets.i.foodSprite;