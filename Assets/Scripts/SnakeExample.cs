using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeExample : MonoBehaviour
{
	public static SnakeExample i;
	private SpriteRenderer snakeHead;
	private Sprite snakeHeadSprite, snakeVioletSprite, snakeBlueSprite, snakeKingSprite;

	public void Awake()
	{
		i = this;

		snakeHeadSprite = Resources.Load<Sprite>("SnakeHead");
		snakeVioletSprite = Resources.Load<Sprite>("SnakeHeadViolet");
		snakeBlueSprite = Resources.Load<Sprite>("SnakeHeadBlue");
		snakeKingSprite = Resources.Load<Sprite>("SnakeHeadCrown");

		snakeHead = GetComponent<SpriteRenderer>();
		snakeHead.sprite = snakeHeadSprite;
	}

	public void ChangeExample ()
	{
		if (snakeHead != null)
		{
			if (snakeHead.sprite == snakeHeadSprite)
			{
				snakeHead.sprite = snakeVioletSprite;
			}
			else if (snakeHead.sprite == snakeVioletSprite)
			{
				snakeHead.sprite = snakeBlueSprite;
			}
			else if (snakeHead.sprite == snakeBlueSprite)
			{
				snakeHead.sprite = snakeKingSprite;
			}
			else if (snakeHead.sprite == snakeKingSprite)
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
