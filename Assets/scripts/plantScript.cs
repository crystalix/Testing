using UnityEngine;
using System.Collections;

public class plantScript : MonoBehaviour 
{
	public float growLength;
	public string plantType;
		
	public enum States
	{
		Unplanted,
		Growing,
		Ready
	};
	
	public Texture2D[] textures;
	public States state = States.Unplanted;

	// Use this for initialization
	void Start () 
	{
		plantType = "";
		state = States.Unplanted;
	}
	
	IEnumerator Growing()
	{
		state = States.Growing;
		yield return new WaitForSeconds(growLength);
		
		switch(plantType)
		{
			case "carrot":
			this.gameObject.renderer.material.mainTexture = textures[4];break;
			case "wheat":
			this.gameObject.renderer.material.mainTexture = textures[5];break;
			case"turnip":
			this.gameObject.renderer.material.mainTexture = textures[5];break;
		}
		state = States.Ready;
	}
	
}
