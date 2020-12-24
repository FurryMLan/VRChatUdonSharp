
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FlagCount : UdonSharpBehaviour
{
    public GetFlagGame GetFlagGame;

    public CapsuleCollider[] selfColliders;
    int throughColliders;

    public GameObject[] flag;
    int throughFlag;

    public VRC_Pickup[] flagPickup;
    int throughPickUp;
    int[] isInBlue = new int[5];
    int[] isInRed = new int[5];

    public Transform originalFlag;
    public int team;//1蓝 2红

    float time;
    bool openColliders;

    private void Update()
    {
        if (openColliders)//优化6
        {
            time += Time.deltaTime;
        }

        //if (team == 1)//放入碰撞后测试
        //{
        //    GetFlagGame.blueFlagCount = isInBlue[0] + isInBlue[1] + isInBlue[2] + isInBlue[3] + isInBlue[4];
        //}
        //if (team == 2)
        //{
        //    GetFlagGame.redFlagCount = isInRed[0] + isInRed[1] + isInRed[2] + isInRed[3] + isInRed[4];
        //}

        if (openColliders && time > 10f)
        {
            for (throughColliders = 0; throughColliders < selfColliders.Length; throughColliders++)
            {
                selfColliders[throughColliders].enabled = true;
            }
            throughColliders = 0;
            openColliders = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            if (flagPickup[0].IsHeld)
            {
                flagPickup[0].Drop();
            }
            flagPickup[0].pickupable = false;
            selfColliders[0].enabled = false;
            if (team == 1)
            {
                isInBlue[0] = 1;
            }
            if (team == 2)
            {
                isInRed[0] = 1;
            }
        }
        if (other.gameObject.layer == 25)
        {
            if (flagPickup[1].IsHeld)
            {
                flagPickup[1].Drop();
            }
            flagPickup[1].pickupable = false;
            selfColliders[1].enabled = false;
            if (team == 1)
            {
                isInBlue[1] = 1;
            }
            if (team == 2)
            {
                isInRed[1] = 1;
            }
        }
        if (other.gameObject.layer == 26)
        {
            if (flagPickup[2].IsHeld)
            {
                flagPickup[2].Drop();
            }
            flagPickup[2].pickupable = false;
            selfColliders[2].enabled = false;
            if (team == 1)
            {
                isInBlue[2] = 1;
            }
            if (team == 2)
            {
                isInRed[2] = 1;
            }
        }
        if (other.gameObject.layer == 27)
        {
            if (flagPickup[3].IsHeld)
            {
                flagPickup[3].Drop();
            }
            flagPickup[3].pickupable = false;
            selfColliders[3].enabled = false;
            if (team == 1)
            {
                isInBlue[3] = 1;
            }
            if (team == 2)
            {
                isInRed[3] = 1;
            }
        }
        if (other.gameObject.layer == 28)
        {
            if (flagPickup[4].IsHeld)
            {
                flagPickup[4].Drop();
            }
            flagPickup[4].pickupable = false;
            selfColliders[4].enabled = false;
            if (team == 1)
            {
                isInBlue[4] = 1;
            }
            if (team == 2)
            {
                isInRed[4] = 1;
            }
        }

        if (team == 1)//放入碰撞后测试，优化11
        {
            GetFlagGame.blueFlagCount = isInBlue[0] + isInBlue[1] + isInBlue[2] + isInBlue[3] + isInBlue[4];
        }
        if (team == 2)
        {
            GetFlagGame.redFlagCount = isInRed[0] + isInRed[1] + isInRed[2] + isInRed[3] + isInRed[4];
        }
    }

    public void ResetFlag()
    {
        for (throughFlag = 0; throughFlag < flag.Length; throughFlag++)
        {
            flag[throughFlag].transform.SetPositionAndRotation(originalFlag.position, originalFlag.rotation);
        }
        throughFlag = 0;

        for (throughPickUp = 0; throughPickUp < flagPickup.Length; throughPickUp++)
        {
            flagPickup[throughPickUp].pickupable = true;
        }
        throughPickUp = 0;

        time = 0;
        openColliders = true;

        if (team == 1)
        {
            isInBlue[0] = 0;
            isInBlue[1] = 0;
            isInBlue[2] = 0;
            isInBlue[3] = 0;
            isInBlue[4] = 0;
        }

        if (team == 2)
        {
            isInRed[0] = 0;
            isInRed[1] = 0;
            isInRed[2] = 0;
            isInRed[3] = 0;
            isInRed[4] = 0;
        }
    }
}
