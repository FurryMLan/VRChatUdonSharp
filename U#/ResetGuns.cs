
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetGuns : UdonSharpBehaviour
{
    public ResetGame ResetGame;
    public GameObject[] guns;
    int throughGuns;
    Vector3[] savePosition;
    Quaternion[] saveRotation;
    public VRC_Pickup[] allDrop;
    int throughDrop;
    bool isAllReset;
    public float time;
    void Start()
    {
        isAllReset = false;
        savePosition = new Vector3[guns.Length];
        saveRotation = new Quaternion[guns.Length];
        for(int i = 0;i < guns.Length; i++)
        {
            savePosition[i] = guns[i].transform.position;
            saveRotation[i] = guns[i].transform.rotation;
        }
    }
    private void Update()
    {
        if (isAllReset)//优化10
        {
            time += Time.deltaTime;
        }

        if (!isAllReset && ResetGame.startReset)
        {
            time = 0;
            ResetGunsPR();
            isAllReset = true;
        }

        if(time > 1f && isAllReset)//&& isAllReset)
        {
            ResetGame.startReset = false;
            isAllReset = false;
        }
    }

    public void ResetGunsPR()
    {
        for(throughDrop = 0;throughDrop < allDrop.Length; throughDrop++)
        {
            if (allDrop[throughDrop].IsHeld)
            {
                allDrop[throughDrop].Drop();
            }
        }
        throughDrop = 0;

        for(throughGuns = 0;throughGuns < guns.Length; throughGuns++)
        {
            guns[throughGuns].transform.position = savePosition[throughGuns];
            guns[throughGuns].transform.rotation = saveRotation[throughGuns];
        }
        throughGuns = 0;
    }
}
