using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class VirtualUIDataSource : MonoBehaviour, IChartSource {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ChartDataValue[] ChartData;

    public virtual decimal MinimumValue
    {
        get
        {
            if (ChartData == null) return 0;
            if (ChartData.Length == 0) return 0;
            return this.ChartData.Min(b => b.Value);
        }
    }

    public virtual decimal MaximumValue
    {
        get
        {
            if (ChartData == null) return 0;
            if (ChartData.Length == 0) return 0;
            return this.ChartData.Max(b => b.Value);
        }
    }

    public virtual ChartDataValue[] GetChartData()
    {
        if (this.ChartData == null)
            this.ChartData = new ChartDataValue[0];
        return this.ChartData;
    }


}
