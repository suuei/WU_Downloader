WU Downloader
====

MBSA(Microsoft Baseline Security Analyzer)が出力するXMLファイルからWindowsのアップデータを取得し、一括でインストールするバッチファイルを作成します。

## 注意
このソフトウエアのバグにより、上手くアップデータが適用されず、Windowsに脆弱性が残ってしまう可能性もあります。  
そのような場合でも、こちらでは保証をすることはできません。  
自己責任でご利用ください。  
(At your own risk.)

## 使い方
アップデータを適用したい環境にMBSAをインストールした後、コマンドプロントで以下のコマンドを実行し、出力されたXMLファイルをこのソフトウエアに投げ込んでください。  
`"C:\Program Files\Microsoft Baseline Security Analyzer 2\mbsacli.exe" /xmlout /unicode /nd /nvc > result.xml`  
(mbsacli.exeまでのパスは環境に応じて適宜変更してください。)

## 動作環境
.NET Framework 3.5以降 (Windows7以降には標準でインストールされています。)

