using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class MockupChartSource : VirtualUIDataSource {

    public ChartDataValue[] Source =
    {
        new ChartDataValue()
        {
            Ordinal = 1, Name = "Test 1", Value = 50
        },
        new ChartDataValue()
        {
            Ordinal = 2, Name = "Test 2", Value = 20
        },
        new ChartDataValue()
        {
            Ordinal = 3, Name = "Test 3", Value = 70
        },
        new ChartDataValue()
        {
            Ordinal = 4, Name = "Test 4", Value = 40
        },
        new ChartDataValue()
        {
            Ordinal = 5, Name = "Test 5", Value = 30
        },
        new ChartDataValue()
        {
            Ordinal = 6, Name = "Test 6", Value = 10
        },
        new ChartDataValue()
        {
            Ordinal = 7, Name = "Test 7", Value = 90
        },
        new ChartDataValue()
        {
            Ordinal = 8, Name = "Test 8", Value = 40
        }
    };

    private void Awake()
    {
        if (this.ChartData == null)
            this.ChartData = this.Source;
    }

    // Use this for initialization
    void Start () {
        if (this.ChartData == null)
            this.ChartData = this.Source;
    }

    // Update is called once per frame
    void Update () {
        if (this.ChartData == null)
            this.ChartData = this.Source;
    }

}
