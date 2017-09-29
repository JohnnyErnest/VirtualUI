using UnityEngine;
using System.Collections;

public class VirtualUIJSONDownloader : MonoBehaviour {

	public string URL;
	public TextMesh StatusText;

	public WWW Downloader;
	public bool DownloadDone = false;
	public string DownloadedString = string.Empty;
	public SimpleJSON.JSONNode DownloadedNode;
	public bool StartedDownload = false;
	public event System.EventHandler DownloadDoneEvent;

	// Use this for initialization
	void Start () {
		this.UpdateStatus("Ready for Download...");
	}

	public void SetTextAlpha(float alpha) {
		if (this.StatusText != null) {
			Color col = this.StatusText.color;
			col.a = alpha;
			this.StatusText.color = col;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (StartedDownload) {
			if (Downloader.isDone && !DownloadDone) {
				DownloadDone = true;
				StartedDownload = false;
				this.UpdateStatus("Download Completed...");
				DownloadedString = Downloader.text;
				DownloadedNode = SimpleJSON.JSONData.Parse(DownloadedString);
				if (this.DownloadDoneEvent != null) {
					this.DownloadDoneEvent(this, new System.EventArgs());
				}
			}
		}
	}

	public void UpdateStatus(string text) {
		if (this.StatusText != null) {
			this.StatusText.text = text;
		}
	}

	public void StartDownload() {
		this.DownloadDone = false;
		this.StatusText.text = "Downloading...";
		this.Downloader = new WWW(URL);
		this.StartedDownload = true;
	}
}
