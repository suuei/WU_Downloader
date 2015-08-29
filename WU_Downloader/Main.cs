using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace WU_Downloader
{
    public partial class Main : Form
    {
        public Main() {
            InitializeComponent();
        }

        DataTable dt_UpdateList;

        private void Main_DragEnter(object sender, DragEventArgs e) {
            if(e.Data.GetDataPresent(DataFormats.FileDrop)){
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Main_DragDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            text_xmlPath.Text = files[0];
        }

        private void button_Start1_Click(object sender, EventArgs e) {

            XmlDocument XmlDocument = new XmlDocument();

            try {
                XmlDocument.Load(text_xmlPath.Text);
            } catch (FileNotFoundException) {
                MessageBox.Show("XMLファイルが読み込めませんでした。", "エラー");
                return;
            } catch (Exception) {
                MessageBox.Show("XMLファイルを確認してください。(Code:1)", "エラー");
                return;
            }

            //データ格納用のDataTableを用意
            dt_UpdateList = new DataTable();
            dt_UpdateList.TableName = "WUList";
            dt_UpdateList.Columns.Add("KBID", typeof(Int32));
            //dt_UpdateList.Columns.Add("IsInstalled", typeof(bool));
            dt_UpdateList.Columns.Add("Title", typeof(String));
            dt_UpdateList.Columns.Add("DownloadURL", typeof(String));

            XmlNodeList NodeCheckList = XmlDocument.SelectNodes("/XMLOut/Check");

            for (int i = 0; i < NodeCheckList.Count; i++) {
                XmlNodeList NodeUDList = NodeCheckList[i].SelectNodes("Detail/UpdateData");
                for (int j = 0; j < NodeUDList.Count; j++ ) {
                    int KBID = Convert.ToInt32(NodeUDList[j].SelectSingleNode("@KBID").Value);
                    bool IsInstalled = Convert.ToBoolean(NodeUDList[j].SelectSingleNode("@IsInstalled").Value);
                    string Title = NodeUDList[j].SelectSingleNode("Title").InnerText;
                    string DownloadURL = NodeUDList[j].SelectSingleNode("References/DownloadURL").InnerText;

                    //インストール済みの更新プログラムは無視する
                    if (IsInstalled) continue;

                    DataRow dr = dt_UpdateList.NewRow();
                    dr["KBID"] = KBID;
                    //dr["IsInstalled"] = IsInstalled;
                    dr["Title"] = Title;
                    dr["DownloadURL"] = DownloadURL;
                    dt_UpdateList.Rows.Add(dr);
                }
            }

            if (dt_UpdateList.Rows.Count == 0) {
                MessageBox.Show("インストールされていない更新プログラムは見つかりませんでした。");
                return;
            }

            //ダウンロードスタート
            button_Start1.Enabled = false;
            button_Cancel.Enabled = true;
            button_XMLPath.Enabled = false;
            text_xmlPath.ReadOnly = true;
            bgDownload.RunWorkerAsync();

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            text_Log.AppendText("ダウンロードを中止しています。\r\n");
            bgDownload.CancelAsync();
        }

        private void bgDownload_DoWork(object sender, DoWorkEventArgs e) {

            //ダウンロード先ディレクトリの作成
            if (!Directory.Exists(".\\DL"))
                Directory.CreateDirectory(".\\DL");

            //インストール用のバッチファイルを作成
            StreamWriter sw = new StreamWriter(".\\InstallUpdate.bat", false, Encoding.Default);
            sw.WriteLine("@echo off");

            sw.WriteLine("echo このツールは自己責任でご利用ください。");
            sw.WriteLine("echo.");
            sw.WriteLine("echo アップデートのインストールを開始します。");
            sw.WriteLine("echo 中断する場合は、このままウインドウを閉じてください。");
            sw.WriteLine("echo 続行する場合は、それ以外のキーを押してください。");
            sw.WriteLine("pause");

            System.Net.WebClient wc = new System.Net.WebClient();
            DataView dv = new DataView(dt_UpdateList);
            dv.Sort = "KBID";

            int totalNum = dt_UpdateList.Rows.Count;

            for (int i = 0; i < totalNum; i++) {

                if (bgDownload.CancellationPending) {
                    sw.WriteLine("echo インストールが終了しました。");
                    sw.WriteLine("echo 再起動を行います。");
                    sw.WriteLine("pause");
                    sw.WriteLine("shutdown -r -t 0");
                    sw.Close();
                    e.Cancel = true;
                    return;
                }

                string url = dv[i]["DownloadURL"].ToString();
                string[] urlArr = url.Split('/');
                string DLPath = ".\\DL\\";
                string filename = urlArr[urlArr.Length - 1];

                //進捗を報告
                bgDownload.ReportProgress(i / totalNum, dv[i].Row);

                wc.DownloadFile(url, DLPath + filename);

                //ファイルの拡張子で処理を変更
                string ext = Path.GetExtension(filename);

                sw.WriteLine("echo インストール中: KB" + dv[i]["KBID"]);
                sw.WriteLine("echo " + dv[i]["Title"]);

                switch (ext)
                {
                    case ".cab":
                        sw.WriteLine("md " + DLPath + "KB" + dv[i]["KBID"]);
                        sw.WriteLine("start /wait expand -f:* " + DLPath + filename + " " + DLPath + "KB" + dv[i]["KBID"]);
                        sw.WriteLine("start /wait pkgmgr /ip /m:" + DLPath + "KB" + dv[i]["KBID"] + " /quiet /norestart");
                        break;

                    case ".exe":
                        sw.WriteLine("start /wait " + DLPath + filename + " /quiet /norestart");
                        break;

                    case ".msu":
                        sw.WriteLine("start /wait wusa " + DLPath + filename + " /quiet /norestart");
                        break;
                }

            }

            wc.Dispose();

            sw.WriteLine("echo インストールが終了しました。");
            sw.WriteLine("echo 再起動を行います。");
            sw.WriteLine("pause");
            sw.WriteLine("shutdown -r -t 0");

            sw.Close();
        }

        private void bgDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataRow dr = (DataRow)e.UserState;
            text_Log.AppendText("ダウンロード中: KB" + dr["KBID"].ToString() + "\r\n" + dr["Title"].ToString() + "\r\n");
        }

        private void bgDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) {
                MessageBox.Show("ダウンロードを中断しました。");
            } else {
                MessageBox.Show("ダウンロードが完了しました。");
            }

            button_Start1.Enabled = true;
            button_Cancel.Enabled = false;
            button_XMLPath.Enabled = true;
            text_xmlPath.ReadOnly = false;
        }

        
    }
}
