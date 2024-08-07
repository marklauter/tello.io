# update - June 2024
If anyone *REALLY* wants me to continue this project, comment in the [Issues](https://github.com/marklauter/tello.io/issues) section. 
The DJI Tello has been discontinued and I'm busy with less boring stuff, so unless this project gets some interest in the Issues section, I'm abandoning it.

## Build Status
[![.NET Tests](https://github.com/marklauter/tello.io/actions/workflows/dotnet.tests.yml/badge.svg)](https://github.com/marklauter/tello.io/actions/workflows/dotnet.tests.yml)
[![Nuget](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/)
##
![tello.io logo](https://raw.githubusercontent.com/marklauter/tello.io/main/images/drone.png)

# tello.io
Flight Controller for DJI/Ryze Tello Drone for dotnet. 
Based on [Tello 3.0 SDK](https://github.com/marklauter/tello.io/blob/main/Tello_SDK_3.0_User_Guide_en.pdf).

This is a rewrite of my Tello API for the 2.0 protocol: [github.com/marklauter/TelloAPI-SDK-2.0](https://github.com/marklauter/TelloAPI-SDK-2.0). 

## References
- https://www.dji.com/robomaster-tt/downloads?site=brandsite&from=insite_search
- [Tello_SDK_3.0_User_Guide_en.pdf](https://github.com/marklauter/tello.io/blob/main/Tello_SDK_3.0_User_Guide_en.pdf)


## Developer Notes
### 11 MAY 2024
- created project
- I aim to provide a set of Nuget packages
- original project was overly complicated because I was using it as a test for some C# techniques
- I aim to simplify this version
- will be interesting to see if I've learned anything in the last 6 years.

### 13 MAY 2024
- My Tello battery turned into a lithium balloon after 5 years, so my first pass of API will be based on a simulator.
- Created ITelloClient and ITelloClientHandler. These work like HttpClient and HttpClientHandler. The simulator is a ITelloClientHandler implementation.

### 15 MAY 2024
- Created Tello.IO.Parser and test projects.
- Setup the Lexer for the Tello SDK 3.0 commands.
