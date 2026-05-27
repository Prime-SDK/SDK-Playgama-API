1. Delete existing `WebGLTemplates`, `PlayGamaBridge` folder in `Assets` folder if it exists, and delete file `PlaygamaBridge.jslib` in `Assets/Plugins` folder if it exists.

2. Setup the PlayGama SDK for Unity using official documentation steps:
https://wiki.playgama.com/playgama/sdk/engines/unity/setup

3. Add this package in `Unity Package Manager` using the following URL:

```
https://github.com/Prime-SDK/SDK-Playgama-API.git
```

4. If you install from PrimeSDK Toolkit, use automatic installation so Playgama Bridge is installed before this API package.

5. Select `PlaygamaConfiguration` configuration in `PrimeSDK Toolkit` build configuration dropdown.
