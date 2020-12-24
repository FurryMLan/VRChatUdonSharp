
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RedTrigger : UdonSharpBehaviour
{
    public bool isStartGame;
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!isStartGame)
        {
            player.SetGravityStrength(1.002f);
        }
    }
    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (!isStartGame)
        {
            player.SetGravityStrength(1f);
        }
    }
}
