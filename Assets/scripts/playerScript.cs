using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour 
{
	public plantScript selectedPlant;
	public float carrotGrowLength;
	public float wheatGrowLength;
	public float turnipGrowLength;
	public int money;
	int exp;
	int level = 1;
	public Texture2D mytexture;
	
	public enum States
	{
		Idle,
		Planting,
		Harvesting,
		Selling
	}
	
	public States state = States.Idle;
	
	public int carrots;
	public int wheat;
	public int turnip;

	// Use this for initialization
	void Start () 
	{	
		state = States.Idle;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0)&& state == States.Idle)
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				if(hit.collider.gameObject.GetComponent<plantScript>())
				{
					selectedPlant = hit.collider.gameObject.GetComponent<plantScript>();
					if(selectedPlant.state == plantScript.States.Unplanted)
					{
						state = States.Planting;
						
					}
					else if(selectedPlant.state  == plantScript.States.Ready)
					{
						state = States.Harvesting;
						
					}
				}
			}
		}
	}
	
	public void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height),mytexture);
		
		if(state == States.Idle)
		{
			if(wheat>=1 || carrots>=1 || turnip>=1 && state != States.Selling)
			{
				if(GUI.Button(new Rect(Screen.width-150,0,100,50),"Sell Food?"))
				{
					state = States.Selling;
				}
			}
		}
		
		if(state == States.Selling)
		{
			GUILayout.BeginArea(new Rect(Screen.width-150, 0,100, 300));
					GUILayout.BeginVertical();
					if(carrots>=1)
					{
						if(GUILayout.Button("Sell Carrot?"))
						{
						carrots-=1;
						state = States.Idle;
						money= money+40;
						}
					}
					if(wheat>=1)
					{
						if(GUILayout.Button("Sell Wheat?"))
						{
						wheat-=1;
						state = States.Idle;
						money=money+20;
						}
					}
					if(turnip>=1)
					{
						if(GUILayout.Button("Sell Turnip?"))
						{
						turnip-=1;
						state = States.Idle;
						money=money+70;
						}
					}
			GUILayout.EndVertical();
			GUILayout.EndArea(); 
		}
			
		if(state == States.Planting)
		{
			GUILayout.BeginArea(new Rect(0, 0,150, 300));
			GUILayout.BeginVertical();
			if(GUILayout.Button("Carrot Seed: 20coins")&& money>=20)
			{
				money= money-20;
				selectedPlant.gameObject.renderer.material.mainTexture = selectedPlant.textures[1];
				selectedPlant.state = plantScript.States.Growing;
				selectedPlant.plantType = "carrot";
				selectedPlant.growLength = carrotGrowLength;
				state = States.Idle;
				selectedPlant.StartCoroutine("Growing");

			}
			if(GUILayout.Button("Wheat Seed:10coins")&& money>=10)
			{
				money = money-10;
				selectedPlant.gameObject.renderer.material.mainTexture = selectedPlant.textures[2];
				selectedPlant.state = plantScript.States.Growing;
				selectedPlant.plantType = "wheat";
				selectedPlant.growLength = wheatGrowLength;
				state = States.Idle;
				selectedPlant.StartCoroutine("Growing");
			}
			if(level>1)
			{
				if(GUILayout.Button("Turnip Seed:40coins")&& money>=40)
			{
				money = money-40;
				selectedPlant.gameObject.renderer.material.mainTexture = selectedPlant.textures[3];
				selectedPlant.state = plantScript.States.Growing;
				selectedPlant.plantType = "turnip";
				selectedPlant.growLength = turnipGrowLength;
				state = States.Idle;
				selectedPlant.StartCoroutine("Growing");
			}
			}
			if(GUILayout.Button("Cancel"))
			{
				selectedPlant.state = plantScript.States.Unplanted;
				state = States.Idle;
				selectedPlant.plantType = "";
			}
			GUILayout.EndVertical();
			GUILayout.EndArea(); 
		}
		else if(state == States.Harvesting)
		{
			GUILayout.BeginArea(new Rect(0, 0,100, 300));
			GUILayout.BeginVertical();
			if(GUILayout.Button("Harvest"))
			{
				selectedPlant.gameObject.renderer.material.mainTexture = selectedPlant.textures[0];
				selectedPlant.state = plantScript.States.Unplanted;
				state = States.Idle;
				switch(selectedPlant.plantType)
				{
				case "carrot":
					carrots+=1;
					exp+=20;break;
					
				case "wheat":
					wheat+=1;
					exp+=10;break;
				
				case"turnip":
					turnip+=1;
					exp+=50;break;
				}
			}
			if(GUILayout.Button("Scrap"))
			{
				selectedPlant.gameObject.renderer.material.mainTexture = selectedPlant.textures[0];
				selectedPlant.state = plantScript.States.Unplanted;
				state = States.Idle;
			}
			if(GUILayout.Button("Cancel"))
			{
				selectedPlant.state = plantScript.States.Ready;
				state = States.Idle;
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
		
		GUI.Label(new Rect(10, Screen.height-30,100,50),"Carrots:"+ carrots);
		GUI.Label(new Rect(100, Screen.height-30,100,50),"Wheat:"+ wheat);
		if(level>1)
		{
		GUI.Label(new Rect(190, Screen.height-30,100,50),"turnip:"+ turnip);
		}
		GUI.Label(new Rect(250, Screen.height-30,100,50),"Money:"+ money);
		GUI.Label(new Rect(Screen.width/2-50, 0,100,50),"Level:"+ level);
		
		switch(exp)
		{
			case 100: level=2;break;
			case 300: level=3;break;
			
		}
		
	}

}
