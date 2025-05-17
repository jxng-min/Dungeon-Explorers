using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private static LoadingManager m_instance;
    public static LoadingManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindFirstObjectByType<LoadingManager>();

                if(m_instance == null)
                {
                    m_instance = CreateInstance();
                }
            }
            
            return m_instance;
        }
    }

    private string m_target_scene_name;
    public string Scene
    {
        get { return m_target_scene_name; }
    }

    [Header("로딩 UI의 캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group;

    [Header("로딩 상태를 표현 할 라벨")]
    [SerializeField] private TMP_Text m_loading_label;

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }   

        DontDestroyOnLoad(gameObject);
    }

    private static LoadingManager CreateInstance()
    {
        return Instantiate(Resources.Load<LoadingManager>("Loading UI"));
    }

    public void LoadScene(string scene_name)
    {
        gameObject.SetActive(true);

        SceneManager.sceneLoaded += OnSceneLoaded;

        m_target_scene_name = scene_name;

        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        m_loading_label.text = "0%";

        yield return StartCoroutine(Fade(true));

        var op = SceneManager.LoadSceneAsync(m_target_scene_name);
        op.allowSceneActivation = false;

        float elapsed_time = 0f;

        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                m_loading_label.text = (op.progress * 100).ToString("F0") + "%";
            }
            else
            {
                elapsed_time += Time.unscaledDeltaTime;
                
                m_loading_label.text = (Mathf.Lerp(0.9f, 1f, elapsed_time) * 100).ToString("F0") + "%";

                if(m_loading_label.text == "100%")
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private IEnumerator Fade(bool is_fade_in)
    {
        float elapsed_time = 0f;
        float target_time = 1f;


        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            m_canvas_group.alpha = is_fade_in ? Mathf.Lerp(0f, 1f, elapsed_time) : Mathf.Lerp(1f, 0f, elapsed_time);
        }

        if(!is_fade_in)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == m_target_scene_name)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
