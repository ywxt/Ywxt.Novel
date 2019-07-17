#  一、无关紧要的话

之前的一个版本使用WPF写的，Bug很多，我也没改。这次，我用 .Net Core 重写了一个命令行版的小说下载器。

现在没了GUI，只有命令行的黑框框。同时，自定义匹配模板改成了Css选择器，而不是用 .Net 程序集的方式加载插件，虽然失去了灵活性，好在这样子上手简单，同时可以安装远程的模板。

**支持Windows，Linux 和 macOS**

# 二、使用方式

以下命令使用命令行或者终端输入。

## 下载
```bash
Ywxt.Novel download url -t|--template template [-o|--output output]
```
- url:地址
- template:模板ID，可以使用 `list` 命令查看。软件预安装了两个模板(分别是qu.la和xbiquge.cc)
- output:下载地址，默认为当前目录
## 安装模板
```bash
Ywxt.Novel install url [-l|--local] [-o|--orverride]
```
- url:地址
- -l:是否本地路径，默认为false
- -o:如果存在同ID模板，是否覆盖，默认为true
## 查看模板
```bash
Ywxt.Novel list
```
## 创建模板
```bash
Ywxt.Novel new
```

# 三、 下载地址

- 项目地址：https://github.com/ywxt/Ywxt.Novel
- 下载地址：https://github.com/ywxt/Ywxt.Novel/releases

# 四、示例

## 下载

- 下载https://www.qu.la/book/3137/ ，模板为ywxt.biquge，保存位置为当前路径，文件名为小说名

  ![下载1](https://i.loli.net/2019/04/14/5cb30b558494f.png)

- 下载https://www.qu.la/book/3137/，模板为ywxt.biquge，保存位置为当前目录的books.txt

  ![下载](https://i.loli.net/2019/04/14/5cb30b55ba272.png)

## 安装模板

安装位于https://blog-static.cnblogs.com/files/ywxt/ywxt.xbiquge.css的模板

![new](https://i.loli.net/2019/04/14/5cb30b5600fc8.png)

添加`-l` 参数指定安装本地模板

## 查看模板

![下载](https://i.loli.net/2019/04/14/5cb30b564a1f0.png)



## 创建自定义模板

![下载](https://i.loli.net/2019/04/14/5cb30b564ba6c.png)



关于创建过程中需要使用的Css选择器，使用Chrome浏览器，在开发者人员工具的Element选项卡选中元素，右键Copy->Copy Selector

![Element](https://i.loli.net/2019/04/14/5cb30b564b57c.png)

了解一下Css选择器，可以轻松创建模板。