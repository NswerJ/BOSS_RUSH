using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    [Header("List")]
    [SerializeField]
    private List<Book> _bookList = new List<Book>();

    private List<int> _shuffleList = new List<int>();
    Sequence seq;

    [Header("Shuffle")]
    [SerializeField]
    private List<float> _cardChangeTimeList = new List<float>();

    [SerializeField]
    private List<Vector2> _5cardShufflePos = new List<Vector2>();


    private void Awake()
    {
        seq = DOTween.Sequence();
        for(int i = 0; i < _bookList.Count; i++)
        {
            _bookList[i].Back.HitEvent += OpenBook;
        }
    }

    public void ShuffleBook(int idx)
    {
        for (int i = 0; i < _bookList.Count; ++i)
        {
            _bookList[i].DamageOff();
            _bookList[i].ResetChildPos();
            _bookList[i].transform.position = (Vector3)_5cardShufflePos[i];
            _bookList[i].Open();
        }

        StartCoroutine(ShuffleCo(3, idx));
    }

    IEnumerator ShuffleCo(int repeat, int index)
    {
        yield return new WaitForSeconds(1f);

        _shuffleList.Clear();
        for (int i = 0; i < _bookList.Count; i++)
        {
            _bookList[i].FlipBack();
            _shuffleList.Add(i);

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        seq = DOTween.Sequence();
        float cardMoveTime = _cardChangeTimeList[index];
        WaitForSeconds wait = new WaitForSeconds(cardMoveTime);

        for (int i = 0; i < repeat; ++i)
            for(int count = 0; count < _shuffleList.Count - 1; ++count)
            {
                int randomIdx = Random.Range(count + 1, _shuffleList.Count);
                Transform firstTrm = _bookList[_shuffleList[count]].transform;
                Transform secondTrm = _bookList[_shuffleList[randomIdx]].transform;

                seq.Kill();
                seq = DOTween.Sequence();

                seq.Append(firstTrm.DOMove(secondTrm.position, cardMoveTime))
                    .Join(secondTrm.DOMove(firstTrm.position, cardMoveTime));

                int temp = _shuffleList[count];
                _shuffleList[count] = _shuffleList[randomIdx];
                _shuffleList[randomIdx] = temp;

                yield return wait;
            }

        for (int i = 0; i < _bookList.Count; ++i)
        {
            _bookList[i].DamageOn();
        }
    }

    private void OpenBook(Book book)
    {
        for (int i = 0; i < _bookList.Count; i++)
        {
            if (_bookList[i] == book) continue;

            _bookList[i].Fold();
        }

        book.Open();

    }
}
