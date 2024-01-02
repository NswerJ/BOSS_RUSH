using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    [SerializeField]
    private List<Book> _bookList = new List<Book>();

    private List<int> _shuffleList = new List<int>();
    Sequence seq;

    private void Awake()
    {
        seq = DOTween.Sequence();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            ShuffleBook();
    }

    public void ShuffleBook()
    {
        _shuffleList.Clear();
        for(int i = 0; i < _bookList.Count; i++)
        {
            _bookList[i].FlipBack();
            _shuffleList.Add(i);
        }

        ShuffleAToB(0, 3);
    }

    private void ShuffleAToB(int count, int repeat)
    {
        if (count >= _shuffleList.Count)
        {
            if(repeat <= 0)
            {
                OpenBook();
            }
            else
            {
                ShuffleAToB(0, repeat - 1);
            }

            return;
        }

        seq.Kill();
        seq = DOTween.Sequence();

        int randomIdx = Random.Range(count + 1, _shuffleList.Count);
        Transform firstTrm = _bookList[_shuffleList[count]].transform;
        Transform secondTrm = _bookList[_shuffleList[randomIdx]].transform;


        seq.Append(firstTrm.DOMove(secondTrm.position, 0.5f))
            .Join(secondTrm.DOMove(firstTrm.position, 0.5f))
            .OnComplete(()=>
            {
                ShuffleAToB(count + 1, repeat);
            });

        int temp = _shuffleList[count];
        _shuffleList[count] = _shuffleList[randomIdx];
        _shuffleList[randomIdx] = temp;
    }

    private void OpenBook()
    {
        for (int i = 0; i < _bookList.Count; i++)
        {
            _bookList[i].Flip(false);
        }
    }
}
