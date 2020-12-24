
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupTakeWay : UdonSharpBehaviour
{
    public VRC_Pickup selfPickup;
    public PickUpUse PickUpUse;
    public PickUpUse2 PickUpUse2;
    public PickUpUse3 PickUpUse3;
    public bool inLoading;
    public bool finishLoading;
    bool once;
    float pickupTime;
    float pickup2Time;
    float pickup3Time;


    private void Start()
    {
        once = true;
    }

    private void Update()
    {
        if(PickUpUse != null && PickUpUse.loadingDown)//优化7
        {
            pickupTime += Time.deltaTime;
        }
        if (PickUpUse2 != null && PickUpUse2.loadingDown)//优化8
        {
            pickup2Time += Time.deltaTime;
        }
        if (PickUpUse3 != null && PickUpUse3.loadingDown)//优化9
        {
            pickup3Time += Time.deltaTime;
        }

        if (selfPickup.IsHeld)
        {
            if (PickUpUse != null)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    pickupTime = 0;
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResetTime));
                    PickUpUse.loadingDown = true;
                }
            }
            if (PickUpUse2 != null)
            {
                if (Input.GetKeyDown(KeyCode.T)) 
                {
                    pickup2Time = 0;
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResTime2));
                    PickUpUse2.loadingDown = true;
                }
            }
            if (PickUpUse3 != null)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    pickup3Time = 0;
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResTime3));
                    PickUpUse3.loadingDown = true;
                }
            }
        }
        if (pickupTime > 1.2f && PickUpUse != null && PickUpUse.loadingDown)
        {
            PickUpUse.loadingDown = false;
        }
        if (pickup2Time > 2f && PickUpUse2 != null && PickUpUse2.loadingDown)
        {
            PickUpUse2.loadingDown = false;
        }
        if (pickup3Time > 0.8f && PickUpUse3 != null && PickUpUse3.loadingDown)
        {
            PickUpUse3.loadingDown = false;
        }


        if (inLoading && once)
        {
            once = false;
            if (PickUpUse != null)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResetTime));
                PickUpUse.loadingDown = true;
            }
            if (PickUpUse2 != null)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResTime2));
                PickUpUse2.loadingDown = true;
            }
            if (PickUpUse3 != null)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResTime3));
                PickUpUse3.loadingDown = true;
            }
        }
        if (finishLoading)
        {
            once = true;
        }
    }

    void ResetTime()
    {
        PickUpUse.loadingTime = 0;
    }
    void ResTime2()
    {
        PickUpUse2.loadingTime = 0;
    }
    void ResTime3()
    {
        PickUpUse3.loadingTime = 0;
    }
}
