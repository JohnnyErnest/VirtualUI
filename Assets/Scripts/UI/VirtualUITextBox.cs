using UnityEngine;
using System.Collections;

public class VirtualUITextBox : MonoBehaviour {

	public TextMesh TextObject;
	public LineRenderer LineObject;
	// Use this for initialization
	void Start () {

		float halfSizeY = this.TextObject.GetComponent<Renderer>().bounds.size.y / 2f;
		float fullSizeX = this.TextObject.GetComponent<Renderer>().bounds.size.x;
		Vector3 min = new Vector3(0f,0f,0f);
		Vector3 max = new Vector3(fullSizeX,0f,0f);
		this.LineObject.SetPosition(0, min);
		this.LineObject.SetPosition(1, max);

		Debug.Log(this.TextObject.GetComponent<Renderer>().bounds.extents);
		Debug.Log(this.TextObject.GetComponent<Renderer>().bounds.center);
		Debug.Log(this.TextObject.GetComponent<Renderer>().bounds.size);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
