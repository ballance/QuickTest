using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickTest
{
    public class InvalidLocalFileSpecification
    {
        public bool IsSatisfiedBy(string fullPath)
        {
            var rex = new Regex(@"\.\w{3,4}$");
            return rex.IsMatch(fullPath);
        }
    }
}
