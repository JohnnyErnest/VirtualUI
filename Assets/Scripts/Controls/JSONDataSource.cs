using Horsey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class JSONDataSource : VirtualUIDataSource {

    /// <summary>
    /// A URL supplied to obtain data via JSON
    /// </summary>
    public string URL { get; set; }

    //public Dictionary<string, string> Headers { get; set; }
    //public Dictionary<string, string> Cookies { get; set; }

    /// <summary>
    /// The supplied results of a JSON call
    /// </summary>
    public string JSONResults { get; set; }

    /// <summary>
    /// A function that you supply to transform the JSON result into a Linear Data Set for use in Data Grids
    /// </summary>
    public Func<string, Type, Horsey.ISQLResult[]> DataTransformation { get; set; }
    public Type HorseyDataType { get; set; }

    /// <summary>
    /// A function that you supply to transform the JSON result into Chart Data for Pie/Bar/Area Charts
    /// </summary>
    public Func<string, ChartDataValue[]> ChartDataTransformation { get; set; }

    /// <summary>
    /// Gets all types that are of SQLResult within the currently executing Assembly via Reflection.
    /// This is needed to enumerate which types are actually data objects you can use for Data Grids
    /// or for Pie/Bar/Area Charts.
    /// </summary>
    /// <returns>All types that are of SQLResult within the currently executing Assembly</returns>
    public Dictionary<string, Type> FindAllSQLTypes()
    {
        Dictionary<string, Type> results = new Dictionary<string, Type>();
        Debug.Log(results.Count);
        var list = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().ToList().Where(b => b.IsSubclassOf(typeof(SQLResult)));
        foreach (var name in list)
        {
            if (name.IsSubclassOf(typeof(SQLResult)))
                results.Add(name.Name, name);
        }
        return results;
    }

    // Use this for initialization
    void Start () {
        FindAllSQLTypes();

        this.ChartDataTransformation = (inputString) =>
        {
            return new ChartDataValue[] {
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
                    }
                };
        };

        //this.DataTransformation = (inputString, inputType) => 
        //{
        //    return new ChartDataValue[] {
        //        new ChartDataValue()
        //        {
        //            Ordinal = 1, Name = "Test 1", Value = 50
        //        },
        //        new ChartDataValue()
        //        {
        //            Ordinal = 2, Name = "Test 2", Value = 20
        //        },
        //        new ChartDataValue()
        //        {
        //            Ordinal = 3, Name = "Test 3", Value = 70
        //        },
        //        new ChartDataValue()
        //        {
        //            Ordinal = 4, Name = "Test 4", Value = 40
        //        }
        //    };
        //};


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public async void PopulateData()
    {
        FindAllSQLTypes();

        WebClient client = new WebClient();
        this.JSONResults = client.DownloadString(URL);

        if (this.DataTransformation != null)
        {
            var result = this.DataTransformation(this.JSONResults, HorseyDataType);
            //result.GetProperties();
        }

        //WebAction action = new WebAction();
        //var headers = new Dictionary<string, string>();
        ////headers.Add("Accept-Encoding", "gzip, deflate");
        //headers.Add("Accept-Language", "en-US;en;q=0.5");
        //try
        //{
        //    var result = await action.RunAction(new WebActionParameters(URL)
        //    {
        //        Method = "GET",
        //        UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:55.0) Gecko/20100101 Firefox/55.0",
        //        ActionHeaders = headers,
        //        Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
        //    });
        //    this.JSONResults = result.ResponseText;
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.ToString());
        //}
    }

}

public class WebActionResult
{
    public bool IsError { get; set; }
    public Exception ErrorException { get; set; }
    public Dictionary<string, string> ResponseHeaders { get; set; }
    public Dictionary<string, Cookie> ResponseCookies { get; set; }
    public string ResponseText { get; set; }
    public string ResponseContentType { get; set; }
}

public class WebActionParameters
{
    public string UserAgent { get; set; }
    public string UrlParent { get; set; }
    public string Url { get; set; }
    public string Session { get; set; }
    public string Accept { get; set; }
    public Dictionary<string, string> ActionHeaders { get; set; }
    public Dictionary<string, string> ActionHeaderCookies { get; set; }
    public List<Cookie> ActionCookies { get; set; }
    public string PostBody { get; set; }
    public byte[] PostBodyBinary { get; set; }
    public string Method { get; set; }
    public string ContentType { get; set; }
    public bool IsBinaryPost { get; set; }

    public WebActionParameters(string urlParent)
    {
        this.UrlParent = urlParent;
    }
}

public class WebAction
{
    public string Session { get; set; }
    public string UserAgent { get; set; }

    public async Task<WebActionResult> RunAction(WebActionParameters parameters)
    {
        string requestURL = parameters.UrlParent + parameters.Url;
        Debug.Log("Starting request");

        HttpWebRequest request = HttpWebRequest.Create(requestURL) as HttpWebRequest;
        if (string.IsNullOrEmpty(parameters.Accept))
            request.Accept = "application/json";
        else
            request.Accept = parameters.Accept;

        if (parameters.ActionHeaders != null)
        {
            foreach (KeyValuePair<string, string> kvp in parameters.ActionHeaders)
            {
                Debug.Log("Setting Header: " + kvp.Key + ", " + kvp.Value);
                request.Headers.Add(kvp.Key, kvp.Value);
            }
        }

        if (parameters.ActionHeaderCookies != null)
        {
            foreach (KeyValuePair<string, string> kvp in parameters.ActionHeaderCookies)
            {
                request.Headers.Add(HttpRequestHeader.Cookie, kvp.Key + "=" + kvp.Value);
            }
        }

        if (parameters.ActionCookies != null)
        {
            foreach (Cookie cookie in parameters.ActionCookies)
            {
                request.CookieContainer.Add(cookie);
            }
        }

        if (string.IsNullOrEmpty(parameters.ContentType))
            request.ContentType = "application/json; charset=UTF-8";
        else
            request.ContentType = parameters.ContentType;

        if (!string.IsNullOrEmpty(UserAgent))
            request.UserAgent = parameters.UserAgent;
        else
            request.UserAgent = "Unity3D";

        request.Method = parameters.Method;
        if (request.Method == "POST")
        {
            if (parameters.IsBinaryPost)
            {
                request.ContentLength = parameters.PostBodyBinary.LongLength;
                BinaryWriter writer = new BinaryWriter(request.GetRequestStream());
                writer.Write(parameters.PostBodyBinary);
                writer.Close();
            }
            else
            {
                request.ContentLength = parameters.PostBody.Length;
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(parameters.PostBody);
                writer.Close();
            }
        }

        WebActionResult result = new WebActionResult();
        try
        {
            Debug.Log("Sending Request");
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            result.ResponseText = reader.ReadToEnd();
            Debug.Log("Got Response from Server");
            result.ResponseContentType = response.ContentType;
            result.ResponseHeaders = new Dictionary<string, string>();
            result.ResponseCookies = new Dictionary<string, Cookie>();
            for (int z = 0; z < response.Headers.Count; z++)
            {
                result.ResponseHeaders.Add(response.Headers.Keys[z], response.Headers[z]);
            }
            for (int z = 0; z < response.Cookies.Count; z++)
            {
                result.ResponseCookies.Add(response.Cookies[z].Name, response.Cookies[z]);
            }
            response.Close();
            result.IsError = false;
        }
        catch (Exception ex)
        {
            result.IsError = true;
            result.ErrorException = ex;
            Debug.Log("Exception: " + ex.ToString());
        }
        return result;
    }
}


/// <summary>
/// A single data value for a Bar, Pie, or Area Chart
/// </summary>
public class ChartDataValue : SQLResult
{
    public int Ordinal { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}

public interface IChartSource
{
    ChartDataValue[] GetChartData();
    decimal MinimumValue { get; }
    decimal MaximumValue { get; }
}
