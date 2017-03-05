using UnityEngine;
using Jerry;

public class AudioMgr : SingletonMono<AudioMgr>
{
    private AudioSource m_AudioSource;
    public AudioClip m_Jump; 

    public override void Awake()
    {
        base.Awake();
        m_AudioSource = this.GetComponent<AudioSource>();

        JerryEventMgr.AddEvent(Enum_Event.PlayAudio.ToString(), EventPlayAudio);
    }

    private void EventPlayAudio(object[] args)
    {
        if (args == null || args.Length != 1)
        {
            return;
        }
        Enum_Audio au = (Enum_Audio)args[0];
        switch (au)
        {
            case Enum_Audio.Jump:
                {
                    m_AudioSource.PlayOneShot(m_Jump);
                }
                break;
        }
    }

    void OnDestroy()
    {
        JerryEventMgr.RemoveEvent(Enum_Event.PlayAudio.ToString(), EventPlayAudio);
    }
}

public enum Enum_Event
{
    None = 0,
    PlayAudio,
}

public enum Enum_Audio
{
    None = 0,
    Jump,
}