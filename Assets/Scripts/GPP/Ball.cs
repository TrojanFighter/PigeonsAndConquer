using UnityEngine;

public class Boss : MonoBehaviour
{
    private readonly TaskManager _tm = new TaskManager();

    private void Start ()
    {
        DoMyThing();
    }

    private void Update ()
    {
        _tm.Update();
    }

    private void DoMyThing()
    {
        // Just setting up some variables so the task constructors below are a little easier to read...
        var startPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 10));
        var endPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 10));
        var midPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));

        var startScale = Vector3.one;
        var endScale = startScale * 2;

        // Teleport to the left of the screen immediately...
        _tm.Do(new SetPos(gameObject, startPos))

            // Move to the middle over half a second...
            .Then(new Move(gameObject, startPos, midPos, 0.5f))

            // Then scale up for one quarter of a second
            .Then(new Scale(gameObject, startScale, endScale, 0.25f))

            .Then(new Scale(gameObject, endScale, endScale + new Vector3(2, 0, 0), 0.25f))

            .Then(new Rotate(gameObject, Vector3.zero, new Vector3(0, 0, 180), 0.5f))

            .Then(new Scale(gameObject, endScale + new Vector3(2, 0, 0), endScale, 0.25f))

            // Then scale down for one quarter of a second
            .Then(new Scale(gameObject, endScale, startScale, 0.25f))

            // Then move off screen over half a second
            .Then(new Move(gameObject, midPos, endPos, 0.25f))

            // Then reset the whole thing
            .Then(new ActionTask(DoMyThing));
    }

}

public class Rotate : TimedGOTask
{
    private readonly Vector3 _startRotation, _endRotation;

    public Rotate(GameObject gameObject, Vector3 startRotation, Vector3 endRotation, float duration) : base(gameObject, duration)
    {
        _startRotation = startRotation;
        _endRotation = endRotation;
    }

    protected override void OnTick(float t)
    {
        gameObject.transform.rotation = Quaternion.Euler(Vector3.Lerp(_startRotation, _endRotation, t));
    }
}






