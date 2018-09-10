using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

/// <summary>
/// this moulde use restfulAPI of firestore.
/// for details, see https://firebase.google.com/docs/firestore/reference/rest/
/// </summary>
public class NetworkManager : MonoBehaviour
{
    static private IEnumerator RequestRestfulAPI(string url, byte[] postData, Dictionary<string, string> headers, Action<string> OnSuccess, Action<HttpStatusCode, string> OnFailed)
    {
        using (WWW www = (headers != null) ? new WWW(url, postData, headers) : (postData != null) ? new WWW(url, postData) : new WWW(url))
        {
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                HttpStatusCode status = 0;
                string errMessage = "";

                // Parse status code
                if (www.responseHeaders.ContainsKey("STATUS"))
                {
                    string str = www.responseHeaders["STATUS"] as string;
                    string[] components = str.Split(' ');
                    int code = 0;
                    if (components.Length >= 3 && int.TryParse(components[1], out code))
                        status = (HttpStatusCode)code;
                }

                if (www.error.Contains("crossdomain.xml") || www.error.Contains("Couldn't resolve"))
                {
                    errMessage = "No internet connection or crossdomain.xml policy problem";
                }
                else
                {
                    // Parse error message
                    try
                    {
                        if (!string.IsNullOrEmpty(www.text))
                        {

                            SimpleJSON.JSONNode rootNode = SimpleJSON.JSONNode.Parse(www.text);
                            errMessage = rootNode[0]["error"]["message"];
                        }
                    }
                    catch
                    {
                    }
                }

                if (OnFailed != null)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        errMessage = www.error;

                    if (errMessage.Contains("Failed downloading"))
                    {
                        errMessage = "Request failed with no info of error.";
                    }

                    OnFailed(status, errMessage);
                }

            }
            else
            {
                if (OnSuccess != null)
                {
                    OnSuccess(www.text);
                }
            }
        }
    }


    static class FetchHighScoresVars
    {
        public static string URL = "https://firestore.googleapis.com/v1beta1/projects/mrwaves-mrwaves/databases/(default)/documents:runQuery";
        public static Dictionary<string, string> HEADERS = new Dictionary<string, string>
        {
            {"Content-Type", "application/json" },
            {"X-HTTP-Method-Override", "POST"}
        };
        public static byte[] QUERY = Encoding.UTF8.GetBytes(
            @"{
            ""structuredQuery"" :{
                ""select"": {
                    ""fields"":[{""fieldPath"":""score""}]
                },
                ""from"": [{""collectionId"":""playrecord""}],
                ""orderBy"": [{
                    ""field"":{""fieldPath"":""score""},
                    ""direction"":""DESCENDING""
                }],
                ""limit"" :10
            }
        }");
    }


    static public IEnumerator FetchBestScores(Action<List<int>> OnSuccess, Action OnFailed)
    {
        yield return RequestRestfulAPI(FetchHighScoresVars.URL,
            FetchHighScoresVars.QUERY,
            FetchHighScoresVars.HEADERS,
            (result) =>
            {
                List<int> values = new List<int>(10);
                SimpleJSON.JSONNode rootNode = SimpleJSON.JSONNode.Parse(result);
                for (int i = 0; i < rootNode.Count; i++)
                {
                    if (rootNode[i]["document"] == null)
                        continue;
                    int score = rootNode[i]["document"]["fields"]["score"]["integerValue"].AsInt;
                    values.Add(score);
                }

                if (OnSuccess != null)
                    OnSuccess(values);
            },
            (code, reason) =>
            {
                Debug.LogError(String.Format("Cannot Complete FetchHighScores - StatusCode : ({0}){1}\nMessage : {2}", code, (int)code, reason));

                if (OnFailed != null)
                    OnFailed();
            });
    }

    static class PostScoreVars
    {
        public static string URL = "https://firestore.googleapis.com/v1beta1/projects/mrwaves-mrwaves/databases/(default)/documents/playrecord";
        public static Dictionary<string, string> HEADERS = new Dictionary<string, string>
        {
            {"Content-Type", "application/json" },
            {"X-HTTP-Method-Override", "POST"}
        };
    }

    static public IEnumerator PostScore(int score, Action OnSuccess, Action OnFailed)
    {
        byte[] query = Encoding.UTF8.GetBytes(
            @"{
            ""fields"" :{
                ""score"": {
                    ""integerValue"":" + score.ToString() + @"
                }
            }
        }");
        yield return RequestRestfulAPI(PostScoreVars.URL,
            query,
            PostScoreVars.HEADERS,
            (result) =>
            {
                if (OnSuccess != null)
                    OnSuccess();
            },
            (code, reason) =>
            {
                Debug.LogError(String.Format("Cannot Complete PostScore - StatusCode : ({0}){1}\nMessage : {2}", code, (int)code, reason));
                if (OnFailed != null)
                    OnFailed();
            });
    }
}
