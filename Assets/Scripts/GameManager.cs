using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject PlayerPrefab;
    public GameObject NextPlayer;
    int leveltracker = 0;

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

        if (RespawnAnchor != null)
        {
            SpawnLoc = RespawnAnchor.transform.position + new Vector3(0, .7f, 0);
        }
        else
        {
            //Spawn at level begining
            //change to gameobject
            SpawnLoc = GameObject.Find("StartingSpawn").transform.position;
        }


        NextPlayer = Instantiate(PlayerPrefab, SpawnLoc, Quaternion.identity, null);

        var pi = NextPlayer.GetComponent<PlayerInput>();
        pi.SwitchCurrentActionMap("Player");
        pi.ActivateInput();
        pi.actions.Enable();
        pi.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);

        
        //Debug.Log($"PI enabled={pi.enabled} map={pi.currentActionMap?.name} actions={(pi.actions != null)}");

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("CinemaCam"))
        {
            Camera CamRef = o.GetComponent<Camera>();
            CamRef.depth = -1;
        }

        Debug.Log("Player respawned.");
        ResetCamera(NextPlayer);

    }
    public void ResetCamera(GameObject PlayerPrefab)
    {
        //GameObject MainCamera = GameObject.Find("Main Camera");
        //GameObject MainCamera = GameObject.Find("TestingCamera");
        GameObject CinemachineObject = GameObject.Find("CinemachineCamera");

        CinemachineCamera CinemachineCam = CinemachineObject.GetComponent<CinemachineCamera>();

        Vector3 playerPos = PlayerPrefab.transform.position;


        CinemachineCam.Follow = PlayerPrefab.transform;



    }
    public void EndGame()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("CreditsScreen");
    }
    public void NextLevel()
    {
        leveltracker += 1;
        switch (leveltracker)
        {
            case 1:
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SceneManager.LoadScene("Level 2");
                break;
            case 3:
                SceneManager.LoadScene("Level 3");
                break;
            default:
                SceneManager.LoadScene("CreditsScreen");
                leveltracker = 0;
                break;
        }
    }
}
