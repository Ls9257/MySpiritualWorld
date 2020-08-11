using UnityEngine;
using EZFramework;

namespace EZFramework
{
	public class InIt : MonoBehaviour
	{

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoad()
		{

			EZModel.BaseParent = new GameObject("[Application]", typeof(InIt));
			DontDestroyOnLoad(EZModel.BaseParent);
			AddModel();

		}

		private static void AddModel()
		{
            EZModel.AddModel<UIModel>();

		}
	}
}
