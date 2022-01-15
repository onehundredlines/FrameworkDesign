<h1>学习QFramework的记录</h1>

> QFramework框架的作者：凉鞋
<br>
> QFramework框架的地址：https://qframework.cn

<b>
QFramework系统设计架构
<br>
架构分为四个层级：
<br>
1、表现层：IController接口，负责接收输入和状态变化时的表现，一般情况下，MonoBehaviour均为表现层。
<br>
2、系统层：ISystem接口，帮助IController承担一部分逻辑，在多个表现层共享的逻辑，比如计时系统、商城系统、成就系统等。
<br>
3、模型层：IModel接口，负责数据的定义、数据的增删查改方法的提供。
<br>
4、工具层：IUtility接口，负责提供基础设施，比如存储方法、序列化方法、网络连接方法、蓝牙方法、SDK、框架继承等。
<br>
使用规则：
<br>
1、IController更改ISystem、IModel的状态必须用Command。
<br>
2、ISystem、IModel状态发生变更后通知IController必须用事件或BindableProperty。
<br>
3、IController可以获取ISystem、IModel对象来进行数据查询。
<br>
4、ICommand不能有状态。
<br>
5、上层可以直接获取下层，下层不能获取上层对象。
<br>
6、下层向上层通信用事件。
<br>
7、上层向下层通信用方法调用（只是做查询，状态变更用Command），IController的交互逻辑为特别情况，只能用Command。
</b>
