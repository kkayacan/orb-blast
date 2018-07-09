using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameWorld : MonoBehaviour {

    public Transform newOrb;

    public float fallSpeed = -0.002f;
    private int score = 0;
    public Text scoreText;
    private int nextTreshold = 10;
    public float orbSpeed = 10f;
    public float repeatInterval = 3f;

    private BannerView bannerView;

    void Start()
    {
        Time.timeScale = 1.0f;

        GameObject.Find("Canvas").transform.Find("RestartButton").gameObject.GetComponent<Button>().onClick.AddListener(OnRestartClick);

        UpdateScore(0);
        Transform orb = Instantiate(newOrb, new Vector3(0f, -4.5f, 0), Quaternion.identity);
        Utility.setColor(orb.gameObject);
        orb = Instantiate(newOrb, new Vector3(-1f, -4.5f, 0), Quaternion.identity);
        Utility.setColor(orb.gameObject);
        orb = Instantiate(newOrb, new Vector3(-2f, -4.5f, 0), Quaternion.identity);
        Utility.setColor(orb.gameObject);

        Invoke("throwOrb", repeatInterval);
    }

    void Update()
    {
        if (Time.timeScale == 0.0f && bannerView == null)
        {
            string appId = Constants.AdmobAppId;
            MobileAds.Initialize(appId);
            string adUnitId = Constants.AdmobAdUnitId;
            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("start");
        }
    }

    void throwOrb()
    {
        Vector3 center = new Vector3(0f, -4.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapCircle(center, 0.5f);
        if(hitCollider != null)
        {
            hitCollider.gameObject.GetComponent<OrbController>().isLoaded = true;
        }

        hitCollider = null;
        center = new Vector3(-1f, -4.5f, 0);
        hitCollider = Physics2D.OverlapCircle(center, 0.5f);
        if (hitCollider != null)
        {
            hitCollider.gameObject.GetComponent<OrbController>().isRolling = true;
            hitCollider.gameObject.GetComponent<OrbController>().startPosition = center;
        }

        hitCollider = null;
        center = new Vector3(-2f, -4.5f, 0);
        hitCollider = Physics2D.OverlapCircle(center, 0.5f);
        if (hitCollider != null)
        {
            hitCollider.gameObject.GetComponent<OrbController>().isRolling = true;
            hitCollider.gameObject.GetComponent<OrbController>().startPosition = center;
        }

        Transform orb = Instantiate(newOrb, new Vector3(-2f, -4.5f, 0), Quaternion.identity);
        Utility.setColor(orb.gameObject);

        Invoke("throwOrb", repeatInterval);
    }

    public void UpdateScore(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
        if(score >= nextTreshold)
        {
            repeatInterval -= 0.2f;
            nextTreshold += 10;
        }
    }

    void OnRestartClick()
    {
        bannerView.Destroy();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
