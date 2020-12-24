
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class PickUpUse2 : UdonSharpBehaviour
{
    public GameObject bulletParticle;
    public GameObject contactTheBulletParticle;
    [UdonSynced(UdonSyncMode.Smooth)] private int sync;
    private int local;
    [UdonSynced(UdonSyncMode.Smooth)] private int syncOnce;
    public PlayerTP PlayerTP;
    public PickupTakeWay PickupTakeWay;
    private float time;
    VRCPlayerApi localPlayer;

    //上弹
    [UdonSynced] public bool loadingDown;
    [UdonSynced] private int bullets;
    public Text showBullets;
    int fullBullets;

    public float customLoadingTime = 3f;
    bool isLoading;
    public Text showLoadingTime;
    public float loadingTime;

    public AudioSource emptyBullet;
    [UdonSynced] int syncEmpty;
    int localEmpty;
    public AudioSource shooting;
    public AudioSource loading;

    void Start()
    {
        bullets = 0;
        fullBullets = 30;
        localPlayer = Networking.LocalPlayer;
    }
    void FixedUpdate()
    {
        //打空弹匣自动上弹
        //if(bullets >= fullBullets && !localPlayer.IsUserInVR())
        //{
        //    loadingDown = true;
        //}

        //连续点击时单次射击
        if (syncOnce > local && time > 0.1f && !isLoading)
        {
            time = 0f;
            if (bullets < fullBullets)
            {
                bulletParticle.GetComponent<ParticleSystem>().Emit(1);
                contactTheBulletParticle.GetComponent<ParticleSystem>().Emit(1);
                shooting.Play();
                PlayerTP.shotsCount = PlayerTP.shotsCount + 1f;
            }
            local = local + 1;
            bullets = bullets + 1;
        }
        if (syncOnce > local)
        {
            local = syncOnce;
        }

        time += Time.deltaTime;

        //长按时自动射击
        if (sync == 1 && time > 0.1f && !isLoading)
        {
            time = 0f;
            if (bullets < fullBullets)
            {
                bulletParticle.GetComponent<ParticleSystem>().Emit(1);
                contactTheBulletParticle.GetComponent<ParticleSystem>().Emit(1);
                shooting.Play();
                PlayerTP.shotsCount = PlayerTP.shotsCount + 1f;
            }
            bullets = bullets + 1;
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
        sync = 1;
        syncOnce = syncOnce + 1;
        syncEmpty = syncEmpty + 1;
    }
    public override void OnPickupUseUp()
    {
        sync = 0;
    }
}
