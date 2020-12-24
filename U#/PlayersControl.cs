
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class PlayersControl : UdonSharpBehaviour
{
    public VRCPlayerApi[] players = new VRCPlayerApi[50];
    private int pl_i = 0;
    public CapsuleCollider[] playerCollision=new CapsuleCollider[50];
    private int throughAllPlayer;
    public Transform original;
    public Transform logoOriginal;
    public Vector3 up;
    public float time;
    public GameObject[] blueTeamLogo = new GameObject[50];
    public GameObject[] redTeamLogo = new GameObject[50];
    [UdonSynced][HideInInspector]public int playerTotalCount = 0;

    int redPlayersCount;
    int bluePlayersCount;
    public int redCount;
    public int blueCount;


    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        playerTotalCount = player.playerId;
        UpdatePlayerCount();
    }
    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        time = 0;
        players = new VRCPlayerApi[50];
        UpdatePlayerCount();
    }
    public void UpdatePlayerCount()
    {
        pl_i = 0;
        for (int i = 0; i <= playerTotalCount; i++)
        {
            if (true)
            {
                if (VRCPlayerApi.GetPlayerById(i) != null)
                {
                    players[pl_i] = VRCPlayerApi.GetPlayerById(i);
                    pl_i++;
                }
            }
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        for (throughAllPlayer = 0 ; throughAllPlayer < players.Length && time > 0.2f ; throughAllPlayer++)
        {
            if(players[throughAllPlayer] != null)
            {
                playerCollision[throughAllPlayer].transform.localScale = (players[throughAllPlayer].GetBonePosition(HumanBodyBones.Head) - players[throughAllPlayer].GetBonePosition(HumanBodyBones.LeftFoot));
                playerCollision[throughAllPlayer].transform.position = players[throughAllPlayer].GetPosition();

                if (players[throughAllPlayer].GetGravityStrength() == 1.001f)
                {
                    blueTeamLogo[throughAllPlayer].transform.position = players[throughAllPlayer].GetPosition() + up;
                    bluePlayersCount++;
                }

                if (players[throughAllPlayer].GetGravityStrength() == 1.002f)
                {
                    redTeamLogo[throughAllPlayer].transform.position = players[throughAllPlayer].GetPosition() + up;
                    redPlayersCount++;
                }

                if (players[throughAllPlayer].GetGravityStrength() == 1f)
                {
                    blueTeamLogo[throughAllPlayer].transform.position = logoOriginal.position;
                    redTeamLogo[throughAllPlayer].transform.position = logoOriginal.position;
                }
            }
            if (players[throughAllPlayer] == null)
            {
                playerCollision[throughAllPlayer].transform.localScale = original.localScale;
                playerCollision[throughAllPlayer].transform.position = original.position;
                blueTeamLogo[throughAllPlayer].transform.position = logoOriginal.position;
                redTeamLogo[throughAllPlayer].transform.position = logoOriginal.position;
            }
        }
        throughAllPlayer = 0;
        blueCount = bluePlayersCount;
        redCount = redPlayersCount;
        bluePlayersCount = 0;
        redPlayersCount = 0;
    }
}
