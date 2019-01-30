# 解释器

本文件夹包含词法分析器语法分析器以及语义分析器和抽象语法树、单词

```txt

                   +-------+                      +--------+
-- source code --> | lexer | --> token stream --> | parser | --> assembly
                   +-------+                      +--------+

```

```txt

Symbol table:
----+-----+----+----+----+-----+-----+-----+------+------+----
 .. |token|hash|name|type|class|value|btype|bclass|bvalue| ..
----+-----+----+----+----+-----+-----+-----+------+------+----
    |<---       one single identifier                --->|

```

program.body
```txt

type:描述该语句的类型 --变量声明语句
kind：变量声明的关键字 -- var
declaration: 声明的内容数组，里面的每一项也是一个对象
    type: 描述该语句的类型 
    id: 描述变量名称的对象
        type：定义
        name: 是变量的名字
    init: 初始化变量值得对象
        type: 类型
        value: 值 "is tree" 不带引号
        row: "\"is tree"\" 带引号

```

```

VariableName : DataType [=  Initial Value]

```
```txt
name: String := 'wagsn';
main: (args: String[]) => Int {
	print(name);
	print(3+2);
};
```
词法分析
read file;  读取文件
gen lexemes; 生成词素数组
gen tokens;  生成记号流
预处理：
gen global; 生成全局对象（全局上下文，global）及其变量表（VarTable）
添加全局函数：print，import，load，save，compile等，添加全局变量：fs，io等
gen name;  生成变量 name
global.VarTable.Add(name);  添加到变量表
gen main;  生成主函数
global.VarTable.Add(main);
开始执行：
gen funStatck;
read main;
main.VarTable.Add(args);
funStack.Push(main);  // 将主函数压入函数栈
print(name);
	load print;
	load name;
	call print,main,[name];

交互执行

```txt
> print('123154'); print(true);
123154
< None
```
gen Script Context -> context
load source
gen tokens
gen mainCall
funStatck.Push(mainCall)  // 函数栈的栈顶为当前执行代码所在函数
	gen str: String Literal
	gen printCall: Function Call {}  // 从函数模板生成一个函数实例（每次函数调用都会生成一个函数调用实例，系统函数没有上下文）
	funStack.Push(printCall)  // 将当前函数调用压入函数栈 函数调用
		call printCall, context, [str] -> result //  函数调用 注入调用上下文和实际参数
	funStack.Pop()
	gen boo: Bool
	gen printCall2
	funStack.Push(printCall2)
		gen params
		varTable.Add(params);
		native call 
	funStack.Pop()
funStack.Pop() -> mainResult


print: (params: Object[]) => Void {
	[native code]
}


