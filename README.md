
---

### THTMS_GUI / `README.md`

# THTMS_GUI

**东方幕华祭 永夜篇（THMHJ III）资源处理一体化 GUI 工具**

- 图像加/解密（Graphics）
- Data 资源（脚本 / 图像 / 通用加密块）
- BGM 音频（XNA 封装的 `*.xna`）
- DDS 与常见图片格式互转（依赖 `texconv`）

底层算法与 keys 都是按 THMHJ III 实际资源格式逆向写的。

## 运行环境与依赖

- 平台：**Windows**
- 语言：C# / WinForms
- 目标框架：`.NET Framework 4.8.1`
- 依赖：
  - `WindowsAPICodePack` 一系列 DLL（通过 NuGet 自动恢复）
  - 外部 `texconv.exe`（用于 DDS ↔ 图片 转换）
- 工程文件：`THTMS_GUI.csproj`  
  用 **Visual Studio 2022** 打开解决方案后还原 NuGet，再编译即可。

## 界面与模块概览

主窗口大致分为四个分区：

1. `Graphics（图像加/解密）`
2. `Data（图像/Data 通用）`
3. `BGM（音频）`
4. `DDS（互转）`

底部有统一的进度条、任务状态显示和一个“强行停止”按钮
### 1. Graphics（图像加/解密）

用于处理游戏里加密的图像 / 纹理文件，例如 `.xna` 纹理、`.rpy` 回放等。

主要控件：

- 源路径：单文件或文件夹
- 输出路径：默认跟源路径；勾选“递归模式”时，需要设置根输出目录
- 选项：
  - `递归模式`：递归处理子目录，保持相对目录结构
  - `处理后删除`：处理成功后删除原文件（慎用）
- 操作：
  - `解包`
  - `打包`
  - `密钥说明`：弹出说明窗口

流程说明（解包）：

1. 选择源路径（文件或目录）。
2. 选择合适的密钥编号。
3. 选择输出路径（递归模式下通常是一个新目录）。
4. 点击 `解包`：

内部逻辑：

- 按字节签名自动识别文件类型（PNG/JPG/BMP/TGA/GIF/HDR/JXR/DDS 等）：
  - 优先用签名推断扩展名
  - 识别失败时，回退到基于原始扩展名的临时后缀
- 对 `.xna` / `.rpy` 使用密钥解密后输出实际图片或二进制
- 处理完成后会在 `%AppData%\THMHJIIIResourceHacker` 下生成一份 `glist_yyyyMMddHHmmss.txt`：
  - 里面是本次解包得到的图片文件名列表
  - 供后续 DDS 模块做“排除/过滤”用

打包流程则反向执行，加密后写回 `.xna` / `.rpy`。

### 2. Data（图像/Data 通用）

用于处理游戏里其他加密块（例如脚本、配置或内嵌图像等），使用的是 **TripleDES** 加/解密。

关键点：

- 支持解包的扩展名：`.dat` / `.xna`
- 支持打包的扩展名：`.txt` / `.dds` / `.png` 
- 加密算法：
  - `TripleDESCryptoServiceProvider`
  - `ECB + PKCS7`
  - `Key` 从内置 `DataKeys` 数组中选择，不对外暴露明文

控件：

- 源路径（多选）
- 输出路径
- `处理后删除`
- `解包到目标`
- `打包`
- `生成最新 list`

解包行为：

- 对 `.dat` / `.xna`：
  - 使用不同 key 解密
  - `.dat` 优先尝试当作“加密图片”：
    - 按签名自动识别 PNG/JPG/BMP/TGA/GIF/HDR/DDS
    - 猜到是图片时自动改扩展名
  - 非图片则直接按二进制/文本输出
- 解包时会收集所有图片文件名，写到 `%AppData%\THMHJIIIResourceHacker\list_yyyyMMddHHmmss.txt`

打包行为：

- 根据当前选择的 key，将文本 / 图片重新加密为 `.dat` 或 `.xna`。
- 可用“生成最新 list”辅助构建/更新 list 文件。

### 3. BGM（音频）

处理存放在 `*.xna` 内的 BGM 音频。

功能：

- 解包：`*.xna`（加密 OGG） → `*.ogg`
- 打包：`*.ogg` → `*.xna`
- 音频本体使用简单 XOR 流方式加/解密：
  - 前 128 字节头部保留，之后的部分按固定 `AudioKey` 做异或

附加：循环信息文件

- 工具内置了一份 `LoopInfo` 表，按曲目编号记录：
  - `loopStart`（样本位置）
  - `loopLength`（循环长度）
- 解包时，如果能从文件名中解析出曲目编号，且 LoopInfo 里有对应记录：
  - 会额外生成一份 `xxx.loop.txt`，写出：
    - 曲目编号
    - 循环起点 / 长度 / 结束位置（单位：样本）
    - 换算成秒的说明
- 方便你在其他工具或音乐播放器里验证循环点。

### 4. DDS（互转）

基于 DirectXTex 的 `texconv.exe` 做的封装，负责：

- DDS → 常见图片格式（PNG/JPEG/BMP/TGA 等）
- 图片 → DDS

关键点：

- **texconv 自动下载**：
  - 第一次点击 `↧` 按钮（`获取 texconv`）时：
    - 如果 `%AppData%\THMHJIIIResourceHacker\texconv\texconv.exe` 不存在：
      - 从预置的 GitHub 地址下载 `texconv.exe`
    - 若已存在，会询问是否覆盖
- GPU 选择：
  - 使用 `texconv -gpu` 输出自动解析 GPU 列表
  - 下拉框展示已清洗格式的 GPU 名称
  - 内部调用 texconv 时会附带选择的 GPU 参数
- 排除表：
  - 支持勾选：
    - `使用 list 作为排除`
    - `使用 glist 作为排除`
    - `使用默认排除表`
  - 可以避免对某些 UI / 占位纹理反复转换

操作流程示例（DDS → PNG）：

1. 点击源路径，选择一个 DDS 文件或目录（可选递归）。
2. 设置输出目录。
3. 选择输出图片格式（如 `PNG`）。
4. 确认已下载 `texconv`（`↧` 按钮）。
5. 点击 `DDS → 图片`。

反向转换（图片 → DDS）流程类似。

## 任务管理与强制停止

长时间批处理时，工具使用统一的任务系统：

- 开始任务：
  - 锁定所有主界面控件，防止误操作
  - 显示底部大号进度条
  - 显示“强行停止”按钮
- 任务进行中：
  - 采用 `Parallel.ForEach` 并行处理，默认会利用多核 CPU
  - 可以随时点击“强行停止”，触发 `CancellationTokenSource.Cancel()`：
    - 当前正在处理的文件允许正常收尾
    - 后续任务不再进入
- 任务结束：
  - 恢复控件可用

## 快速实战用法示例

一个典型的 THMHJ III 资源修改流程可以是：

1. **先备份原始游戏目录**。
2. 用 **Graphics** 把纹理 `.xna` 解密成 PNG / DDS，必要时启用递归模式。
3. 用 **DDS** 模块做格式转换、批量压缩或解压。
4. 用 **Data** 解包/回写脚本或配置 `.dat` / `.xna`。
5. 用 **BGM** 解包 BGM `.xna` 为 `.ogg`，在外部音频编辑后，再打包回去。
6. 全程让工具自动生成 `list_*.txt` / `glist_*.txt`，方便管理要处理或排除的资源。

## 与东方幕华祭 永夜篇（THMHJ III）的关系

- 所有加/解密 key、Data TripleDES 配置、音频 XOR、LoopInfo 表等，都是根据 **东方幕华祭 永夜篇（THMHJ III）** 实际资源逆向而来。
- 目录与缓存路径统一使用：`%AppData%\THMHJIIIResourceHacker`。
- 对其他游戏不保证适用。

## 免责声明

- 本仓库不包含任何游戏二进制或数据文件。
- 所有功能仅用于研究、调试和个人自用 MOD 开发，请勿用作盗版传播或商业用途。
- 使用前请务必备份原始游戏文件。