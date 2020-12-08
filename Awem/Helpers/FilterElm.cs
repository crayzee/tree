namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Grid filter row mod element
    /// </summary>
    public class FilterElm : InlElm
    {
        /// <summary>
        /// default editor type
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// clear btn
        /// </summary>
        public bool? clearBtn { get; set; }
    }
}