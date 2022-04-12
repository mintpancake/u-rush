
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TextEffects : MonoBehaviour
{
    public TextMeshProUGUI myText;

    public float charsPerSecond = 0.2f;//����ʱ����
    private string words;//������Ҫ��ʾ������

    private bool isActive = false;
    private float timer;//��ʱ��
    private Text scoreTx;
    private Text attributeTx;
    private int currentPos = 0;//��ǰ����λ��

    // Use this for initialization
    void Start()
    {
        timer = 0;
        isActive = true;
        //charsPerSecond = Mathf.Max(0.2f, charsPerSecond);
        myText = GetComponent<TMPro.TextMeshProUGUI>();
        words = myText.text;
        myText.SetText("");
        //myText.text = "";//��ȡText���ı���Ϣ�����浽words�У�Ȼ��̬�����ı���ʾ���ݣ�ʵ�ִ��ֻ���Ч��
        //Debug.Log(myText);   
    }

    // Update is called once per frame
    void Update()
    {
        OnStartWriter();
        //Debug.Log (isActive);
    }

    public void StartEffect()
    {
        isActive = true;
    }
    /// <summary>
    /// ִ�д�������
    /// </summary>
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//�жϼ�ʱ��ʱ���Ƿ񵽴�
                timer = 0;
                currentPos++;
                myText.SetText(words.Substring(0, currentPos));
                //myText.text = words.Substring(0, currentPos);//ˢ���ı���ʾ����

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// �������֣���ʼ������
    /// </summary>
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.SetText(words);
        //myText.text = words;
    }
}