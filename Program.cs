using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace EigenVectorsAndValuesUsingDotNet
{
    class Program
    {
        static void Main(string[] args)
        {

            //List<double> mv = new List<double> {2, 1, 0, 1, 3, -1, 0, -1, 6};
            //SquareRealMatrix A = new SquareRealMatrix(3, 3, mv);
            RealVector rv = new RealVector{1,2,3,4};

            HtmlOutputMethods.WriteLatexToHtmlAndLaunch(rv.ToLatex(), "funk.html");
        }
    }
}
