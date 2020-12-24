
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
using VRC.Udon;

public class ParticleCollision : UdonSharpBehaviour
{
    public float damage;
    public PlayerTP PlayerTP;
    public StartGame StartGame;
    public override void OnPlayerParticleCollision(VRCPlayerApi player)
    {
        player = PlayerTP.playerApi = Networking.LocalPlayer;
        if (StartGame.alreadyStart)
        {
            PlayerTP.HP = PlayerTP.HP - damage;
        }
    }
}
