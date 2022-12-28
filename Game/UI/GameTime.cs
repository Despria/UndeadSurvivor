using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using System.Text;

/// <summary>
/// 인게임 타이머 스크립트.
/// </summary>

public class GameTime : MonoBehaviour
{
    #region Field
    [Header("Timer Setting")]
    [SerializeField] IntVariable minuteSetting;
    [SerializeField] FloatVariable secondSetting;

    [Header("Clock Status")]
    [SerializeField] IntVariable minute;
    [SerializeField] FloatVariable second;
    [SerializeField] FloatVariable gameElapsedTime;
    [SerializeField] BoolVariable isTimeOver;

    [Header("Text Setting")]
    [SerializeField] TextMeshProUGUI clockText;
    StringBuilder stringBuilder;
    #endregion

    #region Job
    public NativeArray<int> timeMinuteArray;
    public NativeArray<float> timeSecondArray;
    public NativeArray<float> gameElapsedTimeArray;
    public NativeArray<bool> isTimeOverArray;

    JobHandle timeCalculateJobHandle;
    TimeCalculateJob timeCalculateJob;
    
    [BurstCompile]
    struct TimeCalculateJob : IJob
    {
        public NativeArray<int> minuteArray;
        public NativeArray<float> secondArray;
        public NativeArray<float> gameElapsedTimeArray;
        public NativeArray<bool> timeOverArray;

        public int minute;
        public float second;
        public float gameElapsedTime;
        public bool isTimeOver;

        public float deltaTime;

        public void Execute()
        {
            if (!isTimeOver)
            {
                second -= deltaTime;
                gameElapsedTime += deltaTime;

                if ((int)second < 0)
                {
                    minute--;

                    if (minute <= 0)
                    {
                        isTimeOver = true;
                        timeOverArray[0] = isTimeOver;
                        return;
                    }

                    second = 60f;
                }

                minuteArray[0] = minute;
                secondArray[0] = second;
                gameElapsedTimeArray[0] = gameElapsedTime;
            }
        }
    }
    #endregion

    #region Unity Event
    void OnEnable()
    {
        clockText = GetComponent<TextMeshProUGUI>();
        minute.runtimeValue = minuteSetting.runtimeValue;
        second.runtimeValue = secondSetting.runtimeValue;
        isTimeOver.runtimeValue = false;

        timeMinuteArray = new NativeArray<int>(1, Allocator.Persistent);
        timeSecondArray = new NativeArray<float>(1, Allocator.Persistent);
        isTimeOverArray = new NativeArray<bool>(1, Allocator.Persistent);
        gameElapsedTimeArray = new NativeArray<float>(1, Allocator.Persistent);
        timeMinuteArray[0] = minute.runtimeValue;
        timeSecondArray[0] = second.runtimeValue;

        stringBuilder = new StringBuilder();
    }

    void Update()
    {
        timeCalculateJob = new TimeCalculateJob()
        {
            minuteArray = timeMinuteArray,
            secondArray = timeSecondArray,
            gameElapsedTimeArray = gameElapsedTimeArray,
            timeOverArray = isTimeOverArray,

            minute = minute.runtimeValue,
            second = second.runtimeValue,
            gameElapsedTime = gameElapsedTime.runtimeValue,
            isTimeOver = isTimeOver.runtimeValue,

            deltaTime = Time.deltaTime
        };

        timeCalculateJobHandle = timeCalculateJob.Schedule();
    }

    void LateUpdate()
    {
        timeCalculateJobHandle.Complete();

        minute.runtimeValue = timeMinuteArray[0];
        second.runtimeValue = timeSecondArray[0];
        gameElapsedTime.runtimeValue = gameElapsedTimeArray[0];
        isTimeOver.runtimeValue = isTimeOverArray[0];

        stringBuilder.Clear();
        clockText.text = stringBuilder.AppendFormat("{0}:{1:00}", minute.runtimeValue, (int)second.runtimeValue).ToString();
    }

    void OnDisable()
    {
        timeMinuteArray.Dispose();
        timeSecondArray.Dispose();
        isTimeOverArray.Dispose();
        gameElapsedTimeArray.Dispose();
    }
    #endregion
}