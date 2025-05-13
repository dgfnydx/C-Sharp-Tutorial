# Unity中的组件和游戏对象

## 介绍

Unity的核心理念是组件式架构，游戏对象（GameObject）是场景中的基本单位，而组件（Component）则定义了游戏对象的行为和属性。理解游戏对象和组件的关系及其使用方法，是掌握Unity开发的关键。本教程将详细介绍Unity中的游戏对象和组件系统，以及如何通过C#脚本与它们交互。

## 游戏对象（GameObject）

### 什么是游戏对象

游戏对象是Unity场景中的基本实体，可以表示角色、道具、环境元素等。每个游戏对象都有一些基本属性：

- 名称（Name）
- 标签（Tag）
- 图层（Layer）
- 变换组件（Transform）- 唯一一个所有游戏对象都必须拥有的组件

### 创建游戏对象

在Unity中创建游戏对象的方法：

1. 通过Unity编辑器：Hierarchy窗口 > 右键 > Create Empty（或其他预设对象）
2. 通过C#代码：

```csharp
// 创建空游戏对象
GameObject emptyObject = new GameObject("EmptyObject");

// 创建带有特定组件的游戏对象
GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
```

### 查找游戏对象

C#提供了多种方法来查找场景中的游戏对象：

```csharp
// 通过名称查找
GameObject player = GameObject.Find("Player");

// 通过标签查找
GameObject player = GameObject.FindWithTag("Player");
GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

// 通过类型查找组件
Camera mainCamera = Camera.main; // 查找标记为MainCamera的相机
Light[] lights = FindObjectsOfType<Light>(); // 查找所有Light组件
```

> **注意**：频繁使用Find方法会影响性能，建议在Start或Awake中查找并缓存引用。

### 游戏对象的激活与销毁

```csharp
// 激活/禁用游戏对象
gameObject.SetActive(true); // 激活
gameObject.SetActive(false); // 禁用

// 检查游戏对象是否激活
if (gameObject.activeSelf)
{
    Debug.Log("游戏对象已激活");
}

// 销毁游戏对象
Destroy(gameObject); // 立即销毁
Destroy(gameObject, 2.0f); // 2秒后销毁
```

## 组件（Component）

### 什么是组件

组件是附加到游戏对象上的功能模块，定义了游戏对象的行为、外观和其他特性。Unity内置了许多组件类型：

- Transform：定义位置、旋转和缩放
- Renderer：渲染网格、精灵等
- Collider：处理碰撞检测
- Rigidbody：添加物理行为
- AudioSource：播放声音
- 等等

### 添加和获取组件

通过C#脚本添加和获取组件：

```csharp
// 添加组件
Rigidbody rb = gameObject.AddComponent<Rigidbody>();

// 获取组件
Transform transform = gameObject.GetComponent<Transform>();
AudioSource audioSource = GetComponent<AudioSource>(); // 在MonoBehaviour中可以直接使用

// 获取特定类型的所有组件
Collider[] colliders = gameObject.GetComponents<Collider>();

// 获取子对象上的组件
Camera childCamera = gameObject.GetComponentInChildren<Camera>();

// 获取父对象上的组件
Canvas parentCanvas = gameObject.GetComponentInParent<Canvas>();
```

### 检查和移除组件

```csharp
// 检查组件是否存在
if (gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
{
    rb.mass = 2.0f;
}

// 检查组件是否存在（另一种方式）
if (gameObject.GetComponent<Rigidbody>() != null)
{
    Debug.Log("该对象有Rigidbody组件");
}

// 移除组件
Rigidbody rb = gameObject.GetComponent<Rigidbody>();
Destroy(rb);
```

## Transform组件

Transform是Unity中最基础的组件，每个游戏对象都必须有一个Transform组件，它定义了对象在3D空间中的位置、旋转和缩放。

### 位置、旋转和缩放

```csharp
// 访问和修改位置
Vector3 position = transform.position; // 世界坐标
Vector3 localPosition = transform.localPosition; // 相对于父对象的本地坐标

transform.position = new Vector3(1, 2, 3); // 设置世界坐标
transform.localPosition = Vector3.zero; // 设置本地坐标为原点

// 访问和修改旋转
Quaternion rotation = transform.rotation; // 世界旋转（四元数）
Vector3 eulerAngles = transform.eulerAngles; // 世界旋转（欧拉角）

transform.rotation = Quaternion.identity; // 重置旋转
transform.eulerAngles = new Vector3(0, 90, 0); // 设置Y轴旋转90度

// 访问和修改缩放
Vector3 scale = transform.localScale; // 只有本地缩放
transform.localScale = new Vector3(2, 2, 2); // 放大到2倍
```

### 移动和旋转方法

```csharp
// 移动对象
transform.Translate(Vector3.forward * Time.deltaTime * 5); // 沿局部前方移动
transform.Translate(Vector3.up * Time.deltaTime, Space.World); // 沿世界上方移动

// 旋转对象
transform.Rotate(Vector3.up, 45); // 绕Y轴旋转45度
transform.Rotate(new Vector3(0, 1, 0), 45, Space.World); // 在世界空间中旋转

// 朝向目标
Transform target = GameObject.Find("Target").transform;
transform.LookAt(target); // 朝向目标
```

### 父子关系

```csharp
// 设置父对象
transform.SetParent(parentTransform);
transform.SetParent(parentTransform, false); // false表示保持世界坐标不变

// 获取父对象和子对象
Transform parent = transform.parent;
int childCount = transform.childCount;
Transform firstChild = transform.GetChild(0);

// 遍历所有子对象
foreach (Transform child in transform)
{
    Debug.Log(child.name);
}

// 分离对象（设置为根对象）
transform.SetParent(null);
```

## 预制体（Prefab）

预制体是可重用的游戏对象模板，可以在多个场景中使用，并且可以快速创建多个实例。

### 使用预制体

```csharp
// 加载预制体
GameObject bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

// 实例化预制体
GameObject bullet = Instantiate(bulletPrefab);

// 在特定位置和旋转实例化
GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

// 设置父对象
GameObject bullet = Instantiate(bulletPrefab, transform); // 作为当前对象的子对象
```

## 实践示例：简单的对象管理器

下面是一个综合示例，展示了如何创建一个简单的对象管理器，用于生成和管理多个游戏对象：

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 预制体引用
    public GameObject enemyPrefab;
    public GameObject collectiblePrefab;
    
    // 对象池
    private List<GameObject> activeEnemies = new List<GameObject>();
    private List<GameObject> activeCollectibles = new List<GameObject>();
    
    // 生成设置
    public int maxEnemies = 5;
    public int maxCollectibles = 10;
    public float spawnRadius = 10f;
    
    void Start()
    {
        // 初始生成
        SpawnEnemies();
        SpawnCollectibles();
    }
    
    void Update()
    {
        // 检查并补充敌人
        if (activeEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
        
        // 清理无效引用
        CleanupLists();
    }
    
    // 生成所有敌人
    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }
    
    // 生成单个敌人
    void SpawnEnemy()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0; // 保持在地面上
        
        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        enemy.name = "Enemy_" + activeEnemies.Count;
        
        // 添加到列表中跟踪
        activeEnemies.Add(enemy);
    }
    
    // 生成所有收集品
    void SpawnCollectibles()
    {
        for (int i = 0; i < maxCollectibles; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = 0.5f; // 稍微抬高一点
            
            GameObject collectible = Instantiate(collectiblePrefab, randomPos, Quaternion.identity);
            collectible.name = "Collectible_" + i;
            
            // 添加到列表中跟踪
            activeCollectibles.Add(collectible);
        }
    }
    
    // 清理无效对象引用
    void CleanupLists()
    {
        activeEnemies.RemoveAll(item => item == null);
        activeCollectibles.RemoveAll(item => item == null);
    }
    
    // 销毁所有对象
    public void DestroyAllObjects()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        
        foreach (GameObject collectible in activeCollectibles)
        {
            if (collectible != null)
            {
                Destroy(collectible);
            }
        }
        
        activeEnemies.Clear();
        activeCollectibles.Clear();
    }
}
```

## 练习

1. **基础练习**：创建一个场景，包含多个不同类型的游戏对象（立方体、球体等），并编写脚本在运行时修改它们的位置、旋转和缩放。

2. **组件操作**：创建一个脚本，在运行时动态添加、获取和移除组件（如Rigidbody、Light等）。

3. **对象生成器**：创建一个对象生成器，定期在随机位置生成预制体，并在它们离开视野或达到一定时间后销毁它们。

4. **父子关系**：创建一个太阳系模型，使用父子关系让行星围绕太阳旋转，卫星围绕行星旋转。

## 总结

在本教程中，我们学习了：

- 游戏对象的基本概念和操作
- 组件系统及其使用方法
- Transform组件的详细用法
- 预制体的创建和实例化
- 如何通过C#脚本管理游戏对象和组件

理解游戏对象和组件是Unity开发的基础，掌握它们的使用方法将使你能够创建复杂的游戏场景和交互。在下一章中，我们将学习Unity的物理系统和碰撞检测，这将使你的游戏对象能够以更真实的方式相互作用。

## 下一步学习

继续学习[物理系统和碰撞检测](04_物理系统和碰撞检测.md)，了解如何为游戏对象添加物理行为和碰撞响应。