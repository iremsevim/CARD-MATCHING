﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebApi : MonoBehaviour
{
    public static WebApi instance;
    private void Awake()
    {
        instance = this;
    }

    public static string SendSuccessRateUrl(string UserID, string GameCode, string rate)
    {
        return "http://bookg.net/games/insert_or_update_game_informations/" + UserID + "/" + GameCode + "/0/0/"+rate;
    }
    public static string SendTimeAndScoreUrl(string UserID, string GameCode,string Score, string PlayingTime)
    {
        return "http://bookg.net/games/insert_or_update_game_informations/" + UserID + "/" + GameCode + "/" + PlayingTime + "/"+Score;
    }
    public static string SendTimeOnlyUrl(string UserID, string GameCode, string PlayingTime)
    {
        return "http://bookg.net/games/insert_or_update_game_informations/"+UserID+"/"+GameCode+"/"+PlayingTime;
    }
    public static string UpdateDataUrl(string UserID,string GameCode,string PlayingTime,string PointEarned,string RateofSuccess)
    {
        return "http://www.bookg.net/games/insert_or_update_game_informations/" + UserID + "/" + GameCode + "/" + PlayingTime + "/" + PointEarned + "/" + RateofSuccess;
    }
    public static string RegisterUrl(string Username)
    {
        return "http://www.bookg.net/games/get_user_id/" + Username;
    }

    public IEnumerator Service(string Url, System.Action<string> OnResponse, System.Action<string> OnError = null)
    {
        Debug.Log(Url);
        UnityWebRequest www = UnityWebRequest.Get(Url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            OnError?.Invoke(www.error);
        }
        else
        {
            string text = www.downloadHandler.text;
            text=System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "");

            OnResponse?.Invoke(text);
        }
    }
   
}