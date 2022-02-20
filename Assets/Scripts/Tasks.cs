using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    private int _numberFrames = 60;

    CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
   
    void Start()
    {
        CancellationToken cancellationToken = _cancellationTokenSource.Token;
        Task.Run(() => WhatTaskFasterAsync(cancellationToken, FirstTusk(cancellationToken), SecondTask(cancellationToken)));
        Task.Run(() => FirstTusk(cancellationToken));
        Task.Run(() => SecondTask(cancellationToken));
        
    }

    public async Task FirstTusk(CancellationToken cancellationToken)
    {
        await Task.Delay(1000,cancellationToken);
            Debug.Log("First task completed ");
        
    }
    public async Task SecondTask(CancellationToken cancellationToken)
    {
        for (int i = 0; i < _numberFrames; i++)
        {
            await Task.Yield();
        }
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        Debug.Log("Second task completed");
    }

    public async  Task<bool> WhatTaskFasterAsync(CancellationToken ct, Task task1, Task task2)
    {
        Task firstCompleted =  await Task.WhenAny(task1, task2);
        if (firstCompleted == task1)
        {
            Debug.Log("First task done first");
            _cancellationTokenSource.Cancel();
            return true;
        }
        else
        {
            Debug.Log("Second task done first");
            _cancellationTokenSource.Cancel();
            return false;
        }    
    }
    private void OnDisable()
    {
        _cancellationTokenSource.Dispose();
    }
}
