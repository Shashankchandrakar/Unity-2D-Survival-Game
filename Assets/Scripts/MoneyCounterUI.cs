using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour {

		[SerializeField]
		private Text moneyCount;

		void Awake()
		{
		moneyCount = GetComponent<Text> ();

		}

		
		// Update is called once per frame
		void Update ()
		{
			moneyCount.text="Money: "+GameMaster.Money.ToString()+" $";
		}


}
