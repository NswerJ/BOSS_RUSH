using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Talker
{

    가디언,
    요정

}

[System.Serializable]
public class Talk
{

    [TextArea] public string text;
    public float textBoxScale;
    public Talker talker;
    public UnityEvent talkStartEvent;
    public UnityEvent talkEndEvent;

}

[System.Serializable]
public class Cut
{

    public List<Talk> talks;
    public UnityEvent startEvent;
    public UnityEvent endEvent;

}

public class TutorialController : MonoBehaviour
{

    [SerializeField] private TextBox fairyTextBox, playerTextBox;
    [SerializeField] private List<Cut> cuts;
    [SerializeField] private Image fadingImage;

    private CinemachineVirtualCamera cvcam;
    private bool textSettingEnd;
    private int currentTalk;

    private void Awake()
    {
        
        cvcam = FindObjectOfType<CinemachineVirtualCamera>();

    }

    private void Start()
    {

        StartFading();

    }

    public void StartTalk(int idx)
    {

        currentTalk = idx;

        StartCoroutine(TalkCo());

    }

    private TextBox GetTextBox(Talker talker)
    {

        return talker switch
        {

            Talker.가디언 => playerTextBox,
            Talker.요정 => fairyTextBox,
            _ => null

        };

    }

    public void SetCameraFollow(Transform trm)
    {

        cvcam.Follow = trm;


    }

    public void SetCameraOffsetY(float y)
    {

        cvcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = y;

    }

    private IEnumerator TalkCo()
    {

        cuts[currentTalk].startEvent?.Invoke();


        foreach(var talk in cuts[currentTalk].talks)
        {

            var box = GetTextBox(talk.talker);

            box.gameObject.SetActive(true);
            box.SetText("");
            box.SetSize(talk.textBoxScale);

            var co = StartCoroutine(SettingTextCo(talk.text, box));

            yield return new WaitUntil(() => textSettingEnd || Input.GetMouseButtonDown(0));

            if (!textSettingEnd)
            {

                box.SetText(talk.text);
                StopCoroutine(co);

            }

            yield return null;

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            textSettingEnd = false;

            box.gameObject.SetActive(false);

            yield return null;

        }

        cuts[currentTalk].endEvent?.Invoke();

    }

    public void StartFading()
    {

        fadingImage.DOFade(1, 0);
        fadingImage.DOFade(0, 1.5f).OnComplete(() =>
        {

            StartTalk(0);

        });

    }

    public void FadingAndSceneChange(string sceneName)
    {

        if (sceneName == "") return;


        fadingImage.DOFade(1, 1.5f).OnComplete(() =>
        {

            SceneManager.LoadScene(sceneName);

        });



    }

    private IEnumerator SettingTextCo(string text, TextBox textbox)
    {

        string curText = "";

        foreach(var ch in text)
        {

            curText += ch;
            textbox.SetText(curText);
            yield return new WaitForSeconds(0.1f);

        }

        textSettingEnd = true;
    }

}
