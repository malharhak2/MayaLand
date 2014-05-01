using UnityEngine;
using System.Collections;

public class ModifyTerrain : MonoBehaviour {

	public Transform player;
	public int loadDistanceX;
	public int loadDistanceY;
	public bool mouseCapture = false;

	private World world;
	private GameObject cameraGO;
	private Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
	// Use this for initialization
	void Start () {
		world = gameObject.GetComponent<World>();
		cameraGO = GameObject.FindGameObjectWithTag("MainCamera");
		Screen.lockCursor = true;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			ReplaceBlockCursor(BlockType.Air);
		}
		
		if(Input.GetMouseButtonDown(1)){
			AddBlockCursor(BlockType.Stone);
		}
		if (Input.GetButtonDown ("CaptureMouse")) {
			mouseCapture = !mouseCapture;
		}
		if (mouseCapture) {
			Screen.lockCursor = true;
		} else {
			Screen.lockCursor = false;
		}
		LoadChunks(player.position, loadDistanceX, loadDistanceY);
	}
	public void LoadChunks(Vector3 playerPos, float distToLoad, float distToUnload) {
		for(int x=0;x<world.chunks.GetLength(0);x++){
			for(int z=0;z<world.chunks.GetLength(2);z++){
				
				float dist=Vector2.Distance(new Vector2(x*world.actChunkSize,
				                                        z*world.actChunkSize),
				                            new Vector2(playerPos.x,playerPos.z));
				
				if(dist<distToLoad){
					if(world.chunks[x,0,z]==null){
						world.GenColumn(x,z);
					}
				} else if(dist>distToUnload){
					if(world.chunks[x,0,z]!=null){
						
						world.UnloadColumn(x,z);
					}
				}
				
			}
		}
	}
	public void SetBlockAt(Vector3 position, BlockType block) {
		//adds the specified block at these coordinates
		
		world.SetBlock(position, block);
		
	}

	public void ReplaceBlockAt(RaycastHit hit, BlockType block) {
		//removes a block at these impact coordinates, you can raycast against the terrain and call this with the hit.point
		Vector3 position = hit.point;
		position+=(hit.normal*-0.5f);
		
		SetBlockAt(position, block);
	}
	public void AddBlockAt(RaycastHit hit, BlockType block) {
		//adds the specified block at these impact coordinates, you can raycast against the terrain and call this with the hit.point
		Vector3 position = hit.point;
		position+=(hit.normal*0.5f);
		
		SetBlockAt(position,block);
		
	}
	public void ReplaceBlockCursor(BlockType block){
		//Replaces the block specified where the mouse cursor is pointing
		
		Ray ray = Camera.main.ScreenPointToRay (screenCenter);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			
			ReplaceBlockAt(hit, block);
			Debug.DrawLine(ray.origin,ray.origin+( ray.direction*hit.distance),
			               Color.green,2);
			
		}
		
	}
	
	public void AddBlockCursor( BlockType block){
		//Adds the block specified where the mouse cursor is pointing
		
		Ray ray = Camera.main.ScreenPointToRay (screenCenter);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			
			AddBlockAt(hit, block);
			Debug.DrawLine(ray.origin,ray.origin+( ray.direction*hit.distance),
			               Color.green,2);
		}
		
	}
	public void ReplaceBlockCenter(float range, BlockType block){
		//Replaces the block directly in front of the player
		
		Ray ray = new Ray(cameraGO.transform.position, cameraGO.transform.forward);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			
			if(hit.distance<range){
				ReplaceBlockAt(hit, block);
			}
		}
		
	}
	
	public void AddBlockCenter(float range, BlockType block){
		//Adds the block specified directly in front of the player
		
		Ray ray = new Ray(cameraGO.transform.position, cameraGO.transform.forward);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			
			if(hit.distance<range){
				AddBlockAt(hit,block);
			}
			Debug.DrawLine(ray.origin,ray.origin+( ray.direction*hit.distance),Color.green,2);
		}
		
	}
	public void UpdateChunkAt(int x, int y, int z) { 
		//Updates the chunk containing this block
		
		int updateX= Mathf.FloorToInt( x/world.chunkSize);
		int updateY= Mathf.FloorToInt( y/world.chunkSize);
		int updateZ= Mathf.FloorToInt( z/world.chunkSize);
		
		print("Updating: " + updateX + ", " + updateY + ", " + updateZ);
  
		world.chunks[updateX,updateY, updateZ].update = true;
		if(x-(world.chunkSize*updateX)==0 && updateX!=0){
			world.chunks[updateX-1,updateY, updateZ].update=true;
		}
		
		if(x-(world.chunkSize*updateX)==15 && updateX!=world.chunks.GetLength(0)-1){
			world.chunks[updateX+1,updateY, updateZ].update=true;
		}
		
		if(y-(world.chunkSize*updateY)==0 && updateY!=0){
			world.chunks[updateX,updateY-1, updateZ].update=true;
		}
		
		if(y-(world.chunkSize*updateY)==15 && updateY!=world.chunks.GetLength(1)-1){
			world.chunks[updateX,updateY+1, updateZ].update=true;
		}
		
		if(z-(world.chunkSize*updateZ)==0 && updateZ!=0){
			world.chunks[updateX,updateY, updateZ-1].update=true;
		}
		
		if(z-(world.chunkSize*updateZ)==15 && updateZ!=world.chunks.GetLength(2)-1){
			world.chunks[updateX,updateY, updateZ+1].update=true;
		}
	  
	}
}
