namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Grid inline edit mod editor element
    /// </summary>
    public class InlElm
    {
        /// <summary>
        /// html format
        /// </summary>
        public string format { get; set; }

        /// <summary>
        /// js format
        /// </summary>
        public string jsFormat { get; set; }

        /// <summary>
        /// model property used for binding in post action (save)
        /// </summary>
        public string modelProp { get; set; }

        /// <summary>
        /// value property
        /// </summary>
        public string valProp { get; set; }
    }
}