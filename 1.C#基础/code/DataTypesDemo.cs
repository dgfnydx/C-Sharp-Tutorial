// DataTypesDemo.cs - C#数据类型演示程序
using System;

namespace DataTypesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== C#数据类型演示程序 =====\n");
            
            // 1. 整数类型演示
            Console.WriteLine("【整数类型演示】");
            byte byteValue = 255;                 // 8位无符号整数
            sbyte sbyteValue = -128;              // 8位有符号整数
            short shortValue = 32767;             // 16位有符号整数
            ushort ushortValue = 65535;           // 16位无符号整数
            int intValue = 2147483647;            // 32位有符号整数
            uint uintValue = 4294967295;          // 32位无符号整数
            long longValue = 9223372036854775807; // 64位有符号整数
            ulong ulongValue = 18446744073709551615; // 64位无符号整数
            
            Console.WriteLine($"byte值: {byteValue} (范围: 0 到 255)");
            Console.WriteLine($"sbyte值: {sbyteValue} (范围: -128 到 127)");
            Console.WriteLine($"short值: {shortValue} (范围: -32,768 到 32,767)");
            Console.WriteLine($"ushort值: {ushortValue} (范围: 0 到 65,535)");
            Console.WriteLine($"int值: {intValue} (范围: -2,147,483,648 到 2,147,483,647)");
            Console.WriteLine($"uint值: {uintValue} (范围: 0 到 4,294,967,295)");
            Console.WriteLine($"long值: {longValue} (范围: -9,223,372,036,854,775,808 到 9,223,372,036,854,775,807)");
            Console.WriteLine($"ulong值: {ulongValue} (范围: 0 到 18,446,744,073,709,551,615)");
            Console.WriteLine();
            
            // 2. 浮点类型演示
            Console.WriteLine("【浮点类型演示】");
            float floatValue = 3.14159265359f;    // 单精度浮点数，注意f后缀
            double doubleValue = 3.14159265359;   // 双精度浮点数
            decimal decimalValue = 3.14159265359m; // 高精度十进制浮点数，注意m后缀
            
            Console.WriteLine($"float值: {floatValue} (精度约为7位数字)");
            Console.WriteLine($"double值: {doubleValue} (精度约为15-16位数字)");
            Console.WriteLine($"decimal值: {decimalValue} (精度约为28-29位有效数字)");
            Console.WriteLine();
            
            // 3. 字符和字符串演示
            Console.WriteLine("【字符和字符串演示】");
            char charValue = 'A';                 // 单个字符
            string stringValue = "你好，C#世界！";  // 字符串
            
            Console.WriteLine($"char值: {charValue} (Unicode字符)");
            Console.WriteLine($"string值: {stringValue} (Unicode字符串)");
            Console.WriteLine($"string长度: {stringValue.Length} 个字符");
            Console.WriteLine();
            
            // 4. 布尔类型演示
            Console.WriteLine("【布尔类型演示】");
            bool trueValue = true;
            bool falseValue = false;
            
            Console.WriteLine($"true值: {trueValue}");
            Console.WriteLine($"false值: {falseValue}");
            Console.WriteLine($"true AND false: {trueValue && falseValue}");
            Console.WriteLine($"true OR false: {trueValue || falseValue}");
            Console.WriteLine($"NOT true: {!trueValue}");
            Console.WriteLine();
            
            // 5. 日期时间演示
            Console.WriteLine("【日期时间演示】");
            DateTime now = DateTime.Now;
            DateTime today = DateTime.Today;
            DateTime specificDate = new DateTime(2023, 1, 1, 12, 0, 0);
            
            Console.WriteLine($"当前日期和时间: {now}");
            Console.WriteLine($"今天日期: {today:yyyy年MM月dd日}");
            Console.WriteLine($"特定日期: {specificDate:yyyy年MM月dd日 HH:mm:ss}");
            Console.WriteLine($"现在距离特定日期的天数: {(now - specificDate).Days}");
            Console.WriteLine();
            
            // 6. 类型转换演示
            Console.WriteLine("【类型转换演示】");
            
            // 6.1 隐式转换（从较小的类型到较大的类型）
            Console.WriteLine("隐式转换示例：");
            int smallerInt = 100;
            long biggerLong = smallerInt;  // 隐式从int转换为long
            float floatFromInt = smallerInt; // 隐式从int转换为float
            double doubleFromFloat = floatValue; // 隐式从float转换为double
            
            Console.WriteLine($"int值 {smallerInt} 隐式转换为long: {biggerLong}");
            Console.WriteLine($"int值 {smallerInt} 隐式转换为float: {floatFromInt}");
            Console.WriteLine($"float值 {floatValue} 隐式转换为double: {doubleFromFloat}");
            Console.WriteLine();
            
            // 6.2 显式转换（强制类型转换）
            Console.WriteLine("显式转换示例：");
            double piValue = 3.14159;
            int intPi = (int)piValue;  // 显式从double转换为int（小数部分被截断）
            long largeLong = 9876543210;
            int truncatedInt = (int)largeLong; // 显式从long转换为int（可能导致数据丢失）
            
            Console.WriteLine($"double值 {piValue} 显式转换为int: {intPi} (小数部分被截断)");
            Console.WriteLine($"long值 {largeLong} 显式转换为int: {truncatedInt} (可能导致数据丢失)");
            Console.WriteLine();
            
            // 6.3 使用Convert类转换
            Console.WriteLine("使用Convert类转换示例：");
            string numberString = "123";
            string doubleString = "3.14159";
            string boolString = "True";
            
            int convertedInt = Convert.ToInt32(numberString);
            double convertedDouble = Convert.ToDouble(doubleString);
            bool convertedBool = Convert.ToBoolean(boolString);
            
            Console.WriteLine($"字符串 \"{numberString}\" 转换为int: {convertedInt}");
            Console.WriteLine($"字符串 \"{doubleString}\" 转换为double: {convertedDouble}");
            Console.WriteLine($"字符串 \"{boolString}\" 转换为bool: {convertedBool}");
            Console.WriteLine();
            
            // 6.4 使用Parse方法转换
            Console.WriteLine("使用Parse方法转换示例：");
            int parsedInt = int.Parse(numberString);
            double parsedDouble = double.Parse(doubleString);
            bool parsedBool = bool.Parse(boolString);
            
            Console.WriteLine($"字符串 \"{numberString}\" 使用Parse转换为int: {parsedInt}");
            Console.WriteLine($"字符串 \"{doubleString}\" 使用Parse转换为double: {parsedDouble}");
            Console.WriteLine($"字符串 \"{boolString}\" 使用Parse转换为bool: {parsedBool}");
            Console.WriteLine();
            
            // 7. 常量演示
            Console.WriteLine("【常量演示】");
            const double PI = 3.14159;
            const string APP_NAME = "C#数据类型演示程序";
            
            Console.WriteLine($"常量PI值: {PI}");
            Console.WriteLine($"常量APP_NAME值: {APP_NAME}");
            Console.WriteLine();
            
            // 程序结束
            Console.WriteLine("===== 演示结束 =====\n");
            Console.ReadLine();
        }
    }
}