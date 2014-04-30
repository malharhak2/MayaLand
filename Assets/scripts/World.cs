using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject chunk;
	public VoxelChunk[,,] chunks;
	public int chunkSize = 16;

	public BlockType[,,] data;
	public int worldX = 16;
	public int worldY = 16;
	public int worldZ = 16;

	public float blockSize = 0.25f;
	public int actChunkSize;
	// Use this for initialization
	void Start () {
		actChunkSize = (int) (chunkSize * blockSize);
		data = new BlockType[worldX, worldY, worldZ];

		for (int x = 0; x < worldX; x++) {
			for (int z = 0; z < worldZ; z++) {
				int sX = x;
				int sZ = z;
				int stone = PerlinNoise(sX, 0, sZ, 40, 6,  1.2f);
				stone += PerlinNoise(sX, 300, sZ, 80, 8, 0) + 10;
				int dirt = PerlinNoise (sX, 100, sZ, 100, 2, 0) + 1; 

				for (int y = 0; y < worldY; y++) {
					if (y <= stone) {
						data[x, y, z] = BlockType.Stone;
					} else if (y <= dirt + stone) {
						data[x, y, z] = BlockType.Grass;
					}
				}
			}
		}

		chunks = new VoxelChunk[Mathf.FloorToInt (worldX / chunkSize),
		                        Mathf.FloorToInt(worldY / chunkSize),
		                        Mathf.FloorToInt(worldZ / chunkSize)];
		//GenEverything();

	}
	public void SetBlock (Vector3 position, BlockType block) {
		int x = (int) (position.x / blockSize);
		int y = (int) (position.y / blockSize);
		int z = (int) (position.z / blockSize);
		data[x, y, z] = block;
		updateChunks (x, y, z);
	}
	private void updateChunks (int x, int y, int z) {
		int updateX= Mathf.FloorToInt( x / chunkSize);
		int updateY= Mathf.FloorToInt( y / chunkSize);
		int updateZ= Mathf.FloorToInt( z / chunkSize);
		

		chunks[updateX,updateY, updateZ].update = true;
		if(x-(chunkSize*updateX)==0 && updateX!=0){
			chunks[updateX-1,updateY, updateZ].update=true;
		}
		
		if(x-(chunkSize*updateX)==15 && updateX!=chunks.GetLength(0)-1){
			chunks[updateX+1,updateY, updateZ].update=true;
		}
		
		if(y-(chunkSize*updateY)==0 && updateY!=0){
			chunks[updateX,updateY-1, updateZ].update=true;
		}
		
		if(y-(chunkSize*updateY)==15 && updateY!=chunks.GetLength(1)-1){
			chunks[updateX,updateY+1, updateZ].update=true;
		}
		
		if(z-(chunkSize*updateZ)==0 && updateZ!=0){
			chunks[updateX,updateY, updateZ-1].update=true;
		}
		
		if(z-(chunkSize*updateZ)==15 && updateZ!=chunks.GetLength(2)-1){
			chunks[updateX,updateY, updateZ+1].update=true;
		}
	}

	public void GenEverything () {
		for (int x = 0; x < chunks.GetLength (0); x++) {
			for (int y = 0; y < chunks.GetLength (1); y++) {
				for (int z = 0; z < chunks.GetLength (2); z++) {
					GameObject newChunk = Instantiate (chunk,
					         	new Vector3(
									x * actChunkSize - .5f * blockSize,
									y * actChunkSize + .5f * blockSize,
									z * actChunkSize - .5f * blockSize),
					   	Quaternion.identity) as GameObject;
					newChunk.transform.parent = transform;
					VoxelChunk newChunkScript = newChunk.GetComponent<VoxelChunk>();
					chunks[x, y, z] = newChunkScript;
					newChunkScript.worldGO = gameObject;
					newChunkScript.chunkSize = chunkSize;
					newChunkScript.chunkX = x * chunkSize;
					newChunkScript.chunkY = y * chunkSize;
					newChunkScript.chunkZ = z * chunkSize;
				}
			}
		}
	}
	public void GenColumn (int x, int z) {

		for (int y = 0; y < chunks.GetLength (1); y++) {
			GameObject newChunk = Instantiate (chunk,
                   new Vector3 (
						x * actChunkSize -.5f * blockSize, 
			            y * actChunkSize + .5f * blockSize, 
						z * actChunkSize - .5f * blockSize),
                   new Quaternion (0, 0, 0, 0)) as GameObject;
			newChunk.transform.parent = transform;
			VoxelChunk newChunkScript = newChunk.GetComponent<VoxelChunk>();
			chunks[x, y, z] = newChunkScript;
			newChunkScript.worldGO = gameObject;
			newChunkScript.chunkSize = chunkSize;
			newChunkScript.chunkX = x * chunkSize;
			newChunkScript.chunkY = y * chunkSize;
			newChunkScript.chunkZ = z * chunkSize;
		}
	}
	public void UnloadColumn (int x, int z) {
		for (int y = 0; y < chunks.GetLength (1); y++) {
			Object.Destroy (chunks[x, y, z].gameObject);
		}
	}

	public BlockType getBlock (int x, int y, int z) {
		x = (int) (x);
		y = (int) (y);
		z = (int) (z);
		if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0 ) {
			return BlockType.Stone;
		} 

		return data[x, y, z];
	}

	int PerlinNoise(int x,int y, int z, float scale, float height, float power){
		float rValue;
		rValue=Noise.GetNoise (((double)x) / scale, ((double)y)/ scale, ((double)z) / scale);
		rValue*=height;
		
		if(power!=0){
			rValue=Mathf.Pow( rValue, power);
		}
		
		return (int) rValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
