
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class GetFlagGame : UdonSharpBehaviour
{
    public StartGame StartGame;
    public ResetGame ResetGame;
    public PlayersControl PlayersControl;

    [UdonSynced] int win;

    public Transform originalFlag;

    public Text[] gameText;//两个玩家数1，2；两个已拿到旗子数3，4；一个状态0

    public int redFlagCount;
    public int blueFlagCount;
    public int winCount;
    float time;
    bool resetTime;

    private void Start()
    {
        resetTime = true;
    }
    private void Update()
    {
        if(!resetTime)//优化1
        {
            time += Time.deltaTime;
        }
        if (blueFlagCount == winCount || redFlagCount == winCount)
        {
            if (blueFlagCount == winCount)
            {
                win = 1;
            }
            if (redFlagCount == winCount)
            {
                win = 2;
            }


            if (resetTime)
            {
                time = 0;
                resetTime = false;
            }
            ResetGame.sync++;
            if (time > 2f)
            {
                blueFlagCount = 0;
                redFlagCount = 0;
                resetTime = true;
                win = 0;
            }
            ShowWinTeam();
        }
        ShowFlagCount();
        GetPlayersCountInGame();
    }

    void GetPlayersCountInGame()
    {
        gameText[1].text = PlayersControl.blueCount.ToString();
        gameText[2].text = PlayersControl.redCount.ToString();
    }

    void ShowFlagCount()
    {
        gameText[3].text = blueFlagCount.ToString();
        gameText[4].text = redFlagCount.ToString();
    }
    void ShowWinTeam()
    {
        if (win == 1)
        {
            gameText[0].text = "Blue Team Win !";
        }
        if (win == 2)
        {
            gameText[0].text = "Red Team Win !";
        }
    }

}
