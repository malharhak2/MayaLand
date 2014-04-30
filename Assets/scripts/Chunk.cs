using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

	public GameObject worldGO;
	public int chunkSize = 16;
	public int chunkX;
	public int chunkY;
	public int chunkZ;
	public bool update;
	public float blockScale = .25f;

	private World world;

	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();

	private float tUnit = 0.25f;
	private Mesh mesh;
	private MeshCollider col;

	private int faceCount;
	// Use this for initialization
	void Start () {
		world = worldGO.GetComponent<World>();
		mesh = GetComponent<MeshFilter>().mesh;
		col = GetComponent<MeshCollider>();
		GenerateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LateUpdate () {
		if (update) {
			GenerateMesh ();
			update = false;
		}
	}

	BlockType getBlock (int x, int y, int z) {
		return world.getBlock (x + chunkX, y + chunkY, z + chunkZ);
	}
	public void  GenerateMesh () {
		for (int x= 0; x < chunkSize; x++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int z = 0; z < chunkSize; z++) {
					if (getBlock (x, y, z) != 0) {
						if (getBlock (x, y+1, z) == BlockType.Air) {
							MakeSide (x, y, z, BlockSide.Top);
						}
						if (getBlock (x, y - 1, z) == BlockType.Air) {
							MakeSide (x, y, z, BlockSide.Bot);
						}
						if(getBlock(x+1,y,z)== BlockType.Air){
							MakeSide (x,y,z, BlockSide.Right);
						}
						if(getBlock(x-1,y,z)== BlockType.Air){
							MakeSide (x,y,z, BlockSide.Left);
						}
						if(getBlock(x,y,z+1)== BlockType.Air){
							MakeSide (x,y,z, BlockSide.Front);
						}
						if(getBlock(x,y,z-1)==BlockType.Air){
							MakeSide (x,y,z, BlockSide.Back);
						}
					}
				}
			}
		}
		UpdateMesh();
	}


	void MakeSide (int x, int y, int z, BlockSide side) {
		switch (side) {
		case BlockSide.Left:
			newVertices.Add(new Vector3 (x, y- 1, z + 1) * blockScale);
			newVertices.Add(new Vector3 (x, y, z + 1) * blockScale);
			newVertices.Add(new Vector3 (x, y, z) * blockScale);
			newVertices.Add(new Vector3 (x, y - 1, z) * blockScale);
			break;
		case BlockSide.Right:
			newVertices.Add(new Vector3 (x + 1, y - 1, z) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y, z) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y, z + 1) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y - 1, z + 1) * blockScale);
			break;
		case BlockSide.Front:
			newVertices.Add(new Vector3 (x + 1, y-1, z + 1) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y, z + 1) * blockScale);
			newVertices.Add(new Vector3 (x, y, z + 1) * blockScale);
			newVertices.Add(new Vector3 (x, y-1, z + 1) * blockScale);
			break;
		case BlockSide.Back:
			newVertices.Add(new Vector3 (x, y - 1, z) * blockScale);
			newVertices.Add(new Vector3 (x, y, z) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y, z) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y - 1, z) * blockScale);
			break;
		case BlockSide.Top:
			newVertices.Add(new Vector3 (x,  y,  z + 1) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y,  z + 1) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y,  z ) * blockScale);
			newVertices.Add(new Vector3 (x,  y,  z ) * blockScale);
			break;
		case BlockSide.Bot:
			newVertices.Add(new Vector3 (x,  y-1,  z ) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y-1,  z ) * blockScale);
			newVertices.Add(new Vector3 (x + 1, y-1,  z + 1) * blockScale);
			newVertices.Add(new Vector3 (x,  y-1,  z + 1) * blockScale);
			break;
		}
		Vector2 texturePos = chooseTexture (x, y, z, side);
		Cube (texturePos);
	}

	Vector2 chooseTexture (int x, int y, int z, BlockSide side) {
		BlockType bType = getBlock (x, y, z);
		Block block = blocksList.blocks[bType];
		return block.faces[side];
	}
	void Cube (Vector2 texturePos) {
		newTriangles.Add(faceCount * 4  ); //1
		newTriangles.Add(faceCount * 4 + 1 ); //2
		newTriangles.Add(faceCount * 4 + 2 ); //3
		newTriangles.Add(faceCount * 4  ); //1
		newTriangles.Add(faceCount * 4 + 2 ); //3
		newTriangles.Add(faceCount * 4 + 3 ); //4
		
		newUV.Add(new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
		newUV.Add(new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y));
		
		faceCount++;
	}

	void UpdateMesh () {
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
		
		col.sharedMesh=null;
		col.sharedMesh=mesh;
		
		newVertices.Clear();
		newUV.Clear();
		newTriangles.Clear();
		
		faceCount=0;
	}
}
