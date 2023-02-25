using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// The PlayerDamageReceiver class is responsible for receiving damage inflicted by enemies.
    /// </summary>
    public class PlayerDamageReceiver : MonoBehaviour
    {
        /// <summary>
        /// The health component of the player.
        /// </summary>
        private HealthComponent playerHealth;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            playerHealth = GetComponent<HealthComponent>();
        }

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="collision">The collider component of the object that entered the trigger.</param>
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == ("Enemy"))
                playerHealth.TakeDamage(4);
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
        /// </summary>
        /// <param name="collision">The collision data associated with this collision.</param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == ("Enemy"))
                playerHealth.TakeDamage(4);
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerDamageReceiver))]
	//[CanEditMultipleObjects]

    public class CustomPlayerDamageReceiverInspector : Editor
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

        //base.OnInspectorGUI();
        PlayerDamageReceiver PlayerDamageReceiver = (PlayerDamageReceiver)target;
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