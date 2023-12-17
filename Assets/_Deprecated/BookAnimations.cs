using System;
using UnityEngine;

public class BookAnimations : MonoBehaviour
{
    private Animator _animator;
    public bool IsAnimating { get; private set; }

    // Full book open/close
    private static readonly int OpenBook = Animator.StringToHash("OpenBook");
    private static readonly int CloseBook = Animator.StringToHash("CloseBook");
    private static readonly int FlipPage = Animator.StringToHash("FlipPage");

    public event Action OnFullOpenComplete; 
    public event Action OnFullCloseComplete; 
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         FullBookOpen();
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         FullBookFlip();
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         FullBookClose();
    //     }
    // }

    public void FullBookOpen()
    {
        if (!IsAnimating)
        {
            _animator.SetTrigger(OpenBook);
            IsAnimating = true;
        }
    }

    public void FullBookFlip()
    {
        if (!IsAnimating)
        {
            _animator.SetTrigger(FlipPage);
        }
    }

    public void FullBookClose()
    {
        if (!IsAnimating)
        {
            _animator.SetTrigger(CloseBook);
            IsAnimating = true;
        }
    }

    public void FullOpenEvent()
    {
        OnFullOpenComplete?.Invoke();
        _animator.ResetTrigger(CloseBook);
        _animator.ResetTrigger(OpenBook);
        _animator.ResetTrigger(FlipPage);
        IsAnimating = false;
    }
    
    public void FullCloseEvent()
    {
        OnFullCloseComplete?.Invoke();
        _animator.ResetTrigger(CloseBook);
        _animator.ResetTrigger(OpenBook);
        _animator.ResetTrigger(FlipPage);
        IsAnimating = false;
    }
}