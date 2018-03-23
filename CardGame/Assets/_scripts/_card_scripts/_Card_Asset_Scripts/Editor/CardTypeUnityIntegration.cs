using UnityEngine;
using UnityEditor;

public class CardTypeUnityIntegration : MonoBehaviour {
	[MenuItem("Assets/Create/CardTypeAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<CardTypeAsset>();
	}
}
