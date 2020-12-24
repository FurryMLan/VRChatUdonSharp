using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

public class PlayerTP : UdonSharpBehaviour
{
    public GameObject followUI;//显示
    public VRCPlayerApi playerApi;//本地玩家
    //private int randomRespawn;//用于随机取得复活地点
    public Transform respawnArea;
    public float HP = 100f;
    public Text playerName;
    public Text HPWatch;
    public Text shots;
    public Text playerTag;
    public float shotsCount;
    //public Transform[] respawnAreaGroup;//储存所有可能的复活地点
    public VRC_Pickup[] deathDropGun;//死亡掉落
    private int throughDeathDropGun;
    public void Start()
    {
        playerApi = Networking.LocalPlayer;
    }

    public void FixedUpdate()
    {
        shots.text = shotsCount.ToString()+ " shots has been fired";
        playerName.text= playerApi.displayName;//玩家名字显示
        HPWatch.text= HP.ToString();//玩家生命值显示
        playerTag.text = playerApi.GetGravityStrength().ToString();

        if (HP <= 0)
        {
            for (throughDeathDropGun = 0; throughDeathDropGun < deathDropGun.Length; throughDeathDropGun++)
            {
                if (deathDropGun[throughDeathDropGun].IsHeld)
                {
                    deathDropGun[throughDeathDropGun].Drop();
                }
            }
            throughDeathDropGun = 0;
            //randomRespawn = Random.Range(0, respawnAreaGroup.Length - 1);//随机地点复活
            //playerApi.TeleportTo(respawnAreaGroup[randomRespawn].position, respawnAreaGroup[randomRespawn].rotation);//复活
            playerApi.TeleportTo(respawnArea.position, respawnArea.rotation);
            HP = 100;//生命回满
        }
    }
    public void Update()
    {
        followUI.transform.position = playerApi.GetPosition();//跟随位置UI
        followUI.transform.rotation = playerApi.GetRotation();//跟随旋转UI
    }
}
