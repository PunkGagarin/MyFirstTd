using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour
{


	public Text moneyText;

	// Update is called once per frame
	void Update()
	{
		moneyText.text = "$" + PlayerStats.Money.ToString();
	}
}
