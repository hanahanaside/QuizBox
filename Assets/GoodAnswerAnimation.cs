using UnityEngine;
using System.Collections;

public class GoodAnswerAnimation : MonoBehaviour
{

	void OnAnimationFinished ()
	{
		Destroy (gameObject);
	}

}
