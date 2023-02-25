using UnityEngine;
using System.Collections.Generic;

namespace DP.Gameplay
{
    /// <summary>
    /// A generic object pool for Unity GameObjects. Used for object recycling in scenarios where creating new GameObject instances is expensive. 
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        /// <summary>
        /// Queue of pooled GameObjects available for spawning.
        /// </summary>
        public Queue<GameObject> pooledObjects = new Queue<GameObject>();

        /// <summary>
        /// Queue of spawned GameObjects, used to keep track of them.
        /// </summary>
        public Queue<GameObject> spawnedObjects = new Queue<GameObject>();

        /// <summary>
        /// Array of GameObjects to be pooled.
        /// </summary>
        [SerializeField] GameObject[] pooledObject;

        /// <summary>
        /// The transform to which the cloned GameObjects should be parented. 
        /// </summary>
        [SerializeField] Transform container;

        /// <summary>
        /// The number of objects of each type that should spawn at start.
        /// </summary>
        [Tooltip("How many objects of each type should spawn at start?")]
        [SerializeField] int prewarmObjectsPerType;

        /// <summary>
        /// Returns a GameObject from the object pool. If no object is available in the pool, a new one is instantiated.
        /// </summary>
        /// <returns>A GameObject from the object pool.</returns>
        public GameObject Get()
        {
            if (pooledObjects.Count == 0)
            {
                var reusedObject = spawnedObjects.Dequeue();
                reusedObject.SetActive(false);
                pooledObjects.Enqueue(reusedObject);
            }
            var spawnedObject = pooledObjects.Dequeue();
            spawnedObjects.Enqueue(spawnedObject);
            return spawnedObject;
        }

        /// <summary>
        /// Adds specified number of GameObjects to the pool.
        /// </summary>
        /// <param name="count">Number of GameObjects to add.</param>
        void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (GameObject newObject in pooledObject)
                {
                    GameObject newObjectInst = GameObject.Instantiate(newObject);
                    newObjectInst.SetActive(false);
                    newObjectInst.transform.parent = container;
                    pooledObjects.Enqueue(newObjectInst);
                }
            }
        }

        /// <summary>
        /// Instantiates the prewarmObjectsPerType number of GameObjects at the start of the game.
        /// </summary>
        private void Awake()
        {
            AddObjects(prewarmObjectsPerType);
        }

        /// <summary>
        /// Returns a GameObject to the object pool, making it available for reuse.
        /// </summary>
        /// <param name="objectToReturn">GameObject to return to the pool.</param>
        public void ReturnToPool(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pooledObjects.Enqueue(objectToReturn);
            spawnedObjects.Dequeue();
        }
    }

    public class OldExplainedObjectPool : MonoBehaviour
    {
        public Queue<GameObject> pooledObjects = new Queue<GameObject>();//objects available to spawn
        public Queue<GameObject> spawnedObjects = new Queue<GameObject>();//keep track of spawned objects in another queue, you'll see why
        [SerializeField] GameObject[] pooledObject;//what will you pool?
        [SerializeField] Transform container;//where to send the clones? you sure don't want them just jerking around in the root of hierarchy

        //even if the ram doesn't spike,don't go too far, 500 decals yielded 15 fps on full Overclock.
        [Tooltip("How many objects of each type should spawn at start?")] [SerializeField] int prewarmObjectsPerType;

        public GameObject Get()
        {
            //call this method when you want to spawn whatever this pool holds I.E var enemy = pool.Get();
            //from there you can do stuff like enemy.setactive();

            if (pooledObjects.Count == 0)//if we have no pooled objects
            {
                var reusedObject = spawnedObjects.Dequeue();//we define a variable and that is pull our first spawned object out of the spawned queue
                reusedObject.SetActive(false);//then we disable it to to override the Despawner class behavior
                pooledObjects.Enqueue(reusedObject);//then we give that object to the queue that handles spawning

                //infact you could call this as ReturnToPool(reusedObject); but I prefer to explain
                //and that would dequeue an additional object, but you can extract the method for organization
            }
            var spawnedObject = pooledObjects.Dequeue();// given that we now will always have stuff to spawn, we dequeue the next spawnable object
            spawnedObjects.Enqueue(spawnedObject);//we send it to the spawned queue so we can call it in case we run out of objects (dont use this with enemies)
            return spawnedObject; // and given that this method is a GameObject, it must return a GO, so return the Object to the method this was called from
        }

        private void Awake()
        {
            AddObjects(prewarmObjectsPerType);//Instantiate what we need before the first frame
        }
        public void ReturnToPool(GameObject objectToReturn)// we call this from our despawner as pool.ReturnToPool(gameObject);
        {
            objectToReturn.SetActive(false);//
            pooledObjects.Enqueue(objectToReturn);
            spawnedObjects.Dequeue();
        }
        void AddObjects(int count)//add objects, when you call this method, you need to specify how many objects you want
        {
            for (int i = 0; i < count; i++)//for every int fed into the Method we'll do:
            {
                foreach (GameObject newObject in pooledObject)//for every object to pool, this means it'll create (prewarmObjectsPerType) of each type
                {
                    GameObject newObjectInst = GameObject.Instantiate(newObject);//create a variable that instantiates our object
                    //newObjectInst.GetComponent<DecalDespawner>().pool = this;

                    //Since we are using prefabs, the reference to this pool will be lost, so we feed it before disabling the GO
                    //Optionally, and if you customize this into several specific pools you can make public static Pool Instances so you 
                    //don't need to feed the despawner, you only call SpecificPool.Instance.whatYouWantToAccess;

                    newObjectInst.SetActive(false);//deactivate our pooled GO
                    newObjectInst.transform.parent = container;//send it to our container
                    pooledObjects.Enqueue(newObjectInst);//send it to the spawnable queue

#if UNITY_EDITOR
                    Debug.Log(pooledObjects.Count + " pooled Decals");//if you want to log how many pooled objects you have activate this
#endif
                }
            }
        }
    }
}