 using Valve.VR;

public class SteamInputModule : VRInputModule
{
    
    public SteamVR_Input_Sources m_Source = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources m_InactiveSource = SteamVR_Input_Sources.LeftHand;

    public Pointer m_InactivePointer;
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
            pointer.SetLineVisibility(false);

            //change the input source 
            SteamVR_Input_Sources tempSource = m_Source;
            m_Source = m_InactiveSource;
            m_InactiveSource = tempSource;

            // change the input pointer camera
            Pointer tempPointer = pointer;
            pointer = m_InactivePointer;
            m_InactivePointer = tempPointer;

            


        }
        // Release
        if (m_Click.GetStateUp(m_Source))
            Release();
    }


}
