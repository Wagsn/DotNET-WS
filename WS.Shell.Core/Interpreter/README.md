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

// Token流解析

print ( '123456' );

```regex
ID = $@"\w[\w\d]*"
STR = String =$@"'.*?'"
BOOL = Boolean = $@"true|false"
NUM = Number =$@"(\d+)(\.\d+)?"
LIT = Literal = $@"({NUM})|({STR})|({BOOL})"
// 双子操作符
BOP = Binary Operator = $@"[(==|>=|<=|!=)\+\-\*/]"
// 赋值
VDOP = $@":="
// 等号
EQOP = $@"=="
// 分号
SEM = $@";"
// 冒号
COL = colon = $@":"
// 问号
QUE = Question mark = $@"\?"
// 点号
DOT = $@"\."
LP = $@"\("
RP = $@"\)"
// 逗号
COM = comma = $@","
// 双子表达式
BinExpr =
	| <Expr> <BOP> <Expr>
// 赋值表达式
VarDecExpr =
	| <ID> <VDOP> <Expr>
ConExpr = ConditionalExpression =
	| <Expr> <QUE> <Expr> <COL> <Expr>
// 相等表达式
EqExpr =
	| <Expr> <EQOP> <Expr>
// 成员表达式
MemExpr =
	| (<ID>|<MemExpr>*) (<DOT> <ID>)
// 调用表达式
CallExpr =
	| $@"(({ID})|({MemExpr}))({WS})({DOT})({LP})(({Expr})(({COM})({Expr}))*)?({RP})"
	| (<ID>|<MemExpr>) <DOT> <LP> (<Expr> (<COM> <Expr>)*)? <RP>   // person.name.eat('apple', 'meat')
// 赋值表达式
AssExpr = AssignmentExpression =
	| <ID> <VDOP> (<Expr>|<ID>|<LIT>)
// 有返回值（Boolean，String，Number，Object等）
Expr =
	| LIT
	| ID
	| BinExpr
	| VarDecExpr
	| EqExpr
	| CallExpr
	| \( <Expr> \)
// 表达式语句
ExprStmt =
	| <Expr> <SEM>
Stmt =
    | (<Expr>)? <SEM>
Stmts =	Body =
	| \{ (<Stmt>)* \}
```

```
NextExpr(tokens){
	var currPos = 0
	currToken = tokens[currPos]
	exprStack
	switch currToken.type
		case ";"
			var last = exprStack.pop()
			return last
		case LIT  // LIT == LitExpr  // 字面量
			if(!exprStack.empty()){
				// 是否可以组合
				if(CanCombine(exprStack.top(), LIT)){
					var exp = Combine(exprStack.pop(), LIT)
					exprStack.push(exp)
				}else{
					// 不可以组合表示在必须有分号的情况下出现了问题
				}
			}else{
				exprStack.push(LIT)
			}
		case ID // 标识符
			if(!exprStack.empty()){
				// 是否可以组合
				if(CanCombine(exprStack.top(), ID)){
					var exp = Combine(exprStack.pop(), ID)
					exprStack.push(exp)
				}else{
					// 不可以组合表示在必须有分号的情况下出现了问题
				}
			}else{
				exprStack.push(ID)
			}
		case "."  // 成员操作符
			var last = exprStack.Pop()
			var currExpr = new MemExpr
			currExpr.object = last
			currExpr.property = null
			exprStack.push(currExpr)
		case "+-*/"  // binary expression
			var last = exprStack.Pop()
			var currExpr = new BinExpr
			currExpr.left = last
			var next = NextExpr(tokens.tail(currPos+1))
			currExpr.right = next.expr  // tail 索引及之后
			currPos += next.count;
			exprStack.push(currExpr)
		case "("  // 可能是双子表达式的开头，或者调用表达式，或者函数定义的形式参数
			if(!exprStack.empty()){
				if(exprStack.top()==MemExpr||ID||LIT){
					var currExpr =new CallExpr
					currExpr.callee = exprStack.pop()
					currExpr.arguments =null
				}
				else if(exprStack.top()==BinExpr){
					var last = exprStack.top()
					if(last.right==null){
						continue;
					}
					else{
					
					}
				}
			}
			switch exprStack.top()
				case 
}


```