using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject gameOverText;
    public GameObject startGameText;
    public Text scoreText;
    public Text highscoreText;
    public Text gameInfoText;
    public float scrollSpeed = -1.5f;
	public float menuSpeed = -0.5f;
    public bool gameOver = false;
    public bool gameStarted = false;

    private int score = 0;
    private int highscore = 0;
	private float realSpeed;
    private string platformFamily;

	/*(crash test)
    string GetPlatformFamily()
    {
        #if NETFX_CORE
            return Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
        #else
            return null;
        #endif
    }
	*/
    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	void Start () {
		if(!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
        }

        highscore = PlayerPrefs.GetInt("HighScore");
        highscoreText.text = "High Score: " + highscore.ToString();
		
		if(Input.GetJoystickNames().Length > 0)
        {
            gameInfoText.text = "Press (A) to Flap";
        }
        /*
        if((platformFamily = GetPlatformFamily()) != null)
        {
            if (platformFamily.Equals("Windows.Desktop"))
            {
                gameInfoText.text = "Press [SPACE] to Flap";
            }
        }
        */
        DeviceType pinch = SystemInfo.deviceType;
        if(pinch == DeviceType.Desktop)
            gameInfoText.text = "Press [SPACE] to Flap";


    }
	
	// Update is called once per frame
	void Update () {
        if (gameOver 
		&& (Input.GetMouseButtonDown(0)
			|| Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Joystick1Button0)
           )
		)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
		if(!gameStarted && (scrollSpeed != menuSpeed))
		{
			realSpeed = scrollSpeed;
			scrollSpeed = menuSpeed;
		}
	}
    public void BirdTapped()
    {
        startGameText.SetActive(false);
        
        gameStarted = true;
		scrollSpeed = realSpeed;
    }

    public void BirdScrored()
    {
        if (gameOver) return;

        score++;
        scoreText.text = "Score: " + score.ToString();
    }
    public void BirdDie()
    {
        gameOver = true;
        gameOverText.SetActive(true);

        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
            highscoreText.text = "High Score: " + highscore.ToString();
        }
    }
}
