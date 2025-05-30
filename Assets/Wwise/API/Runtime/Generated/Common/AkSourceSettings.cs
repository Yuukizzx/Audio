#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkSourceSettings : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkSourceSettings(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkSourceSettings obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkSourceSettings() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkSourceSettings(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public uint sourceID { set { AkSoundEnginePINVOKE.CSharp_AkSourceSettings_sourceID_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSourceSettings_sourceID_get(swigCPtr); } 
  }

  public global::System.IntPtr pMediaMemory { set { AkSoundEnginePINVOKE.CSharp_AkSourceSettings_pMediaMemory_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSourceSettings_pMediaMemory_get(swigCPtr); }
  }

  public uint uMediaSize { set { AkSoundEnginePINVOKE.CSharp_AkSourceSettings_uMediaSize_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSourceSettings_uMediaSize_get(swigCPtr); } 
  }

  public void Clear() { AkSoundEnginePINVOKE.CSharp_AkSourceSettings_Clear(swigCPtr); }

  public static int GetSizeOf() { return AkSoundEnginePINVOKE.CSharp_AkSourceSettings_GetSizeOf(); }

  public void Clone(AkSourceSettings other) { AkSoundEnginePINVOKE.CSharp_AkSourceSettings_Clone(swigCPtr, AkSourceSettings.getCPtr(other)); }

  public AkSourceSettings() : this(AkSoundEnginePINVOKE.CSharp_new_AkSourceSettings(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.