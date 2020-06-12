using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheEndWin : MonoBehaviour
{
    private TextMeshProUGUI gameOverText;
    private Button retryButton;
    private Button exitButton;
    private float gameOverTimer;
    private float guiTimer;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gameOverTimer = 4f;
        gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        gameOverText.alpha = 0.0f;
        retryButton = GameObject.Find("Canvas/RetryButton").GetComponent<Button>();
        exitButton = GameObject.Find("Canvas/ExitButton").GetComponent<Button>();
        guiTimer = 7f;
        retryButton.onClick.AddListener(delegate { onRetryButtonClick(); });
        retryButton.onClick.AddListener(delegate { onExitButtonClick(); });
        retryButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (guiTimer > 0)
        {
            guiTimer -= Time.deltaTime;
        }
        else
        {
            if (gameOverTimer > 0)
            {
                anim.SetBool("isDead", true);
                gameOverText.alpha += 0.3f * Time.deltaTime;
                gameOverTimer -= Time.deltaTime;
            }
            else
            {
                retryButton.gameObject.SetActive(true);
                exitButton.gameObject.SetActive(true);
            }
        }
    }

    void onRetryButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    void onExitButtonClick()
    {
        Application.Quit();
    }
}
