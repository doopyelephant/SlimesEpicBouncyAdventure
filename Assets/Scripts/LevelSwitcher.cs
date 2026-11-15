using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
    public class LevelSwitcher : MonoBehaviour
    {
        public List<GameObject> PersistentObjects;
        public List<int> Levels;
        private bool ispayload = true;
        public TMP_Text coins;
        public TMP_Text chests;
        public IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(3f);
            ispayload = false;
        }

        public void Awake()
        {
            GlobalLevelManager.Init(this);
        }

        public void LoadLevel(int level)
        {
           StartCoroutine(LoadLevelAsync(level));
        }

        IEnumerator LoadLevelAsync(int level)
        {
            Persist();
            SceneManager.LoadScene(Levels[level]);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            Debug.Log("loaded level " + SceneManager.GetActiveScene().name);
            Integrate(SceneManager.GetActiveScene());
        }

        private void Persist()
        {
            foreach (var obj in PersistentObjects.Append(gameObject))
            {
                if (obj.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    obj.transform.position = GlobalLevelManager.InitPlayer;
                    obj.GetComponent<WallBounce>().targetviewpos = GlobalLevelManager.InitCamera;
                    obj.GetComponent<Player>().IsMapping = false;
                }

                if (obj.gameObject.CompareTag("MainCamera"))
                {
                    obj.transform.position = GlobalLevelManager.InitCamera;
                }

                DontDestroyOnLoad(obj);
            }
        }

        public void ReloadLevel()
        {
            var tmp = new GameObject("Payload");
            var payload = tmp.AddComponent<Payload>();
            payload.name = "level";
            payload.value = GlobalLevelManager.currentlevel;
            DontDestroyOnLoad(tmp);
            SceneManager.LoadScene(1);
        }

        public void Update()
        {
            if (ispayload)
            {
                var payload = GameObject.Find("Payload");

                if (payload != null)
                {
                    var comp = payload.GetComponent<Payload>();
                    if (comp.name == "level")
                    {
                        ispayload = false;
                        GlobalLevelManager.LoadLevel(comp.value);
                        Destroy(payload);
                    }
                }
            }
        }

        private void Integrate(Scene scene)
        {
            foreach (var obj in PersistentObjects.Append(gameObject))
            {
             SceneManager.MoveGameObjectToScene(obj, scene);
            }
        }

    }
