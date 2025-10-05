using HutongGames.PlayMaker;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameObject PlayerPrefab;
    public GameObject NextPlayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    public void RespawnPlayer()
    {
        // Logic to respawn the player
        Vector3 SpawnLoc;
        GameObject RespawnAnchor = GameObject.FindGameObjectWithTag("Respawn");

        if(RespawnAnchor != null)
        {
            SpawnLoc = RespawnAnchor.transform.position + new Vector3(0, .7f, 0);
        }
        else
        {
            //Spawn at level begining
            SpawnLoc = new Vector3(-.08f, 1.4f, -.02f);
        }


            NextPlayer = Instantiate(PlayerPrefab, SpawnLoc, Quaternion.identity, null);

        var pi = NextPlayer.GetComponent<PlayerInput>();
        pi.SwitchCurrentActionMap("Player");
        pi.ActivateInput();
        pi.actions.Enable();
        pi.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);


        //Debug.Log($"PI enabled={pi.enabled} map={pi.currentActionMap?.name} actions={(pi.actions != null)}");

        Debug.Log("Player respawned.");
        ResetCamera(NextPlayer);
        
    }
    public void ResetCamera(GameObject PlayerPrefab)
    {
        //GameObject MainCamera = GameObject.Find("Main Camera");
        GameObject MainCamera = GameObject.Find("TestingCamera"); 
        PlayMakerFSM CameraFSM = MainCamera.GetComponent<PlayMakerFSM>();
        FsmVector3 TargetPos = CameraFSM.FsmVariables.GetFsmVector3("TargetPosition");
        FsmGameObject TargetObject = CameraFSM.FsmVariables.GetFsmGameObject("LookAtTarget");
        GameObject CinemachineObject = GameObject.Find("CinemachineCamera");

        CinemachineCamera CinemachineCam = CinemachineObject.GetComponent<CinemachineCamera>();

        Vector3 playerPos = PlayerPrefab.transform.position;

        Debug.Log("Target Object: " + TargetPos.Value + " going to set to: " + playerPos);


        TargetPos.Value = playerPos;
        TargetObject.Value = PlayerPrefab;
        CinemachineCam.Follow = PlayerPrefab.transform;



    }
    public void EndGame() 
    {         
        Debug.Log("Game Over!");
        SceneManager.LoadScene("CreditsScreen");
    }

}
