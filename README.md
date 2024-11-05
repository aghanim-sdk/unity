# Aghanim Unity SDK

[Installation](#installation) • [Configuration](#configuration) • [Samples](#samples) • [Documentation](https://docs.aghanim.com/integrations/purchase-from-game-client-v2/) • [Support](#support)

---
## Overview

Aghanim Unity SDK is designed to integrate your Unity WebGL project with the [Aghanim platform](https://aghanim.com/), providing essential tools for handling token, saving the player’s progress state, requesting the list of products, making internal and external purchases, and passing a one-time token to the game backend to initialize the player’s session on the provider’s server.

## Installation

To install Aghanim Unity SDK:

1. Download [aghanim-sdk.unitypackage](https://github.com/aghanim-sdk/unity/blob/main/aghanim-sdk.unitypackage).
2. In the Unity Editor, go to **Assets > Import Package > Custom Package** and select the downloaded `aghanim-sdk.unitypackage`.
3. For the latest instructions on importing custom packages, please refer to the [Unity documentation](https://docs.unity3d.com/Manual/CustomPackages.html).

## Configuration

1. Set the **Aghanim** WebGL template in your Unity project’s settings.
2. Add the prefab located at `Prefabs/AghanimSDK` to your starting scene.
3. Populate the required URLs and event handlers on the `AghanimSDK` prefab instance.

## Samples

The package includes example scene that demonstrate SDK usage. You can find these samples in the [Samples folder](https://github.com/aghanim-sdk/unity/tree/main/Assets/Aghanim/Samples) of the repository.

## Documentation

For detailed integration steps and API references, refer to the [Aghanim documentation](https://docs.aghanim.com/integrations/purchase-from-game-client-v2/).

## Support

For support and further assistance, please contact us at [integration@aghanim.com](mailto:integration@aghanim.com).
