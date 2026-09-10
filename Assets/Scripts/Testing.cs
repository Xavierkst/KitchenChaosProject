using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    private void Start() {
        MyClass myClass = new MyClass();
        TestInterface(myClass);

        MyClass2 myClass2 = new MyClass2();
        TestInterface(myClass2);
    }

    private void TestInterface(IMyInterface myInterface) {
        myInterface.TestFunction();
    }
}

public interface IMyInterface : IMyInterfaceTwo {
    public int MyInt { get; set; }
    void TestFunction();
}

public interface IMyInterfaceTwo {
    public void SecondTestFunction();
}

public class MyClass : IMyInterface {
    public void SecondTestFunction() {
        Debug.Log("Second function");
    }

    private int myInt;

    public int MyInt {
        get {
            return myInt;
        }
        set {
            myInt = value;
        }
    }

    public void TestFunction() {
        Debug.Log("MyClass.TestingFunction()");
    }
}

public class MyClass2 : IMyInterface {
    public void SecondTestFunction() {
        // ... 
    }

    private int myInt;
    public int MyInt { get; set; }
    public void TestFunction() {
        Debug.Log("MySecondClass!!");
    }
}

