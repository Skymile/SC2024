
var t1 = new Test1() { Property = 1 };
var t2 = new Test2() { Property = 1 };

MethodTest1(t1);
MethodTest2(ref t2);

Console.WriteLine(t1.Property + " " + t2.Property);
// a) 1 1   b) 1 4   c) 4 1   d) 4 4

void MethodTest1(Test1 t) =>
    t.Property = 4;
void MethodTest2(ref Test2 t) =>
    t.Property = 4;

public class  Test1 { public int Property { get; set; } }
public struct Test2 { public int Property { get; set; } }
