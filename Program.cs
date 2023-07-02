using System;
using System.Text;
using System.Collections.Generic;


namespace OverrideAndNew
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Understanding the difference between override and new
            BaseClass bc = new BaseClass();
            DerivedClass dc = new DerivedClass();
            BaseClass DerivedBackIntoBase = (BaseClass)dc;
            DerivedClass JustDerived = dc;

            DerivedBackIntoBase.OverrideMe();   // Prints "Derived - OverrideMe"
                                                //  - IMPORTANT! This is the only difference between the two. The override REPLACED
                                                //  - the base class method with the derived class method.
            DerivedBackIntoBase.NewMe();        // Prints "Base - NewMe"
                                                //  - Base class method is called because it was not overridden.
            JustDerived.OverrideMe();           // Prints "Derived - OverrideMe".
                                                //  - Predicted.
            JustDerived.NewMe();                // Prints "Derived - NewMe".
                                                //  - Predicted.
            /*
            assume A Subclass : B Baseclass
            
            b {
                void virtual baseclassMethod () { do thing}
            }
            a {
                void override baseclassMethod () { do thing}
            }  
            
             THE DIFFERENCE BETWEEN NEW AND OVERRIDE IS THAT OVERRIDE REPLACES THE BASECLASS METHOD WITH THE SUBCLASS METHOD
 
            p.s. "SEALED" is a keyword to de-virtualize (to seal) an inherited virtual member. It is the opposite of virtual.
             */
            #endregion

            #region Abstract class example
            Console.WriteLine("Abstract class example");
            MyConcreteClass mcc = new MyConcreteClass();
            mcc.x = 2;
            Console.WriteLine($"Overridden abstract property: {mcc.Square_AbstractPropertyExample}");
            mcc.AddStringToList("Hello");
            Console.WriteLine($"Overridden abstract member variable List: {mcc.MyList[0]}");
            Console.WriteLine($"Normal inherited member variable List: {mcc.MyListNonAbstract[0]}");

            #endregion


        }
    }

    class BaseClass
    {

        //LOOK! remove the virtual keyword and see what happens

        /// <summary>
        /// Virtual keyword allows the method to be overridden in a derived class.
        /// </summary>
        public virtual void OverrideMe()
        {
            Console.WriteLine("Base - OverrideMe");
        }

        public void NewMe()
        {
            Console.WriteLine("Base - NewMe");
        }

    }

    class DerivedClass : BaseClass
    {
        public override void OverrideMe()
        {
            Console.WriteLine("Derived - OverrideMe");
        }

        public new void NewMe()
        {
            Console.WriteLine("Derived - NewMe");
        }
    }

    // Abstract class example. Only a single class can be inherited. ONE ABSTRACT INFINITY INTERFACES.
    abstract class MyAbstractClass {

        public int x; //power of 2

        public abstract int Square_AbstractPropertyExample { get; set; }

        public abstract List<string> MyList { get; }

        public List<string> MyListNonAbstract = new() { "bar" };

        public abstract void AbstractMethod();

    }

    class MyConcreteClass : MyAbstractClass
    {

        public override int Square_AbstractPropertyExample
        {
            get {
                return x * x;
            }

            set
            {
                Square_AbstractPropertyExample = value;
            }
        }

        private List<string> _myList = new() { "foo", "bar" };

        public override List<string> MyList { get { return _myList; }}

        public override void AbstractMethod()
        {
            Console.WriteLine("I am an implemented method.");
        }

        public void AddStringToList(string s)
        {
            _myList.Add(s);
        }
    }

    interface IMyInterface
    {
        void MyInterfaceMethod();
    }

    public abstract class AbstractInterfaceClass : IMyInterface
    {
        public abstract void MyInterfaceMethod();

        public abstract void MyAbstractMethod();
    }

    public class NormalClass : AbstractInterfaceClass
    {
        //Notice: abstract DEMANDS 'override' keyword
        public override void MyAbstractMethod()
        {
            throw new NotImplementedException();
        }

        public override void MyInterfaceMethod()
        {
            throw new NotImplementedException();
        }
    }

}