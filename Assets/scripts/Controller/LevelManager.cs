using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private const int TILES_PER_ROW = 10;
	
	private int _rowY;

	public Transform leftSide;
	public Transform rightSide;
	public Transform level;
	public Transform singleBlock;
	public Transform leftBlock;
	public Transform rightBlock;
	public Transform centerBlock;
	public Transform scoreTrigger;
	public Transform laserTrigger;
	public Transform laser;
	public float gapChance;
	public int initialRows;


	void Start () {
		Init();
	}

	public void Init() {
		_rowY = 0;

		foreach (Transform child in level.transform) {
            Destroy(child.gameObject);
        }

		for (int i = 0; i < initialRows; i++) {
			AddRow ();
		}
	}
	
	
	void AddRow() {
		string row = _GenerateRowString();

		for (int i = 0; i < TILES_PER_ROW; i++) {
			bool block_to_left  = false;
			bool block_to_right = false;
			
			if (row[i] == ' ')
				continue;
				
			if (i == 0 || row[i-1] == '0')
				block_to_left = true;
				
			if (i == TILES_PER_ROW - 1 || row[i+1] == '0')
				block_to_right = true;
			
			Transform block_prefab = null;
			if (block_to_left && block_to_right)
				block_prefab = centerBlock;
			if (block_to_left && !block_to_right)
				block_prefab = rightBlock;
			if (!block_to_left && block_to_right)
				block_prefab = leftBlock;
			if (!block_to_left && !block_to_right)
				block_prefab = singleBlock;
				
			Transform block = Instantiate(block_prefab, new Vector2(i * 32 - 144, _rowY), Quaternion.identity) as Transform;
			block.parent = level.transform;
		}

		Transform laser_trigger = Instantiate (laserTrigger, new Vector2(0, _rowY - 36), Quaternion.identity) as Transform;
		laser_trigger.parent = level.transform;

		Transform score_trigger = Instantiate (scoreTrigger, new Vector2(0, _rowY - 68), Quaternion.identity) as Transform;
		score_trigger.parent = level.transform;
		
		_rowY -= 64;
	}

	private string _GenerateRowString() {
		bool reject_level = true;
		string row = "";

		while (reject_level) {
			reject_level = false;
			
			row = "";
			for (int i = 0; i < TILES_PER_ROW; i++) {
				if (Random.value < gapChance) {
					row = row + " ";
				}
				else {
					row = row + "0";
				}
			}
			
			if (row.Contains ("00000")) {
				reject_level = true;
			}
		}
		return row;
	}
	
	public void AddRowWithLaser(LaserTrigger trigger) {
		AddRow ();
		float laser_y = Mathf.Ceil(trigger.transform.position.y / 32) * 32;
		AddLaser(laser_y, trigger.LeftSide, trigger.RightSide);		
	}	
	
	void AddLaser(float y_position, bool left_side, bool right_side) {		
		//SpriteRenderer renderer = laser.GetComponent<SpriteRenderer>();
		//float laser_width = renderer.sprite.bounds.size.x;
		
		if (left_side) {
			Transform laser_transform = Instantiate (laser, new Vector2(leftSide.transform.position.x, y_position), Quaternion.identity) as Transform;
			Laser l = laser_transform.GetComponent<Laser>();
			l.transform.parent = level.transform;
			l.Init("right");
		}

		if (right_side) {
			Transform laser_transform = Instantiate (laser, new Vector2(rightSide.transform.position.x, y_position), Quaternion.identity) as Transform;
			Laser l = laser_transform.GetComponent<Laser>();
			l.transform.parent = level.transform;
			l.Init("left");
		}
	}

	public static LevelManager GetInstance() {
		return GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}
}
