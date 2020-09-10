using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
	private bool firstUpdate = true;

    private void Update()
    {
        if(firstUpdate)
		{
			firstUpdate = false;
			Loader.LoaderCallBack(); // после выполнения функции действия в скрипте Loader вызывается функция LoaderCallBack
		}
    }
}
