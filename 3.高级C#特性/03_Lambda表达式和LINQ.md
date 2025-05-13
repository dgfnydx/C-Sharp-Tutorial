# C# Lambda表达式和LINQ

## 介绍

Lambda表达式和LINQ（Language Integrated Query，语言集成查询）是C#中两项强大的特性，它们通常一起使用，能够大大提高代码的简洁性和可读性。Lambda表达式提供了一种简洁的方式来创建匿名函数，而LINQ则允许我们使用统一的语法对各种数据源进行查询和操作。本教程将详细介绍C#中Lambda表达式和LINQ的概念、语法和使用方法。

## Lambda表达式

### 什么是Lambda表达式？

Lambda表达式是一种匿名函数，它允许我们以简洁的方式编写内联函数。Lambda表达式在LINQ查询、事件处理和异步编程等场景中特别有用。

### Lambda表达式的语法

Lambda表达式的基本语法如下：

```csharp
(parameters) => expression_or_statement_block
```

其中：
- `parameters`：输入参数（可以是零个或多个）
- `=>`：Lambda运算符（读作"goes to"）
- `expression_or_statement_block`：表达式或语句块

### Lambda表达式示例

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 无参数的Lambda表达式
        Action sayHello = () => Console.WriteLine("Hello, World!");
        sayHello();  // 输出：Hello, World!
        
        // 带一个参数的Lambda表达式
        Action<string> greet = name => Console.WriteLine($"Hello, {name}!");
        greet("Alice");  // 输出：Hello, Alice!
        
        // 带多个参数的Lambda表达式
        Func<int, int, int> add = (x, y) => x + y;
        Console.WriteLine(add(5, 3));  // 输出：8
        
        // 带语句块的Lambda表达式
        Func<int, int> factorial = n =>
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        };
        Console.WriteLine(factorial(5));  // 输出：120
    }
}
```

### Lambda表达式的类型

Lambda表达式可以转换为以下类型：

1. **委托类型**：如`Action`、`Func`等
2. **表达式树**：`Expression<TDelegate>`

```csharp
// 转换为委托类型
Func<int, bool> isEven = x => x % 2 == 0;

// 转换为表达式树
Expression<Func<int, bool>> isEvenExpr = x => x % 2 == 0;
```

### Lambda表达式的捕获变量

Lambda表达式可以访问定义它的方法中的局部变量和参数（称为捕获变量）：

```csharp
public void ProcessData(int threshold)
{
    int factor = 10;
    
    // Lambda表达式捕获了threshold和factor变量
    Func<int, bool> filter = x => x * factor > threshold;
    
    var numbers = new[] { 1, 5, 10, 15, 20 };
    var result = numbers.Where(filter);
    
    foreach (var num in result)
    {
        Console.WriteLine(num);
    }
}
```

## LINQ（语言集成查询）

LINQ（Language Integrated Query，语言集成查询）是C#中一项强大的特性，它允许我们使用统一的语法对各种数据源（如集合、XML、数据库等）进行查询和操作。LINQ将查询功能直接集成到C#语言中，使开发人员能够使用类似SQL的语法编写强类型查询，从而提高代码的可读性和可维护性。

## LINQ的基本概念

LINQ提供了一种统一的方式来查询和操作数据，无论数据源是什么。LINQ查询操作通常包括以下三个部分：

1. **获取数据源**：指定要查询的数据源，如数组、列表、XML文档等。
2. **创建查询**：定义要从数据源中检索的信息。
3. **执行查询**：遍历查询结果并获取数据。

LINQ查询可以使用两种语法：查询语法和方法语法。

### LINQ提供商

LINQ有多种实现，用于不同类型的数据源：

- **LINQ to Objects**：用于查询内存中的集合，如数组、列表等。
- **LINQ to XML**：用于查询和操作XML文档。
- **LINQ to SQL**：用于查询SQL Server数据库。
- **LINQ to Entities**：用于通过Entity Framework查询数据库。

## LINQ查询语法

LINQ查询语法类似于SQL，但是顺序相反。LINQ查询以`from`子句开始，以`select`或`group`子句结束。

### 基本语法

```csharp
// 查询语法
var query = from item in collection
           where condition
           orderby property
           select result;
```

### 示例

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 创建一个整数数组
        int[] numbers = { 5, 10, 8, 3, 6, 12 };
        
        // 使用LINQ查询语法查找所有大于5的数字并排序
        var query = from num in numbers
                   where num > 5
                   orderby num
                   select num;
        
        // 执行查询并输出结果
        Console.WriteLine("大于5的数字（升序）：");
        foreach (var num in query)
        {
            Console.WriteLine(num);
        }
        // 输出：6, 8, 10, 12
    }
}
```

## LINQ方法语法

LINQ方法语法使用扩展方法来执行查询操作。这种语法更加灵活，并且可以执行查询语法无法表达的某些操作。

### 基本语法

```csharp
// 方法语法
var query = collection.Where(item => condition)
                     .OrderBy(item => property)
                     .Select(item => result);
```

### 示例

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 创建一个整数数组
        int[] numbers = { 5, 10, 8, 3, 6, 12 };
        
        // 使用LINQ方法语法查找所有大于5的数字并排序
        var query = numbers.Where(num => num > 5)
                          .OrderBy(num => num);
        
        // 执行查询并输出结果
        Console.WriteLine("大于5的数字（升序）：");
        foreach (var num in query)
        {
            Console.WriteLine(num);
        }
        // 输出：6, 8, 10, 12
    }
}
```

## 常用LINQ操作符

LINQ提供了丰富的操作符，用于执行各种查询和转换操作。以下是一些常用的LINQ操作符：

### 筛选操作符

- **Where**：根据条件筛选元素。
- **OfType**：根据指定类型筛选元素。

```csharp
// 查找所有偶数
var evenNumbers = numbers.Where(n => n % 2 == 0);
```

### 排序操作符

- **OrderBy**：按升序排序元素。
- **OrderByDescending**：按降序排序元素。
- **ThenBy**：提供二级排序（升序）。
- **ThenByDescending**：提供二级排序（降序）。
- **Reverse**：反转集合中元素的顺序。

```csharp
// 按姓名升序排序，然后按年龄降序排序
var sortedPeople = people.OrderBy(p => p.Name)
                        .ThenByDescending(p => p.Age);
```

### 投影操作符

- **Select**：将每个元素转换为新形式。
- **SelectMany**：将每个元素转换为序列，然后将这些序列合并为一个序列。

```csharp
// 提取所有人的姓名
var names = people.Select(p => p.Name);

// 提取所有人的所有电话号码（每人可能有多个）
var phoneNumbers = people.SelectMany(p => p.PhoneNumbers);
```

### 集合操作符

- **Distinct**：移除重复元素。
- **Union**：合并两个集合，移除重复项。
- **Intersect**：返回两个集合的交集。
- **Except**：返回存在于第一个集合但不存在于第二个集合的元素。

```csharp
// 获取不重复的数字
var distinctNumbers = numbers.Distinct();

// 获取两个集合的并集
var unionResult = collection1.Union(collection2);
```

### 聚合操作符

- **Count**：计算元素数量。
- **Sum**：计算元素总和。
- **Average**：计算元素平均值。
- **Min**：获取最小值。
- **Max**：获取最大值。
- **Aggregate**：对集合执行自定义聚合操作。

```csharp
// 计算所有数字的和
int sum = numbers.Sum();

// 计算所有人的平均年龄
double averageAge = people.Average(p => p.Age);
```

### 元素操作符

- **First**：返回集合的第一个元素，如果集合为空则抛出异常。
- **FirstOrDefault**：返回集合的第一个元素，如果集合为空则返回默认值。
- **Last**：返回集合的最后一个元素，如果集合为空则抛出异常。
- **LastOrDefault**：返回集合的最后一个元素，如果集合为空则返回默认值。
- **Single**：返回集合中唯一的元素，如果集合为空或包含多个元素则抛出异常。
- **SingleOrDefault**：返回集合中唯一的元素，如果集合为空则返回默认值，如果包含多个元素则抛出异常。
- **ElementAt**：返回集合中指定索引处的元素。

```csharp
// 获取第一个大于10的数字
int first = numbers.First(n => n > 10);

// 尝试获取年龄大于100的人，如果没有则返回null
var oldPerson = people.FirstOrDefault(p => p.Age > 100);
```

### 分组操作符

- **GroupBy**：根据指定的键对元素进行分组。

```csharp
// 按年龄段对人进行分组
var ageGroups = people.GroupBy(p => p.Age / 10);

foreach (var group in ageGroups)
{
    Console.WriteLine($"{group.Key * 10}岁到{group.Key * 10 + 9}岁的人：");
    foreach (var person in group)
    {
        Console.WriteLine($"  {person.Name}, {person.Age}岁");
    }
}
```

### 连接操作符

- **Join**：根据键值对两个序列的元素进行关联。
- **GroupJoin**：根据键值对两个序列的元素进行关联，并将结果分组。

```csharp
// 连接学生和课程
var studentCourses = students.Join(
    courses,
    student => student.CourseId,
    course => course.Id,
    (student, course) => new { StudentName = student.Name, CourseName = course.Name }
);
```

### 分区操作符

- **Take**：从集合的开头获取指定数量的元素。
- **Skip**：跳过集合开头的指定数量的元素，然后返回剩余的元素。
- **TakeWhile**：从集合的开头获取元素，直到条件不满足。
- **SkipWhile**：跳过集合开头的元素，直到条件不满足，然后返回剩余的元素。

```csharp
// 获取前3个数字
var firstThree = numbers.Take(3);

// 跳过前3个数字，获取剩余的数字
var remainingNumbers = numbers.Skip(3);
```

## 延迟执行与即时执行

LINQ查询具有延迟执行（Deferred Execution）的特性，这意味着查询在定义时不会立即执行，而是在遍历结果或调用特定方法时才会执行。

### 延迟执行

```csharp
// 定义查询（此时不执行）
var query = numbers.Where(n => n > 5);

// 添加一个新数字到集合
numbers.Add(15);

// 遍历结果（此时执行查询）
foreach (var num in query)
{
    Console.WriteLine(num);  // 包括新添加的15
}
```

### 即时执行

某些LINQ方法会强制立即执行查询，如`ToList()`、`ToArray()`、`Count()`等。

```csharp
// 立即执行查询并将结果存储在列表中
var list = numbers.Where(n => n > 5).ToList();

// 添加一个新数字到集合
numbers.Add(15);

// 遍历结果（不包括新添加的15）
foreach (var num in list)
{
    Console.WriteLine(num);
}
```

## LINQ to XML

LINQ to XML提供了一种简单而强大的方式来处理XML文档。

### 创建XML文档

```csharp
using System;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // 创建XML文档
        XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement("Books",
                new XElement("Book",
                    new XAttribute("Id", "1"),
                    new XElement("Title", "C#编程指南"),
                    new XElement("Author", "张三"),
                    new XElement("Price", 59.9)
                ),
                new XElement("Book",
                    new XAttribute("Id", "2"),
                    new XElement("Title", "ASP.NET Core实战"),
                    new XElement("Author", "李四"),
                    new XElement("Price", 69.9)
                )
            )
        );
        
        // 保存XML文档
        doc.Save("books.xml");
        
        // 输出XML文档
        Console.WriteLine(doc.ToString());
    }
}
```

### 查询XML文档

```csharp
using System;
using System.Xml.Linq;
using System.Linq;

class Program
{
    static void Main()
    {
        // 加载XML文档
        XDocument doc = XDocument.Load("books.xml");
        
        // 查询所有书籍的标题
        var titles = from book in doc.Descendants("Book")
                    select book.Element("Title").Value;
        
        Console.WriteLine("所有书籍的标题：");
        foreach (var title in titles)
        {
            Console.WriteLine(title);
        }
        
        // 查询价格大于60的书籍
        var expensiveBooks = from book in doc.Descendants("Book")
                           let price = decimal.Parse(book.Element("Price").Value)
                           where price > 60
                           select new
                           {
                               Title = book.Element("Title").Value,
                               Price = price
                           };
        
        Console.WriteLine("\n价格大于60的书籍：");
        foreach (var book in expensiveBooks)
        {
            Console.WriteLine($"{book.Title} - {book.Price}元");
        }
    }
}
```

## LINQ to SQL

LINQ to SQL允许我们使用LINQ语法查询SQL Server数据库。

### 定义数据模型

```csharp
using System.Data.Linq.Mapping;

[Table(Name = "Customers")]
public class Customer
{
    [Column(IsPrimaryKey = true)]
    public int Id { get; set; }
    
    [Column]
    public string Name { get; set; }
    
    [Column]
    public string Email { get; set; }
}
```

### 查询数据库

```csharp
using System;
using System.Data.Linq;
using System.Linq;

class Program
{
    static void Main()
    {
        // 创建数据上下文
        string connectionString = "Data Source=(local);Initial Catalog=MyDatabase;Integrated Security=True";
        DataContext db = new DataContext(connectionString);
        
        // 获取客户表
        var customers = db.GetTable<Customer>();
        
        // 查询所有客户
        var query = from c in customers
                   where c.Name.StartsWith("张")
                   orderby c.Id
                   select c;
        
        Console.WriteLine("姓张的客户：");
        foreach (var customer in query)
        {
            Console.WriteLine($"{customer.Id}: {customer.Name}, {customer.Email}");
        }
    }
}
```

## 实际应用示例

### 复杂对象查询

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }
    }
    
    class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
    }
    
    static void Main()
    {
        // 创建课程列表
        List<Course> courses = new List<Course>
        {
            new Course { Id = 1, Name = "C#编程", Credits = 3 },
            new Course { Id = 2, Name = "数据库原理", Credits = 4 },
            new Course { Id = 3, Name = "Web开发", Credits = 3 },
            new Course { Id = 4, Name = "操作系统", Credits = 5 },
            new Course { Id = 5, Name = "计算机网络", Credits = 4 }
        };
        
        // 创建学生列表
        List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "张三",
                Age = 20,
                Courses = new List<Course> { courses[0], courses[1], courses[2] }
            },
            new Student
            {
                Id = 2,
                Name = "李四",
                Age = 22,
                Courses = new List<Course> { courses[0], courses[3], courses[4] }
            },
            new Student
            {
                Id = 3,
                Name = "王五",
                Age = 21,
                Courses = new List<Course> { courses[1], courses[2], courses[4] }
            }
        };
        
        // 查询：找出选修了"C#编程"课程的所有学生
        var csharpStudents = from student in students
                           where student.Courses.Any(c => c.Name == "C#编程")
                           select student;
        
        Console.WriteLine("选修了C#编程的学生：");
        foreach (var student in csharpStudents)
        {
            Console.WriteLine($"{student.Name}, {student.Age}岁");
        }
        
        // 查询：按年龄分组并计算每个年龄段的学生人数
        var ageGroups = from student in students
                       group student by student.Age into g
                       select new { Age = g.Key, Count = g.Count() };
        
        Console.WriteLine("\n各年龄段学生人数：");
        foreach (var group in ageGroups)
        {
            Console.WriteLine($"{group.Age}岁: {group.Count}人");
        }
        
        // 查询：找出每个学生选修的课程总学分
        var studentCredits = from student in students
                           select new
                           {
                               Name = student.Name,
                               TotalCredits = student.Courses.Sum(c => c.Credits)
                           };
        
        Console.WriteLine("\n学生选修的课程总学分：");
        foreach (var item in studentCredits)
        {
            Console.WriteLine($"{item.Name}: {item.TotalCredits}学分");
        }
        
        // 查询：找出选修课程最多的学生
        var mostCoursesStudent = students
            .OrderByDescending(s => s.Courses.Count)
            .First();
        
        Console.WriteLine($"\n选修课程最多的学生是{mostCoursesStudent.Name}，共选修了{mostCoursesStudent.Courses.Count}门课程。");
    }
}
```

## 性能考虑

使用LINQ时，需要注意以下性能相关的问题：

1. **避免多次执行相同的查询**：如果需要多次使用查询结果，应该使用`ToList()`或`ToArray()`将结果存储起来。
2. **注意延迟执行**：了解查询何时执行，避免在循环中重复执行查询。
3. **选择合适的操作符**：某些操作符比其他操作符更高效，例如，`Any()`通常比`Count() > 0`更高效。
4. **避免不必要的排序**：排序操作相对昂贵，只在必要时使用。

## 练习

1. 创建一个包含学生信息（姓名、年龄、成绩）的列表，并使用LINQ查询找出所有成绩大于80分的学生。

2. 使用LINQ查询对上述学生列表按成绩降序排序，并只选择前三名学生。

3. 创建两个整数列表，并使用LINQ查询找出它们的交集、并集和差集。

4. 使用LINQ to XML解析一个XML文件，并提取特定的信息。

5. 创建一个复杂的对象模型（例如，学生-课程-教师），并使用LINQ编写各种查询来提取和分析数据。

## 下一步学习

恭喜你完成了Lambda表达式和LINQ的学习！接下来，你可以继续学习[高级C#特性/04_异步编程.md](04_异步编程.md)，了解C#中的异步编程模型。