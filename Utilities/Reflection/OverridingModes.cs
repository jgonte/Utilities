namespace Utilities
{
    public enum OverridingModes
    {
        None, // Not hiding
        Virtual, // Not hiding but allowing to be overriden
        Abstract,
        Sealed, // Can not be inherited or overriden
        HidesInherited, // Hiding with the new or Shadows keyword
        Overrides // Hiding with the override or Overrides keyword
    } 
}