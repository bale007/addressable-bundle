# addressable-bundle

## Enligsh

Aaddressable can be very slow if you are to load buncg of assets through Addressable.LoadAssetsAsync. This behaviour is by design since addressable will load and unload everysingle assets from the api call instead of load and unload all at once.

To Address this issue, i made a simple asset wrapper to contains a list of asset needed and then load this very wrapper from addressable only, this largely reduce the project load time (at least for ours).

Feel free to use it and extend the genric bundle wrapper to load the asset you need to load. To build bundle just call BaseBundle.BuildAll() before building or running the game.

Odin inspector is used in our project, just delete those odin attribute if your project dont have odin integrated.

## 中文

在做项目优化阶段的时候发现 Addressable.LoadAssetsAsync 在加载资源数量庞大的情况下会非常的慢。

抽空查看了Addressable源码发现是设计如此，因为Addressable在加载每个资源的时候都会历经“加载资源-回调-卸载资源”这样一个流程。于是为了解决这个问题，我就写了一个简单的wrapper来打包游戏加载所需的资源，并且可以通过扩展泛型来wrap自己需要的资源类型。

打包或者运行游戏前调用一次 BaseBundle.BuildAll() 即可根据目录更新资源

项目使用了odin inspector，因为人比较懒所以没有删掉相关attribute，可以自行删除。





