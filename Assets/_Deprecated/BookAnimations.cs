using System;
using UnityEngine;

public class BookAnimations : MonoBehaviour
{
    private Animator _animator;

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
        _animator.SetTrigger(OpenBook);
        _animator.ResetTrigger(FlipPage);
        _animator.ResetTrigger(CloseBook);
    }

    public void FullBookFlip()
    {
        _animator.SetTrigger(FlipPage);
        _animator.ResetTrigger(OpenBook);
        _animator.ResetTrigger(CloseBook);
    }

    public void FullBookClose()
    {
        _animator.SetTrigger(CloseBook);
        _animator.ResetTrigger(OpenBook);
        _animator.ResetTrigger(FlipPage);
    }

    public void FullOpenEvent()
    {
        OnFullOpenComplete?.Invoke();
    }
    
    public void FullCloseEvent()
    {
        OnFullCloseComplete?.Invoke();
    }
}