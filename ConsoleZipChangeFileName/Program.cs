using System.IO.Compression;

Console.WriteLine("ZIP ファイルのファイル名を変更する");

// ZIP ファイルを取得
var zipPath = ".\\data\\app.zip";

// ZIP ファイルをオープンする
using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Update);

// 新しいファイル名でEntryを作成
var newEntry = archive.CreateEntry("app-123456789.txt");

// 元のファイルをコピーする
var fileEntry = archive.GetEntry("app.txt");
using (var a = fileEntry.Open())
using (var b = newEntry.Open())
{
    a.CopyTo(b);
}

// 元のファイルを削除する
fileEntry.Delete();