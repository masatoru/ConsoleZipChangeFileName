using System.IO.Compression;

Console.WriteLine("ZIP ファイルのフォルダ名を変更する");

// ZIP ファイルを取得
var zipFileName = "XamarinMacAppPipelineTest.app.zip";
var appFileName = "XamarinMacAppPipelineTest.app";
var newZipFileName = System.IO.Path.GetFileNameWithoutExtension(zipFileName)+"-new.zip";
var newAppFileName = System.IO.Path.GetFileNameWithoutExtension(appFileName)+"-123456789.app";
var zipPath = $@".\data\{zipFileName}";
var newZipPath = $@".\data\{newZipFileName}";

// ZIP ファイルをオープンする
using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Read);

// 新しいZIPを作成する
using var memoryStream = new MemoryStream();
using var newArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

foreach (var entry in archive.Entries)
{
    // ディレクトリ名を新しいアプリ名に変更する
    var newEntryName = entry.FullName.Replace(appFileName, newAppFileName);
    
    var newEntry = newArchive.CreateEntry(newEntryName);
    // ファイルの場合はコピーする
    if (!entry.FullName.EndsWith("/"))
    {
        // 元のファイルをコピーする
        using var archiveEntry = entry.Open();
        using var newArchiveEntry = newEntry.Open();
        archiveEntry.CopyTo(newArchiveEntry);
    }
}

using var fileStream = new FileStream(newZipPath, FileMode.Create);
memoryStream.Seek(0, SeekOrigin.Begin);
memoryStream.CopyTo(fileStream);

Console.WriteLine($"{zipPath}");
Console.WriteLine($"==> {newZipPath}");
