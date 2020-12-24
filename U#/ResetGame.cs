
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetGame : UdonSharpBehaviour
{
    public FlagCount[] FlagCount;
    public GetFlagGame GetFlagGame;
    public BlueTrigger BlueTrigger;
    public RedTrigger RedTrigger;
    public Transform resetPoint;
    public ResetTrigger ResetTrigger;
    public StartGame StartGame;
    [UdonSynced] public int sync;
    private int local;

    public MeshRenderer startMesh;
    public MeshRenderer resetMesh;
    public BoxCollider startCollider;
    public BoxCollider resetCollider;

    public bool startReset;

    public GameObject anotherSwitch;
    private VRCPlayerApi localPlayer;
    private void Start()
    {
        local = 1;
        localPlayer = Networking.LocalPlayer;
    }
    private void Update()
    {
        if (sync == local)
        {
            BlueTrigger.isStartGame = false;
            RedTrigger.isStartGame = false;
            ResetTP();
            localPlayer.SetGravityStrength(1f);

            ResetTrigger.inStart = true;
            ResetTrigger.time = 0;

            startCollider.enabled = true;
            resetCollider.enabled = false;
            startMesh.enabled = true;
            resetMesh.enabled = false;

            startReset = true;
            StartGame.alreadyStart = false;
            FlagCount[0].ResetFlag();
            FlagCount[1].ResetFlag();

            if (GetFlagGame.blueFlagCount != GetFlagGame.winCount && GetFlagGame.redFlagCount != GetFlagGame.winCount)
            {
                GetFlagGame.gameText[0].text = "Wait";
            }

            local++;
        }
        if (sync > local)
        {
            local = sync + 1;
        }
    }

    void ResetTP()
    {
        if(localPlayer.GetGravityStrength() == 1.001f || localPlayer.GetGravityStrength() == 1.002f)
        {
            localPlayer.TeleportTo(resetPoint.position, resetPoint.rotation);
        }
    }

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        Networking.SetOwner(Networking.LocalPlayer, anotherSwitch);
        sync++;
    }
}
