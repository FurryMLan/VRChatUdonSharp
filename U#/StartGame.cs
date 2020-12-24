
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class StartGame : UdonSharpBehaviour
{
    public GetFlagGame GetFlagGame;
    public BlueTrigger BlueTrigger;
    public RedTrigger RedTrigger;
    [UdonSynced]private int sync;
    private int local;
    public bool alreadyStart;

    public MeshRenderer startMesh;
    public MeshRenderer resetMesh;
    public BoxCollider startCollider;
    public BoxCollider resetCollider;

    public GameObject anotherSwitch;
    private VRCPlayerApi localPlayer;
    public GameObject startPlane;
    public Transform[] redPoint = new Transform[3];
    public Transform[] bluePoint = new Transform[3];

    [UdonSynced]public bool stop;
    float stopTime;

    float waitTime;
    bool waitOnce;
    public Text waitStart;

    bool stopOnce;

    private void Start()
    {
        waitOnce = true;
        stopOnce = true;
        local = 1;
        localPlayer = Networking.LocalPlayer;
    }

    private void Update()
    {
        if (!stopOnce)//优化4
        {
            stopTime += Time.deltaTime;
        }
        if (!waitOnce)//优化5
        {
            waitTime += Time.deltaTime;
        }

        if (sync == local && waitOnce)
        {
            waitTime = 0;
            waitOnce = false;
            BlueTrigger.isStartGame = true;
            RedTrigger.isStartGame = true;
            startMesh.enabled = false;
            startCollider.enabled = false;
        }
        if (sync == local && !waitOnce && waitTime > 5f)
        {
            GetFlagGame.gameText[0].text = "Game Running !";

            StartTP();

            resetCollider.enabled = true;
            resetMesh.enabled = true;

            alreadyStart = true;

            waitOnce = true;
            local++;
        }
        if (sync > local)
        {
            local = sync + 1;
        }

        if (stop && stopOnce)
        {
            stopTime = 0;
            startPlane.SetActive(true);
            stopOnce = false;
        }
        if (stopTime > 7.5f && !stopOnce)
        {
            startPlane.SetActive(false);
            stop = false;
            stopOnce = true;
        }

        if (waitTime <= 5f)
        {
            waitStart.text = (5f - waitTime).ToString("F1");
        }
        else
        {
            waitStart.text = "";
        }
    }

    void StartTP()
    {
        if (localPlayer.GetGravityStrength() == 1.001f)
        {
            localPlayer.TeleportTo(bluePoint[0].position, bluePoint[0].rotation);
            localPlayer.TeleportTo(bluePoint[1].position, bluePoint[1].rotation);
            localPlayer.TeleportTo(bluePoint[2].position, bluePoint[2].rotation);
        }
        if (localPlayer.GetGravityStrength() == 1.002f)
        {
            localPlayer.TeleportTo(redPoint[0].position, redPoint[0].rotation);
            localPlayer.TeleportTo(redPoint[1].position, redPoint[1].rotation);
            localPlayer.TeleportTo(redPoint[2].position, redPoint[2].rotation);
        }
    }

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        Networking.SetOwner(Networking.LocalPlayer, anotherSwitch);
        sync++;
        stop = true;
    }
}
