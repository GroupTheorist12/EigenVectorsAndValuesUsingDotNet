using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EigenVectorsAndValuesUsingDotNet
{
    public class Real3EigenValues
    {
        public double EigenValue1{get;set;}
        public double EigenValue2{get;set;}
        public double EigenValue3{get;set;}

        public override string ToString()
        {
            return string.Format("{0:0.0000} {1:0.0000} {2:0.0000}", EigenValue1, EigenValue2, EigenValue3);
        }
    }
}