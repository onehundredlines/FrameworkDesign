using UnityEngine;
namespace QFramework.Example
{
    public class CanDoEverything
    {
        public void DoSomething1() { Debug.Log("DoSomething1"); }
        public void DoSomething2() { Debug.Log("DoSomething2"); }
        public void DoSomething3() { Debug.Log("DoSomething3"); }
    }
    public interface IHaveEverything
    {
        CanDoEverything canDoEverything { get; }
    }
    public interface ICanDoSomething1 : IHaveEverything
    {

    }
    public static class ICanDoSomething1Extension
    {
        public static void DoSomething1(this ICanDoSomething1 self) { self.canDoEverything.DoSomething1(); }
    }
    public interface ICanDoSomething2 : IHaveEverything
    {

    }
    public static class ICanDoSomething2Extension
    {
        public static void DoSomething2(this ICanDoSomething2 self) { self.canDoEverything.DoSomething2(); }
    }
    public interface ICanDoSomething3 : IHaveEverything
    {

    }
    public static class ICanDoSomething3Extension
    {
        public static void DoSomething3(this ICanDoSomething3 self) { self.canDoEverything.DoSomething3(); }
    }
    /// <summary>
    /// 静态扩展
    /// </summary>
    public class InterfaceRule : MonoBehaviour
    {
        public class OnlyDo1 : ICanDoSomething1
        {
            CanDoEverything IHaveEverything.canDoEverything { get; } = new CanDoEverything();
        }

        public class OnlyDo2And3 : ICanDoSomething2, ICanDoSomething3
        {
            CanDoEverything IHaveEverything.canDoEverything { get; } = new CanDoEverything();
        }
        private void Start()
        {
            OnlyDo1 onlyDo1 = new OnlyDo1();
            onlyDo1.DoSomething1();

            //OnlyDo1类中显示实现了canDoEverything，限制了三面的三种方调用。
            // onlyDo1.canDoEverything.DoSomething1();
            // onlyDo1.canDoEverything.DoSomething2();
            // onlyDo1.canDoEverything.DoSomething3();

            //除非向上转型，强制类型转换为基类（这里是接口），才能调用限制的其他方法。
            (onlyDo1 as IHaveEverything).canDoEverything.DoSomething1();
            (onlyDo1 as IHaveEverything).canDoEverything.DoSomething2();
            (onlyDo1 as IHaveEverything).canDoEverything.DoSomething3();

            //或者这样
            IHaveEverything doEverythingA = new OnlyDo1();
            doEverythingA.canDoEverything.DoSomething1();
            doEverythingA.canDoEverything.DoSomething2();
            doEverythingA.canDoEverything.DoSomething3();


            OnlyDo2And3 onlyDo2And3 = new OnlyDo2And3();
            onlyDo2And3.DoSomething2();
            onlyDo2And3.DoSomething3();
            (onlyDo2And3 as IHaveEverything).canDoEverything.DoSomething1();
            (onlyDo2And3 as IHaveEverything).canDoEverything.DoSomething2();
            (onlyDo2And3 as IHaveEverything).canDoEverything.DoSomething3();
            IHaveEverything doEverythingB = new OnlyDo2And3();
            doEverythingB.canDoEverything.DoSomething1();
            doEverythingB.canDoEverything.DoSomething2();
            doEverythingB.canDoEverything.DoSomething3();
        }
    }
}