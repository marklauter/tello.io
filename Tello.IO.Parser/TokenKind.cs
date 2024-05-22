﻿namespace Tello.IO.Parser;

public enum TokenId : uint
{
    WhiteSpace,
    Identifier,
    IntegerLiteral,
    DirectionLeft,
    DirectionRight,
    DirectionFront,
    DirectionBack,
    Reboot,
    Command,
    Takeoff,
    Land,
    Stop,
    StreamOn,
    StreamOff,
    Emergency,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    MoveForward,
    MoveBack,
    RotateClockwise,
    RotateCounterClockwise,
    MotorOn,
    MotorOff,
    ThrowFly,
    Flip,
    MoveToPosition,
    Curve,
    WriteSpeed,
    WriteRemoteControl,
    WriteWifi,
    WriteAccessPoint,
    WriteWifiChannel,
    WritePort,
    WriteFramesPerSecond,
    WriteBitrate,
    WriteResolution,
    ReadSpeed,
    ReadBattery,
    ReadTime,
    ReadWifiVersion,
    ReadSdkVersion,
    ReadSerialNumber,
    ReadHardware,
    ReadAccessPoint,
    ReadSsid,
}
