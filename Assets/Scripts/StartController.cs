using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class StartController : MonoBehaviour {

    private BannerView bannerView;

    // Use this for initialization
    void Start () {
        GameObject.Find("Canvas").transform.Find("StartButton").gameObject.GetComponent<Button>().onClick.AddListener(OnStartClick);
        GameObject.Find("Canvas").transform.Find("HowToPlayButton").gameObject.GetComponent<Button>().onClick.AddListener(OnHtpClick);
        string appId = Constants.AdmobAppId;
        MobileAds.Initialize(appId);
        string adUnitId = Constants.AdmobAdUnitId;
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }
    }

    void OnStartClick()
    {
        bannerView.Destroy();
        SceneManager.LoadScene("main");
    }

    void OnHtpClick()
    {
        Application.OpenURL("https://youtu.be/MNr7EI00Jlo");
    }

}