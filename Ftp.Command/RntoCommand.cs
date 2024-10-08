﻿using System.Net;
using Ftp.Command.Abstract;
using Ftp.Core.Connection;
using Serilog;

namespace Ftp.Command;

public class RntoCommand(ILogger logger) : FtpCommandBase(logger)
{
    public override string CommandName => "RNTO";

    public override void Execute(FtpConnectionBase user, string newPath)
    {
        string oldPath = (string)user.LastCommandData;
        newPath = Path.Combine(user.CurrentDirectory, newPath);
        if (user.Filesystem.FileExists(oldPath))
        {
            user.Filesystem.MoveFile(oldPath, newPath);
        }
        else
        {
            user.Filesystem.MoveDirectory(oldPath, newPath);
        }
        user.SendResponse(FtpStatusCode.FileActionOK, "Item moved successfully.");
        LogInformation(FtpStatusCode.FileActionOK, "Item moved successfully.");
    }
}
