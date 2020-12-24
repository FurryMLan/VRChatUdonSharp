
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class PickUpUse : UdonSharpBehaviour
{
    public GameObject bulletParticle;
    public GameObject contactTheBulletParticle;
    private int local;
    public PlayerTP PlayerTP;
    private float time;
    VRCPlayerApi localPlayer;
    [UdonSynced(UdonSyncMode.Smooth)] private int sync;

    //上弹
    [UdonSynced] public bool loadingDown;
    [UdonSynced] private int bullets;
    public Text showBullets;
    public int fullBullets;
    public PickupTakeWay PickupTakeWay;
    //上弹

    bool isLoading;
    public Text showLoadingTime;
    public float loadingTime;
    public float customLoadingTime = 1.2f;

    public AudioSource emptyBullet;
    [UdonSynced] int syncEmpty;
    int localEmpty;
    public AudioSource shooting;
    public AudioSource loading;



    public void Start()
    {
        bullets = 0;
        localPlayer = Networking.LocalPlayer;
    }

    public void FixedUpdate()
    {

        //打空弹匣自动上弹
        //if (bullets >= fullBullets && !localPlayer.IsUserInVR())
        //{
        //    loadingDown = true;
        //}

        time += Time.deltaTime;

        //单点射击
        if (sync > local && time > 0.1f && !isLoading)
        {
            time = 0;
            if(bullets < fullBullets)
            {
                bulletParticle.GetComponent<ParticleSystem>().Emit(1);
                contactTheBulletParticle.GetComponent<ParticleSystem>().Emit(1);
                shooting.Play();
                PlayerTP.shotsCount = PlayerTP.shotsCount + 1f;
            }
            local = local + 1;
            bullets = bullets + 1;
        }
        if(sync > local)
        {
            local = sync;
        }

        //装弹系统
        if (loadingDown && !isLoading && bullets != 0)
        {
            loadingTime = 0;
            isLoading = true;
            loading.Play();
        }
        if (loadingTime > customLoadingTime && isLoading)
        {
            if (localPlayer.IsUserInVR() && PickupTakeWay.finishLoading)
            {
                bullets = 0;
                loadingDown = false;
                isLoading = false;
            }

            if (!localPlayer.IsUserInVR())
            {
                bullets = 0;
                isLoading = false;
            }
        }
        loadingTime += Time.deltaTime;

        //装弹显示
        if (isLoading && customLoadingTime - loadingTime > 0f)
        {
            showLoadingTime.text = (customLoadingTime - loadingTime).ToString("F1");
        }
        else
        {
            showLoadingTime.text = "";
        }

        //弹匣显示
        if (fullBullets - bullets > 0)
        {
            showBullets.text = (fullBullets - bullets).ToString() + "/" + fullBullets.ToString();
        }
        else
        {
            if (syncEmpty > localEmpty)
            {
                emptyBullet.Play();
                localEmpty++;
            }
            showBullets.text = 0.ToString() + "/" + fullBullets.ToString();
        }
        if (syncEmpty > localEmpty)
        {
            localEmpty = syncEmpty;
        }
    }

    public override void OnPickupUseDown()
    {
        sync = sync + 1;
        syncEmpty = syncEmpty + 1;
    }
}
