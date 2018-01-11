using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingTransform : MonoBehaviour
{
    public enum TEasingType
    {
        EXPO=0, CIRC, QUINT, QUART, QUAD, SINE, BACK, BOUNCE, ELASTIC
    }
    public TEasingType m_EasingType;

    public enum TTransformSelected
    {
        POSITION = 0,
        ROTATE,
        SCALE
    }
    public TTransformSelected m_TransformSelected;

    public enum TEasingEnd
    {
        NONE = 0,
        RESET,
        PINGPONG
    }
    public TEasingEnd m_EasingEnd;

    public Vector3 m_InitalValue;
    public Vector3 m_FinallValue;
    Vector3 m_DeltaValue;

    public float m_CurrentTime;
    public float m_EasingDuration;

    public bool m_Restart;
    public bool m_PingPong;
    bool m_TimeForward;

    void Start()
    {
        m_DeltaValue = m_FinallValue - m_InitalValue;
        m_CurrentTime = 0;
        m_TimeForward = false;

        switch(m_EasingEnd)
        {
            case TEasingEnd.NONE:
                m_Restart = false;
                m_PingPong = false;
                break;
            case TEasingEnd.RESET:
                m_Restart = true;
                m_PingPong = false;
                break;
            case TEasingEnd.PINGPONG:
                m_Restart = false;
                m_PingPong = true;
                break;
            default:
                break;
        }
    }

 void Update()
    {
        if(m_CurrentTime <= m_EasingDuration)
        {
            if(!m_TimeForward) m_CurrentTime += Time.deltaTime;
            else if(m_TimeForward) m_CurrentTime -= Time.deltaTime;

            /*Vector3 l_EasingValue = new Vector3(Easing.ExpoEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                                Easing.ExpoEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                                Easing.ExpoEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));

            transform.localPosition = l_EasingValue;*/

            Vector3 l_EasingValue = new Vector3();

            switch(m_EasingType)
            {
                case TEasingType.EXPO:
                    l_EasingValue = new Vector3(Easing.ExpoEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                                Easing.ExpoEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                                Easing.ExpoEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                    break;
                case TEasingType.CIRC:
                    l_EasingValue = new Vector3(Easing.CircEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                                Easing.CircEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                               Easing.CircEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                   break;
               case TEasingType.QUINT:
                   l_EasingValue = new Vector3(Easing.QuintEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                               Easing.QuintEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                               Easing.QuintEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                   break;
               case TEasingType.QUART:
                   l_EasingValue = new Vector3(Easing.QuartEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                               Easing.QuartEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                               Easing.QuartEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                   break;
               case TEasingType.QUAD:
                   l_EasingValue = new Vector3(Easing.QuadEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                               Easing.QuadEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                               Easing.QuadEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                   break;
               case TEasingType.SINE:
                   l_EasingValue = new Vector3(Easing.SineEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                               Easing.SineEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                               Easing.SineEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                   break;
               case TEasingType.BACK:
                   l_EasingValue = new Vector3(Easing.BackEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                                Easing.BackEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                                Easing.BackEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                    break;
                case TEasingType.BOUNCE:
                    l_EasingValue = new Vector3(Easing.BounceEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                                Easing.BounceEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                                Easing.BounceEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                    break;
                case TEasingType.ELASTIC:
                    l_EasingValue = new Vector3(Easing.ElasticEaseInOut(m_CurrentTime, m_InitalValue.x, m_DeltaValue.x, m_EasingDuration),
                                                Easing.ElasticEaseInOut(m_CurrentTime, m_InitalValue.y, m_DeltaValue.y, m_EasingDuration),
                                                Easing.ElasticEaseInOut(m_CurrentTime, m_InitalValue.z, m_DeltaValue.z, m_EasingDuration));
                    break;
                default:
                    break;
            }

            switch(m_TransformSelected)
            {
                case TTransformSelected.POSITION:
                    transform.localPosition = l_EasingValue;
                    break;
                case TTransformSelected.ROTATE:
                    transform.localRotation = Quaternion.Euler(l_EasingValue);
                    break;
                case TTransformSelected.SCALE:
                    transform.localScale = l_EasingValue;
                    break;
                default:
                    break;
            }

            if(m_CurrentTime > m_EasingDuration)
            {
                switch(m_TransformSelected)
                {
                    case TTransformSelected.POSITION:
                        transform.localPosition = m_FinallValue;
                        break;
                    case TTransformSelected.ROTATE:
                        transform.localRotation = Quaternion.Euler(m_FinallValue);
                        break;
                    case TTransformSelected.SCALE:
                        transform.localScale = m_FinallValue;
                        break;
                    default:
                        break;
                }

                if (m_Restart)
                {
                    m_CurrentTime = 0;
                }
                else if (m_PingPong)
                {
                    m_CurrentTime = 0;
                    Vector3 l_Initial = m_InitalValue;
                    m_InitalValue = m_FinallValue;
                    m_FinallValue = l_Initial;
                    m_DeltaValue = m_FinallValue - m_InitalValue;
                }
            }

        }
        else
        {

        }
    }
}
