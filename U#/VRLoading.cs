
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


public class VRLoading : UdonSharpBehaviour
{
    public PickupTakeWay PickupTakeWay;
    public Collider magazineCollider;//用于判定是否装回的碰撞器
    public GameObject magazine;
    public MeshRenderer selfMesh;//用于隐藏可拆卸弹匣而显示不可拆卸弹匣
    VRCPlayerApi localPlayer;//用于判断是否vr

    public Transform target;//回复位置

    public VRC_Pickup selfPickup;//判断是否拿着和PC是否能拾取
    public GameObject selfMove;//本体位置

    public GameObject anotherMagazine;//不可拆卸弹匣
    public float loadingTime;

    float homingTime;//判断十秒内没有放回时自动放回
    bool homing;//放回定位
    bool reset;//下次取下前只回复一次


    float time;//取下后等待时间才可放回
    bool loading;//是否已经拿下来
    bool once;//拿下时只判定一次
    bool onceHoming;//homing内部只运行一次的情况
    void Start()
    {
        magazine.SetActive(false);
        onceHoming = true;
        homing = true;
        localPlayer = Networking.LocalPlayer;
        once = true;
        if (localPlayer.IsUserInVR())
        {
            selfPickup.pickupable = true;
        }
        if (!localPlayer.IsUserInVR())
        {
            selfPickup.pickupable = false;
        }
    }
    private void Update()
    {
        if (homing && onceHoming)//弹匣跟随枪
        {
            onceHoming = false;
            anotherMagazine.SetActive(true);
            selfMesh.enabled = false;
            selfPickup.Drop();
            selfMove.transform.localPosition = target.localPosition;
            selfMove.transform.localRotation = target.localRotation;
        }

        if (reset)//优化2
        {
            homingTime += Time.deltaTime;
        }
        if (magazine.activeSelf == false)//优化3
        {
            time += Time.deltaTime;
        }


        if (selfPickup.IsHeld && once)//已经拿下来
        {
            magazine.SetActive(false);
            homingTime = 0;
            homing = false;
            onceHoming = true;
            anotherMagazine.SetActive(false);
            selfMesh.enabled = true;
            once = false;
            reset = true;
            time = 0;
            loading = true;
            PickupTakeWay.finishLoading = false;
            PickupTakeWay.inLoading = true;
        }

        if (loading && time > loadingTime)
        {
            magazine.SetActive(true);
        }

        if (homingTime >= 10f && reset && !selfPickup.IsHeld)//十秒内未放回
        {
            homing = true;
            reset = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            selfPickup.Drop();
            homing = true;
            loading = false;
            PickupTakeWay.inLoading = false;
            PickupTakeWay.finishLoading = true;
            once = true;
            magazine.SetActive(false);
        }
    }
}
