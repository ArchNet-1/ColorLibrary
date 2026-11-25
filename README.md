<p align="center">
  <a href="https://archnet-plateform.fr"><img src="https://archnet-plateform.fr/ArchNetLogoWithBck.png" alt="ArchNet" width="240"></a>
</p>

# ArchNet · Color Library

Your game's palette in one asset: colors stored by enum or int key, so changing a color updates every place that uses it.

- Unity **6000.0** or newer · Needs `com.archnet.library.core` (the ArchNet installer adds them for you).
- Part of [ArchNet](https://archnet-plateform.fr), free and open-source Unity packages

## Install

*Window > Package Manager > + > Install package from git URL…*

```
https://github.com/ArchNet-1/ColorLibrary.git
```

## Use

*Assets > Create > ArchNet > Color Library*, choose the key type, **Add** a color per key.

```csharp
using ArchNet.Library;

Color primary = _palette.GetColor(Theme.Primary);
```

Add **ArchNet Color Extension** to a UI element to color it from the library without code.

## License

See [LICENSE.md](LICENSE.md).
