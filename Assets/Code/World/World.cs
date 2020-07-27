using Assets.Code.Tools;
using Assets.Code.World.Chunks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Code.World
{
    class World:MonoBehaviour
    {
        public GameObject player;
        public int worldTime;
        public int WorldEndTime = 1000;

        public Camera activeCamera;
        private Chunk[] Chunks;
        private int activeChunks = 0;

        [SerializeField] private int worldTimeAdvanceMultiplier = 1;

        [SerializeField] private int chunkCount = 5;
        private int chunksCreated = 0;

        private float currentRightMost;

        /// <summary>
        /// The x-axis distance between rightmost chunk position and the subject (usually the player) position.<br></br>
        /// Used while checking when to create chunks
        /// </summary>
        public float chunkTriggerDistance = 20; 
        /// <summary>
        /// The offset for deleting chunks after player moves past them
        /// </summary>
        private float chunkRemovalPositionOffset = 15;

        private Coroutine timerCoroutine;
        public void Awake()
        {
            activeCamera = FindObjectOfType<Camera>();
            currentRightMost = float.NegativeInfinity;
            Chunks = new Chunk[5];
            CreateNewChunk(); // Create One Chunk
            timerCoroutine = StartCoroutine(WorldTimer());
        }

        public bool TimerStartStopState(bool shouldActivate)
        {
            if (shouldActivate)
            {
                timerCoroutine = StartCoroutine(WorldTimer());
                return true;
            }
            else if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                return true;
            }

            return false;
        }

        private bool worldTimerCR_isActive = true;
        IEnumerator WorldTimer()
        {
            while (worldTimerCR_isActive)
            {
                yield return new WaitForSeconds(1f);
                ChangeWorldTime(worldTimeAdvanceMultiplier * 1);
            }
            worldTimerCR_isActive = true;
        }

        private float GetRightmost()
        {
            if (chunksCreated == 0 || this.currentRightMost == float.NegativeInfinity) //If chunksCreated is 0 or if currentRightMost is negative infinity, then there is no chunk at all
            {
                //currentRightmost position should be the new chunk's leftmost
                currentRightMost = player.transform.position.x - activeCamera.orthographicSize * activeCamera.aspect; // * aspect is because of unity's orthographicSize is the height value. (This kind of stuff is the reason why i hate unity)
                return currentRightMost;
            }
            else
            {
                Chunk tempChunk;
                float tempRM = float.NegativeInfinity;
                for(int i = 0; i<Chunks.Length; i++)
                {
                    if(Chunks[i] != null)
                    {
                        tempChunk = Chunks[i];
                        float goPos = tempChunk.gameObject.transform.position.x + tempChunk.Size.x / 2;
                        if (goPos > tempRM)
                        {
                            tempRM = goPos;
                        }
                    }
                   
                }
                currentRightMost = tempRM;
            }
            return currentRightMost;
        }
        public Chunk CreateNewChunk()
        {
                //If there is not enough room in the chunk buffer, no chunk should be created
                //In theory, this exception should never be thrown since chunks should be deleted after player passes them 
                    //(and the further buffers should be no more than the buffer size)
                if (activeChunks >= Chunks.Length)
                    throw new Exception("Chunk Buffer Full");

                Chunk chunk = WorldGenerator.GenerateChunk(new Vector2(GetRightmost(), 0), activeCamera);
                activeChunks++;
                chunksCreated++;
                for(int i = 0; i<Chunks.Length; i++)
                {
                    if (Chunks[i] == null)
                    {
                        Chunks[i] = chunk;
                        break;
                    }

                }
                return chunk;

                    
        }
        private int RemoveUnusedChunks(int index)
        {
            Destroy(Chunks[index].gameObject);
            Chunks[index] = null;
            activeChunks--;
            return 0;
        }
        public void MoveChunks(float deltaX)
        {
            UpdateChunks(movementDelta: deltaX);
        }

        public bool ChangeWorldTime(int delta, bool isTimeMachineInput = false)
        {
            //If player tries to alter time but goes beyond world limits, dont change time.
            if (isTimeMachineInput)
                worldTime += ((worldTime + delta > WorldEndTime) || (worldTime + delta < 0)) ? 0 : delta;
            else //If world time progresses naturally just add delta
                worldTime += delta;

            if (worldTime > 1000)
            {
                EndGame();
                return false;
            }
            UpdateChunks(timeDelta:delta);
            return true;
        }
        /// <summary>
        /// <br>When a movement occurs, this method is called by the MovementController class</br>
        /// </summary>
        /// <param name="timeDelta"></param>
        /// <param name="movementDelta"></param>
        /// <returns></returns>
        private int UpdateChunks(float timeDelta = 0, float movementDelta = 0)
        {
            Chunk c;
            for (int i = 0; i < Chunks.Length; i++)
            {
                if (Chunks[i] != null)
                {
                    c = Chunks.ElementAt(i);
                    if (Math.Abs(movementDelta) > 0f)
                    {  
                        c.MoveChunk(movementDelta);
                        //If chunk's rightmost position is now behind the player, then it cannot be seen again. Therefore it can be destoyed.
                        if (c.transform.position.x  + c.Size.x / 2 < (activeCamera.transform.position.x - activeCamera.orthographicSize/2) - chunkRemovalPositionOffset)
                            RemoveUnusedChunks(i);
                    }
                    if(timeDelta != 0f) //Time is being altered. Update chunk blippers
                    {
                        c.UpdateBlippers(); 
                    }
                }
            }
            GetRightmost();
            //Check if there is need for new chunks
            if(currentRightMost < player.transform.position.x + chunkTriggerDistance) //Player nears the end of a chunk. Need to create a new chunk
            {
                CreateNewChunk();
            }

            return 0;
        }

        public void EndGame()
        {
            Debug.Log("Game Ends");
        }
    }
}
