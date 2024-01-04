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

    [SerializeField]
    private AudioClip _cardSettingClip;

    [Header("Shuffle")]
    [SerializeField]
    private List<float> _cardChangeTimeList = new List<float>();

    [SerializeField]
    private List<Vector2> _5cardShufflePos = new List<Vector2>();

    [SerializeField]
    private SpriteRenderer _mainBookSprite;
    private Material _defaultMat;
    [SerializeField]
    private Material _answerMat;

    private void Awake()
    {
        _defaultMat = _mainBookSprite.material;

        seq = DOTween.Sequence();
        for(int i = 0; i < _bookList.Count; i++)
        {
            _bookList[i].Back.BackHitEvent += OpenBook;
        }
    }

    public void ShuffleBook(int idx)
    {
        for (int i = 0; i < _bookList.Count; ++i)
        {
            _bookList[i].DamageOff();
            _bookList[i].ResetChildPos();
            _bookList[i].transform.position = (Vector3)_5cardShufflePos[i];
            SoundManager.Instance.SFXPlay("CardSet", _cardSettingClip);
            _bookList[i].Open();
        }

        StartCoroutine(ShuffleCo(3, idx));
    }

    IEnumerator ShuffleCo(int repeat, int index)
    {
        _mainBookSprite.material = _answerMat;
        yield return new WaitForSeconds(1f);

        _shuffleList.Clear();
        for (int i = 0; i < _bookList.Count; i++)
        {
            _bookList[i].FlipBack();
            _shuffleList.Add(i);

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        _mainBookSprite.material = _defaultMat;

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
                SoundManager.Instance.SFXPlay("CardSet", _cardSettingClip);
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
