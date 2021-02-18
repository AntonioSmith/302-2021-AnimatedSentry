using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimMath
{
    public static float Lerp(float min, float max, float p, bool allowExtrapolation = true)
    {
        if (!allowExtrapolation)
        {
            if (p < 0) p = 0;
            if (p > 1) p = 1;
        }

        return (max - min) * p + min;
    }
    public static Vector3 Lerp(Vector3 min, Vector3 max, float p, bool allowExtrapolation = true)
    {
        if (!allowExtrapolation)
        {
            if (p < 0) return min;
            if (p > 1) return max;
        }

        return (max - min) * p + min;

        //float x = Lerp(min.x, max.x, p);
        //float y = Lerp(min.y, max.y, p);
        //float z = Lerp(min.z, max.z, p);
        //return new Vector3(x, y, z);
    }

    public static Quaternion Lerp(Quaternion min, Quaternion max, float p, bool allowExtrapolation = true)
    {
        //Quaternion rot = Quaternion.identity; // rotation = no rotation
        //rot.x = Lerp(min.x, max.x, p, allowExtrapolation);
        //rot.y = Lerp(min.y, max.y, p, allowExtrapolation);
        //rot.z = Lerp(min.z, max.z, p, allowExtrapolation);
        //rot.w = Lerp(min.w, max.w, p, allowExtrapolation);

        //Quaternion.Lerp(min, max, p);
        if (!allowExtrapolation)
        {
            if (p < 0) p = 0;
            if (p > 1) p = 1;
        }
        return Quaternion.Lerp(min, max, p);
    }

    public static float Slide(float current, float target, float percentLeftAfter1Second) // 1 dimensional slide
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime); // makes this framerate independent
        return AnimMath.Lerp(current, target, p);
    }

    public static Vector3 Slide(Vector3 current, Vector3 target, float percentLeftAfter1Second) // 3 dimensional slide
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime); // makes this framerate independent
        return AnimMath.Lerp(current, target, p);
    }

    public static Quaternion Slide(Quaternion current, Quaternion target, float percentLeftAfter1Second = .05f)
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime); // makes this framerate independent
        return AnimMath.Lerp(current, target, p);
    }
}
