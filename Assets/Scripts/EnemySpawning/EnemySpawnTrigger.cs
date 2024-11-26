using Mechadroids;
using UnityEngine;
using System.IO;
using System.Collections;

public class EnemySpawnTrigger : MonoBehaviour
{
    AISettings aISettings;
    EnemySettings enemySettings;
    AIEntitiesHandler aIHandler;
    public enum EnemyType {
        Runner,
        Tank,
        Walker
    };

    [SerializeField] private EnemyType enemyType;

    public void Initialize(AIEntitiesHandler aIEntitiesHandler) {
        aIHandler = aIEntitiesHandler;
    }

    void Start() {
        aISettings = Resources.Load<AISettings>("");
        enemySettings = Resources.Load<EnemySettings>("EnemySettings");

        DirectoryInfo dir = new DirectoryInfo("Assets/Settings/Resources/EnemySettings");
        FileInfo[] info = dir.GetFiles("*.*");
        foreach(FileInfo f in info) {
            Debug.Log(f.ToString());
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (enemyType) {
            case EnemyType.Runner:
                return;
            case EnemyType.Walker:
                return;
            case EnemyType.Tank:
                return;
        }
    }
}
