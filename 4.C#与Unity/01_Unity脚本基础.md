# Unity脚本基础

## 介绍

Unity是一个强大的跨平台游戏引擎，而C#是Unity中主要的脚本编程语言。在Unity中使用C#编写脚本可以控制游戏对象的行为、响应用户输入、实现游戏逻辑等。本教程将介绍Unity脚本的基础知识，帮助你开始在Unity中使用C#进行游戏开发。

## Unity脚本概述

在Unity中，脚本是附加到游戏对象上的组件，用于定义游戏对象的行为。Unity脚本通常继承自`MonoBehaviour`类，这是Unity提供的基类，包含了许多用于游戏开发的功能。

### 创建第一个Unity脚本

在Unity中创建脚本的步骤：

1. 在Project窗口中右键点击，选择Create > C# Script
2. 为脚本命名（例如"PlayerController"）
3. 双击脚本文件，在代码编辑器中打开它

一个基本的Unity脚本结构如下：

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 在第一帧更新之前调用
    void Start()
    {
        Debug.Log("游戏开始！");
    }

    // 每帧调用一次
    void Update()
    {
        // 游戏逻辑代码
    }
}
```

### 将脚本附加到游戏对象

创建脚本后，需要将其附加到游戏对象上才能生效：

1. 在Hierarchy窗口中选择一个游戏对象
2. 在Inspector窗口中点击"Add Component"按钮
3. 搜索并选择你创建的脚本（例如"PlayerController"）

或者，你可以直接将脚本从Project窗口拖放到Hierarchy窗口中的游戏对象上。

## MonoBehaviour常用方法

MonoBehaviour类提供了许多生命周期方法，它们在游戏运行的不同阶段被调用：

```csharp
public class LifecycleDemo : MonoBehaviour
{
    // 仅在脚本实例被加载时调用一次
    void Awake()
    {
        Debug.Log("Awake: 组件被初始化");
    }

    // 在启用脚本实例后调用一次
    void Start()
    {
        Debug.Log("Start: 游戏开始");
    }

    // 每帧调用一次，用于常规更新
    void Update()
    {
        Debug.Log("Update: 每帧更新");
    }

    // 每帧调用一次，在所有Update函数调用后调用
    void LateUpdate()
    {
        Debug.Log("LateUpdate: 在Update之后");
    }

    // 以固定的时间间隔调用，用于物理计算
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate: 固定时间间隔");
    }

    // 当脚本变为不可用或被销毁时调用
    void OnDisable()
    {
        Debug.Log("OnDisable: 组件被禁用");
    }

    // 当对象被销毁时调用
    void OnDestroy()
    {
        Debug.Log("OnDestroy: 对象被销毁");
    }
}
```

### 常用生命周期方法的使用场景

- **Awake**: 初始化变量或组件状态，不依赖于其他脚本的执行
- **Start**: 在所有Awake调用完成后执行，可以使用Awake中初始化的变量
- **Update**: 实现大多数游戏功能，如移动、旋转、用户输入处理等
- **LateUpdate**: 跟随其他对象移动的摄像机等，确保在所有对象移动后更新
- **FixedUpdate**: 物理相关的代码，如Rigidbody操作

## 访问游戏对象和组件

在Unity脚本中，可以通过多种方式访问游戏对象和组件：

### 访问当前游戏对象

```csharp
public class GameObjectDemo : MonoBehaviour
{
    void Start()
    {
        // 访问当前游戏对象
        GameObject thisGameObject = gameObject;
        Debug.Log("当前游戏对象名称: " + thisGameObject.name);
        
        // 获取当前游戏对象的位置
        Vector3 position = transform.position;
        Debug.Log("位置: " + position);
        
        // 修改游戏对象的位置
        transform.position = new Vector3(0, 2, 0);
    }
}
```

### 访问组件

```csharp
public class ComponentDemo : MonoBehaviour
{
    // 在Inspector中可见的公共变量
    public Rigidbody rb;
    public Light mainLight;
    
    void Start()
    {
        // 获取当前游戏对象上的组件
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            Debug.Log("找到碰撞器组件");
        }
        
        // 获取子游戏对象上的组件
        Renderer childRenderer = GetComponentInChildren<Renderer>();
        
        // 获取父游戏对象上的组件
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        
        // 添加组件
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        
        // 使用已分配的组件
        if (rb != null)
        {
            rb.AddForce(Vector3.up * 10);
        }
    }
}
```

### 查找游戏对象

```csharp
public class FindDemo : MonoBehaviour
{
    void Start()
    {
        // 通过名称查找游戏对象
        GameObject player = GameObject.Find("Player");
        
        // 通过标签查找游戏对象
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        
        // 查找特定类型的组件
        Light[] allLights = FindObjectsOfType<Light>();
        Debug.Log("场景中的灯光数量: " + allLights.Length);
    }
}
```

## 使用变量和Inspector

Unity允许在Inspector中显示和编辑脚本变量，这使得调整游戏参数变得非常方便：

```csharp
public class PlayerStats : MonoBehaviour
{
    // 公共变量在Inspector中可见
    public int health = 100;
    public float moveSpeed = 5f;
    public string playerName = "玩家1";
    
    // [SerializeField]特性使私有变量在Inspector中可见
    [SerializeField]
    private int maxHealth = 100;
    
    // [Header]特性添加标题
    [Header("武器设置")]
    public int damage = 10;
    public float attackRange = 2f;
    
    // [Range]特性限制值的范围
    [Range(0, 100)]
    public int accuracy = 75;
    
    // [Tooltip]特性添加鼠标悬停提示
    [Tooltip("武器冷却时间（秒）")]
    public float cooldown = 1.5f;
    
    // 私有变量在Inspector中不可见
    private int experience = 0;
    
    void Start()
    {
        Debug.Log($"玩家 {playerName} 已准备就绪，生命值: {health}/{maxHealth}");
    }
}
```

## 处理用户输入

Unity提供了多种方式来处理用户输入：

### 使用Input类

```csharp
public class InputDemo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 50f;
    
    void Update()
    {
        // 获取键盘输入
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D或左右箭头
        float verticalInput = Input.GetAxis("Vertical");     // W/S或上下箭头
        
        // 移动游戏对象
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        
        // 检测按键按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("空格键被按下");
            Jump();
        }
        
        // 检测鼠标按钮
        if (Input.GetMouseButtonDown(0)) // 左键
        {
            Debug.Log("鼠标左键被按下");
            Shoot();
        }
        
        // 获取鼠标位置
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log("鼠标位置: " + mousePosition);
    }
    
    void Jump()
    {
        // 跳跃逻辑
    }
    
    void Shoot()
    {
        // 射击逻辑
    }
}
```

### 使用新的Input System（需要安装Input System包）

```csharp
// 需要添加命名空间
using UnityEngine.InputSystem;

public class NewInputDemo : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    
    // 移动输入回调
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    // 跳跃输入回调
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("跳跃");
            // 跳跃逻辑
        }
    }
    
    void Update()
    {
        // 使用输入值移动
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
```

## 使用预制体（Prefabs）

预制体是可重用的游戏对象模板，在脚本中可以实例化预制体：

```csharp
public class PrefabDemo : MonoBehaviour
{
    // 在Inspector中分配预制体
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }
    
    void FireBullet()
    {
        // 实例化预制体
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // 可以访问实例化对象的组件
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.AddForce(firePoint.forward * 20, ForceMode.Impulse);
            }
            
            // 销毁子弹（5秒后）
            Destroy(bullet, 5f);
        }
    }
}
```

## 协程（Coroutines）

协程是Unity中一种特殊的函数，可以暂停执行并在之后继续：

```csharp
public class CoroutineDemo : MonoBehaviour
{
    public Light flashlight;
    
    void Start()
    {
        // 启动协程
        StartCoroutine(FlashLight());
    }
    
    // 协程函数必须返回IEnumerator
    IEnumerator FlashLight()
    {
        Debug.Log("开始闪烁");
        
        for (int i = 0; i < 5; i++)
        {
            // 打开灯光
            flashlight.enabled = true;
            Debug.Log("灯光开启");
            
            // 等待1秒
            yield return new WaitForSeconds(1f);
            
            // 关闭灯光
            flashlight.enabled = false;
            Debug.Log("灯光关闭");
            
            // 等待0.5秒
            yield return new WaitForSeconds(0.5f);
        }
        
        Debug.Log("闪烁结束");
    }
    
    // 不同类型的yield指令
    IEnumerator CoroutineExamples()
    {
        // 等待下一帧
        yield return null;
        
        // 等待固定时间
        yield return new WaitForSeconds(2f);
        
        // 等待固定更新
        yield return new WaitForFixedUpdate();
        
        // 等待直到条件满足
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        
        // 等待直到条件不满足
        yield return new WaitWhile(() => flashlight.enabled);
        
        // 等待另一个协程完成
        yield return StartCoroutine(FlashLight());
    }
    
    void StopAllCoroutinesExample()
    {
        // 停止所有协程
        StopAllCoroutines();
    }
    
    void StopSpecificCoroutine()
    {
        // 停止特定协程
        Coroutine flashRoutine = StartCoroutine(FlashLight());
        StopCoroutine(flashRoutine);
    }
}
```

## 调试技巧

Unity提供了多种调试工具和技术：

```csharp
public class DebuggingDemo : MonoBehaviour
{
    public int playerHealth = 100;
    
    void Start()
    {
        // 基本日志
        Debug.Log("普通日志信息");
        
        // 警告日志
        Debug.LogWarning("警告信息");
        
        // 错误日志
        Debug.LogError("错误信息");
        
        // 带条件的日志
        Debug.Assert(playerHealth > 0, "玩家生命值必须大于0");
        
        // 在场景视图中绘制线条（仅在编辑器中可见）
        Debug.DrawLine(transform.position, Vector3.zero, Color.red, 5f);
    }
    
    void Update()
    {
        // 在游戏视图中绘制射线（仅在Scene视图中可见）
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
    }
    
    void OnDrawGizmos()
    {
        // 绘制Gizmos（在Scene视图中始终可见）
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
    
    void OnDrawGizmosSelected()
    {
        // 仅在对象被选中时绘制Gizmos
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
```

## 练习

1. 创建一个简单的角色控制器，使用键盘控制角色移动和跳跃。

2. 实现一个计时器系统，在游戏开始后倒计时，并在时间结束时触发事件。

3. 创建一个简单的射击系统，按下鼠标左键时从角色位置发射子弹。

4. 实现一个简单的物品收集系统，当角色接触物品时，物品消失并增加分数。

## 总结

本教程介绍了Unity脚本的基础知识，包括脚本的创建和附加、MonoBehaviour生命周期方法、访问游戏对象和组件、处理用户输入、使用预制体、协程以及调试技巧。掌握这些基础知识后，你就可以开始在Unity中创建自己的游戏了。

在接下来的教程中，我们将深入探讨Unity中的MonoBehaviour生命周期、组件系统、物理系统和碰撞检测等更多高级主题。