using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace EigenVectorsAndValuesUsingDotNet
{
    class TestRunner<T> where T : struct
    {
        private Nullable<T> val;
        private Func<T> getValue;

        // Constructor.
        public TestRunner(Func<T> func)
        {
            val = null;
            getValue = func;
        }

        public T Value
        {
            get
            {
                if (val == null)
                    // Execute the delegate.
                    val = getValue();
                return (T)val;
            }
        }
    }
    public class Test_RealVector
    {
        public static Hashtable htTestFuncs = new Hashtable();

        public static int RunIt(string hashEntry)
        {
            TestRunner<int> test = (TestRunner<int>)htTestFuncs[hashEntry];
            return test.Value;
        }
        static Test_RealVector()
        {
            htTestFuncs["ColumnVector"] = new TestRunner<int>(() => Test_RealVector_ColumnVector());
            htTestFuncs["RowVector"] = new TestRunner<int>(() => Test_RealVector_RowVector());
        }
        public static int Test_RealVector_ColumnVector()
        {
            RealVector rv = new RealVector{1,2,3,4};

            HtmlOutputMethods.WriteLatexToHtmlAndLaunch(rv.ToLatex(), "Test_RealVector_ColumnVector.html");
            
            return 0;
        }

        public static int Test_RealVector_RowVector()
        {
            RealVector rv = new RealVector{1,2,3,4};
            rv.IsRowOrColumn = RowColumn.Row;

            HtmlOutputMethods.WriteLatexToHtmlAndLaunch(rv.ToLatex(), "Test_RealVector_RowVector.html");
            
            return 0;
        }

    }
}