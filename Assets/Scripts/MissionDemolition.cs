using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameMode{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Inscribed")]
    public int maxShots = 5;
    public TMP_Text uitLevel;
    public TMP_Text uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        S = this;

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;

        StartLevel();
    }

    void StartLevel(){
        if ( castle != null){
            Destroy(castle);
        }

        Projectile.DESTROY_PROJECTILES();

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI(){
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots Taken: "+shotsTaken;
    }

    void Update()
    {
        UpdateGUI();

        if((mode == GameMode.playing) && Goal.goalMet){
            mode = GameMode.levelEnd;

            FollowCam.SWITCH_VIEW(FollowCam.eView.both);

            Invoke("NextLevel", 2f);
        }

        if(S.shotsTaken > maxShots){
            S.EndGame();
        }
    }

    void NextLevel(){
        level++;
        if (level == levelMax){
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    static public void SHOT_FIRED(){
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE(){
        return S.castle;
    }

    void EndGame(){
        SceneManager.LoadScene (sceneName:"GameOver");
    }
}
