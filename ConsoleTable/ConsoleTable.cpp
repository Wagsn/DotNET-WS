// ConsoleTable.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

using namespace std;

////////////////////////////////////////  
// 功能：打印表格线  
// 参数：  
// location 表格线的位置， 0 表头线，1 表中线，2 表尾线  
// colCount 表格的列数  
// colWidth 表格的列宽  
/////////////////////////////////////////  
void PrintTableLine(int location, int colCount,int colWidth)
{
	//注意：这些是中文符号，所以要用3个字符装(包括/0)
	char lineHead[][3] = { "┌", "├","└" };  // 行首
	char lineMid1[][3] = { "─", "─","─" };  // 字中
	char lineMid2[][3] = { "┬", "┼","┴" };  // 字间
	char lineTail[][3] = { "┐", "┤","┘" };  // 行尾
	cout << lineHead[location]; //行首  
	for(int i=0;i<colCount; i++)
	{
		for(int j=0;j<colWidth;j++)
		{
			cout<<lineMid1[location];
		}
		if(i<colCount-1)cout<<lineMid2[location];
	}
	cout<<lineTail[location]<<endl;//行尾  
}

/////////////////////////////////////////////////////////  
// 功能：获取指定二维数组中的最大显示宽度  
// 参数：A 二维数组名，rowCount 行数，colCount 列数  
/////////////////////////////////////////////////////////  
int GetMaxWidth(int*A, int rowCount, int colCount)
{
	int width= 0;
	for(int i=0;i<rowCount;i++)
	{
		for(int j=0;j<colCount;j++)
		{
			int c=1;
			int temp=A[i*rowCount+j];
			while(temp)
			{
				temp/= 10;
				c++;
			}
			width=width<c?c:width;
		}
	}
	return width;
}

///////////////////////////////////////  
// 功能：将二维数组打印成表格样式  
// 参数：A 二维数组名，rowCount 行数，colCount 列数  
//////////////////////////////////////  
void PrintArray(int* A, int rowCount, int colCount)
{
	char tablines[] = { "┌┬┐├┼┤└┴┘─│" };
	int i, j, colWidth;

	colWidth = GetMaxWidth(A, rowCount, colCount);//获取所有数据中的最大宽度  
	//打印表头线  
	PrintTableLine(0, colCount, colWidth);

	//打印表格内容  
	for(i= 0;i< rowCount;i++)
	{
		if(i> 0)PrintTableLine(1, colCount, colWidth);//打印表中线  
		cout<< "│";//行首  
		for(j= 0;j< colCount;j++)
		{
			cout.width(colWidth);//内容按指定宽度输出  
			cout<< A[i*rowCount + j];
			if(j< colCount-1)cout<< "│";//表中竖线  
		}
		cout<< "│" << endl;//行尾  
	}

	PrintTableLine(2, colCount, colWidth);//打印表尾  
}

int main()
{
    std::cout << "Hello World!\n"; 
	//int A[][3] = { {10, 20, 30}, {40, 50, 60}, {70, 80, 90} };
	int A[][3] = { {1, 2, 3}, {4, 5, 6}, {7, 8, 91444444} };
	PrintArray(&A[0][0], 3, 3);
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门提示: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
