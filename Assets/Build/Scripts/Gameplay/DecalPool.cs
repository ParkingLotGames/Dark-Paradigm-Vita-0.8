using UnityEngine;
using System.Collections.Generic;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    public class DecalPool : MonoBehaviour 
	{
        public Queue<GameObject> pooledMetalDecals = new Queue<GameObject>();
        public Queue<GameObject> spawnedMetalDecals = new Queue<GameObject>();
        public Queue<GameObject> pooledWoodDecals = new Queue<GameObject>();
        public Queue<GameObject> spawnedWoodDecals = new Queue<GameObject>();
        public Queue<GameObject> pooledConcreteDecals = new Queue<GameObject>();
        public Queue<GameObject> spawnedConcreteDecals = new Queue<GameObject>();
        public Queue<GameObject> pooledBloodFX= new Queue<GameObject>();
        public Queue<GameObject> spawnedBloodFX = new Queue<GameObject>();
        
        [SerializeField] GameObject[] metalDecals;
        [Tooltip("How many objects of each type should spawn at start?")] [SerializeField] int prewarmMetalPrefabsPerType;
        [SerializeField] GameObject[] woodDecals;
        [Tooltip("How many objects of each type should spawn at start?")] [SerializeField] int prewarmWoodPrefabsPerType;
        [SerializeField] GameObject[] concreteDecals;
        [Tooltip("How many objects of each type should spawn at start?")] [SerializeField] int prewarmConcretePrefabsPerType;
        [SerializeField] GameObject[] bloodFX;
        [Tooltip("How many objects of each type should spawn at start?")] [SerializeField] int prewarmBloodPrefabsPerType;

        private void Awake()
        {
            AddMetalDecals(prewarmMetalPrefabsPerType);
            AddWoodDecals(prewarmWoodPrefabsPerType);
            AddConcreteDecals(prewarmConcretePrefabsPerType);
            AddBloodFX(prewarmBloodPrefabsPerType);
        }

        public GameObject GetMetalDecal()
        {
            if (pooledMetalDecals.Count == 0)
            {
                var reusedObject = spawnedMetalDecals.Dequeue();
                reusedObject.SetActive(false);
                pooledMetalDecals.Enqueue(reusedObject);

            }
            var spawnedObject = pooledMetalDecals.Dequeue();
            spawnedMetalDecals.Enqueue(spawnedObject);
            return spawnedObject;
        }
        public GameObject GetWoodDecal()
        {
            if (pooledWoodDecals.Count == 0)
            {
                var reusedObject = spawnedWoodDecals.Dequeue();
                reusedObject.SetActive(false);
                pooledWoodDecals.Enqueue(reusedObject);

            }
            var spawnedObject = pooledWoodDecals.Dequeue();
            spawnedWoodDecals.Enqueue(spawnedObject);
            return spawnedObject;
        }
        public GameObject GetConcreteDecal()
        {
            if (pooledConcreteDecals.Count == 0)
            {
                var reusedObject = spawnedConcreteDecals.Dequeue();
                reusedObject.SetActive(false);
                pooledConcreteDecals.Enqueue(reusedObject);

            }
            var spawnedObject = pooledConcreteDecals.Dequeue();
            spawnedConcreteDecals.Enqueue(spawnedObject);
            return spawnedObject;
        }
        public GameObject GetBloodFX()
        {
            if (pooledBloodFX.Count == 0)
            {
                var reusedObject = spawnedBloodFX.Dequeue();
                reusedObject.SetActive(false);
                pooledBloodFX.Enqueue(reusedObject);

            }
            var spawnedObject = pooledBloodFX.Dequeue();
            spawnedBloodFX.Enqueue(spawnedObject);
            return spawnedObject;
        }
        
        public void ReturnToMetalPool(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pooledMetalDecals.Enqueue(objectToReturn);
            spawnedMetalDecals.Dequeue();
        }
        public void ReturnToWoodPool(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pooledWoodDecals.Enqueue(objectToReturn);
            spawnedWoodDecals.Dequeue();
        }
        public void ReturnToConcretePool(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pooledConcreteDecals.Enqueue(objectToReturn);
            spawnedConcreteDecals.Dequeue();
        }
        public void ReturnToBloodPool(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pooledBloodFX.Enqueue(objectToReturn);
            spawnedBloodFX.Dequeue();
        }

        void AddMetalDecals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (GameObject newMetalDecal in metalDecals)
                {
                    GameObject newObjectInst = GameObject.Instantiate(newMetalDecal);//create a variable that instantiates our object
                    newObjectInst.GetComponent<DecalDespawner>().pool = this;

                    //Since we are using prefabs, the reference to this pool will be lost, so we feed it before disabling the GO
                    //Optionally, and if you customize this into several specific pools you can make public static Pool Instances so you 
                    //don't need to feed the despawner, you only call SpecificPool.Instance.whatYouWantToAccess;

                    newObjectInst.SetActive(false);//deactivate our pooled GO
                    newObjectInst.transform.parent = transform;//send it to our container
                    pooledMetalDecals.Enqueue(newObjectInst);//send it to the spawnable queue

#if UNITY_EDITOR
                    Debug.Log(pooledMetalDecals.Count + " pooled Decals");//if you want to log how many pooled objects you have activate this
#endif
                }//for every object to pool, this means it'll create (prewarmObjectsPerType) of each type
            }
        }
        void AddWoodDecals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (GameObject newWoodDecal in woodDecals)//for every object to pool, this means it'll create (prewarmObjectsPerType) of each type
                    {
                        GameObject newObjectInst = GameObject.Instantiate(newWoodDecal);//create a variable that instantiates our object
                        newObjectInst.GetComponent<DecalDespawner>().pool = this;

                        //Since we are using prefabs, the reference to this pool will be lost, so we feed it before disabling the GO
                        //Optionally, and if you customize this into several specific pools you can make public static Pool Instances so you 
                        //don't need to feed the despawner, you only call SpecificPool.Instance.whatYouWantToAccess;

                        newObjectInst.SetActive(false);//deactivate our pooled GO
                        newObjectInst.transform.parent = transform;//send it to our container
                        pooledWoodDecals.Enqueue(newObjectInst);//send it to the spawnable queue

#if UNITY_EDITOR
                        Debug.Log(pooledMetalDecals.Count + " pooled Decals");//if you want to log how many pooled objects you have activate this
#endif
                    }
            }
        }
        void AddConcreteDecals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (GameObject newConcreteDecal in concreteDecals)//for every object to pool, this means it'll create (prewarmObjectsPerType) of each type
                {
                    GameObject newObjectInst = GameObject.Instantiate(newConcreteDecal);//create a variable that instantiates our object
                    newObjectInst.GetComponent<DecalDespawner>().pool = this;

                    //Since we are using prefabs, the reference to this pool will be lost, so we feed it before disabling the GO
                    //Optionally, and if you customize this into several specific pools you can make public static Pool Instances so you 
                    //don't need to feed the despawner, you only call SpecificPool.Instance.whatYouWantToAccess;

                    newObjectInst.SetActive(false);//deactivate our pooled GO
                    newObjectInst.transform.parent = transform;//send it to our container
                    pooledConcreteDecals.Enqueue(newObjectInst);//send it to the spawnable queue

#if UNITY_EDITOR
                    Debug.Log(pooledMetalDecals.Count + " pooled Decals");//if you want to log how many pooled objects you have activate this
#endif
                }
            }
        }
        void AddBloodFX(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (GameObject newBloodFx in bloodFX)//for every object to pool, this means it'll create (prewarmObjectsPerType) of each type
                {
                    GameObject newObjectInst = GameObject.Instantiate(newBloodFx);//create a variable that instantiates our object
                    newObjectInst.GetComponent<DecalDespawner>().pool = this;

                    //Since we are using prefabs, the reference to this pool will be lost, so we feed it before disabling the GO
                    //Optionally, and if you customize this into several specific pools you can make public static Pool Instances so you 
                    //don't need to feed the despawner, you only call SpecificPool.Instance.whatYouWantToAccess;

                    newObjectInst.SetActive(false);//deactivate our pooled GO
                    newObjectInst.transform.parent = transform;//send it to our container
                    pooledBloodFX.Enqueue(newObjectInst);//send it to the spawnable queue

#if UNITY_EDITOR
                    Debug.Log(pooledBloodFX.Count + " pooled Blood FX");//if you want to log how many pooled objects you have activate this
#endif
                }
            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(DecalPool))]
	//[CanEditMultipleObjects]

    public class CustomDecalPoolInspector : Editor
	{
    public override void OnInspectorGUI()
    {
        #region GUIStyles
        //Define GUIStyles
        
        #endregion

        #region Layout Widths
        GUILayoutOption width32 = GUILayout.Width(32);
        GUILayoutOption width40 = GUILayout.Width(40);
        GUILayoutOption width48 = GUILayout.Width(48);
        GUILayoutOption width64 = GUILayout.Width(64);
        GUILayoutOption width80 = GUILayout.Width(80);
        GUILayoutOption width96 = GUILayout.Width(96);
        GUILayoutOption width112 = GUILayout.Width(112);
        GUILayoutOption width128 = GUILayout.Width(128);
        GUILayoutOption width144 = GUILayout.Width(144);
        GUILayoutOption width160 = GUILayout.Width(160);
        #endregion

        base.OnInspectorGUI();
        DecalPool DecalPool = (DecalPool)target;
        //SerializedProperty example = serializedObject.FindProperty("Example");
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

        EditorGUIUtility.labelWidth = 80;
        //EditorGUILayout.PropertyField(example);

        EditorGUILayout.EndHorizontal();


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion
}