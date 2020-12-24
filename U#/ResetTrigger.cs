
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetTrigger : UdonSharpBehaviour
{
    public GameObject self;
    public BoxCollider selfCollider;
    public float time;
    public bool inStart;
    public Vector3 original;

    private void Update()
    {
        time += Time.deltaTime;
        if (inStart)
        {
            selfCollider.enabled = true;
            self.transform.position = Vector3.zero;
            if (time > 0.5)
            {
                self.transform.position = original;
                selfCollider.enabled = false;
                inStart = false;
            }
        }
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
            player.SetGravityStrength(1f);
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
            player.SetGravityStrength(1f);
    }
}
