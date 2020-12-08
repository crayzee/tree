using Microsoft.AspNetCore.Mvc;

namespace AweCoreDemo.Helpers
{
    public static class DataHelpers
    {
        /// <summary>
        /// generate a call to cstorg using an url,
        /// setting .DataFunc(Url.Action("GetMealsImg", "Data").CacheGetFunc())
        /// is equivalent to .Url(Url.Action("GetMealsImg", "Data")) except this way there's no client cache
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CacheGetFunc(this string url)
        {
            return "cstorg.getf('" + url + "')";
        }
    }
}