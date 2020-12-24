
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupWay : UdonSharpBehaviour
{
    public VRC_Pickup selfPickup;
    VRCPlayerApi localPlayer;
    public Transform pickupPC;
    private void Start()
    {
        localPlayer = Networking.LocalPlayer;

        if (!localPlayer.IsUserInVR())
        {
            selfPickup.DisallowTheft = true;
            selfPickup.ExactGrip = selfPickup.ExactGun = pickupPC;
            selfPickup.allowManipulationWhenEquipped = false;
            selfPickup.orientation = VRC_Pickup.PickupOrientation.Gun;
            selfPickup.AutoHold = VRC_Pickup.AutoHoldMode.Yes;
            selfPickup.InteractionText = "";
            selfPickup.UseText = "Shoot";
            selfPickup.ThrowVelocityBoostMinSpeed = 1;
            selfPickup.ThrowVelocityBoostScale = 1;
            selfPickup.pickupable = true;
            selfPickup.proximity = 2;
        }

        if (localPlayer.IsUserInVR())
        {
            selfPickup.DisallowTheft = true;
            selfPickup.ExactGrip = selfPickup.ExactGun = pickupPC;
            selfPickup.allowManipulationWhenEquipped = false;
            selfPickup.orientation = VRC_Pickup.PickupOrientation.Gun;
            selfPickup.AutoHold = VRC_Pickup.AutoHoldMode.Yes;
            selfPickup.InteractionText = "";
            selfPickup.UseText = "Shoot";
            selfPickup.ThrowVelocityBoostMinSpeed = 1;
            selfPickup.ThrowVelocityBoostScale = 1;
            selfPickup.pickupable = true;
            selfPickup.proximity = 2;
        }
    }
}
