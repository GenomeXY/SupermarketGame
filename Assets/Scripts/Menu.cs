using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;

    public RawImage rawImageProduct1;
    public RawImage rawImageProduct2;
    public RawImage rawImageProduct3;

    public TextMeshProUGUI textproduct1;
    public TextMeshProUGUI textproduct2;
    public TextMeshProUGUI textproduct3;

    [SerializeField] private Animator _startButtonAnimator;
    [SerializeField] private Animator _productListAnimator;
    [SerializeField] private Animator _textInstructionsAnimator;    
    void Start()
    {
        SetDataInMenu();
        MyAudioManager.Instance.BackMenuMusic.Play();
        _startButtonAnimator.enabled = true;                // включаем анимацию кнопки старт
        _productListAnimator.enabled = false;               // отключаем анимацию списка продуктов
        _textInstructionsAnimator.enabled = false;          // отключаем анимацию текста с инструкцией
        ObjectInterAction.OnStartTapped += StartGame;       // подписываем StartGame на событие при нажатии на кнопку старт
    }

    private void SetDataInMenu()
    {
        rawImageProduct1.texture = _fallingObjectsSpawner.selectedProducts[0].Sprite.texture;
        rawImageProduct2.texture = _fallingObjectsSpawner.selectedProducts[1].Sprite.texture;
        rawImageProduct3.texture = _fallingObjectsSpawner.selectedProducts[2].Sprite.texture;
        textproduct1.text = "= " + _fallingObjectsSpawner.selectedProducts[0].Amount.ToString();
        textproduct2.text = "= " + _fallingObjectsSpawner.selectedProducts[1].Amount.ToString();
        textproduct3.text = "= " + _fallingObjectsSpawner.selectedProducts[2].Amount.ToString();
    }
    public void StartGame()
    {
        MyAudioManager.Instance.StartButtonClick.Play();    // включаем звук нажатия на кнопку
        StartCoroutine(StartGameProcess());                 // запускаем корутину отключающую меню        
    }

    private IEnumerator StartGameProcess()
    {
        _startButtonAnimator.SetTrigger("Start");        // запускаем анимацию перелета и вращения кнопки
        _productListAnimator.enabled = true;             // запускаем анимацию отлета списка продуктов
        _textInstructionsAnimator.enabled = true;        // запускаем анимацию отслета текста инструкций
        StartCoroutine(SoundDelay());
        yield return new WaitForSeconds(1f);        
        _startButtonAnimator.enabled = false;            // выключаем анимацию кнопки старт         
        MyAudioManager.Instance.BackMenuMusic.Stop(); 
        MyAudioManager.Instance.BackGameMusic.Play();
        _gameManager.CountdownGameState();
    }
    private IEnumerator SoundDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        MyAudioManager.Instance.MenuFadeSound.Play();
    }
}
