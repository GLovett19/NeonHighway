 using Valve.VR;

public class SteamInputModule : VRInputModule
{
    
    public SteamVR_Input_Sources m_Source = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources m_InactiveSource = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean m_Click = null;
    

    
    public override void Process()
    {
        base.Process();

        // Press
        if (m_Click.GetStateDown(m_Source))
        {
            Press();
        }
        else if (m_Click.GetStateDown(m_InactiveSource))
        {
            SteamVR_Input_Sources temp = m_Source;
            m_Source = m_InactiveSource;
            m_InactiveSource = temp;
        }

        // Release
        if (m_Click.GetStateUp(m_Source))
            Release();
    }
    
}
