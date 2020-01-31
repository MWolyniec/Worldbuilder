using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder
{
    public static class TempDataCorrector
    {
        /// <summary>
        /// Corrects badly deserialized empty arrays of a given type in the TempData, initializing them anew.
        /// </summary>
        /// <param name="tempData"></param>
        /// <param name="type">Type of the array.</param>
        public static void CorrectEmptyArrays<T>(this ITempDataDictionary tempData)
        {
            var templist = new List<KeyValuePair<string, object>>();    

                foreach(var position in tempData)
            {
                if(position.Value is JArray)
                    templist.Add(position);
            }
                
                foreach (var item in templist)
            {
                tempData[item.Key] = new T[0];
            }

            tempData.Keep();
            
        }
        //TODO:Do wyjebania kod nie moze lezec zakomentowany
        /*
        public static Dictionary<string, T> CorrectAndCopyTempDataToDictionary<T>(ITempDataDictionary tempData)
        {
            var newOne = tempData;

        }*/
    }
}
