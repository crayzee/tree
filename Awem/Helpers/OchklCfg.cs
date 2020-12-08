namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Ochkl config
    /// </summary>
    public class OchklCfg
    {
        private readonly OchklTag tag = new OchklTag();

        /// <summary>
        /// js func used to render dropdown item
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OchklCfg ItemFunc(string o)
        {
            tag.ItemFunc = o;
            return this;
        }

        internal OchklTag ToTag()
        {
            return tag;
        }
    }
}