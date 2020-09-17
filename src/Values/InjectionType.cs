namespace DIContextAutoLoader
{
    public enum InjectionType
    {
        /// <summary>
        /// If service implements interfaces, inject by all interfaces. If not, inject by implementation.
        /// </summary>
        Auto = 1,

        /// <summary>
        /// Inject by implementation.
        /// </summary>
        ByImplementationType,

        /// <summary>
        /// Inject by all interfaces that the service implements.
        /// </summary>
        ByServiceType,

        /// <summary>
        /// Inject by implementation and by all interface (if any) that the service implements.
        /// </summary>
        ByBoth
    }
}
