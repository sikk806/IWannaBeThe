
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text Description;


    List<string> title = new List<string>();
    List<string> description = new List<string>();

    void Awake()
    {
        title.Add("Hello Potato!");
        description.Add("이 스테이지는 튜토리얼입니다. \n\n즐거운 여행 되시기 바랍니다.(˵ ͡° ͜ʖ ͡°˵)");

        title.Add("Moving");
        description.Add("이동: 방향키 \n\n발사: Z \n\n점프: X");

        title.Add("MoreJump");
        description.Add("아이템을 먹으면 점프 한번 더 가능해요~");

        title.Add("Save");
        description.Add("총알이 풍선에 닿는 순간 현재 위치 저장");

        title.Add("Good Luck!");
        description.Add(@"행운을 빌어요 ¯\_( ͡° ͜ʖ ͡°)_/¯");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SetTheText(int camIndex)
    {
        if(camIndex < title.Count)
        {
            Title.text = title[camIndex];
            Description.text = description[camIndex];
        }
        else
        {
            Title.text = "";
            Description.text = "";
        }
    }
}
