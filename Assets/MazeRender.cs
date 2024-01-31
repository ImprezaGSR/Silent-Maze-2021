using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRender : MonoBehaviour
{
    [SerializeField]
    [Range(1,50)]
    private int width = 10;
    [SerializeField]
    [Range(1,50)]
    private int height = 10;
    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private Transform wallPrehab = null;
    [SerializeField]
    private Transform endWallPrehab = null;
    [SerializeField]
    private Transform floorPrehab = null;
    [SerializeField]
    private Transform spawnerPrehab = null;
    [SerializeField]
    private Transform spawnerPrehab2 = null;
    [SerializeField]
    private Transform[] roomPrehabs = null;
    [SerializeField]
    private Transform centralRoomPrehab = null;

    [Range(1,6)]
    public int chestRooms = 5;

    [Range(1,50)]
    public int maxRoomNum = 10;
    private int overridedRoom = 0;
    // private bool keySpawned = false; 
    // [SerializeField]
    // private Transform playerPrehab = null;
    // [SerializeField]
    // private Transform exitPrehab = null;
    public bool playerSpawn = false;
    private int roomNum = 0;
    private bool roomSpawning = false;
    private bool roomSpawned = false;
    private int delayFrame = 2;
    private GameObject[] rooms;
    public List<GameObject> deletedObjects;
    // Start is called before the first frame update
    void Start()
    {
        if(maxRoomNum < chestRooms){
            chestRooms = maxRoomNum;
        }
        roomNum = maxRoomNum;
        var maze = MazeGenerator.Generate(width,height);
        Draw(maze);
        Invoke("SpawnChestRoom", 1.0f);
        StartCoroutine(waitForHaySpawn(maze));
    }

    
    private void Draw(WallState[,]maze){
        
        // var floor = Instantiate(floorPrehab, transform);
        // floor.localScale = new Vector3(width * size, 1,height * size);
        // floor.position = floor.position + new Vector3(0,-size/2,0);
        for(int i=0; i<width; ++i){
            for(int j=0; j<height; ++j){
                var cell = maze[i,j];
                var spawner = Instantiate(spawnerPrehab, transform) as Transform;
                spawner.localScale = new Vector3(spawner.localScale.x * size, spawner.localScale.y * size, spawner.localScale.z * size);
                var floor = Instantiate(floorPrehab, transform);
                floor.localScale = new Vector3(floor.localScale.x * size, floor.localScale.y * size, floor.localScale.z * size);
                var position = new Vector3((-width/2 + i) * size, 0, (-height/2 + j) * size);
                floor.position = position;
                spawner.position = position;
                if(cell.HasFlag(WallState.UP)){
                    if(j == height - 1){
                        var topWall = Instantiate(endWallPrehab,transform) as Transform;
                        topWall.position = position + new Vector3(0,0,size/2);
                        topWall.localScale = new Vector3(size, topWall.localScale.y * size, topWall.localScale.z * size);
                    }else{
                        var topWall = Instantiate(wallPrehab,transform) as Transform;
                        topWall.position = position + new Vector3(0,0,size/2);
                        topWall.localScale = new Vector3(size, topWall.localScale.y * size, topWall.localScale.z * size);
                    }
                }
                if(cell.HasFlag(WallState.LEFT)){
                    if(i == 0){
                        var leftWall = Instantiate(endWallPrehab, transform) as Transform;
                        leftWall.position = position + new Vector3(-size/2,0,0);
                        leftWall.localScale = new Vector3(size, leftWall.localScale.y * size, leftWall.localScale.z * size);
                        leftWall.eulerAngles = new Vector3(0,90,0);
                    }else{
                        var leftWall = Instantiate(wallPrehab, transform) as Transform;
                        leftWall.position = position + new Vector3(-size/2,0,0);
                        leftWall.localScale = new Vector3(size, leftWall.localScale.y * size, leftWall.localScale.z * size);
                        leftWall.eulerAngles = new Vector3(0,90,0);
                    }
                }
                if(i == width -1){
                    if(cell.HasFlag(WallState.RIGHT)){
                        var rightWall = Instantiate(endWallPrehab, transform) as Transform;
                        rightWall.position = position + new Vector3(size/2,0,0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y * size, rightWall.localScale.z * size);
                        rightWall.eulerAngles = new Vector3(0,90,0);
                    }
                }
                if(j == 0){
                    if(cell.HasFlag(WallState.DOWN)){
                        if(i == (width/2)-1){
                            var centralRoom = Instantiate(centralRoomPrehab, transform);
                            centralRoom.localScale = new Vector3(size, size, size);
                            centralRoom.position = position + new Vector3(0,0,-size*5);
                            FindObjectOfType<PlayerMovement>().transform.position = centralRoom.Find("StartPos").position;
                        }else{
                            var bottomWall = Instantiate(endWallPrehab,transform) as Transform;
                            bottomWall.position = position + new Vector3(0,0,-size/2);
                            bottomWall.localScale = new Vector3(size, bottomWall.localScale.y * size, bottomWall.localScale.z * size);
                        }
                    }
                }
            }
        }
    }
    private void SpawnHay(WallState[,]maze){
        Debug.Log("Spawn Hay");
        for(int i=0; i<width; ++i){
            for(int j=0; j<height; ++j){
                var cell = maze[i,j];
                var spawner2 = Instantiate(spawnerPrehab2, transform) as Transform;
                spawner2.localScale = new Vector3(spawner2.localScale.x * size, spawner2.localScale.y * size, spawner2.localScale.z * size);
                var position = new Vector3((-width/2 + i) * size, 0, (-height/2 + j) * size);
                spawner2.position = position;
            }
        }
    }

    // private void SpawnKey(){
    //     keySpawned = true;
    //     // if (keyNum > Chests.Count){
    //     //     keyNum = Chests.Count;
    //     // }
    //     int chestRand = Random.Range(0,Chests.Count-1);
    //     var player = Instantiate(playerPrehab, Chests[chestRand].position,Chests[chestRand].rotation);
    //     var exit = Instantiate(exitPrehab, Chests[chestRand].position,Chests[chestRand].rotation);

    //     while(keyNum >= 0){
    //         chestRand = Random.Range(0,Chests.Count-1);
    //         Debug.Log(chestRand);
    //         var key = Instantiate(keyPrehab, Chests[chestRand].position,Chests[chestRand].rotation);
    //         keyNum--;
    //     }
    // }

    private void SpawnRooms(){
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        roomSpawning = true;
        int randNum = Random.Range(0,spawners.Length-1);
        int roomRandNum = Random.Range(0,roomPrehabs.Length);
        var room = Instantiate(roomPrehabs[roomRandNum], transform);
        room.localScale = new Vector3(size, size, size);
        room.position = spawners[randNum].transform.position;
        roomNum -= 1;
    }

    // // public void AddChest(Vector3 chestTransform){
    // //     Chests.Add(chestTransform);
    // // }

    // // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(Chests.Count);
        // if(Chests.Count > 0 && !keySpawned){
        //     SpawnKey();
        // }
        if(overridedRoom >= maxRoomNum){
            roomNum = 0;
        }
        if(delayFrame > 0){
            delayFrame -= 1;
        }
        if(delayFrame <= 0){
            if(roomNum > 0){
                SpawnRooms();
            }
            if(roomNum <= 0 && !roomSpawned){
                StartCoroutine(waitForFixedUpdate());
                roomSpawned = true;
            }
        }
    }

    public bool RoomSpawnFinished(){
        return roomSpawned;
    }

    private void reActivateObjects(){
        for(int i = 0; i < transform.childCount; i++){
            if(!transform.GetChild(i).gameObject.activeSelf){
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        // foreach(GameObject deletedObject in deletedObjects){
        //     deletedObject.SetActive(true);
        // }
    }

    IEnumerator waitForFixedUpdate(){
        yield return new WaitForFixedUpdate();
        if(roomNum <= 0){
            reActivateObjects();
        }else{
            roomSpawned = false;
        }
        
    }

    IEnumerator waitForHaySpawn(WallState[,]maze){
        yield return new WaitForSeconds(0.5f);
        SpawnHay(maze);
    }
    public void AddRoomNum(){
        roomNum += 1;
    }
    public void AddOverridedRoom(){
        overridedRoom += 1;
    }
    public void SpawnChestRoom(){
            rooms = GameObject.FindGameObjectsWithTag("Room");
            int remaining = chestRooms;
            while(remaining > 0){
                int randNum = Random.Range(0,rooms.Length);
                if(!rooms[randNum].GetComponent<chestSpawner>().isChestRoom){
                    rooms[randNum].GetComponent<chestSpawner>().isChestRoom = true;
                    remaining--;
                }
            }
        }
}
