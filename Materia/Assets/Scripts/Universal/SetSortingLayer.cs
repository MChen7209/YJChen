using UnityEngine;
using System.Collections;

public class SetSortingLayer : MonoBehaviour {

	public string sortingLayerName;
	public int orderInLayer;
	
	void Start () {
		renderer.sortingLayerName = sortingLayerName;
		renderer.sortingOrder = orderInLayer;
	}
}
