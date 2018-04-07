using UnityEngine;
using UnityEditor;

static class PlayerUnityIntegration 
{
	[MenuItem("Assets/Create/PlayerAsset")]
	public static void CreateYourScriptableObject(){
		ScriptableObjectUtility3.CreateAsset<PlayerAsset> ();
	}
}
