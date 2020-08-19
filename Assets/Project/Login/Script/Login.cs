using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Login : BaseUI
{

	private Button m_opengame;

	public void Start()
	{

		m_opengame = transform.Find("OpenGame_btn").GetComponent<Button>();

	}

	public void Update()
	{

	}

}
