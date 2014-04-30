using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class VoxelChunk : MonoBehaviour {

	public GameObject worldGO;
	public int chunkX;
	public int chunkY;
	public int chunkZ;
	public bool update;

	public int chunkSize = 16;
	public float blockScale = 0.25f;
	public float tUnit = 1/32;
	public float slopeLength = 0.5f;

	private World world;
	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();
	private Mesh mesh;
	private int triangleCount;
	private int faceCount;

	private MeshCollider col;
	// Use this for initialization
	void Start () {
		world = worldGO.GetComponent<World>();
		mesh = GetComponent<MeshFilter>().mesh;
		col = GetComponent<MeshCollider>();
		GenerateMesh();
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
	void GenerateMesh () {
		for (int x = 0; x < chunkSize; x++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int z = 0; z < chunkSize; z++) {
					if (getBlock (x, y, z) != BlockType.Air) {
						GenerateBlock(x, y, z);
					}
				}
			}
		}
		UpdateMesh ();
	}
	// TODO : Optimiser les cas particuliers
	void GenerateBlock (int x, int y, int z) {
		if (getBlock (x, y + 1, z) == BlockType.Air) {
			GenerateFace (x, y, z, BlockSide.Top);
		}
		if (getBlock (x, y -1, z) == BlockType.Air) {
			GenerateFace (x, y, z, BlockSide.Bot);
		}
		if (getBlock (x, y, z + 1) == BlockType.Air) {
			GenerateFace (x, y, z, BlockSide.Front);	
			if (getBlock (x, y - 1, z + 1) != BlockType.Air){
				if (getBlock (x+1, y, z) == BlockType.Air && getBlock (x+1, y - 1, z + 1) != BlockType.Air ) {
					GenerateTriangle (x, y, z, BlockSide.FrontRight);
				}
				if (getBlock (x - 1, y, z) == BlockType.Air && getBlock (x - 1, y - 1 , z + 1) != BlockType.Air) {
					GenerateTriangle (x, y, z, BlockSide.FrontLeft);
				}
			}
		}
		if (getBlock (x, y, z - 1) == BlockType.Air) {
				GenerateFace (x, y, z, BlockSide.Back);
			if (getBlock (x, y - 1, z - 1) != BlockType.Air){
				if (getBlock (x + 1, y, z) == BlockType.Air && getBlock (x + 1, y - 1, z - 1) != BlockType.Air) {
					GenerateTriangle (x, y, z, BlockSide.BackRight);
				}
				if (getBlock (x - 1, y, z) == BlockType.Air && getBlock (x - 1, y - 1, z - 1) != BlockType.Air) {
					GenerateTriangle (x, y, z, BlockSide.BackLeft);
				}
			}

		}
		if (getBlock (x + 1, y, z) == BlockType.Air) {
			GenerateFace (x, y, z, BlockSide.Right);
		}
		if (getBlock (x - 1, y, z) == BlockType.Air) {
			GenerateFace (x, y, z, BlockSide.Left);
		}
	}


	void GenerateFace (int x, int y, int z, BlockSide side) {
		Vector3 direction = blocksList.directions[side];
		Vector3 blockPos = new Vector3(x, y, z) + direction; // Bloc dans la direction actuelle
		if (side != BlockSide.Top && side != BlockSide.Bot && getBlock ((int)blockPos.x, (int)(blockPos.y - 1), (int)blockPos.z) != BlockType.Air) { // bloc d'en dessous
			GenerateSlope(x, y, z, side);
		} else {
			GenerateSquare (x, y, z, side);
		}
	}
	void GenerateSquare (int x, int y, int z, BlockSide side) {
		switch(side) {
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
		Vector2 texturePos = chooseTexture(x, y, z, side);
		Face (texturePos);
	}
	void GenerateSlope (int x, int y, int z, BlockSide direction) {
		if (direction != BlockSide.Top && direction != BlockSide.Bot) {
			switch (direction) {
			case BlockSide.Left:
				newVertices.Add (new Vector3(x, y, z + 1) * blockScale);
				newVertices.Add (new Vector3(x, y, z) * blockScale);
				newVertices.Add (new Vector3(x - slopeLength, y - 1, z) * blockScale);
				newVertices.Add (new Vector3(x - slopeLength, y - 1, z + 1) * blockScale);
				break;
			case BlockSide.Right:
				newVertices.Add (new Vector3(x + 1, y, z) * blockScale);
				newVertices.Add (new Vector3(x + 1, y, z + 1) * blockScale);
				newVertices.Add (new Vector3(x + 1 + slopeLength, y - 1, z + 1) * blockScale);
				newVertices.Add (new Vector3(x + 1 + slopeLength, y - 1, z) * blockScale);
				break;
			case BlockSide.Front:
				newVertices.Add (new Vector3(x + 1, y, z + 1) * blockScale);
				newVertices.Add (new Vector3(x, y, z + 1) * blockScale);
				newVertices.Add (new Vector3(x, y - 1, z + 1 + slopeLength) * blockScale);
				newVertices.Add (new Vector3(x + 1, y - 1, z + 1 + slopeLength) * blockScale);
				break;
			case BlockSide.Back:
				newVertices.Add (new Vector3(x, y, z) * blockScale);
				newVertices.Add (new Vector3(x + 1, y, z) * blockScale);
				newVertices.Add (new Vector3(x + 1, y - 1, z - slopeLength) * blockScale);
				newVertices.Add (new Vector3(x, y - 1, z - slopeLength) * blockScale);
				break;

			}

			Vector2 texturePos = chooseTexture (x, y, z, direction);
			Face (texturePos);
			GenerateSlopeBorders(x, y, z, direction);
		}

	}

	void GenerateSlopeBorders (int x, int y, int z, BlockSide direction) {
		Vector2 texture = chooseTexture(x, y, z, direction);
		switch (direction) {
		case BlockSide.Left:
			newVertices.Add (new Vector3(x - slopeLength, y - 1, z) * blockScale);
			newVertices.Add (new Vector3(x, y, z) * blockScale);
			newVertices.Add (new Vector3(x, y - 1, z) * blockScale);
			Triangle(texture);
			newVertices.Add (new Vector3(x - slopeLength, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x, y, z + 1) * blockScale);
			Triangle(texture);
			break;
		case BlockSide.Right:
			newVertices.Add (new Vector3(x + 1, y, z) * blockScale);
			newVertices.Add (new Vector3(x + 1 + slopeLength, y - 1, z) * blockScale);
			newVertices.Add (new Vector3(x + 1, y - 1, z) * blockScale);
			Triangle(texture);
			newVertices.Add (new Vector3(x + 1, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x + 1 + slopeLength, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x + 1, y, z + 1) * blockScale);
			Triangle(texture);
			break;
		case BlockSide.Front:
			newVertices.Add (new Vector3(x + 1, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x + 1, y, z + 1) * blockScale);
			newVertices.Add (new Vector3(x + 1, y - 1, z + 1 + slopeLength) * blockScale);
			Triangle(texture);
			newVertices.Add (new Vector3(x, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x, y - 1, z + 1 + slopeLength) * blockScale);
			newVertices.Add (new Vector3(x, y, z + 1) * blockScale);
			Triangle(texture);
			break;
		case BlockSide.Back:
			newVertices.Add (new Vector3(x, y - 1, z) * blockScale);
			newVertices.Add (new Vector3(x, y, z) * blockScale);
			newVertices.Add (new Vector3(x, y - 1, z - slopeLength) * blockScale);
			Triangle(texture);
			newVertices.Add (new Vector3(x + 1, y - 1, z) * blockScale);
			newVertices.Add (new Vector3(x + 1, y - 1, z - slopeLength) * blockScale);
			newVertices.Add (new Vector3(x + 1, y, z) * blockScale);
			Triangle(texture);
			break;
		}
	}
	void GenerateTriangle (int x, int y, int z, BlockSide direction) {
		if (direction == BlockSide.FrontRight) {
			newVertices.Add (new Vector3(x + 1, y - 1, z + 1 + slopeLength) * blockScale);
			newVertices.Add (new Vector3(x + 1 + slopeLength, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x + 1, y, z + 1) * blockScale);
		} else if ( direction == BlockSide.FrontLeft) {
			newVertices.Add (new Vector3(x - slopeLength, y - 1, z + 1) * blockScale);
			newVertices.Add (new Vector3(x, y - 1, z + 1 + slopeLength) * blockScale);
			newVertices.Add (new Vector3(x, y, z + 1) * blockScale);
		} else if (direction == BlockSide.BackRight) {
			newVertices.Add (new Vector3(x + 1 + slopeLength, y - 1, z) * blockScale);
			newVertices.Add (new Vector3(x + 1, y - 1, z - slopeLength) * blockScale);
			newVertices.Add (new Vector3(x + 1, y, z) * blockScale);
		} else if (direction == BlockSide.BackLeft) {
			newVertices.Add (new Vector3(x, y - 1, z - slopeLength) * blockScale);
			newVertices.Add (new Vector3(x - slopeLength, y - 1, z) * blockScale);
			newVertices.Add (new Vector3(x, y, z) * blockScale);
		}
		Vector2 texturePos = chooseTexture (x, y, z, direction);
		Triangle (texturePos);
	}
	void Face (Vector2 texturePos) {
		int offset = faceCount * 4 + triangleCount * 3;
		newTriangles.Add(offset  ); //1
		newTriangles.Add(offset + 1 ); //2
		newTriangles.Add(offset + 2 ); //3
		newTriangles.Add(offset  ); //1
		newTriangles.Add(offset + 2 ); //3
		newTriangles.Add(offset + 3 ); //4

		newUV.Add(new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y));
		newUV.Add(new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
		faceCount++;
	}
	void Triangle (Vector2 texturePos) {
		int offset = faceCount * 4 + triangleCount * 3;

		newTriangles.Add (offset);
		newTriangles.Add (offset + 1);
		newTriangles.Add (offset + 2);

		newUV.Add (new Vector2(tUnit * texturePos.x, tUnit * texturePos.y));
		newUV.Add (new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
		newUV.Add (new Vector2(tUnit * texturePos.x + tUnit / 2, tUnit * texturePos.y + tUnit));
		triangleCount++;
	}

	Vector2 chooseTexture (int x, int y, int z, BlockSide side) {
		BlockType bType = getBlock (x, y, z);
		Block block = blocksList.blocks[bType];
		return block.faces[side];
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
		
		triangleCount = 0;
		faceCount = 0;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
