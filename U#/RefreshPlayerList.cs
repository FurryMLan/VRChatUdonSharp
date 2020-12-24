
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RefreshPlayerList : UdonSharpBehaviour
{
    public PlayersControl PlayersControl;
    public PlayerInfo PlayerInfo;
    private float time;
    void Update()
    {
        time += Time.deltaTime;
        if (time > 5f)
        {
            PlayersControl.UpdatePlayerCount();
            PlayerInfo.UpdatePlayerCount();
            time = 0f;
        }
    }
}
