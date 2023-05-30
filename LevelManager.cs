using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager sharedInstance;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPosition;

    int cont = 0;
    bool band4 = true;
    void Awake()
    {
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    void Start () {
        GenerateInitialBlocks();
	}
	
    public void AddLevelBlock(){
        int randomIdx = Random.Range(0, 10);

        cont++;
        if (cont >= 9) {
            randomIdx = 10;
        }

        if (band4) { 
        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0){
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }else{
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks
                [currentLevelBlocks.Count - 1].
                 exitPoint.position;
        }

        block.transform.SetParent(this.transform,false);

        Vector3 correction = new Vector3(
            spawnPosition.x-block.startPoint.position.x,
            spawnPosition.y-block.startPoint.position.y,
            0 );
        block.transform.position = correction;
        currentLevelBlocks.Add(block);

        }
        if (cont >= 9)
        {
            band4 = false;
        }
    }

    public void RemoveLevelBlock(){
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlocks(){
        while(currentLevelBlocks.Count>0){
            RemoveLevelBlock();
        }
        cont = 0;
        band4 = true;
    }

    public void GenerateInitialBlocks(){
        for (int i = 0; i < 2; i++){
            AddLevelBlock();
        }
    }
}
