using UnityEngine;
using System.Collections;

public class VirtualUIDataLoader : MonoBehaviour {

	public VirtualUIJSONDownloader JSONDownloader;

	bool StartDataLoad = false;
	bool DataLoading = false;
	int CurrentRow = 0;

	// Use this for initialization
	void Start () {
		JSONDownloader.DownloadDoneEvent += HandleDownloadDoneEvent;
	}

	void HandleDownloadDoneEvent (object sender, System.EventArgs e)
	{
		StartDataLoad = true;
	}

	void UpdateText(string text) {
		if (this.JSONDownloader == null) return;
		if (this.JSONDownloader.StatusText == null) return;
		this.JSONDownloader.StatusText.text = text;
	}
	
	// Update is called once per frame
	void Update () {
		if (StartDataLoad) {
			DataLoading = true;
			StartDataLoad = false;
			CurrentRow = 0;
		}
		if (DataLoading) {

		}
	}
}
