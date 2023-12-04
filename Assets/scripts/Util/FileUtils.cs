using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileUtils
{

    //清除文本内容
    public static void CleanFileContent(string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
        fs.Close();
    }

    /// <summary>
    /// 复制文件夹内容到新的文件夹
    /// </summary>
    /// <param name="originPath"></param>
    /// <param name="targetPath"></param>
    public static void CopyDirectory(string originPath, string targetPath)
    {
        try
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            DirectoryInfo dir = new DirectoryInfo(originPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)
                {     //判断是否文件夹
                    if (!Directory.Exists(targetPath + "/" + i.Name))
                    {
                        Directory.CreateDirectory(targetPath + "/" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                    }
                    CopyDirectory(i.FullName, targetPath + "/" + i.Name);    //递归调用复制子文件夹
                }
                else
                {
                    File.Copy(i.FullName, targetPath + "/" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static List<string> FindFilesInDirectoryByFilter(string _dirPath, System.Func<string, bool> _filter = null)
    {
        List<string> files = new List<string>();
        if (!Directory.Exists(_dirPath))
        {
            return files;
        }

        DirectoryInfo dirInfo = new DirectoryInfo(_dirPath);
        foreach (FileInfo file in dirInfo.GetFiles())
        {
            if (_filter == null || _filter(file.FullName))
            {
                files.Add(file.FullName);
            }
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            files.AddRange(FindFilesInDirectoryByFilter(dir.FullName, _filter));
        }

        return files;
    }

    public static void DeleteFilesInDirectoryByFilter(string _dirPath, System.Func<string, bool> _filter = null)
    {
        List<string> files = FindFilesInDirectoryByFilter(_dirPath, _filter);
        foreach (string fileName in files)
        {
            File.Delete(fileName);
        }
    }

    public static void ClearDirectory(string _dirPath)
    {
        if (Directory.Exists(_dirPath))
        {
            Directory.Delete(_dirPath, true);
        }
        Directory.CreateDirectory(_dirPath);
    }

    public static void MoveFile(string oldFilePath, string newFilePath, bool overwrite = true)
    {
        if (!File.Exists(oldFilePath) || oldFilePath == newFilePath)
            return;
        string s = Path.GetDirectoryName(newFilePath);
        if (!Directory.Exists(s))
        {
            Directory.CreateDirectory(s);
        }
        File.Copy(oldFilePath, newFilePath, overwrite);
        File.Delete(oldFilePath);
    }

    public static void CopyFile(string oldFilePath, string newFilePath, bool overwrite = true)
    {
        if (!File.Exists(oldFilePath) || oldFilePath == newFilePath)
            return;
        string s = Path.GetDirectoryName(newFilePath);
        if (!Directory.Exists(s))
        {
            Directory.CreateDirectory(s);
        }
        File.Copy(oldFilePath, newFilePath, overwrite);
    }
}

