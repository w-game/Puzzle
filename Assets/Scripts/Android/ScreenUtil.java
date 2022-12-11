package com.shuanger.puzzle;

import android.app.Activity;
import android.os.Build;
import android.util.Log;
import android.view.DisplayCutout;
import android.view.View;
import android.view.WindowInsets;

public class ScreenUtil {
    public int getNotchHeight()
    {
        Log.d("Puzzle ScreenUtil", "Unity写Java代码测试");

        int top = 0;
        try {
            Class<?> classType = Class.forName("com.unity3d.player.UnityPlayer");
            Activity activity = (Activity)classType.getDeclaredField("currentActivity").get(classType);

            View decorView = activity.getWindow().getDecorView();
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
                WindowInsets windowInsets = decorView.getRootWindowInsets();
                top = windowInsets.getStableInsetTop();
//                DisplayCutout displayCutout;
//                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
//                    displayCutout = windowInsets.getDisplayCutout();
//                    Log.d("Puzzle Demo", windowInsets.toString());
//                    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.Q) {
//                        top = displayCutout.getSafeInsetTop();
//                    }
//                }
            }

            Log.d("Puzzle ScreenUtil", String.format("%d", top));
        } catch (Exception ignored)
        {
            Log.e("Puzzle ScreenUtil", ignored.getMessage());
        }
        return top;
    }
}
