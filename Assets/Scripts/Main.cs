using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class Main : MonoBehaviour
{
    private int[] _myArrayInt = new int[] { 1, 7, 25, 4, 13, 55, 54, 2, 6, 17 };
    private Vector3[] _posution = new Vector3[] { Vector3.one, Vector3.right, Vector3.down };
    private Vector3[] _velocities = new Vector3[] { Vector3.forward, Vector3.left, Vector3.negativeInfinity };
    private Vector3[] _finalPositions = new Vector3[3];

    void Start()
    {
        FirsJob();
        SecondJob();
    }
    private void FirsJob()
    {
        NativeArray<int> myArrayInt = new NativeArray<int>(_myArrayInt.Length, Allocator.TempJob);
        MyJob myJob = new MyJob()
        {
            myArrayInt = myArrayInt
        };
        for (int i = 0; i < _myArrayInt.Length; i++)
        {
            myArrayInt[i] = _myArrayInt[i];
        }
        var jobHandle = myJob.Schedule();
        jobHandle.Complete();
        myArrayInt.Dispose();
    }

    private void SecondJob()
    {

        NativeArray<Vector3> Positions = new NativeArray<Vector3>(_posution.Length, Allocator.TempJob);
        NativeArray<Vector3> Velocities = new NativeArray<Vector3>(_velocities.Length, Allocator.TempJob);
        NativeArray<Vector3> FinalPositions = new NativeArray<Vector3>(_finalPositions.Length, Allocator.TempJob);
        MySecondJob mySecondJob = new MySecondJob()
        {
            Positions = Positions,
            Velocities = Velocities,
            FinalPositions = FinalPositions

        };
        for (int i = 0; i < _finalPositions.Length; i++)
        {
            Positions[i] = _posution[i];
            Velocities[i] = _velocities[i];
            FinalPositions[i] = _finalPositions[i];
        }
        var jobHandle = mySecondJob.Schedule(3, 3);
        jobHandle.Complete();
        foreach (var item in FinalPositions)
        {
            Debug.Log(item);
        }
        Positions.Dispose();
        Velocities.Dispose();
        FinalPositions.Dispose();
    }
}


public struct MyJob : IJob
{
    public NativeArray<int> myArrayInt;
    public void Execute()
    {
        for (int i = 0; i < myArrayInt.Length; i++)
        {
            if (myArrayInt[i] > 10)
            {
                myArrayInt[i] = 0;
            }
        }
        foreach (var item in myArrayInt)
        {
            Debug.Log(item);
        }
    }
}
public struct MySecondJob : IJobParallelFor
{
    public NativeArray<Vector3> Positions;
    public NativeArray<Vector3> Velocities;
    public NativeArray<Vector3> FinalPositions;
    public void Execute(int index)
    {
        for (int i = 0; i < FinalPositions.Length; i++)
        {
            FinalPositions[i] = Positions[i] + Velocities[i];
        }
    }
}

