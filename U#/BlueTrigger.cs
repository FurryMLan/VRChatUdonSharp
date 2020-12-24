
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BlueTrigger : UdonSharpBehaviour
{
    //public GameControl GameControl;
    public bool isStartGame;
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!isStartGame)
        {
            player.SetGravityStrength(1.001f);
            //GameControl.blueCount++;
        }
    }
    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (!isStartGame)
        {
            player.SetGravityStrength(1f);
            //GameControl.blueCount--;
        }
    }
}
