using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iProcedure
{
    public class Utils
    {
        async public static Task<string> GetAnnotations(string filename)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("Annotations/Ellipse.txt");
            using var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            return contents;
        }
    }
}
