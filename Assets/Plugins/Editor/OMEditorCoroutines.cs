using UnityEditor;
using System.Collections;

public class OMEditorCoroutines
{
    readonly IEnumerator mRoutine;

    public static OMEditorCoroutines StartEditorCoroutine( IEnumerator routine)
    {
        OMEditorCoroutines coroutine = new OMEditorCoroutines(routine);
        coroutine.start();
        return coroutine;
    }

    OMEditorCoroutines(IEnumerator routine)
    {
        mRoutine = routine;
    }

    void start()
    {
        EditorApplication.update += update;
    }

    void update()
    {
        if(!mRoutine.MoveNext())
        {
            StopEditorCoroutine();
        }
    }

    public void StopEditorCoroutine()
    {
        EditorApplication.update -= update;
    }
}
