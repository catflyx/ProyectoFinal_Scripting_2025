using NUnit.Framework;
using UnityEngine;

public class TimerControllerTests
{
    GameObject timerObj;
    TimerController timer;
    GameObject activator;

    [SetUp]
    public void Setup()
    {
        timerObj = new GameObject("Timer");
        timer = timerObj.AddComponent<TimerController>();
        timer.countdownTime = 1f;
        activator = new GameObject("Activator");
        timer.objectToActivate = activator;
        // no asignamos timer.timerText para evitar dependencia TMP
        timer.ResetTimer();
    }

    [Test]
    public void ForceExpire_ActivatesObject()
    {
        timer.ForceExpire(); // público en la versión sugerida
        // ForceExpire llama a Update() internamente en la versión que propuse, si no, llama timer.Update() aquí.
        Assert.IsTrue(activator.activeSelf);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(timerObj);
        Object.DestroyImmediate(activator);
    }
}
